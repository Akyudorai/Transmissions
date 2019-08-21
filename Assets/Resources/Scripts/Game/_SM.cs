using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class _SM : ScriptableObject
{    
    // -------------------------------------------------------------------
    //                      o Singleton o
    // -------------------------------------------------------------------
    private static _SM instance;
    public static _SM GetInstance()
    {
        if (instance == null)
        {
            instance = CreateInstance<_SM>();
        }

        return instance;
    }

    // -------------------------------------------------------------------
    //                          o Variables o
    // -------------------------------------------------------------------
    int sceneIndex;

    // -------------------------------------------------------------------
    //                      o Methodolgy o
    // -------------------------------------------------------------------
    private void Awake()
    {
        // Prevent Duplicates of Singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
    }

    public void NEXTLEVEL()
    {
        if (sceneIndex < 10)
            SceneManager.LoadScene(sceneIndex + 1, LoadSceneMode.Single);

        else
            SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void RESETLEVEL()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void MAINMENU()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void UpdateSceneIndex()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
