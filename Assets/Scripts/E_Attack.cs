using UnityEngine;
using System.Collections;

public class E_Attack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    GameObject hero;
    bool heroInRange;
    float timer;
    H_Health h_Health;
    
    void Start () {
        hero = GameObject.FindGameObjectWithTag("Hero");
        h_Health = hero.GetComponent<H_Health>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hero)
        {
            heroInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == hero)
        {
            heroInRange = false;
        }
    }

    void FixedUpdate()
    {
        if (!LevelState.levelManager.levelStart)
            return;


        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && heroInRange)
        {
            Attack();
        }

    }
    void Attack()
    {
        timer = 0f;

        if (h_Health.currentHealth > 0)
        {
            h_Health.TakeDamage(attackDamage);
        }
    }

}
