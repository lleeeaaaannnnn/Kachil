using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private Transform[] spawnPoints;

    private int currentEnemyNum = 0;
    public int maxEnemiesAllowed;
    bool startCreating = false;
    void Start()
    {
        //maxEnemiesAllowed = spawnPoints.Length;
        StartCoroutine(OneTimeCreate());

    }
    IEnumerator OneTimeCreate()
    {
        yield return new WaitForSeconds(1.5f);
        startCreating = true;
    }
    void LateUpdate()
    {
        if (startCreating)
        {
            Spawn();
            CheckEnemies();
        }
    }

    void Spawn()
    {

        if (currentEnemyNum >= maxEnemiesAllowed)
            return;

        int i_enemy = Random.Range(0, enemies.Length);
        int i_spawnPoint;
        bool spawnPointActive = false;
        while (!spawnPointActive)
        {
            i_spawnPoint = Random.Range(0, spawnPoints.Length);
            if (spawnPoints[i_spawnPoint].gameObject.activeInHierarchy)
            {
                spawnPointActive = true;
                Instantiate(enemies[i_enemy], spawnPoints[i_spawnPoint].position, spawnPoints[i_spawnPoint].rotation, this.gameObject.transform);
                spawnPoints[i_spawnPoint].gameObject.SetActive(false);
            }
        }

        
        ++currentEnemyNum;

    }

    void CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemyNum = enemies.Length;
    }
}
