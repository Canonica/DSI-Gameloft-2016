using UnityEngine;
using System.Collections;

public class UnitTank : Unit {

    public float bumpForce;
    public float zoneBump;
    bool stayPos = false;
    Vector3 pos;
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


    override public void Attack()
    {
        
        if (_target&&attackReady)
        {
            attackReady = false;
            for (int i = 0; i < _trigger.Count; i++)
            {
                //float dist = Vector3.Distance(transform.position, _trigger[i].transform.position);
                //Debug.Log(dist);
                _trigger[i].GetComponent<Unit>().applyBump(transform.position, bumpForce);
                
            }
            //_target.GetComponent<Unit>().applyBump(transform.position, bumpForce);
        }
        else
        {
            changeTarget();
            return;
        }

        StartCoroutine(reload());
    }
}
