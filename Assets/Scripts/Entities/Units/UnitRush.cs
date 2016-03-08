using UnityEngine;
using System.Collections;

public class UnitRush : Unit {

    public float flyHeight = 5;
    float baseHeight;
    public bool isFlying;
    bool lifeSteal = false;

    [Range(1,100)]
    public int valueLifeSteal = 50;
    bool rangedAttack = false;
    bool rangedReady = true;
    public float rangedAttackSpeed= 1;
    public int rangedDamage = 1;

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

    override
    public void Update()
    {
        base.Update();

        if (_target && rangedAttack && rangedReady)
        {
            Debug.Log("Ranged attack");
            rangedReady = false;
            _target.GetComponent<Unit>().Hit(rangedDamage);
            StartCoroutine(rangedCooldown());

        }
    }

    IEnumerator rangedCooldown()
    {
        yield return new WaitForSeconds(rangedAttackSpeed);
        rangedReady = true;
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

    override public void OnTriggerExit(Collider parOther)
    {
        base.OnTriggerExit(parOther);

        //if (_target == null && !isFlying&&_trigger.Count==0)
        //{
        //    isFlying = true;
        //    StartCoroutine(up());
        //}
    }

    public override void Attack()
    {
        
        if (_target && lifeSteal&&attackReady)
        {
            _life += _damage * (valueLifeSteal / 100);
            _life = Mathf.Min(_life, _lifeMax);
        }
        base.Attack();
    }

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
