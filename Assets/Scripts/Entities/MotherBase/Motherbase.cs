using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
    Vector3 cameraPos;
    public int[] maxNbOfUnits;
    public int[] currentNbOfUnits;


    public Text[] textCurrentNbOfUnits;
    public Image[] reloadUnitImage;
    public Image _lifeImage;

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
        _lifeMax = 10;
        spawning = false;
    }


    // Update is called once per frame


    public override void Start()
    {
        base.Start();
        cameraPos = Camera.main.transform.position;
    }

    void Update()
    {
        if (GameManager.instance.currentGamestate == GameManager.gameState.Playing)
        {
            Debug.Log("update");
            if (!spawning)
            {
                StartCoroutine(loadUnit(0));
                StartCoroutine(loadUnit(1));
                //StartCoroutine(loadUnit(2));
                //StartCoroutine(loadUnit(3));
                spawning = true;
            }

            if (Input.GetButtonDown("Fire " + _playerId))
            {
                Debug.Log("je passe");
                typeOfUnit = 0;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("B_button_" + _playerId))
            {
                typeOfUnit = 1;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("X_button_" + _playerId))
            {
                typeOfUnit = 2;
                corSpawnUnits(typeOfUnit);
            }

            if (Input.GetButtonDown("Y_button_" + _playerId))
            {
                typeOfUnit = 3;
                corSpawnUnits(typeOfUnit);
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

            if ((Input.GetButtonDown("TriggersR_" + _playerId) || Input.GetKey(KeyCode.T)))
            {
                //Actualiser le compteur de temps si cooldown
                //Afficher le spell 2 dans l'UI
            }
            else
            {
                //Masquer le spell 2 dans l'UI
            }



            textCurrentNbOfUnits[0].text = currentNbOfUnits[0] + "/" + maxNbOfUnits[0];
        }

        _lifeImage.fillAmount = (float)((float)_life / (float)_lifeMax);
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
        if (Input.GetKey(KeyCode.S) && _playerId == 2)
        {
            corSpawnUnits(0);
        }
        if (Input.GetKey(KeyCode.D)&& _playerId ==1)
        {
            currentNbOfUnits[1] = 50;
            corSpawnUnits(1);
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
        XInput.instance.useVibe(_playerId, 1, 1, 1);
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
            
            for (int i = 0; i < units[typeOfUnit].GetComponent<Unit>().groupSpawn; i++)
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
            }
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
                yield return StartCoroutine(fillIcon(reloadUnitImage[nbOfUnits], units[nbOfUnits].GetComponent<Unit>()._hatchTime));
            }
            yield return 0;
        }
    }

    public IEnumerator fillIcon(Image icon, float cdTimer)
    {
        float timer = 0;
        while (timer <= cdTimer)
        {
            icon.fillAmount = timer / cdTimer;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        timer = 0;
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
