using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class BaseUnit : Unit {

    public float distanceMaxBetweenCockroachHeal = 3.0f;

    public ParticleSystem PS_Heal;

    /*
        Increases the number of cockroach spawned (to 4/5)
        When a cockroach dies, it heals a nearby cockroach
        All new cockroaches are now improved, with increased stats and new visuals (berserk)
    */

    public bool isRegeneratingCR;
    public int lifeRestored;

    public GameObject berserkerMesh;
    public GameObject baseMesh;

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
        BaseUnit[] baseU = GameObject.FindObjectsOfType<BaseUnit>();
        if(baseU.Length > 0)
        {
            List<BaseUnit> gos = new List<BaseUnit>(baseU.Where(unit => unit._playerId == _playerId));
            if(gos.Count > 0)
            {
                BaseUnit best = gos[0];
                for (int i = 1; i < gos.Count; i++)
                {
                    if(Vector3.Distance(best.transform.position, transform.position) > Vector3.Distance(gos[i].transform.position, transform.position))
                    {
                        best = gos[i];
                    }
                }
                if(Vector3.Distance(best.transform.position, transform.position) < distanceMaxBetweenCockroachHeal)
                {
                    best.PS_Heal.Play(true);
                    best._life += lifeRestored;
                }
            }
        }
    }

    public override void Attack()
    {
        SoundManager.Instance.swarmSound();
        base.Attack();
    }

    public void Berzerker()
    {
        // change l'apparence de l'entité
        attackSpeed *= 1.5f;
        baseMesh.SetActive(false);
        berserkerMesh.SetActive(true);
    }
}
