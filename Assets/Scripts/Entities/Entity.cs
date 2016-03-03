using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

    public enum UpgradeType
    {
        //Mettre les catégories d'Upgrade ici
        TestUpgrade,
        TestUpgrade2,
        TestUpgrade4,
    }
    
    public enum SpellType
    {
        //Mettre les catégories d'Upgrade ici
        TestSpell,
        TestSpell2,
        TestSpell4,
    }

    public GameObject _enemyMotherBase;
    public int _life = 2;
    public int _playerId = 0;

    [Header("Upgrades (Runtime Only)")]
    public Dictionary<UpgradeType, List<Upgrade>> _upgrades;
    public Dictionary<SpellType, List<Spell>> _spells;

    // Use this for initialization
    public virtual void Start () {
        _upgrades = new Dictionary<UpgradeType, List<Upgrade>>();
        _spells = new Dictionary<SpellType, List<Spell>>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate() {

    }

    public void AddUpgrade(Upgrade parUpgrade, UpgradeType parUpgradeType)
    {
        if (parUpgrade != null)
        {
            GetUpgrades(parUpgradeType).Add(parUpgrade);
        }
    }

    public List<Upgrade> GetUpgrades(UpgradeType parUpgradeType)
    {
        if (!_upgrades.ContainsKey(parUpgradeType))
        {
            _upgrades.Add(parUpgradeType, new List<Upgrade>());
        }
        return _upgrades[parUpgradeType];
    }

    public void AddSpell(Spell parSpell, SpellType parSpellType)
    {
        if (parSpell != null)
        {
            GetSpells(parSpellType).Add(parSpell);
        }
    }

    public List<Spell> GetSpells(SpellType parSpellType)
    {
        if (!_spells.ContainsKey(parSpellType))
        {
            _spells.Add(parSpellType, new List<Spell>());
        }
        return _spells[parSpellType];
    }
}
