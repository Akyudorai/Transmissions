using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class LevelEditor : EditorWindow
{
    // -------------------------------------------------------------------
    //                      o Window Components o
    // -------------------------------------------------------------------

    int towerIndex = 0;
    int deviceIndex = 0;
    int surfaceIndex = 0;

    string[] towerList = new string[]
    {
        "Main Tower", "Regular Tower", "Device Tower"
    };

    string[] surfaceList = new string[]
    {
        "Black Wall", "Silver Wall", "Orange Wall"
    };

    string[] deviceList = new string[]
    {
        "Gravity Well", "Gravity Orb", "Door"
    };

    string saveName = "New Level";

    bool saveAs = false;

    // -------------------------------------------------------------------
    //                      o Level Values o
    // -------------------------------------------------------------------

    private static LevelSettings level = null;

    
    [MenuItem("Window/Level Editor")] 
    public static void ShowWindow ()
    {
        GetWindow<LevelEditor>("Level Editor");        
    }
    

    private void OnGUI ()
    {


        // -------------------------------------------------------------------
        //                      o Initialization o
        // -------------------------------------------------------------------        

        GUILayout.Label("Reload", EditorStyles.boldLabel);

        if (GUILayout.Button("Reload"))
        {
            Load();
        }

        //{
        //    GUILayout.Label("Initialize", EditorStyles.boldLabel);

        //    if (GUILayout.Button("Sync EditorCallback"))
        //    {
        //        //EditorCallback.onSceneChanged += Load;
        //        //UnityEngine.SceneManagement.SceneManager.CreateScene("New Level");
        //    }

        //    if (GUILayout.Button("Create Default Scene"))
        //    {
        //        /*
        //        SceneManager.LoadScene("SceneName", LoadSceneMode.Single);
        //        Instantiate(Resources.Load<GameObject>("Scenes/Default"));

        //        string[] path = SceneManager.GetActiveScene().path.Split(char.Parse("/"));
        //        path[path.Length - 1] = "AutoSave_" + path[path.Length - 1];
        //        bool saveOK = EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), string.Join("/", path));
        //        */

        //    }
        //}

        EditorGUILayout.Space();
        // -------------------------------------------------------------------
        //                      o Prefab Manager o
        // -------------------------------------------------------------------
        {
            GUILayout.Label("Prefab Manager", EditorStyles.boldLabel);

            // Surfaces
            {
                surfaceIndex = EditorGUILayout.Popup("Surfaces", surfaceIndex, surfaceList);
                if (GUILayout.Button("Spawn Surface Material"))
                {
                    level.walls.Add(Instantiate(Resources.Load("Prefabs/" + surfaceList[surfaceIndex]), Vector3.zero, Quaternion.identity) as GameObject);
                }
            }
            

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // Towers
            {
                towerIndex = EditorGUILayout.Popup("Towers", towerIndex, towerList);
                if (GUILayout.Button("Spawn Tower"))
                {
                    bool mainTower = false;
                    if (towerIndex == 0)
                    {
                        for (int i = 0; i < level.towers.Count; i++)
                        {
                            if (level.towers[i] != null)
                            {
                                if (level.towers[i].GetTowerType() == "Main")
                                {
                                    Debug.Log("Can only have one main tower in a level");
                                    return;
                                }
                            }
                        }

                        mainTower = true;
                    }

                    GameObject tower = Instantiate(Resources.Load("Prefabs/" + towerList[towerIndex]), Vector3.zero, Quaternion.identity) as GameObject;

                    if (mainTower)
                        level.towers.Insert(0, tower.GetComponent<Tower>());
                    else
                        level.towers.Add(tower.GetComponent<Tower>());

                    Save();
                }
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // Devices
            {
                deviceIndex = EditorGUILayout.Popup("Devices", deviceIndex, deviceList);
                if (GUILayout.Button("Spawn Device"))
                {
                    GameObject device = Instantiate(Resources.Load("Prefabs/" + deviceList[deviceIndex]), Vector3.zero, Quaternion.identity) as GameObject;
                    level.devices.Add(device.GetComponent<Device>());
                }
            }            
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        // -------------------------------------------------------------------
        //                          o Save / Load o
        // -------------------------------------------------------------------
        {
            // Save the Level
            GUILayout.Label("Save", EditorStyles.boldLabel);
            
            saveAs = EditorGUILayout.BeginToggleGroup("Save as", saveAs);

            GUILayout.Label("Warning: This will overwrite scenes of the same name.");
            saveName = EditorGUILayout.TextField("Scene Name", saveName);

            EditorGUILayout.EndToggleGroup();

            
            if (GUILayout.Button("Save Scene"))
            {
                Save();
            }            
        }

        

    }
    
    public void Load()
    {
        if (EditorCallback.sSceneName == "Menu") return;

        // Get the Level Settings
        level = GameObject.Find("Level Settings").GetComponent<LevelSettings>();
        
        Debug.Log("Level Editor Loaded");
    }

    public void Save()
    {
        if (EditorCallback.sSceneName == "Menu") return;
        
        if (saveAs)
        {
            string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
            path[path.Length - 1] = path[path.Length - 1].Remove(0, EditorSceneManager.GetActiveScene().name.Length);
            path[path.Length - 1] = saveName + path[path.Length - 1];
            bool saveOK = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), string.Join("/", path));
            Debug.Log("Saved Scene " + (saveOK ? "OK" : "Error!"));
        }

        else
        {
            string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
            bool saveOK = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), string.Join("/", path));
            Debug.Log("Saved Scene " + (saveOK ? "OK" : "Error!"));
        }
        
    }

}

