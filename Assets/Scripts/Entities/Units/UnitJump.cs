using UnityEngine;
using System.Collections;

public class UnitJump : Unit {

    [HideInInspector]
    public bool isJumping = false;
    bool isActiveAOE = false;
    public int forceAOE ;
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
        base.OnCollisionEnter(col);
        //Motherbase mother = col.gameObject.GetComponent<Motherbase>();
        //if (mother && col.gameObject.CompareTag("MotherBase") && mother._playerId != _playerId)
        //{
        //    EndGameManager.instance.addDamage(_playerId, _damage);
        //    EndGameManager.instance.addDamage((_playerId % 2) + 1, _life);
        //    mother.getDamage(_damage);
        //    Hit(_life);
        //}
    }

    override public void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (_target && !isJumping&& attackReady)
        {
            
            StartCoroutine(dash());
        }
    }

    override public void Attack()
    {
        if (_target && attackReady)
        {
            attackReady = false;
            StartCoroutine(AOE());
            //StartCoroutine(jump());
        }
    }

    IEnumerator dash()
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        if (!_target)
        {
            yield break;
        }
        if (_target.GetComponent<UnitRush>() && _target.GetComponent<UnitRush>().isFlying)
        {
            yield break;
        }
        if (_target.GetComponent<UnitJump>() && _target.GetComponent<UnitJump>().isJumping)
        {
            yield break;
        }
        isJumping = true;
        //GetComponent<Collider>().enabled = false;
        //_navMeshAgent.enabled = false;

        // Calcul direction
        Vector3 dir = _target.transform.position - transform.position;
        float dist = Vector3.Distance(_target.transform.position, transform.position);
        Vector3 dirJump = (dir.normalized * dist) ;

        // smoother = nombre de fram utilisé pour faire le saut
        for (int i = 0; i < smoother; i++)
        {
            if (_target)
            {
                dir = _target.transform.position - transform.position;
                dist = Vector3.Distance(_target.transform.position, transform.position);
                dirJump = (dir.normalized * dist);
                transform.position = transform.position + (dirJump / smoother);
                yield return 0;
            }
            else
            {
                break;
            }
            
        }

        //GetComponent<Collider>().enabled = true;
        //_navMeshAgent.enabled = true;

        if (!isActiveAOE)
            StartCoroutine(AOE());
    }

    IEnumerator jump()
    {
        yield return new WaitForSeconds(Random.Range(0, 10)/10);
        if (_target.GetComponent<UnitRush>()&& _target.GetComponent<UnitRush>().isFlying)
        {
            yield break;
        }
        if (_target.GetComponent<UnitJump>().isJumping)
        {
            yield break;
        }
        isJumping = true;
        //GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;

        // Calcul direction
        Vector3 dir = _target.transform.position - transform.position;
        float dist = Vector3.Distance(_target.transform.position, transform.position);

        // Calcul de la courbe d'ascention
        Vector3 dirJump = (dir.normalized * dist) / 2;
        dirJump.y = heightJump;     // set la hauteur du saut

        // smoother = nombre de fram utilisé pour faire le saut
        for (int i = 0; i < smoother ; i++)
        {
            // Parcour de la courbe
            transform.position = transform.position + (dirJump / smoother);
            yield return 0;
        }

        // Calcul de la courbe descendante
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
            if (_trigger[i])
            {
                if (canStun && Random.Range(0, 100) > 50)
                {
                    _trigger[i].GetComponent<Unit>().getStun();
                }
                if(firstJump)
                {
                    firstJump = false;
                    _trigger[i].GetComponent<Unit>().Hit(_damage * 2);
                }
                else
                {
                    _trigger[i].GetComponent<Unit>().Hit(_damage);
                }
                UnitTank unitT = _trigger[i].GetComponent<UnitTank>();
                if (unitT && unitT.reflectDamage)
                {
                    Hit((int)(_damage * unitT.reflectDamageAmount));
                }
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
            canExplode = false;
            StopAllCoroutines();
            isActiveAOE = false;
            StartCoroutine(AOE());

        }
    }
    
}
