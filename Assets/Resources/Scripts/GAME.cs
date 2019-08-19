using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GAME : MonoBehaviour {
 
    [Header("Scene Managment")]
    [SerializeField] int sceneIndex;
    

    [Header("Game Manager")]
    [SerializeField] TOWER[] broadcastTowers;
    [SerializeField] GameObject obj_Ball;
    GameObject game_Ball;
    [SerializeField] bool spawnBall = false;
    [SerializeField] public static bool pauseGame = false;
    [SerializeField] int lives = 3;
    private bool won = false;

    [Header("UI Elements")]
    [SerializeField] Canvas Canvas_UI;
    [SerializeField] Text Text_Lives;
    [SerializeField] Canvas Canvas_Pause;
    [SerializeField] Canvas Win_Canvas;
    
	private void Start () {
		
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        UpdateUI();
        
        if (!game_Ball && spawnBall)
        {         
            game_Ball = Instantiate(obj_Ball, new Vector2(0.0f, 0.0f), Quaternion.identity);
            game_Ball.GetComponent<CONTROLLER>().SetHooked(true, broadcastTowers[0].getPoint());
        }

	}
	
	private void Update () {
		
        if (!game_Ball && spawnBall)
        {
            game_Ball = Instantiate(obj_Ball, new Vector2(0.0f, 0.0f), Quaternion.identity);
            game_Ball.GetComponent<CONTROLLER>().SetHooked(true, broadcastTowers[0].getPoint());
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pauseGame)
                TogglePause(false);
           
            else
                TogglePause(true);
           
        }

        CheckWin();


	}

    private bool CheckWin() {
        
       

        if (!won)
        {           

            foreach (TOWER t in broadcastTowers)
            {
                if (!t.getTransmission())
                    return false;

            }
            
            Time.timeScale = 0.0f;
            Win_Canvas.enabled = true;
            won = false;
        }
        
        return true;
    }

    public void TogglePause(bool state) {

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

    public void NEXT()
    {
        SceneManager.LoadScene(sceneIndex + 1, LoadSceneMode.Single);
    }

    public void REDO()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        
    }

    public void RETURN()
    {
        Debug.Log("FUNCTION NOT IMPLEMENTED YET");
    }

    public void OPTIONS()
    {
        Debug.Log("FUNCTION NOT IMPLEMENTED YET");
    }

    public void QUIT()
    {
        // Exit the Application        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();        
    }

    private void UpdateUI() {

        Text_Lives.text = "Number of Lives: " + lives;
    }

    public void Life(int i)
    {
        lives += i;
        UpdateUI();
    }
}
