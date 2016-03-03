using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Entity
{
    // Utilisera les Spells

    public GameObject _target;

    public float _hatchTime = 1.0f;
    public bool _hasHatched = false;
    public int groupSpawn = 6;
    public float _attackDelay = 2f;

    public int _damage = 2;

    public float _maxMovementSpeed = 10.0f;
    public float _movementSpeed = 5.0f;

    NavMeshAgent _navMeshAgent;

    public List<GameObject> _trigger;

    bool _isAttacking;

    public float _distanceMinLane = 4f;

    public Waypoint waypointDest;
    public bool laneDone = false;
    public float lastCollision = 0;
    int collideNum = 0;
    public float attackSpeed = 1;
    float lastAttack=0;
    public override void Start()
    {
        base.Start();

        _trigger = new List<GameObject>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {

        base.FixedUpdate();

        if (collideNum>0)
        {
            lastAttack += Time.deltaTime;
            if (lastAttack > attackSpeed)
            {
                Attack();
            }
        }
        
        if (!laneDone && Vector3.Distance(waypointDest.pos, transform.position) < _distanceMinLane)
        {
            if (waypointDest.Next(_playerId) == null)
            {
                laneDone = true;
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
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }

    void Hit(int parDamage)
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
                _target = parOther.gameObject;
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

    void changeTarget()
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
            _isAttacking = false;
            _target = null;
            takeDestination();
        }
    }

    IEnumerator targetMove()
    { 
        while (_target)
        {
            _navMeshAgent.SetDestination(_target.transform.position);

            yield return new WaitForSeconds(0.5f);
        }
        changeTarget();
        takeDestination();
    }

    void Attack()
    {
        if(_target)
        {
            Unit unit = _target.GetComponent<Unit>();
            if (unit && unit._playerId != _playerId)
            {
                unit.Hit(_damage);
            }
        }
    }

    void takeDestination()
    {
        if (laneDone)
        {
            _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
        }
        else
        {
            _navMeshAgent.SetDestination(waypointDest.pos);
        }
    }
}
