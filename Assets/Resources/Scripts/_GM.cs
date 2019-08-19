using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// TO BE MERGED WITH GAMEMANAGER.CS
/// </summary>

public class _GM : MonoBehaviour {

    [Header("Scene Management")]
    [SerializeField] int sceneIndex;
    
    [Header("Game Manager")]
    [SerializeField] TOWER[] broadcastTowers;
    GameObject ball;
    [SerializeField] bool spawnBall = true;
    private int timesDied, shotsTaken;
    private float timeSpent;

    private bool win = false;

    public static bool pauseGame = false;
    
    [Header("UI Manager")]
    [SerializeField] Canvas Canvas_UI;
    [SerializeField] Text txt_Lives, txt_Time, txt_Shots;
    [SerializeField] Canvas Canvas_Pause;
    [SerializeField] Canvas Canvas_Win;

    [Header("Assets")]
    [SerializeField] GameObject obj_ball;

    [Header("Settings")]
    [SerializeField] SETTINGS settings;
    
    private void Awake()
    {        
        settings = GameObject.Find("Settings").GetComponent<SETTINGS>();
        
    }

    void Start () {
		
        if (settings)
        {
            foreach (TOWER t in broadcastTowers)
            {
                
            }
        }

        else
        {
            Debug.Log("File: 'Settings.ini' not found!");
        }

        sceneIndex = UpdateSceneIndex();
        OnLevelWasLoaded(sceneIndex);

        timeSpent = 0;
        shotsTaken = 0;
        timesDied = 0;

        if (!ball && spawnBall)
        {
            ball = Instantiate(obj_ball, new Vector2(0.0f, 0.0f), Quaternion.identity);            
            ball.GetComponent<CONTROLLER>().SetHooked(true, broadcastTowers[0].getPoint());
        }
    }
	
	// Update is called once per frame
	void Update () {

        UpdateUI();
        timeSpent += Time.deltaTime;
        
        // Ball Respawn Check
        if (!ball && spawnBall)
        {
            timesDied++;

            ball = Instantiate(obj_ball, new Vector2(0.0f, 0.0f), Quaternion.identity);
            ball.GetComponent<CONTROLLER>().SetHooked(true, broadcastTowers[0].getPoint());
        }

        // Pause Menu Check
        if (Input.GetKeyDown(KeyCode.Escape) && !win)
        {
            if (pauseGame)
                TOGGLEPAUSE(false);

            else
                TOGGLEPAUSE(true);
        }

        // Win Check
        if (!win)
        {
            win = WinCheck();
            
        }
	}

    // -------------------------------------------------------------------
    //                      o Game Management o
    // -------------------------------------------------------------------

    private bool WinCheck() 
    {
        foreach (TOWER t in broadcastTowers)
        {            
            if (!t.getTransmission())
                return false;

        }

        pauseGame = true;
        Time.timeScale = 0.0f;
        Canvas_Win.enabled = true;
        return true;
    }

    public void AddShot() {
        shotsTaken++;
    }
    
    // -------------------------------------------------------------------
    //                      o Canvas Management o
    // -------------------------------------------------------------------

    private void UpdateUI() {

        txt_Lives.text = "Times Died: " + timesDied;
        txt_Shots.text = "Shots Taken: " + shotsTaken;

        if (timeSpent / 60 < 1)
            txt_Time.text = Mathf.Floor(timeSpent) + "s";
        else if (timeSpent / 60 >= 1)
            txt_Time.text = Mathf.Floor(timeSpent / 60) + "m " + (Mathf.Round(timeSpent - 60 * Mathf.Floor(timeSpent / 60))) + "s";


    }

    public void TOGGLEPAUSE(bool state) {

        pauseGame = state;

        if (state)
        {
            Time.timeScale = 0.0f;
            Canvas_Pause.enabled = true;
        }

        else
        {
            Time.timeScale = 1.0f;
            Canvas_Pause.enabled = false;
        }
    }

    public void NEXT() {

        if (sceneIndex < 10)
            SceneManager.LoadScene(sceneIndex + 1, LoadSceneMode.Single);

        else
            SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void REDO() {
        
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void RETURN() {

        Canvas_Pause.enabled = false;
    }

    public void MENU() {
        
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void QUIT() {
        
        // Exit the Application        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    // -------------------------------------------------------------------
    //                      o Scene Management o
    // -------------------------------------------------------------------

    private void OnLevelWasLoaded(int level) {
        
        TOGGLEPAUSE(false);
    }

    private int UpdateSceneIndex() {

        return SceneManager.GetActiveScene().buildIndex;
    }


}
