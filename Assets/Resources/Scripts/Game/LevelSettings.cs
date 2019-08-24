using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[System.Serializable]
public class LevelSettings : MonoBehaviour
{
    // -------------------------------------------------------------------
    //                          o Components o
    // -------------------------------------------------------------------

    [Header("Components")]
    public List<Tower> towers = null;
    public List<GameObject> walls = null;
    public List<Device> devices = null;

    [Header("Canvas")]
    public Canvas canvas_UI;
    public Text text_Lives, text_Time, text_Shots;
    public Canvas canvas_Pause;
    public Canvas canvas_Win;

    // -------------------------------------------------------------------
    //                      o Canvas Management o
    // -------------------------------------------------------------------

    private void Update()
    {
        GameManager.GetInstance().Update();
    }

    public void UpdateUI()
    {
        PlayerStatistics stats = GameManager.GetInstance().stats;
        
        text_Lives.text = "Times Died: " + stats.timesDied;
        text_Shots.text = "Shots Taken: " + stats.shotsTaken;

        if (stats.timeSpent / 60 < 1)
            text_Time.text = Mathf.Floor(stats.timeSpent) + "s";

        else if (stats.timeSpent / 60 >= 1)
            text_Time.text = Mathf.Floor(stats.timeSpent / 60) + "m " + (Mathf.Round(stats.timeSpent - 60 * Mathf.Floor(stats.timeSpent / 60))) + "s";

    }

    public void RESTART()
    {
        GameManager.GetInstance().Reset();

        foreach (Tower tower in towers)
        {
            // Reset Tower
        }
    }

    public void PAUSE(bool state)
    {
        GameManager.GetInstance().Pause(state);
    }

    // -------------------------------------------------------------------
    //                      o Player Statistics o
    // -------------------------------------------------------------------

    public class PlayerStatistics
    {
        public int timesDied = 0;
        public int shotsTaken = 0;
        public float timeSpent = 0;
    }
}
