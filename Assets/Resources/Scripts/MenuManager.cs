using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    // Start Screen
    [SerializeField] GameObject startScreen;

    // Level Select Screen
    [SerializeField] GameObject levelSelectScreen;
    int navIndex_levelSelect = 1;
    [SerializeField] Image[] nav_levelSelect = new Image[4];
    [SerializeField] GameObject[] navContainer_levels = new GameObject[4];

    [SerializeField] Button[] settings_levelAccess = new Button[64];

    private void Awake()
    {
        for (int i = 1; i < settings_levelAccess.Length; i++)
        {
            settings_levelAccess[i].interactable = false;
            settings_levelAccess[i].GetComponentInChildren<Text>().text = "??";
        }

        for (int i = 0; i < navContainer_levels.Length; i++)
        {
            navContainer_levels[i].SetActive(false);
        }
    }

    private void Start()
    {
        NAV_LevelSelect(1);
    }

    public void NAV_PlayButton()
    {
        startScreen.SetActive(false);
        levelSelectScreen.SetActive(true);
    }

    public void NAV_ReturnToStart()
    {
        startScreen.SetActive(true);
        levelSelectScreen.SetActive(false);
    }

    public void NAV_LevelSelect(int index)
    {
        nav_levelSelect[navIndex_levelSelect - 1].fillCenter = false;
        navContainer_levels[navIndex_levelSelect - 1].SetActive(false);

        nav_levelSelect[index - 1].fillCenter = true;
        navContainer_levels[index - 1].SetActive(true);

        navIndex_levelSelect = index;
    }

}
