﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using System;

public class Unit : Entity
{
    [Header("Tool")]
    [Tweakable]
    public Texture2D img;
    [Tweakable]
    public string unitName = "Unit";

    [Header("Unit Option")]
    [Tweakable]
    public float attackSpeed = 1;
    protected bool attackReady = false;
    [Tweakable]
    public float _movementSpeed = 5.0f;
    [Tweakable]
    public int groupSpawn = 1;
    [Tweakable]
    public int _damage = 2;
    // Utilisera les Spells
    [Tweakable]
    public float _hatchTime = 1.0f;
    [Tweakable]
    public float bumpResist=1;

    [Tweakable]
    public int manaCost;

    [Header("Bump Option")]
    public int smoother = 20;

    [Header("Other")]
    public GameObject _target;
    protected NavMeshAgent _navMeshAgent;
    public List<GameObject> _trigger;
    public float _distanceMinLane = 4f;
    public Waypoint waypointDest;
    public bool laneEnd = false;
    public float lastCollision = 0;
    public int collideNum = 0;
    public bool isBumped = false;
    private int _startingLife;
    public int _actualLane;
    bool isStunn = false;
    [Tweakable]
    public float timeStun = 1;

    [Header("FX")]
    [SerializeField]
    private GameObject FxHitBlood;

    [SerializeField]
    private GameObject FxDeathBlood;

    [Header("Sound")]
    public AudioClip spawnFX;
    public AudioClip hitFX;

    private Animation _allAnims;


