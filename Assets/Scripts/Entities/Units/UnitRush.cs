using UnityEngine;
using System.Collections;

public class UnitRush : Unit {

    public float flyHeight = 5;
    float baseHeight;
    public bool isFlying;
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

    override public void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
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

    IEnumerator up()
    {
        float height = baseHeight;
        while (height < flyHeight &&isFlying)
        {
            height += height / smoother;
            _navMeshAgent.baseOffset = height;
            yield return 0;
        }
        _navMeshAgent.baseOffset = flyHeight;
    }

    IEnumerator down()
    {
        isFlying = false;
        float height = _navMeshAgent.baseOffset;
        while (height >= baseHeight)
        {
            height -= height/smoother;
            _navMeshAgent.baseOffset = height;
            yield return 0;
        }
        
        _navMeshAgent.baseOffset = baseHeight;
    }
}
