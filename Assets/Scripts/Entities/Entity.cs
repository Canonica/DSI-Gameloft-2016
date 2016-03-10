using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
    [Header("Entity")]
    public GameObject _enemyMotherBase;
    public Motherbase _motherBase;
    public int _life;
    public int _playerId = 0;

    [Tweakable]
    public int _lifeMax;

    public virtual void Start ()
    {
        _life = _lifeMax;
    }

    // Update is called once per frame
    public virtual void FixedUpdate() {

    }

    public virtual void Update()
    {

    }

}
