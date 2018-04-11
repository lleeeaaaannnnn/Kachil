using UnityEngine;
using System.Collections;
using CnControls;

public class FollowPlayer : MonoBehaviour {

    public Transform p_Transform;
    public Transform h_Transform;
    
    [SerializeField] float cam_y_offset;
    [SerializeField] float cam_z_offset;
    [SerializeField] float camEulerAngle_x;
    [SerializeField] float cameraFollowSpeed = 2f;
    [SerializeField] float distanceBeforeZoom;
    [SerializeField] float camZoomFactor = 1.5f;

    Vector3 offset;
    Quaternion angle;
    float cam_y;
    float cam_z;
    float cam_x;
    float distance;


    void Start()
    {
        Vector3 midPoint = (p_Transform.position + h_Transform.position) / 2; // midpoint between hero and player
        offset = transform.position - midPoint;
    }


    void FixedUpdate () {

        //cam_y_offset = camEulerAngle_x;
        //cam_z_offset = camEulerAngle_x * -1;


        if (!LevelState.levelManager.levelStart) return;

        Vector3 midPoint = (p_Transform.position + h_Transform.position) / 2; // midpoint between hero and player

        distance = (p_Transform.position - h_Transform.position).magnitude; // absolute distance between Hero and Player

        cam_x = midPoint.x;
        cam_y = (distance > distanceBeforeZoom) ? midPoint.y + (camZoomFactor * cam_y_offset) : midPoint.y + cam_y_offset; // if distance is greater than allowed, zoom out by certain factor
        cam_z = (distance > distanceBeforeZoom) ? midPoint.z + (camZoomFactor * cam_z_offset) : midPoint.z + cam_z_offset; // if distance is greater than allowed, zoom out by certain factor

        Vector3 newPosition = new Vector3(cam_x, cam_y, cam_z);
        transform.position = Vector3.Lerp(transform.position, newPosition, cameraFollowSpeed * Time.deltaTime); // adjust camera to new position by certain speed

        //transform.position = offset + midPoint;

        angle.eulerAngles = new Vector3(camEulerAngle_x, 0f, 0f);
        transform.rotation = angle; // maintain camera angle
    }


}
