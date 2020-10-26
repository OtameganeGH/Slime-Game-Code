using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SegmentedRopeHook : MonoBehaviour
{
    public GameObject hook;
    GameObject curHook;
    public bool ropeActive;
    public GameObject head;   
    int nodeNum;   
    public Vector2 spot;
    public List<GameObject> newNodes = new List<GameObject>();
    public GameObject particle;
    public GameObject player;
    public bool canFire = true;
   

   //PLAYER SCRIPT 
    void Update()
    {
        if (curHook != null)
        {
            newNodes = curHook.GetComponent<SegmentedRope>().Nodes;
            nodeNum = curHook.GetComponent<SegmentedRope>().nodeCount;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //CHECKS ON INPUT IF THERE IS ALREADY A ROPE IN THE SCENE AND IF THE PLAYER IS CLICKING ON A BUTTON
            //IF PLAYER IS CLICKING ON A BUTTON A HOOK WONT BE SPAWNED
            //TURNS ON THE ROTATING HEAD PIECE
            if (ropeActive == false &! EventSystem.current.IsPointerOverGameObject(0) && canFire == true)
            {
                FireRope();
                head.SetActive(true);
            }         
        }
         
        //WHEN RELEASING INPUT THE CREATED HOOK IS DESTROYED
        //TURNS OFF ROTATING HEAD PIECE
        //CLEARS NODE LIST TO PREVENT ERRORS WHEN FETCHING NODES
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(curHook);
            ropeActive = false;
            head.SetActive(false);
            newNodes.Clear();
        }       
    }
    void FireRope()
    {

        //THIS FUNCTION SHOOTS THE GRAPPLING HOOK FROM THE PLAYER TOWARDS THE MOUSE POSITION
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Hookspawn = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        Vector3 position = transform.position;
        Vector3 direction = mousePosition - position;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);
        //THE RAY IS FIRED TOWARDS MOUSE POS AND FIRST COLLIDER HIT ACTS AS THE TARGER DESTINATION FOR THE GRAPPLING HOOK
        if (hit.collider != null)
        {           
            spot = hit.point;
            Vector2 destiny = hit.point;           
            curHook = (GameObject)Instantiate(hook, Hookspawn, Quaternion.identity);
            curHook.GetComponent<SegmentedRope>().destiny = destiny;
            ropeActive = true;
        }

    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //DEATH MECHANIC SO ANY OBJECT WITH A DEATH TAG WILL KILL THE PLAYER AND DISPLAY THE DEATH MSG
        //PLAYER IS NOT DESTROYED SO CAMERA CAN STILL TRACK TO SHOW CAUSE OF DEATH
        if (collision.gameObject.tag == "Death")
        {
            GameObject.Find("Canvas").GetComponent<UIHandler>().Death1.SetActive(true);
            GameObject.Find("Canvas").GetComponent<UIHandler>().Death2.SetActive(true);            
            Instantiate(particle, transform.position, transform.rotation);           
            Destroy(curHook);            
            gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Finish")        {

            gameObject.SetActive(false);
            GameObject.Find("Canvas").GetComponent<UIHandler>().SaveHighScore();
            GameObject.Find("Canvas").GetComponent<UIHandler>().FinishScreen();
        }
    }
}
