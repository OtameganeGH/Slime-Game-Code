using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.15f;
    private Vector3 offset = new Vector3(0.1f, 0.1f, 0);
    Vector3 refVel = Vector3.zero;

    void LateUpdate()
    {
        //THIS LETS THE CAMERA FOLLOW A SLIME AT A CERTAIN SPEED, 
        //CHANGE SMOOTHING VARIABLE TO SPEED UP OR SLOW DOWN THE CAMERA
        Vector3 posAim = new Vector3(target.transform.position.x, target.transform.position.y + 1.5f, -10);
        Vector3 smoothAim = Vector3.Lerp (transform.position, posAim, smoothing * Time.deltaTime);        
        transform.position = smoothAim;
     
    }

}
