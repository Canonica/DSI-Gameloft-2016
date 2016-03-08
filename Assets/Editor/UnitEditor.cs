using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

class UnitEditor : EditorWindow
{
    // [START_REPLACE]
    private string _editorName = "UnitEditor";
    private string _loadingPath = "Prefabs/Entities";
    private Type type = typeof(Unit);


    [MenuItem("Window/UnitEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(UnitEditor));

    }

    // [END_REPLACE]


    private int _fieldWidth = 128;

    private List<FieldInfo> _values = new List<FieldInfo>();

    private string[] _prefabs;
    private List<GameObject> _gameObjects = new List<GameObject>();

    private int tick = 0;
    private bool loaded = false;
    private Vector2 scrollPos = Vector2.zero;
    private int PickUpID = 0;
    private bool selected = false;


    private int controlID;

    private Dictionary<int, int> PickUp_i = new Dictionary<int, int>();
    private Dictionary<int, int> PickUp_j = new Dictionary<int, int>();

    private GameObject[] MB;


    void FetchValues()
    {
        _values.Clear();

        System.Reflection.FieldInfo[] fields = type.GetFields();

        for (int i = 0; i <= fields.Length - 1; i++)
        {
            if (fields[i].GetCustomAttributes(typeof(Tweakable), true).Length == 1)
            {
                _values.Add(fields[i]);
            }
        }

    }

    void OnFocus()
    {
        //		EditorUtility.DisplayProgressBar ("Fetching Prefabs", "Loading...", 0); 
        LoadPrefabs();
        //		EditorUtility.DisplayProgressBar ("Fetching Prefabs", "Loading...", .5f); 
        FetchValues();
        //		EditorUtility.DisplayProgressBar ("Fetching Prefabs", "Loading...", 1f); 
        loaded = true;
        //		EditorUtility.ClearProgressBar ();
    }

    void OnGUI()
    {
        tick++;

        // Assets loading
        if (tick == 0 || tick % 60 == 0 || !loaded)
        {
            if (!LoadPrefabs())
            {
                Debug.Log("Directory not found");
            }
            FetchValues();
            loaded = true;
        }


        // Styles creation
        GUIStyle h1 = new GUIStyle(EditorStyles.boldLabel);
        h1.fontSize = 24;
        h1.alignment = TextAnchor.UpperCenter;
        GUIStyle h2 = new GUIStyle(EditorStyles.boldLabel);
        h2.fontSize = 16;
        h2.alignment = TextAnchor.UpperCenter;
        GUIStyle alignCenter = new GUIStyle(GUI.skin.label);
        //		alignCenter.alignment = TextAnchor.MiddleCenter;

        if (loaded)
        {
            int perLine = Mathf.Min((int)(Mathf.Floor(position.width / 280f)), _gameObjects.Count);
            int rowWidth = Mathf.FloorToInt(position.width / perLine);
            GUILayout.Label(_editorName, h1);

            GUILayout.Space(20);



            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MinWidth(position.width));
            GUILayout.Label("Motherbase", h2);

            MB[0].GetComponent<Motherbase>()._lifeMax = EditorGUILayout.IntField("Health : ", MB[0].GetComponent<Motherbase>()._lifeMax);
            MB[1].GetComponent<Motherbase>()._lifeMax = MB[0].GetComponent<Motherbase>()._lifeMax;

            MB[0].GetComponent<Motherbase>()._maxMana = EditorGUILayout.IntField("Max Mana : ", MB[0].GetComponent<Motherbase>()._maxMana);
            MB[1].GetComponent<Motherbase>()._maxMana = MB[0].GetComponent<Motherbase>()._maxMana;

            MB[0].GetComponent<Motherbase>()._delayMana = EditorGUILayout.FloatField("Mana rate : ", MB[0].GetComponent<Motherbase>()._delayMana);
            MB[1].GetComponent<Motherbase>()._delayMana = MB[0].GetComponent<Motherbase>()._delayMana;

            MB[0].GetComponent<Motherbase>()._addMana = EditorGUILayout.IntField("Mana earnt : ", MB[0].GetComponent<Motherbase>()._addMana);
            MB[1].GetComponent<Motherbase>()._addMana = MB[0].GetComponent<Motherbase>()._addMana;

            MB[0].GetComponent<Motherbase>()._manaToSacrifice = EditorGUILayout.IntField("Mana à sacrifier: ", MB[0].GetComponent<Motherbase>()._manaToSacrifice);
            MB[1].GetComponent<Motherbase>()._addMana = MB[0].GetComponent<Motherbase>()._manaToSacrifice;

            MB[0].GetComponent<Motherbase>().experienceByMana = EditorGUILayout.IntField("Multiplicateur mana sacrifiée : ", MB[0].GetComponent<Motherbase>().experienceByMana);
            MB[1].GetComponent<Motherbase>().experienceByMana = MB[0].GetComponent<Motherbase>().experienceByMana;


            GUILayout.Space(20);
            GUILayout.BeginHorizontal();



            for (int i = 0; i <= _gameObjects.Count - 1; i++)
            {
                Component u = _gameObjects[i].GetComponent(type.ToString());

                GUILayout.BeginVertical(GUILayout.MaxWidth(rowWidth));
                GUILayout.Label(u.name, h2);


                for (int j = 0; j <= _values.Count - 1; j++)
                {
                    GUILayout.BeginHorizontal();
                    if (_values[j].Name == "img" && _values[j].GetValue(u).GetType() == typeof(Texture2D))
                    {
                        /*
						 * THUMBNAIL
						 * */

                        Texture2D image = new Texture2D(256, 256);

                        if (_values[j].GetValue(u) as Texture2D == null)
                        {
                            image = Texture2D.blackTexture;
                            _values[j].SetValue(u, image);
                        }
                        image = _values[j].GetValue(u) as Texture2D;
                        //							image = (Texture2D)(EditorGUILayout.ObjectField (varNameToLabel (_values [j].Name) + ": ", (Texture2D)_values [j].GetValue (u), typeof(Texture2D), false, GUILayout.Width (rowWidth * .87f)));

                        GUILayout.Label("", GUILayout.Width((rowWidth * .87f - image.width) / 2));
                        if (GUILayout.Button(_values[j].GetValue(u) as Texture2D))
                        {
                            controlID = ++PickUpID;
                            PickUp_i[PickUpID] = i;
                            PickUp_j[PickUpID] = j;
                            EditorGUIUtility.ShowObjectPicker<UnityEngine.Texture2D>(null, false, "", controlID);
                        }

                        GUILayout.Label("", GUILayout.Width((rowWidth * .87f - image.width) / 2));

                        _values[j].SetValue(u, image);

                    }
                    else if (_values[j].GetValue(u).GetType() == typeof(int))
                    {
                        /*
						 * INT
						 */
                        _values[j].SetValue(u, (EditorGUILayout.IntField(varNameToLabel(_values[j].Name) + ": ", int.Parse(_values[j].GetValue(u).ToString()), GUILayout.Width(rowWidth * .65f))));

                        if (GUILayout.Button("+", GUILayout.Width(rowWidth * .1f)))
                        {
                            int nv = int.Parse(_values[j].GetValue(u).ToString()) + 1;
                            _values[j].SetValue(u, nv);
                        }

                        if (GUILayout.Button("-", GUILayout.Width(rowWidth * .1f)))
                        {
                            int nv = int.Parse(_values[j].GetValue(u).ToString()) - 1;
                            _values[j].SetValue(u, nv);
                        }
                    }
                    else if (_values[j].GetValue(u).GetType() == typeof(float))
                    {
                        /*
						 * FLOAT
						 */
                        _values[j].SetValue(u, (EditorGUILayout.FloatField(varNameToLabel(_values[j].Name) + ": ", float.Parse(_values[j].GetValue(u).ToString()), GUILayout.Width(rowWidth * .65f))));

                        if (GUILayout.Button("+", GUILayout.Width(rowWidth * .1f)))
                        {
                            float nv = float.Parse(_values[j].GetValue(u).ToString()) + 0.1f;
                            _values[j].SetValue(u, nv);
                            GUI.FocusControl("");
                        }

                        if (GUILayout.Button("-", GUILayout.Width(rowWidth * .1f)))
                        {
                            float nv = float.Parse(_values[j].GetValue(u).ToString()) - 0.1f;
                            _values[j].SetValue(u, nv);
                            GUI.FocusControl("");
                        }
                    }
                    else if (_values[j].GetValue(u).GetType() == typeof(GameObject))
                    {
                        /*
						 * GAMEOBJECT
						 */
                        _values[j].SetValue(u, (GameObject)(EditorGUILayout.ObjectField(varNameToLabel(_values[j].Name) + ": ", (GameObject)_values[j].GetValue(u), typeof(GameObject), false, GUILayout.Width(rowWidth * .87f))));
                    }


                    GUILayout.EndVertical();
                }

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Spawn", GUILayout.Width(rowWidth * .43f)))
                {
                    Instantiate(_gameObjects[i]);
                }
                if (GUILayout.Button("Select", GUILayout.Width(rowWidth * .43f)))
                {
                    EditorGUIUtility.PingObject(_gameObjects[i]);
                }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();

                if ((i + 1) % perLine == 0)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();

        }



        string commandName = Event.current.commandName;
        if (commandName == "ObjectSelectorUpdated")
        {
            selected = true;
        }
        else if (commandName == "ObjectSelectorClosed")
        {
            int pickID = EditorGUIUtility.GetObjectPickerControlID();
            bool PickUpExist;
            int PickUpValue;
            PickUpExist = PickUp_i.TryGetValue(pickID, out PickUpValue);
            if (PickUpExist)
            {
                Component u = _gameObjects[PickUp_i[pickID]].GetComponent(type.ToString());
                if (selected)
                {

                    Texture2D image = EditorGUIUtility.GetObjectPickerObject() as Texture2D;
                    if (image == null)
                    {
                        image = new Texture2D(256, 256);
                        image = Texture2D.whiteTexture;
                        image.Resize(256, 256);
                    }
                    _values[PickUp_j[pickID]].SetValue(u, image);
                    selected = false;
                }
            }
        }
    }

    bool LoadPrefabs()
    {
        try
        {
            MB = GameObject.FindGameObjectsWithTag("MotherBase");
            _prefabs = Directory.GetFiles(Application.dataPath + "/" + _loadingPath, "*.prefab", SearchOption.AllDirectories);


            _gameObjects = new List<GameObject>();

            foreach (string p in _prefabs)
            {
                string pPath = "Assets" + p.Replace(Application.dataPath, "").Replace("\\", "/");
                GameObject g = AssetDatabase.LoadAssetAtPath(pPath, typeof(GameObject)) as GameObject;
                _gameObjects.Add(g);
            }
            return true;
        }
        catch (DirectoryNotFoundException e)
        {
            return false;
        }
    }

    string varNameToLabel(string s)
    {
        string r = s.Replace("_", "");
        r = r.Substring(0, 1).ToUpper() + r.Substring(1, r.Length - 1);
        string[] rArray = Regex.Split(r, "(?=[A-Z])");
        return string.Join(" ", rArray);
    }

}
