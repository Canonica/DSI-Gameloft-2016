using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using System;

public class Unit : Entity
{
    

    [Header("Unit Tweak")]
    [Tweakable]
    public float attackSpeed;
    [Tweakable]
    public float _movementSpeed;
    [Tweakable]
    public int groupSpawn;
    [Tweakable]
    public int _damage ;
    [Tweakable]
    public float bumpResist = 1;
    [Tweakable]
    public int manaCost;

    [Header("Tool")]
    [Tweakable]
    public Texture2D img;
    [Tweakable]
    public string unitName = "Unit";
    protected bool attackReady = false;
    [Header("Bump Option")]
    public int smoother = 5;

    [Header("Other")]
    [HideInInspector]
    public GameObject _target;
    protected NavMeshAgent _navMeshAgent;

    public List<GameObject> _trigger;
    public float _distanceMinLane = 4f;
    [HideInInspector]
    public Waypoint waypointDest;
    [HideInInspector]
    public bool laneEnd = false;
    [HideInInspector]
    public int collideNum = 0;
    [HideInInspector]
    public bool isBumped = false;
    private int _startingLife;
    [HideInInspector]
    public int _actualLane;
    bool isStunn = false;
    [Tweakable]
    public float timeStun = 1;

    [Header("FX")]
    [SerializeField]
    private GameObject FxHitBlood;

    [SerializeField]
    private GameObject FxDeathBlood;

    [Header("Sound")]
    public AudioClip spawnFX;
    public AudioClip hitFX;

    private Animation _allAnims;


    public override void Start()
    {
        _actualLane = 0;
        _startingLife = _life;
        base.Start();
        if (bumpResist == 0)
            bumpResist = 1;
        _trigger = new List<GameObject>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;
        attackReady = true;
        _allAnims = GetComponentInChildren<Animation>();
        if (spawnFX)
            SoundManager.Instance.playSound(spawnFX, 0.3f);
        //_allAnims.Play("RUN");
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        if (!laneEnd && Vector3.Distance(waypointDest.pos, transform.position) < _distanceMinLane)
        {
            if (waypointDest.Next(_playerId) == null)
            {
                laneEnd = true;
            }
            else
            {
                _actualLane = waypointDest.transform.parent.GetComponent<Lane>().num;
                waypointDest = waypointDest.Next(_playerId);
                if (waypointDest.isTeleport)
                {
                    transform.position = waypointDest.pos;
                    waypointDest = waypointDest.Next(_playerId);
                }
            }
            takeDestination();
        }

        
        if (attackReady && _target && collideNum > 0)
        {
            Attack();
        }

        base.Update();
    }

    void LateUpdate()
    {
        if (_life <= 0)
        {
            OnDeath();

            //Instantiate(FxDeathBlood, this.gameObject.transform.position, Quaternion.Euler(new Vector3(-50, 0, 0)));
            Camera.main.DOKill(true);
            Camera.main.DOShakePosition(0.05f * _startingLife / 4, 0.3f * _startingLife / 4);
            dead();
            //StartCoroutine(animDeath());

        }
    }

    public virtual void OnDeath()
    {

    }

    IEnumerator animDeath()
    {
        //_allAnims.Play("DEATH");
        yield return new WaitForSeconds(0.5f);// _allAnims.GetClip("DEATH").length);
        dead();
    }

    void dead()
    {
        StopAllCoroutines();
        EndGameManager.instance.addDeath(_playerId);
        Destroy(this.gameObject);
    }

    
    // true if it kill
    public virtual void Hit(int parDamage)
    {
        _life -= parDamage;
    }

    void OnCollisionExit(Collision parOther)
    {
        GameObject other = parOther.gameObject;
        if (other && other.CompareTag("Unit") && other.GetComponent<Unit>()._playerId != _playerId)
        {
            //_target = other;
            if (collideNum > 0)
            {
                collideNum--;
            }
        }
    }

    public virtual void OnCollisionEnter(Collision parOther)
    {
        GameObject other = parOther.gameObject;
        Motherbase mother = other.GetComponent<Motherbase>();
        if (mother && other.CompareTag("MotherBase") && mother._playerId != _playerId)
        {
            EndGameManager.instance.addDamage(_playerId, _damage);
            EndGameManager.instance.addDamage((_playerId % 2) + 1, _life);
            mother.getDamage(_damage);
            dead();
        }else
        if (other && other.CompareTag("Unit") && other.GetComponent<Unit>()._playerId != _playerId)
        {
            _target = other;
            collideNum++;
            Attack();
        }
    }

    public void getStun()
    {
        if (!isStunn)
        {
            isStunn = true;
            _navMeshAgent.SetDestination(transform.position);
            StartCoroutine(stunTime());
        }
    }

    IEnumerator stunTime()
    {
        yield return new WaitForSeconds(timeStun);
        isStunn = false;
        takeDestination();
    }

    public virtual void OnTriggerEnter(Collider parOther)
    {

        if (parOther.CompareTag("Unit") && parOther.GetComponent<Unit>()._playerId != _playerId && (parOther.GetComponent<Unit>()._actualLane == _actualLane || _actualLane == 0 || parOther.GetComponent<Unit>()._actualLane ==0))
        {
            
            if (_trigger.IndexOf(parOther.gameObject) < 0)
            _trigger.Add(parOther.gameObject);
            if (!_target)
            {
                changeTarget();
                StartCoroutine(targetMove());
                _navMeshAgent.velocity /= 5;
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
        if (_target && attackReady && !isStunn)
        {
            attackReady = false;
            Unit unit = _target.GetComponent<Unit>();
            if (unit && unit._playerId != _playerId)
            {
                if (hitFX)
                    SoundManager.Instance.playSound(hitFX, 1);
                unit.Hit(_damage);
               // GameObject fxToDestroy = Instantiate(FxHitBlood, _target.transform.position, Quaternion.Euler(new Vector3(-50, 0, 0))) as GameObject;
                EndGameManager.instance.addDamage(_playerId, _damage);
            }
            StartCoroutine(reload());
        }
        else
        {
            
            //if (!_target)
                //applyBump(_target.transform.position, 0.1f, 2);
            
        }
    }

    public virtual IEnumerator reload()
    {
        //_allAnims.Play("ATTACK");
        attackReady = false;
        yield return new WaitForSeconds(attackSpeed);
        attackReady = true;
        //Attack();
        //_allAnims.Play("RUN");
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
            if (_navMeshAgent.isActiveAndEnabled && _navMeshAgent.isOnNavMesh)
            {

                _navMeshAgent.SetDestination(waypointDest.pos);
            }
            else
            {
                dead();
            }
            
        }
    }

    public void applyBump(Vector3 from, float force, int useSmoother=0)
    {
        if (!isBumped)
        {
            isBumped = true;
            Vector3 dir = transform.position - from;
            
            force = force / bumpResist;
            if (useSmoother == 0)
                StartCoroutine(bump(dir.normalized * force));
            else
                StartCoroutine(bump(dir.normalized * force, useSmoother));
        }
        
    }

    IEnumerator bump(Vector3 distance, int localSmoother=0)
    {
        //_navMeshAgent.enabled = false;
        GetComponent<Collider>().enabled = false;
        if (localSmoother == 0)
            localSmoother = smoother;
        for (int i=0; i< localSmoother; i++)
        {
            transform.position = transform.position + (distance / localSmoother);
            yield return 0;
        }
        isBumped = false;
        
        GetComponent<Collider>().enabled = true;
        //_navMeshAgent.enabled = true;
    }
    

    
}
