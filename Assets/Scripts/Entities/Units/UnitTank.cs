using UnityEngine;
using System.Collections;

public class UnitTank : Unit {

    public float bumpForce;
    public float zoneBump;

    public bool reflectDamage;
    [Range(0,1)]
    public float reflectDamageAmount = 1.0f;

    public bool negateDamage;
    [Range(0, 1)]
    public float negateDamageAmount = 1.0f;
    public float negateDamageRange = 10.0f;

    public bool poison;
    public bool isActivePoison;
    public Coroutine poisonGas;
    public float poisonDelay = 1.0f;
    public int poisonDamage = 1;

    public ParticleSystem PS_poison;

    public bool buffy = false;
    [Range(0, 1)]
    public float buffedUnit;
    public float buffedUnitRange = 5.0f;

    override
    public void Start()
    {
        base.Start();
    }
    override
    public void FixedUpdate()
    {
        if(poison && poisonGas == null)
        {
            PS_poison.Play(true);
            poisonGas = StartCoroutine(PoisonousGas());
        }
        if(isActivePoison)
        {
            base.Attack();
        }
        base.FixedUpdate();
    }

    public override void Update()
    {
        
        base.Update();
    }

    public override void OnCollisionEnter(Collision parOther)
    {
        base.OnCollisionEnter(parOther);
        
    }

    public override void OnTriggerEnter(Collider parOther)
    {
        base.OnTriggerEnter(parOther);
        
        
    }


    override public void Attack()
    {
        
        if (_target&&attackReady)
        {
            attackReady = false;
            for (int i = 0; i < _trigger.Count; i++)
            {
                //float dist = Vector3.Distance(transform.position, _trigger[i].transform.position);
                //Debug.Log(dist);
                if(_trigger[i])
                {
                    _trigger[i].GetComponent<Unit>().applyBump(transform.position, bumpForce);
                }
                
            }
            //_target.GetComponent<Unit>().applyBump(transform.position, bumpForce);
        }
        else
        {
            changeTarget();
            return;
        }

        StartCoroutine(reload());
    }

    IEnumerator PoisonousGas()
    {
        while(_life > 0)
        {
            for (int i = 0; i < _trigger.Count; i++)
            {
                if(_trigger[i])
                _trigger[i].GetComponent<Unit>().Hit(poisonDamage);
            }
            Debug.Log("Poison degats");
            yield return new WaitForSeconds(poisonDelay);
        }
        poison = false;
    }
}
