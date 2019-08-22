using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;

using System;


[InitializeOnLoad]
public class EditorCallback 
{
    public static string sSceneName = null;

    public static Action onSceneChanged;

    static EditorCallback ()
    {
        EditorApplication.update += Update;
    }

    static void Update ()
    {
        
        if (sSceneName != EditorSceneManager.GetActiveScene().name)
        {
            
            // New scene has been loaded            
            sSceneName = EditorSceneManager.GetActiveScene().name;
            Debug.Log("New Scene Loaded: " + sSceneName);

            //onSceneChanged.Invoke();

        }
    }
}
