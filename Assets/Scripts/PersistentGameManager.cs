using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Audio;


public class PersistentGameManager : MonoBehaviour {

    public static PersistentGameManager manager;
    
    
    [HideInInspector] public int loadedLevel = 0;
    [HideInInspector] public GameObject hero;
    [HideInInspector] public GameObject player;

    public bool levelLoaded_ = false;
    public Text messageText;
    public int currentLevel = 1;
    public GameObject bg;
    public AudioSource audioSource;
    AudioMixerGroup originalMixerGroup;
    MenuManager menuManager;
    public GameObject loadingScreen;
    GameObject terrain;
    public AudioClip gong;
    void Awake()
    {
        //Debug.Log("scene count: " + SceneManager.sceneCountInBuildSettings);
        //Debug.Log(Application.persistentDataPath);

        if (manager == null)
        {
            DontDestroyOnLoad(this);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        levelLoaded_ = false;
        Load();
    }

    void Start()
    {
        //audioSource.Play();
        hero = GameObject.FindGameObjectWithTag("Hero");
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        menuManager = GetComponent<MenuManager>();
        messageText.text = messageText.text + " " + currentLevel;
    }

    public void Reset()
    {
        
        currentLevel = 1;
        messageText.text = messageText.text + " " + currentLevel;
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);

        PlayerData data = new PlayerData(currentLevel);

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData) bf.Deserialize(file);

            currentLevel = data.level;
        }
    }

    public void EndLevel(bool levelComplete)
    {

        menuManager.InLevelMenu(false);

        int loadLevel;
        if (levelComplete)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(gong,1);
            loadLevel = SceneManager.GetActiveScene().buildIndex + 1;
            Save();
        }
        else
        {
            loadLevel = SceneManager.GetActiveScene().buildIndex;
        }


        if (levelComplete && loadLevel >= SceneManager.sceneCountInBuildSettings)
        {
            loadLevel = 0;
            StartCoroutine(EndGameSequence());
            return;
        }

        StartCoroutine(EndLevelSequence(loadLevel, levelComplete));
        
    }
    public void LoadLastLevel()
    {
        LoadGameLevel(currentLevel);

    }

    public void LoadGameLevel(int level)
    {
        loadingScreen.SetActive(true);
        ActiveLevel(level);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        levelLoaded_ = false;

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        menuManager.ShowMenu(1);
        InactiveLevel();
        
    }
    
    public void ActiveLevel(int level)
    {
        menuManager.HideAllMenus();
        menuManager.InLevelMenu(true);
        //audioSource.clip = kulintang;
        audioSource.Play();
        bg.SetActive(false);
        loadedLevel = level;
    }

    public void InactiveLevel()
    {
        audioSource.Stop();
        bg.SetActive(true);
    }

    IEnumerator EndLevelSequence(int loadLevel, bool levelComplete)
    {
        FadeManager.Instance.Fade(1f, levelComplete, true);
        yield return new WaitForSeconds(2f);
        LoadGameLevel(loadLevel);
        FadeManager.Instance.Fade(1f, levelComplete, false);
    }


    // Temporary End Game Sequence
    IEnumerator EndGameSequence()
    {
        FadeManager.Instance.Fade(1f, true, true); // level Complete image fade In
        yield return new WaitForSeconds(2f);
        FadeManager.Instance.Fade(1f, true, false);  // level Complete image fade Out

        // Back to Main Menu
        LoadMainMenu();
    }
    
    void FixedUpdate()
    {
        //Debug.Log("Level loaded: " + levelLoaded_);

        if (!levelLoaded_)
        {
            hero = GameObject.FindGameObjectWithTag("Hero");
            player = GameObject.FindGameObjectWithTag("Player");
            terrain = GameObject.FindGameObjectWithTag("Terrain");

            if (hero != null && player != null && terrain != null)
            {
                loadingScreen.SetActive(false);
                levelLoaded_ = true;
            }
        }

    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}

[Serializable]
class PlayerData
{
    public int level;

    public PlayerData(int lvl)
    {
        level = lvl;
    }
}
