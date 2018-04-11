using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFader : MonoBehaviour {


    Transform camTransform;
    float distance;
    Renderer rend;
    float maxSolidDistance;
    float newAlpha;
    Transform treeTransform;

    void Start () {
        
        rend = GetComponent<Renderer>();
        newAlpha = 1f;
        maxSolidDistance = 50;
        treeTransform = GetComponentInParent<Transform>();
    }
	
	void FixedUpdate () {

        if (!LevelState.levelManager.levelStart) return;

        //float newAlpha = 1f;
        distance = Vector3.Distance(Camera.main.transform.position, treeTransform.position);


        if (distance < maxSolidDistance)
        {
            newAlpha -= 0.1f;
        }
        else
        {
            newAlpha = 1f;
        }

        newAlpha = Mathf.Clamp(newAlpha, 0.1f, 1f); // limits transparency from 0.1f to 1f
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b,
            newAlpha);
        
    }

}
