using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
///  TO BE MERGED WITH MENUMANAGER.CS
/// </summary>

public class _MM : MonoBehaviour {

    [Header("Canvas Elements")]
	[SerializeField] Canvas canvas_Main, canvas_LevelSelect;

    [Header("BGM")]
    [SerializeField] GameObject bgm;

    private void Start() {
        
        if (!GameObject.Find("BGM"))
        {
            GameObject obj = Instantiate(bgm, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            obj.name = "BGM";
        }

    }

    public void TOGGLE_LEVELSELECT() {

        if (canvas_LevelSelect.enabled)        
            canvas_LevelSelect.enabled = false;
        
        else        
            canvas_LevelSelect.enabled = true;
        
    }

    public void LOAD_LEVEL(int index) {

        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void QUIT() {

        // Exit the Application        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

}
