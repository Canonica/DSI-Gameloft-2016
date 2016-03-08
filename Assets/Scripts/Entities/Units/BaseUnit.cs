using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BaseUnit : Unit {
    

    public bool isRegeneratingCR;
    public int lifeRestored;

    // Use this for initialization
    override
    public void Start() {
        base.Start();
	}

	override
	public void FixedUpdate() {
        base.FixedUpdate();
	}

    public override void OnDeath()
    {
        base.OnDeath();
        List<Unit> gos = new List<Unit>(GameObject.FindObjectsOfType<Unit>().Where(unit => unit._playerId == _motherBase._playerId));
        Unit best = gos[0];
        for (int i = 1; i < gos.Count; i++)
        {
            if(Vector3.Distance(best.transform.position, transform.position) > Vector3.Distance(gos[i].transform.position, transform.position))
            {
                best = gos[i];
            }
        }

        best._life += lifeRestored;
    }

    public void Berzerker()
    {
        // change l'apparence de l'entité
        attackSpeed *= 0.5f;
    }
}
