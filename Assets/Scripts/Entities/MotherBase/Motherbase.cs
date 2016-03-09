using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Motherbase : Entity
{

    [Header("MotherBase - Spawner Option")]
    public GameObject[] units;
    [HideInInspector]
    public Waypoint waypoint;
    public GameObject targetBase;
    bool spawning;
    int typeOfUnit;
    [HideInInspector]
    public ChangeLane _currentLane;
    [HideInInspector]
    public int _laneSpawning;
    int setNb;

    [Header("Mana Option")]

    public int _maxMana;
    public int _currentMana;
    public int experienceByMana = 1;
    public float _delayMana;
    public int _addMana;
    public int _manaToSacrifice;
    bool _canSacrificeMana;

    [Header("Editor Option")]
    public Image _manaImage;
    public Text _manaText;

    public Text[] textCurrentNbOfUnits;
    public Image[] reloadUnitImage;
    public Image _lifeImage;

    [Header("Spell Option")]
    public Spell primarySpell;
    public Spell secondarySpell;

    float cooldownPrimarySpell;
    float cooldownSecondarySpell;

    Coroutine primarySpellCd;
    Coroutine secondarySpellCd;

    public List<Spell> primarySpells;
    public List<Spell> secondarySpells;
    [Header("Upgrades Option")]
    public List<Upgrade> upgrades;
    public float upgradeDelay = 0.3f;
    public float lastUpgrade = 0.0f;

    public List<int> experienceLevel;
    public List<int> maxExperienceLevel;

    public List<bool> hasUsedLevel;

    public List<int> upgradeNumber;

    [Header("FX")]
    [SerializeField]
    private GameObject FxBlood;

    [Header("Sound")]
    public AudioClip spawnSwarmFX;

   

    // Use this for initialization
    void Awake()
    {
        spawning = false;
    }


    // Update is called once per frame


    public override void Start()
    {
        //cameraPos = Camera.main.transform.position;
        _currentMana = 0;
        _canSacrificeMana = true;
        _currentLane = GetComponent<ChangeLane>();
        experienceLevel = new List<int>();
        for (int i = 0; i < maxExperienceLevel.Count; i++)
        {
            experienceLevel.Add(maxExperienceLevel[i]);
        }
        hasUsedLevel = new List<bool>();
        for (int i = 0; i < experienceLevel.Count; i++)
        {
            hasUsedLevel.Add(false);
        }
        base.Start();
    }

    public override void Update()
    {

        _laneSpawning = _currentLane.currentWP;
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
        if (Input.GetKeyDown(KeyCode.Z) && _playerId == 1)
        {
            corSpawnUnits(0);
        }
        if (Input.GetKeyDown(KeyCode.E) && _playerId == 2)
        {
            corSpawnUnits(0);
        }
        if (Input.GetKeyDown(KeyCode.D) && _playerId == 2)
        {
            corSpawnUnits(2);
        }
        if (Input.GetKeyDown(KeyCode.S) && _playerId == 1)
        {
            corSpawnUnits(2);
        }

        if (GameManager.instance.currentGamestate == GameManager.gameState.Playing)
        {
            if (!spawning)
            {
                StartCoroutine(loadMana());
                spawning = true;
            }

            if (Input.GetButtonDown("Fire " + _playerId))
            {

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



            if (Input.GetAxis("TriggersR_" + _playerId) > 0.3)
            {
                if (_manaToSacrifice <= _currentMana && _canSacrificeMana)
                {
                    _currentMana -= _manaToSacrifice;
                    _canSacrificeMana = false;
                    AddExperience(_manaToSacrifice*experienceByMana);
                    StartCoroutine(delaySacrifice());
                }
                else
                {
                    // can't add xp;
                }
            }
            if (Input.GetAxis("TriggersR_" + _playerId) == 0)
            {
                _canSacrificeMana = true;
            }

            //if ((Input.GetButtonDown("TriggersL_" + _playerId) || Input.GetKey(KeyCode.R)))
            //{
            //    Actualiser le compteur de temps si cooldown
            //    Afficher le spell 1 dans l'UI
            //}
            //else
            //{
            //    Masquer le spell 1 dans l'UI
            //}

            //if ((Input.GetButtonDown("TriggersR_" + _playerId) || Input.GetKey(KeyCode.T)))
            //{
            //    Actualiser le compteur de temps si cooldown
            //    Afficher le spell 2 dans l'UI
            //}
            //else
            //{
            //    Masquer le spell 2 dans l'UI
            //}

            //textCurrentNbOfUnits[0].text = currentNbOfUnits[0] + "/" + maxNbOfUnits[0];
            //textCurrentNbOfUnits[1].text = currentNbOfUnits[1] + "/" + maxNbOfUnits[1];
            //textCurrentNbOfUnits[2].text = currentNbOfUnits[2] + "/" + maxNbOfUnits[2];
            //textCurrentNbOfUnits[3].text = currentNbOfUnits[3] + "/" + maxNbOfUnits[3];
        }

        _lifeImage.fillAmount = (float)((float)_life / (float)_lifeMax);
        _manaImage.fillAmount = ((float)_currentMana / (float)_maxMana);
        _manaText.text = _currentMana + "/" + _maxMana;
        base.Update();
    }

    IEnumerator delaySacrifice()
    {
        yield return new WaitForSeconds(0.3f);
        _canSacrificeMana = true;
    }

    public override void FixedUpdate()
    {
        int levelDispo = 0;

        for (int i = 0; i < experienceLevel.Count; i++)
        {
            if (experienceLevel[i] == 0)
            {
                levelDispo++;
            }
        }
        for (int i = 0; i < hasUsedLevel.Count; i++)
        {
            if (hasUsedLevel[i])
            {
                levelDispo--;
            }
        }

        if(levelDispo > 0)
        {
            if(upgradeNumber == null || upgradeNumber.Count == 0)
            {
                upgradeNumber = new List<int>();
                for (int i = 0; i < upgrades.Count; i++)
                {
                    upgradeNumber.Add(upgrades[i].PreLevelUp());
                }
            }
            else
            {
                float upgradeH = Input.GetAxis("DPad_XAxis_" + _playerId);
                float upgradeV = Input.GetAxis("DPad_YAxis_" + _playerId);

                if (lastUpgrade + upgradeDelay < Time.time)
                {
                    if (upgradeH > 0.3) // RIGHT
                    {
                        UseLevel(1, upgradeNumber[1]);
                    }
                    else if (upgradeH < -0.3) // LEFT
                    {
                        UseLevel(2, upgradeNumber[2]);
                    }

                    if (upgradeV > 0.3) // UP
                    {
                        UseLevel(3, upgradeNumber[3]);
                    }
                    else if (upgradeV < -0.3) // DOWN
                    {
                        UseLevel(0, upgradeNumber[0]);
                    }
                }

                
            }
            
        }

        base.FixedUpdate();
    }

    public void getDamage(int dmg)
    {
        Instantiate(FxBlood, transform.position, Quaternion.Euler(new Vector3(-50, 0, 0)));
        XInput.instance.useVibe(_playerId-1 , 1, 0.5f, 0.5f);
        

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
        if (typeOfUnit == 0)
            if (spawnSwarmFX)
                SoundManager.Instance.playSound(spawnSwarmFX, 0.3f);
        int unitToSpawn = units[typeOfUnit].GetComponent<Unit>().groupSpawn;
        EndGameManager.instance.addSpawn(_playerId, unitToSpawn);

        //bool isActiveSpellPrimary = false;
        //bool isActiveSpellSecondary = false;
        //if ((Input.GetButtonDown("TriggersL_" + _playerId) || Input.GetKey(KeyCode.R)) && primarySpellCd == null)
        //{
        //    primarySpellCd = StartCoroutine(corCooldownSpell(primarySpell));
        //    cooldownPrimarySpell = Time.time;
        //    isActiveSpellPrimary = true;
        //}
        //else if (primarySpellCd != null)
        //{
        //    Debug.Log("Recharge spell 1");
        //}
        //if ((Input.GetButtonDown("TriggersR_" + _playerId) || Input.GetKey(KeyCode.T)) && secondarySpellCd == null)
        //{
        //    secondarySpellCd = StartCoroutine(corCooldownSpell(secondarySpell));
        //    cooldownSecondarySpell = Time.time;
        //    isActiveSpellSecondary = true;
        //}
        //else if (secondarySpellCd != null)
        //{
        //    Debug.Log("Recharge spell 2");
        //}
        if (_currentMana >= units[typeOfUnit].GetComponent<Unit>().manaCost)
        {
            for (int i = 0; i < unitToSpawn; i++)
            {
                GameObject prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
                Unit unit = prefabOfUnit.GetComponent<Unit>();
                NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent>();


                //if (isActiveSpellPrimary)
                //{
                //    switch (primarySpell._name)
                //    {
                //        case "BuffAtk":
                //            unit._damage += (int)primarySpell._value;
                //            break;
                //        default:
                //            break;
                //    }
                //}
                //if (isActiveSpellSecondary)
                //{
                //    switch (primarySpell._name)
                //    {
                //        case "BuffAtk":
                //            unit._damage += (int)primarySpell._value;
                //            break;
                //        default:
                //            break;
                //    }
                //}
                upgrades[typeOfUnit].Use(unit);
                unit._playerId = _playerId;
                unit._motherBase = this;
                if (waypoint == null)
                {
                    nav.SetDestination(targetBase.transform.position);
                    unit.laneEnd = true;
                }
                else
                {
                    nav.SetDestination(waypoint.pos);
                }
                    
                unit._enemyMotherBase = targetBase;
                unit.waypointDest = waypoint;
                unit._actualLane = 0;
                prefabOfUnit.transform.parent = transform.parent;
            }
            _currentMana -= units[typeOfUnit].GetComponent<Unit>().manaCost;
        }
    }

    private void AddExperience(int experience)
    {
        int exp = experience;
        int level = 0;
        do
        {
            int expCurr = Mathf.Max(experienceLevel[level] - exp, 0);
            exp = Mathf.Max(exp - experienceLevel[level], 0);
            experienceLevel[level] = expCurr;
            level++;
        }
        while (exp > 0 && level < experienceLevel.Count);
    }

    private void UseLevel(int indexUpgrade, int indexLevel)
    {
        if(indexLevel > 0)
        {
            upgrades[indexUpgrade].LevelUp(indexLevel);
            upgradeNumber.Clear();
            upgradeNumber = null;
            int level = 0;
            while (hasUsedLevel[level])
            {
                level++;
            }
            hasUsedLevel[level] = true;
            lastUpgrade = Time.time;
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

    //IEnumerator corCooldownSpell(Spell spellToRecharge)
    //{
    //    yield return new WaitForSeconds(spellToRecharge._cost);
    //    rechargeSpell(spellToRecharge);
    //}

    IEnumerator loadMana()
    {
        while (_life>0)
        {
            if(_currentMana < _maxMana)
            {
                _currentMana = Mathf.Min(_currentMana + _addMana, _maxMana);
            }
            yield return new WaitForSeconds(_delayMana);
        }

    }

    //void rechargeSpell(Spell spellToRecharge)
    //{
    //    if (spellToRecharge == primarySpell && primarySpells.Count > 0)
    //    {
    //        int rand = Random.Range(0, primarySpells.Count);
    //        spellToRecharge = primarySpells[rand];
    //    }
    //    else if (spellToRecharge == secondarySpell && secondarySpells.Count > 0)
    //    {
    //        int rand = Random.Range(0, secondarySpells.Count);
    //        spellToRecharge = primarySpells[rand];
    //    }
    //}

}
