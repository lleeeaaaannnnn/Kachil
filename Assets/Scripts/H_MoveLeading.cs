using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class H_MoveLeading : MonoBehaviour {

    public Transform[] pathPoints;


    UnityEngine.AI.NavMeshAgent h_nav;
    int currDest = 0;
    H_Health h_health;
    Animator h_anim;


    void Start()
    {
        h_nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        h_health = GetComponent<H_Health>();
        h_anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (h_health.isDead || !LevelState.levelManager.levelStart)
        {
            h_anim.SetBool("isRunning", false);
            h_nav.SetDestination(transform.position);
            return;
        }


        h_nav.SetDestination(pathPoints[currDest].position);
        h_anim.SetBool("isRunning", true);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");

        if (other.gameObject.CompareTag("PathPoint") && currDest < pathPoints.Length)
        {
            other.gameObject.SetActive(false);
            currDest++;
            //Debug.Log("Point reached");
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            // Level end here.. use :manager:
            LevelState.levelManager.StopLevel(true);
            //Debug.Log("Level Complete!");
        }
        
    }
}
