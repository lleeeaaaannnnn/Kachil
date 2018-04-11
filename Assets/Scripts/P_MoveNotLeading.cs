using UnityEngine;
using System.Collections;
using CnControls;

public class P_MoveNotLeading : MonoBehaviour
{

    public H_Health h_health;

    float moveX, moveZ;
    public float moveSpeed = 20f;
    Rigidbody p_Rigidbody;
    Animator p_anim;
    Vector3 movement;
    UnityEngine.AI.NavMeshAgent p_nav;
    Transform h_transform;

    void Start()
    {
        p_nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        p_Rigidbody = GetComponent<Rigidbody>();
        p_anim = GetComponent<Animator>();
        h_transform = h_health.gameObject.transform;
    }

    void FixedUpdate()
    {

        Moving();

    }
    void Moving()
    {

        moveX = CnInputManager.GetAxis("Horizontal");
        moveZ = CnInputManager.GetAxis("Vertical");
        
        // Only allow movement if Hero is alive and Level has started
        if (h_health.currentHealth > 0 && (moveX != 0 || moveZ != 0) && LevelState.levelManager.levelStart)
        {

            movement = new Vector3(moveX, 0f, moveZ).normalized;
            //movement = transform.TransformDirection(moveX, 0f, moveZ);


            // Check Distance of Hero and Player
            Vector3 distance = (transform.position - h_transform.position);

            // Force distance to stay at 35 by zeroing movement values
            if (distance.magnitude > 35f)
            {
                movement = Vector3.zero;
            }
            // Move via Navmesh and set animation
            p_nav.Move(movement * Time.deltaTime * moveSpeed);
            p_anim.SetBool("isRunning", true);

            // Turning
            Quaternion target_rotation = Quaternion.LookRotation(movement);
            p_Rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, target_rotation, 10f));


        }
        else 
        {
            // Stop animation
            p_anim.SetBool("isRunning", false);
        }


    }

}
