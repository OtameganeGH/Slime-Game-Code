using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public float force = 6000.0f;//Why does this have to be so high >:(
    public bool jumpToggle = true;
    public GameObject block;
    private float tilt;
   
    

    void Update()
    {
        //INPUT FOR TILT CONTROLS, THIS LETS THE SLIME SLIDE AND SWING
        tilt = (Input.acceleration.x * 1.5f) * force;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(tilt, 0));



        //DEV PC CONTROLS FOR TESTING
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-force, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space)&& jumpToggle == true)
        {
            jumpToggle = false;
            block.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + Vector2.up * 7;           
        }
        
    }
    
}
