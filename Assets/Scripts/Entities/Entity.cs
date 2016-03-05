using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

    public GameObject _enemyMotherBase;
    public int _life;
    public int _lifeMax;
    public int _playerId = 0;

    public List<Spell> _spells;
    public List<Upgrade> _upgrades;
        
    public virtual void Start ()
    {
        _upgrades = new List<Upgrade>(GetComponents<Upgrade>());
        _spells = new List<Spell>(GetComponents<Spell>());
        _life = _lifeMax;
    }

    // Update is called once per frame
    public virtual void FixedUpdate() {

    }

    public virtual void Update()
    {

    }

}
