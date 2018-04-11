using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CnControls;

public class LevelState : MonoBehaviour {

    public static LevelState levelManager;

    [SerializeField]
    private Text levelStateText;
    [SerializeField]
    private Image levelStateImage;
    
    public H_Health h_health;
    private EnemyCreator enemyCreator;
    public AudioClip kulintangAudio;
    public GameObject p_Controls;
    public bool levelStart;
    bool STOPPED;

    void OnEnable()
    {
        levelStart = false;
    }

    void Start()
    {
        STOPPED = false;
        levelManager = this;
        levelStart = false;
        enemyCreator = GetComponent<EnemyCreator>();
        enemyCreator.enabled = false;
        p_Controls.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!STOPPED && PersistentGameManager.manager.levelLoaded_ && !levelStart && (CnInputManager.TouchCount > 0 || Input.GetKeyUp(KeyCode.Space)))
        {
            StartLevel();
        }
    }

    void StartLevel()
    {
        levelStart = true;
        p_Controls.SetActive(true);
        enemyCreator.enabled = true;
        levelStateText.color = Color.clear;
        levelStateImage.color = Color.clear;
    }

    public void StopLevel(bool levelComplete)
    {
        if (levelComplete) h_health.currentHealth *= 5;
        STOPPED = true;
        levelStart = false;
        p_Controls.SetActive(false);
        
        PersistentGameManager.manager.EndLevel(levelComplete);
    }


}
