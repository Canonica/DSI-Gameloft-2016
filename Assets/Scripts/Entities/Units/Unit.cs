using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class Unit : Entity
{
    [Header("Tool")]
    [Tweakable]
    public string img = "Unit";
    [Tweakable]
    public string name = "Unit";

    [Header("Unit Option")]
    [Tweakable]
    public float attackSpeed = 1;
    [Tweakable]
    public float _movementSpeed = 5.0f;
    [Tweakable]
    public int groupSpawn = 1;
    [Tweakable]
    public int _damage = 2;
    // Utilisera les Spells
    public float _hatchTime = 1.0f;


    [Header("Bump Option")]
    public int smoother = 20;

    [Header("Other")]
    public GameObject _target;
    protected NavMeshAgent _navMeshAgent;
    public List<GameObject> _trigger;
    bool _isAttacking;
    public float _distanceMinLane = 4f;
    public Waypoint waypointDest;
    public bool laneEnd = false;
    public float lastCollision = 0;
    public int collideNum = 0;
    float lastAttack = 0;
    bool isBumped = false;
    private int _startingLife;

    [Header("FX")]
    [SerializeField]
    private GameObject FxHitBlood;

    [SerializeField]
    private GameObject FxDeathBlood;





    public override void Start()
    {
        _startingLife = _life;
        base.Start();

        _trigger = new List<GameObject>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {
        
        if (!laneEnd && Vector3.Distance(waypointDest.pos, transform.position) < _distanceMinLane)
        {
            if (waypointDest.Next(_playerId) == null)
            {
                laneEnd = true;
            }
            else
            {
                waypointDest = waypointDest.Next(_playerId);
                if (waypointDest.isTeleport)
                {
                    transform.position = waypointDest.pos;
                    waypointDest = waypointDest.Next(_playerId);
                }
            }
            takeDestination();
        }

        base.FixedUpdate();

    }

    void LateUpdate()
    {
        if (_life <= 0)
        {
            Instantiate(FxDeathBlood, this.gameObject.transform.position, Quaternion.Euler(new Vector3(-50, 0, 0)));
            Camera.main.DOKill(true);
            Camera.main.DOShakePosition(0.05f * _startingLife / 4, 0.3f * _startingLife / 4);

            EndGameManager.instance.addDeath(_playerId);
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }

    
    // true if it kill
    public void Hit(int parDamage)
    {
        _life -= parDamage;
    }

    void OnCollisionExit(Collision parOther)
    {
        GameObject other = parOther.gameObject;
        if (other && other.CompareTag("Unit") && other.GetComponent<Unit>()._playerId != _playerId)
        {
            _target = other;
            collideNum--;
        }
    }

    public virtual void OnCollisionEnter(Collision parOther)
    {
        GameObject other = parOther.gameObject;
        Motherbase mother = other.GetComponent<Motherbase>();
        if (other && other.CompareTag("Unit") && other.GetComponent<Unit>()._playerId != _playerId)
        {
            _target = other;
            collideNum++;
            lastAttack = 0;
            Attack();
        }
        else if (mother && other.CompareTag("MotherBase") && mother._playerId != _playerId)
        {
            mother.getDamage(1);
            Hit(_life);
        }
    }

    public virtual void OnTriggerEnter(Collider parOther)
    {
        if (parOther.CompareTag("Unit") && parOther.GetComponent<Unit>()._playerId != _playerId)
        {
            _trigger.Add(parOther.gameObject);
            if (!_target)
            {
                _isAttacking = true;
                changeTarget();
                StartCoroutine(targetMove());
            }
        }
    }

    public virtual void OnTriggerExit(Collider parOther)
    {
        
        _trigger.Remove(parOther.gameObject);
        if (_target == parOther.gameObject)
            changeTarget();

    }

    protected void changeTarget()
    {
        _trigger = _trigger.Where(trigger => trigger != null).ToList();

        if (_trigger.Count > 0)
        {
            _target = _trigger[0];
        }
        else
        {
            collideNum = 0;
            _isAttacking = false;
            _target = null;
            takeDestination();
        }
    }

    IEnumerator targetMove()
    { 
        while (_trigger.Count>0)
        {
            if (_target == null)
                changeTarget();
            else
            {
                if(_navMeshAgent.enabled == true)
                    _navMeshAgent.SetDestination(_target.transform.position);
            }
          
            
            yield return new WaitForSeconds(0.5f);
        }
        changeTarget();
    }

    public virtual void Attack()
    {
        if (_target)
        {
            Unit unit = _target.GetComponent<Unit>();
            if (unit && unit._playerId != _playerId)
            {
                unit.Hit(_damage);
                GameObject fxToDestroy = Instantiate(FxHitBlood, _target.transform.position, Quaternion.Euler(new Vector3(-50, 0, 0))) as GameObject;
                Destroy(fxToDestroy, 1.0f);
                EndGameManager.instance.addDamage(_playerId, _damage);
                applyBump(unit.transform.position, 0.1f,2);
            }
        }
        else
        {
            _isAttacking = false;
        }
    }

    void takeDestination()
    {
        if (laneEnd)
        {
            if (_navMeshAgent.enabled)
                _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
        }
        else
        {
            if(_navMeshAgent.enabled)
            _navMeshAgent.SetDestination(waypointDest.pos);
        }
    }

    public void applyBump(Vector3 from, float force, int useSmoother=0)
    {
        if (!isBumped)
        {
            Vector3 dir = transform.position - from;
            isBumped = true;
            //dir.y = force*2;
            if (useSmoother == 0)
                StartCoroutine(bump(dir * force));
            else
                StartCoroutine(bump(dir * force, useSmoother));
        }
        
    }

    IEnumerator bump(Vector3 distance, int localSmoother=0)
    {
        if (localSmoother == 0)
            localSmoother = smoother;
        for (int i=0; i< localSmoother; i++)
        {
            transform.position = transform.position + (distance / localSmoother);
            yield return 0;
        }
        isBumped = false;
    }

    
}
