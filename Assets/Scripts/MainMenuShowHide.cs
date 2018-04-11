using UnityEngine;
using System.Collections;

public class MainMenuShowHide : MonoBehaviour {

    public GameObject mainMenuCanvas;
    public GameObject levelSelection;

    public void ShowLevelSelection()
    {
        mainMenuCanvas.SetActive(false);
        levelSelection.SetActive(true);
    }
    public void ShowMenu()
    {
        levelSelection.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    public void HideLevelSelection()
    {
        mainMenuCanvas.SetActive(false);
        levelSelection.SetActive(false);
    }
}
