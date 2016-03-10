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

    public override IEnumerator attacking()
    {
        _allAnims.Play("ATTACK");
        _navMeshAgent.Stop();
        StartCoroutine(reload());
        yield return new WaitForSeconds(_allAnims.GetClip("ATTACK").length - 0.65f);

        for (int i = 0; i < _trigger.Count; i++)
        {
            if (_trigger[i])
            {
                Unit unit = _trigger[i].GetComponent<Unit>();
                unit.applyBump(transform.position, bumpForce);
                unit.Hit(_damage);
            }
        }

        yield return new WaitForSeconds(0.65f);
        _navMeshAgent.Resume();
        _allAnims.Play("RUN");

    }

    override public void Attack()
    {

        if (_target && attackReady)
        {
            attackReady = false;
            StartCoroutine(attacking());
        }

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
