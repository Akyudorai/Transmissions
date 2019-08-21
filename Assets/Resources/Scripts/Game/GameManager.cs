using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ScriptableObject
{
    // -------------------------------------------------------------------
    //                      o Singleton o
    // -------------------------------------------------------------------
    private static GameManager instance;
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = CreateInstance<GameManager>();
        }

        return instance;
    }

    // -------------------------------------------------------------------
    //                          o Components o
    // -------------------------------------------------------------------
    
    GameObject activeBall = null;    
    LevelSettings level = null;
    
    // -------------------------------------------------------------------
    //                          o Variables o
    // -------------------------------------------------------------------

    public static bool pauseGame = false;
    bool spawnBall = true;
    bool win = false;
    public LevelSettings.PlayerStatistics stats = null;
    bool initialized = false;
    

    // -------------------------------------------------------------------
    //                      o Initialization o
    // -------------------------------------------------------------------

    private void Awake()
    {
        // Prevent Duplicates of Singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        _SM.GetInstance().UpdateSceneIndex();

        level = GameObject.Find("Level Settings").GetComponent<LevelSettings>();        
    }

    private void InitializeLevel()
    {
        stats = new LevelSettings.PlayerStatistics
        {
            timeSpent = 0,
            shotsTaken = 0,
            timesDied = 0
        };

        if (!activeBall && spawnBall)
        {
            activeBall = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"), Vector2.zero, Quaternion.identity);
            activeBall.GetComponent<CONTROLLER>().SetHooked(true, level.towers[0].GetShotPoint());
        }

        initialized = true;
    }

    public void Reset()
    {
        InitializeLevel();
    }

    // -------------------------------------------------------------------
    //                      o Game Management o
    // -------------------------------------------------------------------

    public void Update()
    {   
        // Initialize the Game
        if (!initialized)
        {
            InitializeLevel();
            return;
        }

        // Update GUI
        level.UpdateUI();

        // Keep Track of Time
        stats.timeSpent += Time.deltaTime;

        // Respawn Ball Check
        if (!activeBall && spawnBall)
        {
            stats.timesDied++;

            activeBall = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"), Vector2.zero, Quaternion.identity);
            activeBall.GetComponent<CONTROLLER>().SetHooked(true, level.towers[0].GetShotPoint());
        }

        // Pause
        if (Input.GetKeyDown(KeyCode.Escape) && !win)
        {
            if (pauseGame) Pause(true);
            else Pause(false);
        }

        // Win Condition
        if (!win)
        {
            win = WinCheck();
        }        
    }

    private bool WinCheck()
    {
        foreach (Tower t in level.towers)
        {
            if (!t.GetTransmission())
                return false;

        }

        pauseGame = true;
        Time.timeScale = 0.0f;
        level.canvas_Win.enabled = true;
        return true;
    }

    public void Pause(bool state)
    {
        pauseGame = state;

        if (state)
        {
            Time.timeScale = 0.0f;
            level.canvas_Pause.enabled = true;
        }

        else
        {
            Time.timeScale = 1.0f;
            level.canvas_Pause.enabled = false;
        }
    }
    
}
