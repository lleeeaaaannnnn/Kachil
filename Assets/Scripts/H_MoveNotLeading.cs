using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class H_MoveNotLeading : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent h_nav;
    public Transform p_transform;
    public Button boostButton;
    public Image coolDownImage;
    public Image boostIndicator;
    public Image boostGlowImage;


    bool goto_p = true;
    H_Health h_health;
    Animator h_anim;
    float boostCooldown;
    bool boosting = false;
    Color indicatorColor;
    Color boostedColor;
    float glowAlpha;
    float factor = 1f;
    void Start()
    {
        h_health = GetComponent<H_Health>();
        h_nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        h_anim = GetComponent<Animator>();
        boostButton.onClick.AddListener(Boost);
        boostButton.interactable = true;
        boosting = false;
        indicatorColor = new Color(1f, 0, 0,0.5f);
        boostedColor = new Color(1f, 0, 0, 0.8f);
        boostIndicator.color = indicatorColor;
    }

    void FixedUpdate()
    {

        coolDownImage.fillAmount = boostCooldown;

        if (boostCooldown > 0 && !boosting)
        {
            boostCooldown -= Time.fixedDeltaTime;
        }
        else if (!boosting && boostCooldown <= 0)
        {
            boostButton.interactable = true;
        }

        if (goto_p && !h_health.isDead && LevelState.levelManager.levelStart)
        {
            h_nav.SetDestination(p_transform.position);
            h_anim.SetBool("isRunning", true);
        }
        else
        {
            h_anim.SetBool("isRunning", false);
            h_nav.SetDestination(transform.position);
        }

        if (boosting)
        {
            BoostingEffects();



        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");

        if (other.gameObject.CompareTag("Player"))
        {
            goto_p = false;
            if (boosting) StopBoost();
        }

        if (other.gameObject.CompareTag("Goal")) LevelState.levelManager.StopLevel(true);
    }
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            goto_p = true;
        }



        //Debug.Log("Exit" + other.gameObject.tag + goto_p);
    }

    public void Boost()
    {
        //boostIndicator.color = boostedColor;
        h_nav.speed = 25f;
        //h_nav.acceleration = 25f;
        boostButton.interactable = false;
        boosting = true;
        glowAlpha = .1f;
    }
    void StopBoost()
    {
        boostCooldown = 1f;
        boostIndicator.color = indicatorColor;
        boosting = false;
        h_nav.speed = 14f;
        boostGlowImage.color = Color.clear;
        //h_nav.acceleration = 16f;
    }
    void BoostingEffects()
    {
        if (glowAlpha >= 0.8f) factor = -1f;
        else if (glowAlpha <= 0.1) factor = 1f;
        glowAlpha += factor * 0.05f;
        boostGlowImage.color = new Color(1, 0, 0, glowAlpha);
        boostIndicator.color = new Color(1, 0, 0, glowAlpha);
    }

}
