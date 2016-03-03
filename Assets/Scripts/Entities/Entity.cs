using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

    public GameObject _enemyMotherBase;
    public int _life;
    public int _lifeMax = 2;
    public int _playerId = 0;

    /*public Dictionary<UpgradeType, List<Upgrade>> _upgrades;
    public Dictionary<SpellType, List<Spell>> _spells;*/

    public List<Spell> _spells;
    public List<Upgrade> _upgrades;
        
    public virtual void Start ()
    {
        /*_upgrades = new Dictionary<UpgradeType, List<Upgrade>>();
        _spells = new Dictionary<SpellType, List<Spell>>();*/
        _upgrades = new List<Upgrade>(GetComponents<Upgrade>());
        _spells = new List<Spell>(GetComponents<Spell>());
        _life = _lifeMax;
    }

    // Update is called once per frame
    public virtual void FixedUpdate() {

    }

    public void AddUpgrade(Upgrade parUpgrade, Upgrade.UpgradeType parUpgradeType)
    {
        if (parUpgrade != null)
        {
            GetUpgrades(parUpgradeType).Add(parUpgrade);
        }
    }

    public List<Upgrade> GetUpgrades(Upgrade.UpgradeType parUpgradeType)
    {
        switch (parUpgradeType)
        {
            case Upgrade.UpgradeType.TestUpgrade :
                //Do sth
        	break;
            default:
                return _upgrades;
        }
        return null;
    }

    public void AddSpell(Spell parSpell, Spell.SpellType parSpellType)
    {
        if (parSpell != null)
        {
            GetSpells(parSpellType).Add(parSpell);
        }
    }

    public List<Spell> GetSpells(Spell.SpellType parSpellType)
    {

        switch (parSpellType)
        {
            case Spell.SpellType.TestSpell:
                //Do sth
                break;
            default:
                return _spells;
        }
        return null;
    }
}
