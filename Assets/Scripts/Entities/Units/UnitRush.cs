using UnityEngine;
using System.Collections;

public class UnitRush : Unit {

    public float flyHeight = 5;
    float baseHeight;
    public bool isFlying;
    public bool lifeSteal = false;
    public bool bloodyRash = false;
    public int bloodyFactor = 1;

    [Range(1,100)]
    public int valueLifeSteal = 50;
    public bool rangedAttack = false;
    bool rangedReady = true;
    public int rangedDamage = 1;
    public GameObject dard;

    public bool stunAttack;
    bool stunAttackReady = true;

    override
    public void Start()
    {
        base.Start();
        baseHeight = _navMeshAgent.baseOffset;
        flyHeight += baseHeight;
        _navMeshAgent.baseOffset = flyHeight;
        isFlying = true;
        _distanceMinLane += flyHeight;
    }

    override
    public void FixedUpdate()
    {
        base.FixedUpdate();
        //if (isFlying && _target&& Vector3.Distance(_target.transform.position, transform.position) < flyHeight+4)
        //{
        //    isFlying = false;
        //    StartCoroutine(down());
        //}
    }

    public override void applyLevelUp()
    {
        if (rangedAttack)
        {
            dard.SetActive(true);
        }
        
    }

    override
    public void Update()
    {
        base.Update();

        if (_target && rangedAttack && rangedReady)
        {
            Debug.Log("Ranged attack");
            rangedReady = false;
            _target.GetComponent<Unit>().Hit(rangedDamage);
            //StartCoroutine(rangedCooldown());
        }
        if (_target && stunAttack && stunAttackReady)
        {
            Debug.Log("Ranged attack");
            stunAttackReady = false;
            _target.GetComponent<Unit>().getStun();
        }
    }

    override public void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (_target)
        {
            if (!_target.GetComponent<UnitJump>() && col.gameObject.GetComponent<UnitJump>())
            {
                _target = col.gameObject;
            }
            else if (_target.GetComponent<UnitTank>() && col.gameObject.GetComponent<UnitTank>())
            {
                _target = col.gameObject;
            }
        }
        

        if (isFlying && (_target || (col.tag == "MotherBase" && col.GetComponent<Motherbase>()._playerId != _playerId)))

        StartCoroutine(down());
    }

    protected override void changeTarget()
    {
        base.changeTarget();
        rangedReady = true;
        stunAttackReady = true;
    }

    override public void OnTriggerExit(Collider parOther)
    {
        base.OnTriggerExit(parOther);

        //if (_target == null && !isFlying&&_trigger.Count==0)
        //{
        //    isFlying = true;
        //    StartCoroutine(up());
        //}
    }

    void DoDamageTo(Unit other)
    {

        if (bloodyRash)
        {
            other.Hit((int)((((float)_damage * _lifeMax) / _life) / bloodyFactor));
        }
        else
        {
            other.Hit(_damage);
        }
        if (lifeSteal)
        {
            Debug.Log("Life steal " + _damage * (valueLifeSteal / (float)100));
            _life += (int)(_damage * (valueLifeSteal / (float)100));
            _life = Mathf.Min(_life, _lifeMax);
        }
    }

    public override void Attack()
    {

        if (_target && attackReady)
        {
            attackReady = false;
            Unit unit = _target.GetComponent<Unit>();
            if (unit && unit._playerId != _playerId)
            {
                if (hitFX)
                    SoundManager.Instance.playSound(hitFX, 1);
                DoDamageTo(unit);
                // GameObject fxToDestroy = Instantiate(FxHitBlood, _target.transform.position, Quaternion.Euler(new Vector3(-50, 0, 0))) as GameObject;
                EndGameManager.instance.addDamage(_playerId, _damage);
            }
            
            StartCoroutine(attacking());
        }
    }

    //IEnumerator up()
    //{
    //    float height = baseHeight;
    //    while (height < flyHeight && isFlying)
    //    {
    //        _life += _damage * (valueLifeSteal / 100);
    //        _life = Mathf.Min(_life, _lifeMax);
    //    }
    //    base.Attack();
    //}

    IEnumerator down()
    {    
        isFlying = false;
        float height = _navMeshAgent.baseOffset- baseHeight;
        while (height >= baseHeight)
        {
            height -= height/smoother;
            _navMeshAgent.baseOffset = height;
            yield return 0;
        }
        _navMeshAgent.baseOffset = baseHeight;
    }


}
