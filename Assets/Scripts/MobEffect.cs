using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEffect : MonoBehaviour {

    E_Movement e_mov1;                                       // This Enemy instance

    void Start()
    {
        e_mov1 = GetComponentInParent<E_Movement>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyMobEffect"))
        {

            E_Movement e_mov2 = other.gameObject.GetComponentInParent<E_Movement>(); // Collided with, Enemy instance
            if (e_mov2.startChasing)
            {
                e_mov1.InitiateChase();

            }
        }
    }
}
