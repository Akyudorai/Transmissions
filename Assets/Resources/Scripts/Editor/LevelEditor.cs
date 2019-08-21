using UnityEngine;
using UnityEditor;

using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class LevelEditor : EditorWindow
{
    string saveName = "New Level";

    int selected = 0;
    string[] options = new string[]
    {
        "Black Wall", "Silver Wall", "Orange Wall",
        "Main Tower", "Regular Tower", "Device Tower",
        "Gravity Well", "Gravity Orb", "Door"
    };


    [MenuItem("Window/Level Editor")] 

    public static void ShowWindow ()
    {
        GetWindow<LevelEditor>("Level Editor");
    }

    private void OnGUI ()
    {
        

        GUILayout.Label("Initialize", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Blank Scene"))
        {

            //UnityEngine.SceneManagement.SceneManager.CreateScene("New Level");
        }

        if (GUILayout.Button("Create Default Scene"))
        {
            /*
            SceneManager.LoadScene("SceneName", LoadSceneMode.Single);
            Instantiate(Resources.Load<GameObject>("Scenes/Default"));

            string[] path = SceneManager.GetActiveScene().path.Split(char.Parse("/"));
            path[path.Length - 1] = "AutoSave_" + path[path.Length - 1];
            bool saveOK = EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), string.Join("/", path));
            */

        }

        // Prefab Manager
        // Window Code
        GUILayout.Label("Prefab Manager", EditorStyles.boldLabel);
       
        
        selected = EditorGUILayout.Popup("Prefab Manager", selected, options);

        if (GUILayout.Button("Spawn Prefab"))
        {
            Instantiate(Resources.Load("Prefabs/" + options[selected]), Vector3.zero, Quaternion.identity);
        }

        // Save the Level
        GUILayout.Label("Save", EditorStyles.boldLabel);

        saveName = EditorGUILayout.TextField("Name", saveName);
        if (GUILayout.Button("Save Scene"))
        {
            //UnityEngine.SceneManagement.SceneManager.CreateScene("New Level");
        }


    }

    
}
