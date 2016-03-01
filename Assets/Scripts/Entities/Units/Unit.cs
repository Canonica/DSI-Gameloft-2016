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

    public int _life = 10;
    public int _damage = 2;

    public float _maxMovementSpeed = 10.0f;
    public float _movementSpeed = 5.0f;

    public virtual void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(Hatch());
    }

    // Update is called once per frame
    public virtual void Update () {

        if (_hasHatched)
        {
            if(_target)
            {
                _rigid.AddForce((_target.transform.position - transform.position).normalized * _movementSpeed);
                _rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, _maxMovementSpeed);
            }
            else
            {
                _rigid.AddForce(transform.forward * _movementSpeed);
                _rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, _maxMovementSpeed);
            }
            transform.LookAt(transform.position + _rigid.velocity.normalized * 2);
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
        Debug.Log("lol");
        if (parOther.gameObject == _target)
        {
            Debug.Log("prepare");
            StartCoroutine(Attack());
        }
    }

    void OnTriggerEnter(Collider parOther)
    {
        if(parOther.CompareTag("Unit") && parOther.GetComponent<Unit>() && parOther.GetComponent<Unit>()._playerId != _playerId)
        {
            _target = parOther.gameObject;
        }
    }

    void OnTriggerStay(Collider parOther)
    {
        if (_target)
        {
            OnTriggerEnter(parOther);
        }
    }

    void OnTriggerExit(Collider parOther)
    {
        if (parOther.gameObject == _target)
        {
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

    void OnCollisionEnter(Collision parOther)
    {
        if(parOther.gameObject.CompareTag("Map"))
        {
            GetComponent<NavMeshAgent>().SetDestination(transform.position + Vector3.forward * 30);
        }
    }
}
