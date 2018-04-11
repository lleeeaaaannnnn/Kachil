using UnityEngine;
using System.Collections;

public class ResetGameProgress : MonoBehaviour {

    void ResetGame()
    {
        PersistentGameManager.manager.currentLevel = 1;
    }
}
