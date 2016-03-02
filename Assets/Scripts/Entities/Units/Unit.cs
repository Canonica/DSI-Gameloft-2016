using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Entity
{
    Rigidbody _rigid;

    public GameObject _target;

    public float _hatchTime = 1.0f;
    public bool _hasHatched = false;

    public float _attackDelay = 2f;

    public int _damage = 2;

    public float _maxMovementSpeed = 10.0f;
    public float _movementSpeed = 5.0f;

    NavMeshAgent _navMeshAgent;

    public List<GameObject> _trigger;

    bool _isAttacking;

    public float _distanceMinLane = 4f;

    public Vector3 _Lane;
    public bool isInLane = false;

    public override void Start()
    {
        base.Start();
        _trigger = new List<GameObject>();
        _rigid = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(Hatch());
    }

    // Update is called once per frame

    public override void Update()
    {
        base.Update();

        if (_hasHatched)
        {
            if (!isInLane && Vector3.Distance(_Lane, transform.position) < _distanceMinLane)
            {
                isInLane = true;
                _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
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
        if (other.CompareTag("Unit") && other.GetComponent<Unit>()._playerId != _playerId && !_isAttacking)
        {
            _target = other;
            _isAttacking = true;
            Attack();
        }

        else if (other.CompareTag("MotherBase") && mother._playerId != _playerId)
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
                _target = parOther.gameObject;
                StartCoroutine(targetMove());
            }
        }
        else
        {
            //takeDestination();
        }

    }

    void OnTriggerExit(Collider parOther)
    {
        _trigger.Remove(parOther.gameObject);
        if (_trigger.Count > 0)
        {
            _target = _trigger[0];
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

    void Attack()
    {
        if(_target)
        {
            Unit unit = _target.GetComponent<Unit>();
            if (unit && unit._playerId != _playerId)
            {
                Debug.Log("Attack");
                unit.Hit(_damage);
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
        if (isInLane)
        {
            _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
        }
        else
        {
            _navMeshAgent.SetDestination(_Lane);
        }
    }
}
