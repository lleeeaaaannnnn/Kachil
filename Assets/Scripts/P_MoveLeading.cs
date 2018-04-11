using UnityEngine;
using System.Collections;
using CnControls;

public class P_MoveLeading : MonoBehaviour
{

    public H_Health h_health;

    float moveX, moveZ;
    public float moveSpeed = 20f;
    Rigidbody p_Rigidbody;
    UnityEngine.AI.NavMeshAgent p_nav;



    void Start()
    {
        p_nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        p_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        if (h_health.currentHealth > 0)
        {
            Moving();
        }



    }
    void Moving()
    {

        moveX = CnInputManager.GetAxis("Horizontal");
        moveZ = CnInputManager.GetAxis("Vertical");


        if (moveX != 0 || moveZ != 0)
        {

            p_nav.Move(new Vector3(moveX, 0f, moveZ) * Time.deltaTime * moveSpeed);
            //transform.Rotate(new Vector2(moveX, moveZ));
            Quaternion target_rotation = Quaternion.LookRotation(new Vector3(moveX, 0f, moveZ));
            p_Rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, target_rotation, 10f));

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {

        }
    }
}
