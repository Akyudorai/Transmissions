using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIFFICULTY { Easy, Medium, Hard };

public class SETTINGS : MonoBehaviour {

    [SerializeField]
    private DIFFICULTY currentDifficulty;

    private bool setTowerDecay, setTimeTrial, setLifeCounter, setLifeLimit;
    private float setDecayTimer = 0;    

    private void Start()
    {
        InitializeDefaultSettings();

        DontDestroyOnLoad(this.gameObject);
    }

    private void InitializeDefaultSettings()
    {
        currentDifficulty = DIFFICULTY.Easy;
        InitializeSettings(currentDifficulty);
    }

    private void InitializeSettings(DIFFICULTY difficulty)
    {
        switch (difficulty)
        {
            case DIFFICULTY.Easy:
                setTowerDecay = false;
                setTimeTrial = true;
                setLifeCounter = false;
                setLifeLimit = false;
                setDecayTimer = 0.0f;
                break;

            case DIFFICULTY.Medium:
                setTowerDecay = true;
                setTimeTrial = true;
                setLifeCounter = true;
                setLifeLimit = false;
                setDecayTimer = 30.0f;
                break;

            case DIFFICULTY.Hard:
                setTowerDecay = true;
                setTimeTrial = true;
                setLifeCounter = true;
                setLifeLimit = true;
                setDecayTimer = 30.0f;
                break;

            default:
                Debug.Log("ERROR");                
                break;
        }
    }

    public bool GetTowerDecay()
    {
        return setTowerDecay;
    }

    public bool GetTimeTrial()
    {
        return setTimeTrial;
    }

    public bool GetLifeCount()
    {
        return setLifeCounter;
    }

    public bool GetLifeLimit()
    {
        return setLifeLimit;
    }
    
}
