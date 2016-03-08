using UnityEngine;
using System.Collections;

public class UnitJump : Unit {

    public bool isJumping = false;
    bool isActiveAOE = false;
    public int forceAOE = 1;
    float timeAOE = 2;
    public bool canStun = false;
    public bool canExplode = false;
    public int heightJump = 5;
    public bool firstJump = false;


    override
    public void Start()
    {
        base.Start();
    }
    override
    public void FixedUpdate()
    {
        base.FixedUpdate();
    }

    override public void OnCollisionEnter(Collision col)
    {
        //base.OnCollisionEnter(col);
        Motherbase mother = col.gameObject.GetComponent<Motherbase>();
        if (mother && col.gameObject.CompareTag("MotherBase") && mother._playerId != _playerId)
        {
            EndGameManager.instance.addDamage(_playerId, _damage);
            EndGameManager.instance.addDamage((_playerId % 2) + 1, _life);
            mother.getDamage(_damage);
            Hit(_life);
        }
    }

    override public void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (_target && !isJumping&& attackReady)
        {
            isJumping = true;
            StartCoroutine(jump());
        }
    }

    override public void Attack()
    {
        if (_target&& attackReady)
        {
            attackReady = false;
            //StartCoroutine(AOE());
            StartCoroutine(jump());
        }
    }

    IEnumerator jump()
    {
        if (_target.GetComponent<UnitRush>()&& _target.GetComponent<UnitRush>().isFlying)
        {
            yield break;
        }

        if (_target.transform.position.y > transform.position.y)
        {
            yield break;
        }

        //GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
        Vector3 dir = _target.transform.position - transform.position;
        float dist = Vector3.Distance(_target.transform.position, transform.position);
        
        Vector3 dirJump = (dir.normalized * dist) / 2;
        dirJump.y = heightJump;

        for (int i = 0; i < smoother ; i++)
        {
            Debug.Log("jump"); 
            transform.position = transform.position + (dirJump / smoother);
            yield return 0;
        }
        dirJump = (dir.normalized * dist) / 2;
        dir.y -= heightJump;
        for (int i = 0; i < smoother; i++)
        {
            transform.position = transform.position + (dirJump / smoother);
            yield return 0;
        }
        
        //GetComponent<Collider>().enabled = true;
        _navMeshAgent.enabled = true;
        if(!isActiveAOE)
        StartCoroutine(AOE());
    }

    IEnumerator AOE()
    {
        isActiveAOE = true;
        for (int i=0; i < _trigger.Count;i++)
        {
            if (_trigger[i] )
            {
                EndGameManager.instance.addDamage(_playerId, _damage);
                if (canStun && Random.Range(0, 100) > 50)
                {
                    _trigger[i].GetComponent<Unit>().getStun();
                }
                _trigger[i].GetComponent<Unit>().Hit(_damage);
                Debug.Log("Attack " + _trigger[i].name + " dmg " + _damage);
                _trigger[i].GetComponent<Unit>().applyBump(transform.position, forceAOE);
            }
            yield return 0;
        }
        isActiveAOE = false;
        StartCoroutine(reload());
    }

    public override void Hit(int parDamage)
    {
        
        base.Hit(parDamage);
        if (_life<=0 && canExplode)
        {
            StartCoroutine(AOE());
        }
    }

    public override IEnumerator reload()
    {
        yield return base.reload();
        Attack();
    }
}
