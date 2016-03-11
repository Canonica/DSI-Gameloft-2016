using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using XInputDotNetPure;

public class Motherbase : Entity
{
    [Header("MotherBase - Spawner Option")]
    public Animation animQueen;
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

	[Header ("Mana Option")]

	public int _maxMana;
	public int _currentMana;
	public int experienceByMana = 1;
	public float _delayMana;
	public int _addMana;
	public int _manaToSacrifice;
	bool _canSacrificeMana;

	[Header ("Editor Option")]
	public Image _manaImage;
	public Text _manaText;

	public Text[] textCurrentNbOfUnits;
	public Image[] reloadUnitImage;
	public Image _lifeImage;
    

	[Header ("Spell Option")]
	public Spell primarySpell;
	public Spell secondarySpell;

	float cooldownPrimarySpell;
	float cooldownSecondarySpell;

	Coroutine primarySpellCd;
	Coroutine secondarySpellCd;

	public List<Spell> primarySpells;
	public List<Spell> secondarySpells;
	[Header ("Upgrades Option")]
	public List<Upgrade> upgrades;
	public float upgradeDelay = 0.51f;
	public float lastUpgrade = 0.0f;

	public CanvasGroup _arrowImages;
	public CanvasGroup[] _arrowArray;

	public List<int> experienceLevel;
	public List<int> maxExperienceLevel;

    public float expRate = 0.3f;

    public List<bool> hasUsedLevel;

	public List<int> upgradeNumber;

	public List<GameObject> UnitsSprite;

	[Header ("FX")]
	[SerializeField]
	private GameObject FxBlood;
	[Header ("Sound")]
	public AudioClip spawnSwarmFX;
    bool animPlay = false;


	public GameObject UpgradeSprite;
	public GameObject ManaCostCanvas;
	public GameObject TouchSprite;
	public GameObject ManaText;
	public GameObject ManaFull;

	private List<GameObject> Upgrader = new List<GameObject> ();

	bool buttonPressedA = false, buttonPressedB = false, buttonPressedX = false, buttonPressedY = false;

	private int lastLevelDispo = 0;
	private GameObject XpBar;
	// Use this for initialization
	void Awake ()
	{
		spawning = false;
	}


	// Update is called once per frame


	public override void Start ()
	{
		//cameraPos = Camera.main.transform.position;
		_currentMana = 0;
		_canSacrificeMana = true;
		_currentLane = GetComponent<ChangeLane> ();
		experienceLevel = new List<int> ();
		for (int i = 0; i < maxExperienceLevel.Count; i++) {
			experienceLevel.Add (maxExperienceLevel [i]);
		}
		hasUsedLevel = new List<bool> ();
		for (int i = 0; i < experienceLevel.Count; i++) {
			hasUsedLevel.Add (false);
		}
		_arrowImages.alpha = 1;
		for (int i = 0; i < _arrowArray.Length; i++) {
			_arrowArray [i].alpha = 0;
		}
		base.Start ();

		ManaText = GameObject.Find ("Mana_Text" + _playerId);
		ManaFull = GameObject.Find ("Mana_Full" + _playerId);

		XpBar = GameObject.Find ("XpBar" + _playerId);
	}

	public override void Update ()
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
		if (Input.GetKeyDown (KeyCode.Z) && _playerId == 1) {
			corSpawnUnits (0);
		}
		if (Input.GetKeyDown (KeyCode.E) && _playerId == 2) {
			corSpawnUnits (0);
		}
		if (Input.GetKeyDown (KeyCode.D) && _playerId == 2) {
			corSpawnUnits (2);
		}
		if (Input.GetKeyDown (KeyCode.S) && _playerId == 1) {
			corSpawnUnits (2);
		}

