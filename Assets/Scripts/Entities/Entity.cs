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
    public GameObject _enemyMotherBase;
    public int _life = 2;
    public int _playerId = 0;

    [Header("Upgrades (Runtime Only)")]
    public Dictionary<UpgradeType, List<Upgrade>> _upgrades;

    // Use this for initialization
    public virtual void Start () {
        _upgrades = new Dictionary<UpgradeType, List<Upgrade>>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate() {

    }

    public void AddUpgrade(Upgrade parUpgrade, UpgradeType parTypeUpgrade)
    {
        if (parUpgrade != null)
        {
            GetUpgrades(parTypeUpgrade).Add(parUpgrade);
        }
    }

    public List<Upgrade> GetUpgrades(UpgradeType parTypeUpgrade)
    {
        if (!_upgrades.ContainsKey(parTypeUpgrade))
        {
            _upgrades.Add(parTypeUpgrade, new List<Upgrade>());
        }
        return _upgrades[parTypeUpgrade];
    }
}
