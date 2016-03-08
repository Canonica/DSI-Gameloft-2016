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
            poisonGas = StartCoroutine(PoisonousGas());
        }
        if(isActivePoison)
        {
            base.Attack();
        }
        base.FixedUpdate();
    }

    override public void Attack()
    {
        if (_target)
        {

            for (int i = 0; i < _trigger.Count; i++)
            {
                //float dist = Vector3.Distance(transform.position, _trigger[i].transform.position);
                //Debug.Log(dist);
                if (_trigger[i] && Vector3.Distance(transform.position, _trigger[i].transform.position) <= zoneBump)
                {
                    _trigger[i].GetComponent<Unit>().applyBump(transform.position, bumpForce);
                    _trigger.RemoveAt(i);
                }
            }
            //_target.GetComponent<Unit>().applyBump(transform.position, bumpForce);
        }
        else
        {
            changeTarget();
            return;
        }

        base.Attack();
        
    }

    IEnumerator PoisonousGas()
    {
        while(_life > 0)
        {
            for (int i = 0; i < _trigger.Count; i++)
            {
                _trigger[i].GetComponent<Unit>().Hit(poisonDamage);
            }
            Debug.Log("Poison degats");
            yield return new WaitForSeconds(poisonDelay);
        }
        poison = false;
    }
}
