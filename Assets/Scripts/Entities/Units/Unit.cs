using UnityEngine;
using System.Collections;

public class Unit : Entity
{

    public GameObject _target;

    public float _hatchTime = 1.0f;
    public bool _hasHatched = false;
    public int groupSpawn = 6;
    public float _attackDelay = 2f;

    public int _damage = 2;

    public float _maxMovementSpeed = 10.0f;
    public float _movementSpeed = 5.0f;

    NavMeshAgent _navMeshAgent;


    public float _distanceMinLane = 4f;

    public Waypoint waypointDest;
    public bool laneDone = false;

    public override void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(Hatch());
    }

    // Update is called once per frame

    public override void Update()
    {
        base.Update();
        if (_hasHatched)
        {
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
        }
    }

    void Hit(int parDamage)
    {
        _life -= parDamage;
        if (_life <= 0)
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision parOther)
    {
        GameObject other = parOther.gameObject;
        Motherbase mother = other.GetComponent<Motherbase>();
        if (other == _target)
        {
            StartCoroutine(Attack());
        }

        else if (other.CompareTag("MotherBase") && mother._playerId != _playerId)
        {
            mother.getDamage(1);
            Hit(_life);
        }
    }

    void OnTriggerEnter(Collider parOther)
    {
        Unit unit = parOther.GetComponent<Unit>();
        if (parOther.CompareTag("Unit") && unit._playerId != _playerId && !_target)
        {
            _target = parOther.gameObject;
            StartCoroutine(targetMove());
        }
        else
        {
            //takeDestination();
        }

    }

    IEnumerator targetMove()
    { 
        while (_target)
        {
            _navMeshAgent.SetDestination(_target.transform.position);

            yield return new WaitForSeconds(2);
        }
        _target = null;
        takeDestination();
    }

    IEnumerator Attack()
    {
        while (_target)
        {
            Unit unit = _target.GetComponent<Unit>();
            yield return new WaitForSeconds(_attackDelay);
            if (_target && unit && unit._playerId != _playerId )
            {
                unit.Hit(_damage);
            }
            else
            {
                break;
            }
        }

    }

    IEnumerator Hatch()
    {
        yield return new WaitForSeconds(_hatchTime);
        _hasHatched = true;
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
