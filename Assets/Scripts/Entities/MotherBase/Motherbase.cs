using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Motherbase : Entity
{
    // Utilisera les Upgrades
    // Possèdera les Spells

    [Header("Spawner Option")]
    public GameObject[] units;
    public float delay;
    public Waypoint waypoint;
    public GameObject targetBase;
    bool spawning;
    int typeOfUnit;

    int setNb;

    public int[] maxNbOfUnits;
    public int[] currentNbOfUnits;

    public Spell primarySpell;
    public Spell secondarySpell;

     float cooldownPrimarySpell;
     float cooldownSecondarySpell;

     Coroutine primarySpellCd;
     Coroutine secondarySpellCd;

    public List<Spell> primarySpells;
    public List<Spell> secondarySpells;


    // Use this for initialization
    void Awake()
    {
        _life = 10;
        spawning = false;
    }


    // Update is called once per frame


    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate()
    {

        //if (Input.GetButtonDown("RB_button_" + _playerId))
        //{
        //    if(setNb>(units.Length)/4)
        //    {
        //        setNb--;
        //    }
        //    else
        //    {
        //        setNb++;
        //    }
        //}
        //if (Input.GetButtonDown("LB_button_" + _playerId))
        //{
        //    if (setNb > 0)
        //    {
        //        setNb--;
        //    }
        //}
        // DEBUG
        if (Input.GetKey(KeyCode.S))
        {
            currentNbOfUnits[0] = 50;
            corSpawnUnits(0);
        }

        if (GameManager.instance.currentGamestate == GameManager.gameState.Playing)
        {
            if (!spawning)
            {
                StartCoroutine(loadUnit(0));
                StartCoroutine(loadUnit(1));
                //StartCoroutine(loadUnit(2));
                //StartCoroutine(loadUnit(3));
                spawning = true;
            }

            if (Input.GetButtonDown("RB_button_" + _playerId))
            {
                if (setNb > (units.Length) / 4)
                {
                    setNb--;
                }
                else
                {
                    setNb++;
                }
            }
            if (Input.GetButtonDown("LB_button_" + _playerId))
            {
                if (setNb > 0)
                {
                    setNb--;
                }
            }

            if ((Input.GetButtonDown("TriggersL_" + _playerId) || Input.GetKey(KeyCode.R)))
            {
                //Actualiser le compteur de temps si cooldown
                //Afficher le spell 1 dans l'UI
            }
            else
            {
                //Masquer le spell 1 dans l'UI
            }

            if ((Input.GetButtonDown("TriggersR_" + _playerId) || Input.GetKey(KeyCode.R)))
            {
                //Actualiser le compteur de temps si cooldown
                //Afficher le spell 2 dans l'UI
            }
            else
            {
                //Masquer le spell 2 dans l'UI
            }

            if (Input.GetButtonDown("Fire " + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                Debug.Log("Test");
                typeOfUnit = 0;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("B_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                typeOfUnit = 1;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("X_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                typeOfUnit = 2;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("Y_button_" + _playerId) && GameManager.instance.currentGamestate == GameManager.gameState.Playing)
            {
                typeOfUnit = 3;
                corSpawnUnits(typeOfUnit);
            }


        }
        base.FixedUpdate();
    }
    //IEnumerator Spawner()
    //{
    //    while (_life > 0)
    //    {
    //        corSpawnUnits(typeOfUnit);
    //        yield return new WaitForSeconds(units[typeOfUnit].GetComponent<Unit>()._hatchTime);
    //    }
    //}


    //void spawnUnits(int index)
    //{
    //    GameObject obj = Instantiate(units[index], transform.position, transform.rotation) as GameObject;
    //    obj.GetComponent<Unit>()._playerId = idPlayer;
    //    obj.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].transform.position);
    //    obj.GetComponent<Unit>()._enemyMotherBase = targetBase;
    //    obj.GetComponent<Unit>().waypointDest = waypoints[2].transform.position;
    //    obj.transform.parent = transform;
    //}

    public void getDamage(int dmg)
    {
        if (dmg > _life)
        {
            _life = 0;
            EndGameManager.instance.motherBaseDead(_playerId);
        }
        else
        {
            if (dmg > 0)
                _life -= dmg;
        }
    }

    void corSpawnUnits(int typeOfUnit)
    {
        if (currentNbOfUnits[typeOfUnit] > 0)
        {
            if ((Input.GetButtonDown("TriggersL_" + _playerId) || Input.GetKey(KeyCode.R)) && primarySpellCd == null)
            {
                primarySpellCd = StartCoroutine(corCooldownSpell(primarySpell));
                cooldownPrimarySpell = Time.time;
            }
            else if (primarySpellCd != null)
            {
                Debug.Log("Recharge spell 1");
            }
            if ((Input.GetButtonDown("TriggersR_" + _playerId) || Input.GetKey(KeyCode.T)) && secondarySpellCd == null)
            {
                secondarySpellCd = StartCoroutine(corCooldownSpell(secondarySpell));
                cooldownSecondarySpell = Time.time;
            }
            else if (secondarySpellCd != null)
            {
                Debug.Log("Recharge spell 2");
            }
            GameObject prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
            Unit unit = prefabOfUnit.GetComponent<Unit>();
            NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent>();
            unit._playerId = _playerId;
            nav.SetDestination(waypoint.pos);
            unit._enemyMotherBase = targetBase;
            unit.waypointDest = waypoint;
            prefabOfUnit.transform.parent = transform;
            currentNbOfUnits[typeOfUnit]--;
        }
    }

    IEnumerator loadUnit(int nbOfUnits)
    {
        while (_life > 0)
        {
            if (currentNbOfUnits[nbOfUnits] < maxNbOfUnits[nbOfUnits])
            {
                currentNbOfUnits[nbOfUnits]++;
            }
            yield return new WaitForSeconds(units[nbOfUnits].GetComponent<Unit>()._hatchTime);
        }


    }

    IEnumerator corCooldownSpell(Spell spellToRecharge)
    {
        yield return new WaitForSeconds(spellToRecharge._cost);
        rechargeSpell(spellToRecharge);
    }

    void rechargeSpell(Spell spellToRecharge)
    {
        if(spellToRecharge == primarySpell && primarySpells.Count > 0)
        {
            int rand = Random.Range(0, primarySpells.Count);
            spellToRecharge = primarySpells[rand];
        }
        else if (spellToRecharge == secondarySpell && secondarySpells.Count > 0)
        {
            int rand = Random.Range(0, secondarySpells.Count);
            spellToRecharge = primarySpells[rand];
        }
    }

}
