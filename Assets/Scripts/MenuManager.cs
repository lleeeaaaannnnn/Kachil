using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject main;
    public GameObject levelSelect;
    public GameObject inLevelMenu;

    // 1 = Main menu
    // 2 = Level Select menu
    // 3 = Settings menu

    public void ShowMenu(int menuNumber)
    {
        inLevelMenu.SetActive(false);

        if (menuNumber == 1)
        {
            levelSelect.SetActive(false);
            main.SetActive(true);
        }
        else if (menuNumber == 2)
        {
            main.SetActive(false);
            levelSelect.SetActive(true);
        }

    }

    public void InLevelMenu(bool show)
    {
        inLevelMenu.SetActive(show);
    }

    public void HideAllMenus()
    {
        levelSelect.SetActive(false);
        main.SetActive(false);

    }
}