    public override void Start()
    {
        _startingLife = _life;
        base.Start();
        if (bumpResist == 0)
            bumpResist = 1;
        _trigger = new List<GameObject>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;
        attackReady = true;
        _allAnims = GetComponentInChildren<Animation>();
        if (spawnFX)
            SoundManager.Instance.playSound(spawnFX, 0.3f);
        _allAnims.Play("RUN");
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {
        
        

        base.FixedUpdate();

    }

    public override void Update()
    {
        if (!laneEnd && Vector3.Distance(waypointDest.pos, transform.position) < _distanceMinLane)
        {
            if (waypointDest.Next(_playerId) == null)
            {
                laneEnd = true;
            }
            else
            {
                waypointDest = waypointDest.Next(_playerId);
                if (waypointDest.isTeleport)
                {
                    transform.position = waypointDest.pos;
                    waypointDest = waypointDest.Next(_playerId);
                }
            }
            takeDestination();
        }

        
        //if (attackReady && _target)
        //{
        //    Attack();
        //}

        base.Update();
    }

    void LateUpdate()
    {
        if (_life <= 0)
        {
            OnDeath();

            //Instantiate(FxDeathBlood, this.gameObject.transform.position, Quaternion.Euler(new Vector3(-50, 0, 0)));
            Camera.main.DOKill(true);
            Camera.main.DOShakePosition(0.05f * _startingLife / 4, 0.3f * _startingLife / 4);
            dead();
            //StartCoroutine(animDeath());

        }
    }

    public virtual void OnDeath()
    {

    }

    IEnumerator animDeath()
    {
        _allAnims.Play("DEATH");
        yield return new WaitForSeconds(_allAnims.GetClip("DEATH").length);// _allAnims.GetClip("DEATH").length);
        dead();
    }

    void dead()
    {
        StopAllCoroutines();
        EndGameManager.instance.addDeath(_playerId);
        Destroy(this.gameObject);
    }

    
    // true if it kill
    public virtual void Hit(int parDamage)
    {
        _life -= parDamage;
    }

    void OnCollisionExit(Collision parOther)
    {
        GameObject other = parOther.gameObject;
        if (other && other.CompareTag("Unit") && other.GetComponent<Unit>()._playerId != _playerId)
        {
            _target = other;
            collideNum--;
        }
    }

    public virtual void OnCollisionEnter(Collision parOther)
    {
        GameObject other = parOther.gameObject;
        Motherbase mother = other.GetComponent<Motherbase>();
        if (other && other.CompareTag("Unit") && other.GetComponent<Unit>()._playerId != _playerId)
        {
            _target = other;
            collideNum++;
            Attack();
        }
        else if (mother && other.CompareTag("MotherBase") && mother._playerId != _playerId)
        {
            EndGameManager.instance.addDamage(_playerId, _damage);
            EndGameManager.instance.addDamage((_playerId % 2) + 1, _life);
            mother.getDamage(_damage);
            Hit(_life);
        }
    }

    public void getStun()
    {
        if (!isStunn)
        {
            isStunn = true;
            _navMeshAgent.SetDestination(transform.position);
            StartCoroutine(stunTime());
        }
    }

    IEnumerator stunTime()
    {
        yield return new WaitForSeconds(timeStun);
        isStunn = false;
        takeDestination();
    }

    public virtual void OnTriggerEnter(Collider parOther)
    {
        if (parOther.CompareTag("Unit") && parOther.GetComponent<Unit>()._playerId != _playerId && parOther.GetComponent<Unit>()._actualLane == _actualLane)
        {
            
            if (_trigger.IndexOf(parOther.gameObject) < 0)
            _trigger.Add(parOther.gameObject);
            if (!_target)
            {
                changeTarget();
                StartCoroutine(targetMove());
                _navMeshAgent.velocity /= 5;
            }
        }
    }

    public virtual void OnTriggerExit(Collider parOther)
    {
        
        _trigger.Remove(parOther.gameObject);
        if (_target == parOther.gameObject)
            changeTarget();

    }

    protected void changeTarget()
    {
        _trigger = _trigger.Where(trigger => trigger != null).ToList();

        if (_trigger.Count > 0)
        {
            _target = _trigger[0];
        }
        else
        {
            collideNum = 0;
            _target = null;
            takeDestination();
        }
    }

    IEnumerator targetMove()
    { 
        while (_trigger.Count>0)
        {
            if (_target == null)
                changeTarget();
            else
            {
                if(_navMeshAgent.enabled == true)
                    _navMeshAgent.SetDestination(_target.transform.position);
            }
            yield return new WaitForSeconds(0.5f);
        }
        changeTarget();
    }

    public virtual void Attack()
    {
        Debug.Log("Attack from" + gameObject.name);
        if (_target && attackReady && !isStunn)
        {
            attackReady = false;
            Unit unit = _target.GetComponent<Unit>();
            if (unit && unit._playerId != _playerId)
            {
                if (hitFX)
                    SoundManager.Instance.playSound(hitFX, 1);
                unit.Hit(_damage);
               // GameObject fxToDestroy = Instantiate(FxHitBlood, _target.transform.position, Quaternion.Euler(new Vector3(-50, 0, 0))) as GameObject;
                EndGameManager.instance.addDamage(_playerId, _damage);
            }
            StartCoroutine(reload());
        }
        else
        {
            
            if (!_target)
                applyBump(_target.transform.position, 0.1f, 2);
            
        }
    }

    public virtual IEnumerator reload()
    {
        _allAnims.Play("ATTACK");
        attackReady = false;
        yield return new WaitForSeconds(attackSpeed);
        attackReady = true;
        _allAnims.Play("RUN");
    }


    void takeDestination()
    {
        if (laneEnd)
        {
            if (_navMeshAgent.enabled)
                _navMeshAgent.SetDestination(_enemyMotherBase.transform.position);
        }
        else
        {
            if (_navMeshAgent.isActiveAndEnabled && _navMeshAgent.isOnNavMesh)
            {

                _navMeshAgent.SetDestination(waypointDest.pos);

            }
            else
            {
                StopAllCoroutines();
                //Destroy(this.gameObject);
            }
            
        }
    }

    public void applyBump(Vector3 from, float force, int useSmoother=0)
    {
        if (!isBumped)
        {
            Vector3 dir = transform.position - from;
            isBumped = true;
            force = force / bumpResist;
            if (useSmoother == 0)
                StartCoroutine(bump(dir * force));
            else
                StartCoroutine(bump(dir * force, useSmoother));
        }
        
    }

    IEnumerator bump(Vector3 distance, int localSmoother=0)
    {
        GetComponent<Collider>().enabled = false;
        if (localSmoother == 0)
            localSmoother = smoother;
        for (int i=0; i< localSmoother; i++)
        {
            transform.position = transform.position + (distance / localSmoother);
            yield return 0;
        }
        isBumped = false;
        GetComponent<Collider>().enabled = true;
    }
    

    
}
