using UnityEngine;
using System.Collections;

public class UnitTank : Unit {

    public float bumpForce;
    public float zoneBump;
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

    override public void Attack()
    {
        if (_target)
        {

            for (int i = 0; i < _trigger.Count; i++)
            {
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
}
