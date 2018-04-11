using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class E_Movement : MonoBehaviour {

    public bool startChasing = false;

    GameObject chaseImageObj;
    Image chaseImage;
    GameObject hero;
    Transform h_Transform;
    H_Health h_health;
    UnityEngine.AI.NavMeshAgent e_nav;
    Animator e_anim;
    GameObject e_image;
    float glowAlpha;
    float factor = 1f;

    void Start()
    {
        e_nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        chaseImageObj = GameObject.FindGameObjectWithTag("ChaseImage");
        hero = GameObject.FindGameObjectWithTag("Hero");
        h_Transform = hero.transform;
        h_health = hero.GetComponent<H_Health>();
        e_anim = GetComponent<Animator>();
        e_image = GetComponentInChildren<Image>().gameObject;
        e_image.SetActive(false);

        chaseImage = chaseImageObj.GetComponent<Image>();
    }
     
    void FixedUpdate()
    {
        if (!LevelState.levelManager.levelStart)
        {
            e_nav.SetDestination(transform.position);
            e_anim.SetBool("isRunning", false);
            return;
        }

        if (startChasing && h_health.currentHealth > 0)
        {
            ChaseHero();
        }
        else
        {
            StopChase();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == hero)
        {
            InitiateChase();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (startChasing && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SlowDown());
        }
    }
    IEnumerator SlowDown()
    {
        e_nav.speed = 10f;
        e_anim.speed = 0.5f;
        yield return new WaitForSeconds(3f);
        e_nav.speed = 18f;
        e_anim.speed = 1f;
    }

    public void InitiateChase()
    {
        startChasing = true;
        e_image.SetActive(true);
    }

    public void ChaseHero()
    {
        e_nav.SetDestination(h_Transform.position);
        e_anim.SetBool("isRunning", true);

        chaseImage.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        if (glowAlpha >= 0.3f) factor = -1f;
        else if (glowAlpha <= 0.1) factor = 1f;
        glowAlpha += factor * 0.01f;
        chaseImage.color = new Color(1, 0, 0, glowAlpha);
    }

    public void StopChase()
    {
        e_anim.SetBool("isRunning", false);
        e_image.SetActive(false);
        e_nav.SetDestination(transform.position);
    }

}
