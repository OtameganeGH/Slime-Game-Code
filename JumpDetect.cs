using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetect : MonoBehaviour
{
    public GameObject block;   

    private void OnTriggerStay2D(Collider2D collision)
    {
        // CHECKS IF PLAYER LANDS ON A PLATOFRM THEY CAN JUMP OFF
        //IF SO TURNS ON JUMP BUTTON
       
        if (collision.gameObject.tag == "Floor")
        {           
            GameObject.Find("slimey").GetComponent<PlayerMove>().jumpToggle = true;
            
            block.SetActive(false);
        }    

       
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        // CHECKS IF PLAYER LEAVES A PLATFORM WITHOUT JUMPING
        //IF SO TURNS OFF JUMP BUTTON
        if (collision.gameObject.tag == "Floor")
        {          
            GameObject.Find("slimey").GetComponent<PlayerMove>().jumpToggle = false;
            block.SetActive(true);
        }
    }

}