		if (GameManager.instance.currentGamestate == GameManager.gameState.Playing) {
			if (!spawning) {
				StartCoroutine (loadMana ());
				spawning = true;
			}


			//if (Input.GetButtonDown("Fire " + _playerId))
			if (XInput.instance.getButton (_playerId, 'A') == ButtonState.Pressed && !buttonPressedA) {
				buttonPressedA = true;
				typeOfUnit = 0;
				corSpawnUnits (typeOfUnit);
			} else if (XInput.instance.getButton (_playerId, 'A') == ButtonState.Released && buttonPressedA) {
				buttonPressedA = false;
			}

			if (XInput.instance.getButton (_playerId, 'B') == ButtonState.Pressed && !buttonPressedB) {
				typeOfUnit = 1;
				buttonPressedB = true;
				corSpawnUnits (typeOfUnit);
			} else if (XInput.instance.getButton (_playerId, 'B') == ButtonState.Released && buttonPressedB) {
				buttonPressedB = false;
			}


			if (XInput.instance.getButton (_playerId, 'X') == ButtonState.Pressed && !buttonPressedX) {//if (Input.GetButtonDown("X_button_" + _playerId))
				typeOfUnit = 2;
				buttonPressedX = true;
				corSpawnUnits (typeOfUnit);
			} else if (XInput.instance.getButton (_playerId, 'X') == ButtonState.Released && buttonPressedX) {
				buttonPressedX = false;
			}

			//if (Input.GetButtonDown("Y_button_" + _playerId))
			if (XInput.instance.getButton (_playerId, 'Y') == ButtonState.Pressed && !buttonPressedY) {
				typeOfUnit = 3;
				buttonPressedY = true;
				corSpawnUnits (typeOfUnit);
			} else if (XInput.instance.getButton (_playerId, 'Y') == ButtonState.Released && buttonPressedY) {
				buttonPressedY = false;
			}

			//if (Input.GetAxis("TriggersR_" + _playerId) > 0.3)
			if (XInput.instance.getTrigger (_playerId) > 0.3) {

				if (_manaToSacrifice <= _currentMana && _canSacrificeMana) {
					Debug.Log ("add mana" + _playerId);
					_currentMana -= _manaToSacrifice;
					_canSacrificeMana = false;
					AddExperience (_manaToSacrifice * experienceByMana);
					StartCoroutine (delaySacrifice ());
				} else {
					// can't add xp;
				}
			}

			if (XInput.instance.getTrigger (_playerId) == 0) {
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
		}

		_lifeImage.fillAmount = (float)((float)_life / (float)_lifeMax);
		_manaImage.fillAmount = ((float)_currentMana / (float)_maxMana);
		_manaText.text = _currentMana + "/" + _maxMana;



		for (int i = 0; i <= UnitsSprite.Count - 1; i++) {

			if (_currentMana < units [i].GetComponent<Unit> ().manaCost) {
				Image[] changes = UnitsSprite [i].GetComponentsInChildren<Image> ();
				for (int j = 0; j <= changes.Length - 1; j++) {
					changes [j].DOColor (new Color (1, 1, 1, 0.4f), .5f);
					changes [j].transform.DOScale (new Vector3 (0.5f, 0.5f, 0.5f), .5f);


					if (changes [j].transform.parent.parent.parent.name != "Stack") {
						changes [j].transform.DORotate (new Vector3 (0, 0, 90f), .5f);
					}
				}

			} else if (UnitsSprite [i].GetComponentInChildren<Image> ().color.a <= 0.5f) {
				Image[] changes = UnitsSprite [i].GetComponentsInChildren<Image> ();
				for (int j = 0; j <= changes.Length - 1; j++) {
					changes [j].DOColor (new Color (1, 1, 1, 1f), .5f);
					changes [j].transform.DOScale (new Vector3 (1, 1, 1), .5f);
					if (changes [j].transform.parent.parent.parent.name != "Stack") {
						changes [j].transform.DORotate (new Vector3 (0, 0, 0), .5f);
					}
				}
			}
		}



		base.Update ();
	}

	IEnumerator delaySacrifice ()
	{
		yield return new WaitForSeconds (expRate);
		_canSacrificeMana = true;
	}

