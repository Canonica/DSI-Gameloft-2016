using UnityEngine;
using System.Collections;

public class UnitJump : Unit {

    public bool isJumping = false;
    bool isActiveAOE = false;
    public int forceAOE = 2;
    float timeAOE = 2;
    int heightJump = 5;
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
        if (isActiveAOE&&_target)
        {
            
        }
    }

    override public void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (_target && !isJumping)
        {
            isJumping = true;
            StartCoroutine(jump());
        }
    }

    IEnumerator jump()
    {
        _navMeshAgent.enabled = false;
        Vector3 dir = _target.transform.position - transform.position;
        float dist = Vector3.Distance(_target.transform.position, transform.position);
        Vector3 dirJump = (dir.normalized * dist) / 2;
        dirJump.y += heightJump;

        for (int i = 0; i < smoother ; i++)
        {
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
        isActiveAOE = true;
        _navMeshAgent.enabled = true;
        StartCoroutine(AOE());
    }

    IEnumerator AOE()
    {
        for (int i=0; i < _trigger.Count;i++)
        {
            if (_trigger[i])
            {
                _trigger[i].GetComponent<Unit>().applyBump(transform.position, forceAOE);
            }
            yield return 0;
        }
        isActiveAOE = false;
    }
}
