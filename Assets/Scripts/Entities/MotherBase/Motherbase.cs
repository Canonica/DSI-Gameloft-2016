﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using XInputDotNetPure;

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

    public GameObject ManaCostCanvas;
    public GameObject ManaText;
    public GameObject ManaFull;

    [Header("Editor Option")]
    public Image _manaImage;
    public Text _manaText;

    public Text[] textCurrentNbOfUnits;
    public Image[] reloadUnitImage;
    public Image _lifeImage;

    [Header("Upgrades Option")]
    public List<Upgrade> upgrades;
    public float upgradeDelay = 0.3f;
    public float lastUpgrade = 0.0f;

    public CanvasGroup _arrowImages;
    public CanvasGroup[] _arrowArray;

    public List<int> experienceLevel;
    public List<int> maxExperienceLevel;

    public List<bool> hasUsedLevel;

    public List<int> upgradeNumber;

    [Header("FX")]
    [SerializeField]
    private GameObject FxBlood;
    [Header("Sound")]
    public AudioClip spawnSwarmFX;

    bool buttonPressedA = false, buttonPressedB = false, buttonPressedX = false, buttonPressedY = false;

    // Use this for initialization
    void Awake()
    {
        spawning = false;
    }


    // Update is called once per frame


    public override void Start()
    {
        //cameraPos = Camera.main.transform.position;
        //_currentMana = 500;
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
        _arrowImages.alpha = 1;
        for (int i = 0; i < _arrowArray.Length; i++)
        {
            _arrowArray[i].alpha = 0;
        }
        base.Start();

        ManaText = GameObject.Find("Mana_Text" + _playerId);
        ManaFull = GameObject.Find("Mana_Full" + _playerId);
    }

    public override void Update()
    {


        _laneSpawning = _currentLane.currentWP;
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


            //if (Input.GetButtonDown("Fire " + _playerId))
            if (XInput.instance.getButton(_playerId, 'A') == ButtonState.Pressed && !buttonPressedA)
            {
                buttonPressedA = true;
                typeOfUnit = 0;
                corSpawnUnits(typeOfUnit);
            }
            else if (XInput.instance.getButton(_playerId, 'A') == ButtonState.Released && buttonPressedA)
            {
                buttonPressedA = false;
            }

            if (XInput.instance.getButton(_playerId, 'B') == ButtonState.Pressed && !buttonPressedB)
            {
                typeOfUnit = 1;
                buttonPressedB = true;
                corSpawnUnits(typeOfUnit);
            }
            else if (XInput.instance.getButton(_playerId, 'B') == ButtonState.Released && buttonPressedB)
            {
                buttonPressedB = false;
            }


            if (XInput.instance.getButton(_playerId, 'X') == ButtonState.Pressed && !buttonPressedX)
            {//if (Input.GetButtonDown("X_button_" + _playerId))
                typeOfUnit = 2;
                buttonPressedX = true;
                corSpawnUnits(typeOfUnit);
            }
            else if (XInput.instance.getButton(_playerId, 'X') == ButtonState.Released && buttonPressedX)
            {
                buttonPressedX = false;
            }

            //if (Input.GetButtonDown("Y_button_" + _playerId))
            if (XInput.instance.getButton(_playerId, 'Y') == ButtonState.Pressed && !buttonPressedY)
            {
                typeOfUnit = 3;
                buttonPressedY = true;
                corSpawnUnits(typeOfUnit);
            }
            else if (XInput.instance.getButton(_playerId, 'Y') == ButtonState.Released && buttonPressedY)
            {
                buttonPressedY = false;
            }

            //if (Input.GetAxis("TriggersR_" + _playerId) > 0.3)
            if (XInput.instance.getTrigger(_playerId) > 0.3)
            {

                if (_manaToSacrifice <= _currentMana && _canSacrificeMana)
                {
                    Debug.Log("add mana" + _playerId);
                    _currentMana -= _manaToSacrifice;
                    _canSacrificeMana = false;
                    AddExperience(_manaToSacrifice * experienceByMana);
                    StartCoroutine(delaySacrifice());
                }
                else {
                    // can't add xp;
                }
            }

            if (XInput.instance.getTrigger(_playerId) == 0)
            {
                _canSacrificeMana = true;
            }
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

        if (levelDispo > 0)
        {
            //_arrowImages.DOFade(1, 0.5f);

            if (upgradeNumber == null || upgradeNumber.Count == 0)
            {
                upgradeNumber = new List<int>();
                for (int i = 0; i < upgrades.Count; i++)
                {
                    upgradeNumber.Add(upgrades[i].PreLevelUp());
                    if (upgradeNumber[i] != -1)
                    {
                        _arrowArray[i].DOFade(1, 0.5f);
                    }
                    else {
                        _arrowArray[i].alpha = 0;
                    }
                }

            }
            else {
                //float upgradeH = Input.GetAxis("DPad_XAxis_" + _playerId);
                //float upgradeV = Input.GetAxis("DPad_YAxis_" + _playerId);

                //if (lastUpgrade + upgradeDelay < Time.time)
                //{
                //    if (upgradeH > 0.3) // RIGHT
                //    {
                //        UseLevel(1, upgradeNumber[1]);
                //    }
                //    else if (upgradeH < -0.3) // LEFT
                //    {
                //        UseLevel(2, upgradeNumber[2]);
                //    }

                //    if (upgradeV > 0.3) // UP
                //    {
                //        UseLevel(3, upgradeNumber[3]);
                //    }
                //    else if (upgradeV < -0.3) // DOWN
                //    {
                //        UseLevel(0, upgradeNumber[0]);
                //    }
                //}

                if (lastUpgrade + upgradeDelay < Time.time)
                {

                    if (XInput.instance.getDPad(_playerId, 'R') == ButtonState.Pressed)
                    { // RIGHT
                        UseLevel(1, upgradeNumber[1]);
                    }
                    else if (XInput.instance.getDPad(_playerId, 'L') == ButtonState.Pressed)
                    { // LEFT
                        UseLevel(2, upgradeNumber[2]);
                    }
                    else if (XInput.instance.getDPad(_playerId, 'U') == ButtonState.Pressed)
                    { // UP
                        UseLevel(3, upgradeNumber[3]);
                    }
                    else if (XInput.instance.getDPad(_playerId, 'D') == ButtonState.Pressed)
                    { // DOWN
                        Debug.Log(upgradeNumber);
                        UseLevel(0, upgradeNumber[0]);
                    }
                }


            }

        }
        else {
            for (int i = 0; i < _arrowArray.Length; i++)
            {
                _arrowArray[i].DOFade(0, 0.5f);
            }
            //_arrowImages.DOFade(0, 0.5f);
        }
        base.FixedUpdate();
    }

    public void getDamage(int dmg)
    {
        Instantiate(FxBlood, transform.position, Quaternion.Euler(new Vector3(-50, 0, 0)));
        XInput.instance.useVibe(_playerId - 1, 0.5f, 0.5f, 0.5f);

        Camera.main.DOKill(true);
        Camera.main.DOShakePosition(0.5f, 1, 1, 20);

        if (dmg > _life)
        {
            _life = 0;
            EndGameManager.instance.motherBaseDead(_playerId);
        }
        else {
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
        
        if (_currentMana >= units[typeOfUnit].GetComponent<Unit>().manaCost)
        {

            //			units [typeOfUnit].GetComponent<Unit> ().manaCost
            Debug.Log(Mathf.Sign(transform.position.x - _enemyMotherBase.transform.position.x));
            GameObject myManaCanvas = Instantiate(ManaCostCanvas, transform.position + new Vector3(Random.Range(-10f, 10f), 0, 5 * Mathf.Sign(transform.position.x - _enemyMotherBase.transform.position.x)), Quaternion.Euler(0, -90, 0)) as GameObject;
            Text[] myTexts = myManaCanvas.GetComponentsInChildren<Text>();
            myTexts[0].text = "- " + units[typeOfUnit].GetComponent<Unit>().manaCost;
            myTexts[0].transform.localScale = new Vector3(.3f, .3f, .3f) + new Vector3(.3f, .3f, .3f) * units[typeOfUnit].GetComponent<Unit>().manaCost / 150f;
            myTexts[1].text = "- " + units[typeOfUnit].GetComponent<Unit>().manaCost;
            myTexts[1].transform.localScale = new Vector3(.3f, .3f, .3f) + new Vector3(.3f, .3f, .3f) * units[typeOfUnit].GetComponent<Unit>().manaCost / 150f;

            ManaText.GetComponent<Text>().DOKill();
            ManaFull.GetComponent<Text>().DOKill();
            ManaText.GetComponent<Text>().DOBlendableColor(new Color(0x00, 0xe7, 0xfc), 0f);
            ManaText.GetComponent<Text>().DOBlendableColor(new Color(0xff, 0xff, 0xff), 1f).SetEase(Ease.InOutCirc);
            ManaText.transform.DOShakePosition(0.2f, 10);
            ManaFull.transform.DOShakePosition(0.2f, 5);


            // ANALYTICS HERE
            // ANALYTICS HERE
            // ANALYTICS HERE
            // ANALYTICS HERE
            // ANALYTICS HERE


            for (int i = 0; i < unitToSpawn; i++)
            {
                GameObject prefabOfUnit = Instantiate(units[typeOfUnit], transform.position, transform.rotation) as GameObject;
                Unit unit = prefabOfUnit.GetComponent<Unit>();
                NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent>();
                
                upgrades[typeOfUnit].Use(unit);
                unit._playerId = _playerId;
                unit._motherBase = this;
                if (waypoint == null)
                {
                    nav.SetDestination(targetBase.transform.position);
                    unit.laneEnd = true;
                }
                else {
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
        } while (exp > 0 && level < experienceLevel.Count);
    }

    private void UseLevel(int indexUpgrade, int indexLevel)
    {
        if (indexLevel > 0)
        {
            upgrades[indexUpgrade].LevelUp(indexLevel);
            for (int i = 0; i < upgrades.Count; i++)
            {
                upgrades[i].HideImage();
                upgrades[i]._imageBase.enabled = true;
            }
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

    IEnumerator loadMana()
    {
        while (_life > 0)
        {
            if (_currentMana < _maxMana)
            {
                _currentMana = Mathf.Min(_currentMana + _addMana, _maxMana);
            }
            else {
                ManaText.transform.DOShakePosition(0.1f, 3);
                ManaFull.transform.DOShakePosition(0.1f, 3);
            }
            yield return new WaitForSeconds(_delayMana);
        }

    }
}
