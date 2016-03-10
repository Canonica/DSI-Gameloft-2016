using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public ParticleSystem PS_Stomp;
    public ParticleSystem PS_Stun;

    public GameObject leg1;
    public GameObject leg2;
    public GameObject bubons;

    public GameObject fxExplo;
    
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

    public override void applyLevelUp()
    {
        if(canExplode)
        {
            bubons.SetActive(true);
            
        }
        else if(firstJump)
        {
            leg1.SetActive(true);
            leg2.SetActive(true);
        }
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

    public override void OnTriggerExit(Collider parOther)
    {
        if (parOther.gameObject != _target)
        {
            _trigger.Remove(parOther.gameObject);
        }


    }


    override public void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (_target && !isJumping&& attackReady)
        {
            isJumping = true;
            StartCoroutine(dash());
        }
    }

    override public void Attack()
    {
        if (_target && attackReady)
        {
            attackReady = false;
            StartCoroutine(AOE());
        }
    }

    public void DashAttack()
    {
        attackReady = false;
        StartCoroutine(reload());
        isActiveAOE = true;
        List<GameObject> localList = GetComponentInChildren<BumpJumper>().bumpList;
        for (int i = 0; i < localList.Count; i++)
        {
            if (localList[i] && localList[i].GetComponent<Unit>()._playerId != _playerId)
            {
                if (canStun)
                {
                    PS_Stun.Play(true);
                }
                else
                {
                    PS_Stomp.Play(true);
                }
                if (canStun && Random.Range(0, 100) > 50)
                {
                    localList[i].GetComponent<Unit>().getStun();
                }
                if (firstJump)
                {
                    firstJump = false;
                    localList[i].GetComponent<Unit>().Hit(_damage * 2);
                }
                else
                {
                    EndGameManager.instance.addDamage(_playerId, _damage);
                    localList[i].GetComponent<Unit>().Hit(_damage);
                }
                UnitTank unitT = localList[i].GetComponent<UnitTank>();
                if (unitT && unitT.reflectDamage)
                {
                    Hit((int)(_damage * unitT.reflectDamageAmount));
                }
            }
        }
        _allAnims.Play("RUN");
        isActiveAOE = false;
    }

    IEnumerator dash()
    {
        //yield return new WaitForSeconds(Random.Range(0, 1f));
        //if (!_target)
        //{
        //    yield break;
        //}
        //if (_target.GetComponent<UnitRush>() && _target.GetComponent<UnitRush>().isFlying)
        //{
        //    yield break;
        //}
        //if (_target.GetComponent<UnitJump>() && _target.GetComponent<UnitJump>().isJumping)
        //{
        //    yield break;
        //}
        //GetComponent<Collider>().enabled = false;
        //_navMeshAgent.enabled = false;

        // Calcul direction
        //Vector3 dir = _target.transform.position - transform.position;
        //float dist = Vector3.Distance(_target.transform.position, transform.position);
        //Vector3 dirJump = (dir.normalized * dist) ;

        //// smoother = nombre de fram utilisé pour faire le saut
        //for (int i = 0; i < smoother; i++)
        //{
        //    if (_target)
        //    {
        //        dir = _target.transform.position - transform.position;
        //        dist = Vector3.Distance(_target.transform.position, transform.position);
        //        dirJump = (dir.normalized * dist);
        //        transform.position = transform.position + (dirJump / smoother);
        //        yield return 0;
        //    }
        //    else
        //    {
        //        break;
        //    }

        //}

        isJumping = true;

        float dist = Vector3.Distance(_target.transform.position, transform.position);
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (dist > 1f && _target)
        {
            transform.position = Vector3.Lerp(startPosition, _target.transform.position, (Time.time - startTime) / 0.2f);

            dist = Vector3.Distance(_target.transform.position, transform.position);
            yield return new WaitForEndOfFrame();
        }

        DashAttack();
        _allAnims.Play("RUN");
        isJumping = false;

        //GetComponent<Collider>().enabled = true;
        //_navMeshAgent.enabled = true;

        //if (!isActiveAOE)
        //    StartCoroutine(AOE());
    }


    /*IEnumerator jump()
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
    }*/

    IEnumerator AOE()
    {
        _allAnims.Play("ATTACK");
        StartCoroutine(reload());
        yield return new WaitForSeconds(_allAnims.GetClip("ATTACK").length - 0.2f);
        isActiveAOE = true;
        List<GameObject> localList = GetComponentInChildren<BumpJumper>().bumpList;
        for (int i=0; i < localList.Count;i++)
        {
            if (localList[i]&& localList[i].GetComponent<Unit>()._playerId != _playerId)
            {
                if(canStun)
                {
                    PS_Stun.Play(true);
                }
                else
                {
                    PS_Stomp.Play(true);
                }
                if (canStun && Random.Range(0, 100) > 50)
                {
                    localList[i].GetComponent<Unit>().getStun();
                }
                if(firstJump)
                {
                    firstJump = false;
                    localList[i].GetComponent<Unit>().Hit(_damage * 2);
                }
                else
                {
                    localList[i].GetComponent<Unit>().Hit(_damage);
                }
                UnitTank unitT = localList[i].GetComponent<UnitTank>();
                if (unitT && unitT.reflectDamage)
                {
                    Hit((int)(_damage * unitT.reflectDamageAmount));
                }
               // localList[i].GetComponent<Unit>().applyBump(transform.position, forceAOE);
            }
        }
        yield return new WaitForSeconds(0.2f);
        _allAnims.Play("RUN");
        isActiveAOE = false;
    }

    public override void Hit(int parDamage)
    {
        base.Hit(parDamage);
        
        if (_life<=0 && canExplode)
        {
            //Vector3 newPosition = new Vector3(transform.position.x, transform.position.y+8.0f, transform.position.z);
            Destroy(Instantiate(fxExplo, transform.position, Quaternion.Euler(-90, 0, 0)), 3.0f);
            canExplode = false;
            StopAllCoroutines();
            isActiveAOE = false;
            StartCoroutine(AOE());

        }
    }
    
}
