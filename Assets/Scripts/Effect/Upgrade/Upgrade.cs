using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Upgrade : Effect
{
	[HideInInspector]
	public Motherbase _mb;

	public int _levelMax = 3;
	public bool _levelOne;
	public bool _levelTwo;
	public bool _levelThree;

	public Image _imageBase;
	public Image _imageLevelOne;
	public Image _imageLevelTwo;
	public Image _imageLevelThree;



	void Start ()
	{
		
		_mb = GetComponent<Motherbase> ();
		_imageBase.enabled = true;
		//_imageLevelOne.enabled = false;
		_imageLevelTwo.enabled = false;
		_imageLevelThree.enabled = false;


	}

	public virtual void LevelOne (Unit unit)
	{
		//unit._damage += 2;
	}

	public virtual void LevelTwo (Unit unit)
	{
		// unit._hatchTime -= 0.5f;
	}

	public virtual void LevelThree (Unit unit)
	{
		/*if(index < _mb.maxNbOfUnits.Length)
        {
            _mb.maxNbOfUnits[index] += 5;
        }*/
	}

	public int PreLevelUp ()
	{
		int level = -1;
		if (!(_levelTwo && _levelThree)) {
			level = Random.Range (0, _levelMax * 100);
			HideImage ();
			/*if ((level < 100 || (_levelTwo && _levelThree)) && !_levelOne)
            {
                _imageLevelOne.enabled = true;
                level = 1;
            }
            else */
			if ((level < 200 || _levelThree) && !_levelTwo) {
				_imageLevelTwo.enabled = true;
				level = 2;
			} else if (level < 300 && !_levelThree) {
				_imageLevelThree.enabled = true;
				level = 3;
			}
		}
		return level;

	}

	public bool LevelUp (int level)
	{

		/*if (level == 1)
        {
            _imageBase.enabled = true;
            _imageLevelOne.enabled = false;
            return _levelOne = true;
        }
        else */
		if (level == 2) {
			_imageBase.enabled = true;
			_imageLevelTwo.enabled = false;
			return _levelTwo = true;
		} else if (level == 3) {
			_imageBase.enabled = true;
			_imageLevelThree.enabled = false;
			return _levelThree = true;
		}

		return false;

	}

	public void Use (Unit unit)
	{
		/*if (_levelOne)
        {
            LevelOne(unit);
        }*/
		if (_levelTwo) {
			LevelTwo (unit);
		}
		if (_levelThree) {
			LevelThree (unit);
		}
	}

	public void HideImage ()
	{
		_imageBase.enabled = false;
		_imageLevelOne.enabled = false;
		_imageLevelTwo.enabled = false;
		_imageLevelThree.enabled = false;
	}
}