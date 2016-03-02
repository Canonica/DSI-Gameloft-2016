using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    Rigidbody _rigid;

    public GameObject _target;

    public int _playerId = 0;

    public float _hatchTime = 1.0f;
    public bool _hasHatched = false;

    public float _attackDelay = 2f;

    public int _life = 2;
    public int _damage = 2;

    public float _maxMovementSpeed = 10.0f;
    public float _movementSpeed = 5.0f;

    NavMeshAgent _navMeshAgent;

    public GameObject _enemyMotherBase;

    public virtual void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(Hatch());
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public virtual void Update () {

        if (_hasHatched)
        {
            /*if(_target)
            {
                _rigid.AddForce((_target.transform.position - transform.position).normalized * _movementSpeed);
                _rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, _maxMovementSpeed);
            }
            else
            {
                _rigid.AddForce(transform.forward * _movementSpeed);
                _rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, _maxMovementSpeed);
            }
            transform.LookAt(transform.position + _rigid.velocity.normalized * 2);*/
        }
	}

    void Hit(int parDamage)
    {
        _life -= parDamage;
        if(_life <= 0)
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision parOther)
    {
        if (parOther.gameObject == _target)
        {
            Debug.Log("prepare");
            StartCoroutine(Attack());
        }
        else if(parOther.gameObject.CompareTag("MotherBase") && parOther.gameObject.GetComponent<Motherbase>().idPlayer != _playerId)
        {
            Debug.Log("MotherBase");
            parOther.gameObject.GetComponent<Motherbase>().getDamage(1);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider parOther)
    {
        if(parOther.CompareTag("Unit") && parOther.GetComponent<Unit>() && parOther.GetComponent<Unit>()._playerId != _playerId && !_target)
        {
            _target = parOther.gameObject;
            _navMeshAgent.SetDestination(_target.transform.position);
        }
        else
        {
            _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
        }
        
    }

    void OnTriggerStay(Collider parOther)
    {
        if (!_target)
        {
            OnTriggerEnter(parOther);
        }
        else
        {
            _navMeshAgent.SetDestination(_target.transform.position);
        }
        
        

    }

    void OnTriggerExit(Collider parOther)
    {
        if (parOther.gameObject == _target)
        {
            _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
            _target = null;
        }
        

    }

    IEnumerator Attack()
    {
        while(_target)
        {
            yield return new WaitForSeconds(_attackDelay);
            Debug.Log("Attack");
            if (_target && _target.GetComponent<Unit>()._playerId != _playerId)
            {
                _target.GetComponent<Unit>().Hit(_damage);
                Debug.Log("Hit");
            }
        }
    }

    IEnumerator Hatch()
    {
        yield return new WaitForSeconds(_hatchTime);
        _hasHatched = true;
    }
}