	public override void FixedUpdate ()
	{
		int levelDispo = 0;

		for (int i = 0; i < experienceLevel.Count; i++) {
			if (experienceLevel [i] == 0) {
				levelDispo++;
				if (levelDispo > lastLevelDispo) {
					lastLevelDispo = levelDispo;
					GameObject up = Instantiate (UpgradeSprite) as GameObject;
					up.transform.parent = XpBar.GetComponentInChildren<GridLayoutGroup> ().transform;
					Upgrader.Add (up);
				}
			}
		}
		for (int i = 0; i < hasUsedLevel.Count; i++) {
			if (hasUsedLevel [i]) {
				levelDispo--;
			}
		}

		if (levelDispo > 0) {
			//_arrowImages.DOFade(1, 0.5f);
			if (upgradeNumber == null || upgradeNumber.Count == 0) {
//				Debug.Log (upgradeNumber.Count);
				upgradeNumber = new List<int> ();
				for (int i = 0; i < upgrades.Count; i++) {

					upgradeNumber.Add (upgrades [i].PreLevelUp ());
					if (upgradeNumber [i] != -1) {
						_arrowArray [i].DOFade (1, 0.5f);
					} else {
						_arrowArray [i].alpha = 0;
					}
				}

			} else {
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

				if (lastUpgrade + upgradeDelay < Time.time) {
                    
					if (XInput.instance.getDPad (_playerId, 'R') == ButtonState.Pressed) { // RIGHT
						UseLevel (1, upgradeNumber [1]);
					} else if (XInput.instance.getDPad (_playerId, 'L') == ButtonState.Pressed) { // LEFT
						UseLevel (2, upgradeNumber [2]);
					} else if (XInput.instance.getDPad (_playerId, 'U') == ButtonState.Pressed) { // UP
						UseLevel (3, upgradeNumber [3]);
					} else if (XInput.instance.getDPad (_playerId, 'D') == ButtonState.Pressed) { // DOWN
//						Debug.Log (upgradeNumber [0]);
						UseLevel (0, upgradeNumber [0]);


//							(empty.GetComponent<RectTransform> ().anchoredPosition, 1f).OnComplete (() => Debug.Log ("lol"));
					}
				}


			}

		} else {
			for (int i = 0; i < _arrowArray.Length; i++) {
				_arrowArray [i].DOFade (0, 0.5f);
			}
			//_arrowImages.DOFade(0, 0.5f);
		}
		base.FixedUpdate ();
	}

    IEnumerator playAnim()
    {
        if (animPlay == false)
        {
            animPlay = true;
            animQueen.Play("HURT");
            yield return new WaitForSeconds(animQueen.GetClip("HURT").length-0.1f);
            animQueen.Play("IDLE");
            animPlay = false;
        }
    }

	public void getDamage (int dmg)
	{
		_lifeImage.transform.parent.DOKill (true);
		_lifeImage.transform.parent.DOShakePosition (0.1f, 10);

        StartCoroutine(playAnim());
        Instantiate (FxBlood, transform.position, Quaternion.Euler (new Vector3 (-50, 0, 0)));
        if(_playerId == 2)
        {
            Instantiate(FxBlood, transform.position, Quaternion.Euler(new Vector3(-50, 180, 0)));
        }

		XInput.instance.useVibe (_playerId - 1, 0.5f, 0.5f, 0.5f);

		Camera.main.DOKill (true);
		Camera.main.DOShakePosition (0.5f, 1, 1, 20);

		if (dmg > _life) {
			_life = 0;
            animQueen.Play("DEATH");
            EndGameManager.instance.motherBaseDead (_playerId);
		} else {
			if (dmg > 0)
				_life -= dmg;

			if (_life < 250) {
				_lifeImage.DOColor (new Color (0xF4 / 255f, 0x43 / 255f, 0x36 / 255f), 0.5f);
			}
		}
	}



