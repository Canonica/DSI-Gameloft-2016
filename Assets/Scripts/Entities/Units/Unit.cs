using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Entity
{
    [Header("Unit Option")]
    public float attackSpeed = 1;
    public float _movementSpeed = 5.0f;
    public int groupSpawn = 1;
    public int _damage = 2;
    // Utilisera les Spells
    public float _hatchTime = 1.0f;


    [Header("Bump Option")]
    public int smoother = 20;

    [Header("Other")]
    public GameObject _target;
    NavMeshAgent _navMeshAgent;
    public List<GameObject> _trigger;
    bool _isAttacking;
    public float _distanceMinLane = 4f;
    public Waypoint waypointDest;
    public bool laneEnd = false;
    public float lastCollision = 0;
    public int collideNum = 0;
    float lastAttack = 0;
    bool isBumped = false;

    public override void Start()
    {
        base.Start();

        _trigger = new List<GameObject>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {

        base.FixedUpdate();
        
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
            /*if(_target)
            {
                RaycastHit hit;
                Vector3 direc = (_target.transform.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, _target.transform.position - transform.position, out hit))
                {
                    if (hit.collider.gameObject != _target && hit.collider.gameObject != gameObject && _navMeshAgent.destination != transform.position)
                    {
                        _navMeshAgent.SetDestination(transform.position);
                    }
                    else if (_navMeshAgent.destination == transform.position)
                    {
                        _navMeshAgent.SetDestination(_target.transform.position);
                    }
                }
            }*/
            
        
    }

    void LateUpdate()
    {
        if (_life <= 0)
        {
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

    void OnCollisionEnter(Collision parOther)
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

    void OnTriggerEnter(Collider parOther)
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

    void OnTriggerExit(Collider parOther)
    {
        
        _trigger.Remove(parOther.gameObject);
        if (_target == parOther.gameObject)
            changeTarget();

    }

    protected void changeTarget()
    {
        while (_trigger.Count > 0 && _trigger[0] == null)
        {
            
            _trigger.RemoveAt(0);
        }

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
           _navMeshAgent.SetDestination(_target.transform.position);
            
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
            _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
        }
        else
        {
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
