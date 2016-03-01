using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    
    Rigidbody _rigid;

    public float _hatchTime = 1.0f;
    public bool _hasHatched = false;

    public int _life = 10;
    public int _damage = 2;

    public float _maxMovementSpeed = 10.0f;
    public float _movementSpeed = 5.0f;
    
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(Hatch());
    }

    // Update is called once per frame
    public virtual void Update () {

        if (_hasHatched)
        {
            _rigid.AddForce(Vector3.forward * _movementSpeed);
            _rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, _maxMovementSpeed);
        }
	}

    void Hit(int parDamage)
    {
            _life -= parDamage;
            if(_life <= 0)
            {
                //Destroy le gameobject
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