	void corSpawnUnits (int typeOfUnit)
	{
		if (typeOfUnit == 0)
		if (spawnSwarmFX)
			SoundManager.Instance.playSound (spawnSwarmFX, 0.3f);
		int unitToSpawn = units [typeOfUnit].GetComponent<Unit> ().groupSpawn;
		EndGameManager.instance.addSpawn (_playerId, unitToSpawn);

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
		if (_currentMana >= units [typeOfUnit].GetComponent<Unit> ().manaCost) {


			GameObject Touch = Instantiate (TouchSprite);
			Touch.transform.parent = UnitsSprite [typeOfUnit].transform;
			UnitsSprite [typeOfUnit].transform.DOKill (true);
			UnitsSprite [typeOfUnit].transform.DOShakePosition (0.1f, 10);
			Touch.transform.position = UnitsSprite [typeOfUnit].GetComponentsInChildren<Image> () [1].transform.position;
			GameObject myManaCanvas = Instantiate (ManaCostCanvas, transform.position + new Vector3 (Random.Range (-10f, 10f), 0, 5 * Mathf.Sign (transform.position.x - _enemyMotherBase.transform.position.x)), Quaternion.Euler (0, -90, 0)) as GameObject;
			Text[] myTexts = myManaCanvas.GetComponentsInChildren<Text> ();
			myTexts [0].text = "- " + units [typeOfUnit].GetComponent<Unit> ().manaCost;
			myTexts [0].transform.localScale = new Vector3 (.5f, .5f, .5f) + new Vector3 (.2f, .2f, .2f) * units [typeOfUnit].GetComponent<Unit> ().manaCost / 150f;
			myTexts [1].text = "- " + units [typeOfUnit].GetComponent<Unit> ().manaCost;
			myTexts [1].transform.localScale = new Vector3 (.5f, .5f, .5f) + new Vector3 (.2f, .2f, .2f) * units [typeOfUnit].GetComponent<Unit> ().manaCost / 150f;


//			Instantiate(TouchSprite, )
			ManaText.GetComponent<Text> ().DOKill (true);
			ManaFull.GetComponent<Text> ().DOKill (true);
			ManaText.GetComponent<Text> ().DOBlendableColor (new Color (0x00, 0xe7, 0xfc), 0f);
			ManaText.GetComponent<Text> ().DOBlendableColor (new Color (0xff, 0xff, 0xff), 1f).SetEase (Ease.InOutCirc);
			ManaText.transform.DOKill (true);
			ManaText.transform.DOShakePosition (0.2f, 10);
			ManaFull.transform.DOShakePosition (0.2f, 5);


			// ANALYTICS HERE
			// ANALYTICS HERE
			// ANALYTICS HERE
			// ANALYTICS HERE
			// ANALYTICS HERE


			for (int i = 0; i < unitToSpawn; i++) {
				GameObject prefabOfUnit = Instantiate (units [typeOfUnit], transform.position, transform.rotation) as GameObject;
				Unit unit = prefabOfUnit.GetComponent<Unit> ();
				NavMeshAgent nav = prefabOfUnit.GetComponent<NavMeshAgent> ();


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
				upgrades [typeOfUnit].Use (unit);
				unit._playerId = _playerId;
				unit._motherBase = this;
				if (waypoint == null) {
					nav.SetDestination (targetBase.transform.position);
					unit.laneEnd = true;
				} else {
					nav.SetDestination (waypoint.pos);
				}
                    
				unit._enemyMotherBase = targetBase;
				unit.waypointDest = waypoint;
				unit._actualLane = 0;
				prefabOfUnit.transform.parent = transform.parent;
			}
			_currentMana -= units [typeOfUnit].GetComponent<Unit> ().manaCost;
		} else {
			GameObject Touch = Instantiate (TouchSprite);
			Touch.transform.parent = UnitsSprite [typeOfUnit].transform;
			UnitsSprite [typeOfUnit].transform.DOKill (true);
			UnitsSprite [typeOfUnit].transform.DOShakePosition (0.1f, 10);
			Touch.transform.position = UnitsSprite [typeOfUnit].GetComponentsInChildren<Image> () [1].transform.position;
			Touch.GetComponent<Image> ().color = new Color (0xff / 255f, 0x1d / 255f, 0x1d / 255f);
		}
	}

	private void AddExperience (int experience)
	{

		GameObject Touch = Instantiate (TouchSprite);
		GameObject Papa = new GameObject ();
		Touch.transform.parent = XpBar.GetComponentInChildren<Impulse> ().transform;

//		Touch.transform.position = new Vector2 (XpBar.GetComponentInChildren<GridLayoutGroup> ().transform.position.x - XpBar.GetComponentInChildren<GridLayoutGroup> ().gameObject.GetComponent<RectTransform> ().sizeDelta.x * .44f, XpBar.GetComponentInChildren<GridLayoutGroup> ().transform.position.y + XpBar.GetComponentInChildren<GridLayoutGroup> ().gameObject.GetComponent<RectTransform> ().sizeDelta.y);
		Touch.transform.position = XpBar.GetComponentInChildren<Impulse> ().transform.position;
		Touch.GetComponent<Image> ().color = new Color (0xd2 / 255f, 0xc3 / 255f, 0x2a / 255f);
		// 0x d2 c3 2a

		int exp = experience;
		int level = 0;
		do {
			int expCurr = Mathf.Max (experienceLevel [level] - exp, 0);
			exp = Mathf.Max (exp - experienceLevel [level], 0);
			experienceLevel [level] = expCurr;
			level++;
		} while (exp > 0 && level < experienceLevel.Count);

		if (exp > 0) {
			XpBar.GetComponentInChildren<Text> ().DOFade (1f, 0.5f);
		}
	}

