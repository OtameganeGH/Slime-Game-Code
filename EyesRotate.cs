using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesRotate : MonoBehaviour
{
    Quaternion rot;
   
    
    void Awake()
    {
        rot = transform.rotation;
    }

    //THIS KEEPS THE EYES FROM ROTATING FOR VISUAL EFFECT
    void Update()
    {
        transform.rotation = rot;
    }
}