	private void UseLevel (int indexUpgrade, int indexLevel)
	{
		if (indexLevel > 0) {
			upgrades [indexUpgrade].LevelUp (indexLevel);
			for (int i = 0; i < upgrades.Count; i++) {
				upgrades [i].HideImage ();
				upgrades [i]._imageBase.enabled = true;
			}
			upgradeNumber.Clear ();
			upgradeNumber = null;
			int level = 0;
			while (hasUsedLevel [level]) {
				level++;
			}
			hasUsedLevel [level] = true;
			lastUpgrade = Time.time;



			GameObject empty = Instantiate (new GameObject ());
			empty.AddComponent <RectTransform> ();
//			empty.AddComponent <Image> ();
			empty.transform.parent = UnitsSprite [indexUpgrade].GetComponentInChildren<GridLayoutGroup> ().transform;
			GameObject movable = Upgrader [Upgrader.Count - 1];
			Upgrader.RemoveAt (Upgrader.Count - 1);

			Vector3 lastPos = movable.GetComponent<RectTransform> ().position;
//			Debug.Log (movable.GetComponent<RectTransform> ().anchoredPosition);
			movable.transform.SetParent (empty.transform.parent.parent, true);
			movable.GetComponent<Animator> ().enabled = false;

			movable.GetComponent<RectTransform> ().position = lastPos;
//			Debug.Log (movable.GetComponent<RectTransform> ().anchoredPosition);
			movable.GetComponent<RectTransform> ().anchorMax = new Vector2 (0.5f, 0.5f);
			movable.GetComponent<RectTransform> ().anchorMin = new Vector2 (0.5f, 0.5f);
//			movable.GetComponent<RectTransform> ().DOAnchorPos (empty.transform.parent.GetComponent<RectTransform> ().anchoredPosition + new Vector2 (empty.transform.parent.GetComponent<RectTransform> ().GetComponentsInChildren<Image> ().Length * 4, 0), 0.5f).SetEase (Ease.InOutCirc).OnComplete (() => movable.transform.SetParent (empty.transform, true));
			movable.GetComponent<RectTransform> ().DOAnchorPosY (empty.transform.parent.GetComponent<RectTransform> ().anchoredPosition.y + new Vector2 (empty.transform.parent.GetComponent<RectTransform> ().GetComponentsInChildren<Image> ().Length * 4, 0).y, 0.3f).SetEase (Ease.InOutCubic);
			movable.GetComponent<RectTransform> ().DOAnchorPosX (empty.transform.parent.GetComponent<RectTransform> ().anchoredPosition.x + new Vector2 (empty.transform.parent.GetComponent<RectTransform> ().GetComponentsInChildren<Image> ().Length * 4, 0).x, 0.5f).SetEase (Ease.InOutCubic).OnComplete (() => movable.transform.SetParent (empty.transform, true));

			movable.GetComponent<RectTransform> ().DOScale (new Vector3 (2, 2, 2), 0.25f).SetEase (Ease.InOutCirc).OnComplete (() => movable.GetComponent<RectTransform> ().DOScale (new Vector3 (0.5f, 0.5f, 0.5f), 0.25f).SetEase (Ease.InOutCirc));
			StartCoroutine (ImGonnaSetYouStraight (movable));
			GameObject Touch = Instantiate (TouchSprite);
			Touch.transform.parent = UnitsSprite [indexUpgrade].transform;
			UnitsSprite [indexUpgrade].transform.DOKill (true);
			UnitsSprite [indexUpgrade].transform.DOShakePosition (0.1f, 10);
			Touch.transform.position = UnitsSprite [indexUpgrade].GetComponentsInChildren<Image> () [1].transform.position;
			Touch.GetComponent<Image> ().color = new Color (0xd2 / 255f, 0xc3 / 255f, 0x2a / 255f);
			Instantiate (Touch, Touch.transform.position, Touch.transform.rotation);

		}
        
	}

	IEnumerator ImGonnaSetYouStraight (GameObject o)
	{
		yield return new WaitForSeconds (1f);
		o.GetComponentsInChildren<Image> () [0].transform.DORotate (new Vector3 (0, 0, 30), 0.5f); 
		o.GetComponentsInChildren<Image> () [1].transform.DORotate (new Vector3 (0, 0, -30), 0.5f); 
	}

	public IEnumerator fillIcon (Image icon, float cdTimer)
	{
		float timer = 0;
		while (timer <= cdTimer) {
			icon.fillAmount = timer / cdTimer;
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		timer = 0;
	}

	//IEnumerator corCooldownSpell(Spell spellToRecharge)
	//{
	//    yield return new WaitForSeconds(spellToRecharge._cost);
	//    rechargeSpell(spellToRecharge);
	//}

	IEnumerator loadMana ()
	{
		while (_life > 0) {
			if (_currentMana < _maxMana) {
				_currentMana = Mathf.Min (_currentMana + _addMana, _maxMana);
			} else {
				ManaText.transform.DOKill (true);
				ManaText.transform.DOShakePosition (0.1f, 3);
				ManaFull.transform.DOKill (true);
				ManaFull.transform.DOShakePosition (0.1f, 3);
			}
			yield return new WaitForSeconds (_delayMana);
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
