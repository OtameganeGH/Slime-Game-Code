using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentedRope : MonoBehaviour
{
    public Vector2 destiny;
    public Vector2 pos;
    public Vector2 newPos;
    public float speed = 500.0f;
    public float distance = .25f;
    public float rayCheckDist = 0.10f;
    public GameObject nodePrefab;
    public GameObject lastNode;
    public GameObject player;
    public bool done = false;
    int vertexCount = 2;
    public List<GameObject> Nodes = new List<GameObject>();
    public LineRenderer lr;
    public int nodeCount;
    public int nodeLimit = 15;
    public Rigidbody2D rigid;
   
   
    void Start()
    {       
        lr = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();        
        player = GameObject.FindGameObjectWithTag("Player");        
        lastNode = transform.gameObject;
        Nodes.Add(transform.gameObject);
    }

   
   //HOOK SCRIPT
    void Update()
    {  
        if (done == false)
        {
            //MOVES THE HOOK TO POSITION SET IN PLAYER SCRIPT
           transform.position = Vector2.MoveTowards(transform.position, destiny, speed);
            RayCheck();    
        }

        if((Vector2)transform.position != destiny)
        {
            //IF THE HOOK IS STILL TRAVELLING THIS SPAWNS ANOTHER NODE EVERY FEW UNITS
            if (Vector2.Distance(player.transform.position, lastNode.transform.position)>distance)
            {
                CreateNode();
            }
        }else if (done == false)
        {
            done = true;
            while((Vector2.Distance(player.transform.position, lastNode.transform.position) > distance)){
                CreateNode();
            }
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }

        //THIS DELETES THE HOOK AND ROPE IF THERE ARE TOO MANY NODES
       //THIS IS TO LIMIT THE ROPE LENGTH
        if (nodeCount >= nodeLimit)
        {
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
            Destroy(gameObject);
        }

        //IF THE PLAYER DIES THE HOOK WILL BE DESTROYED TOO
         if (player == null)
        {
            Destroy(gameObject);
        }
       RenderLine();
        
    }
 



    void RenderLine()
    {
        //CREATES A LIST OF LINES THAT ARE GENERATED BETWEEN NODES
       //RENDERS EACH LINE ON THE LIST
        lr.SetVertexCount(vertexCount);
        int i;
        for ( i = 0; i < Nodes.Count; i++)
        {
            lr.SetPosition(i, Nodes[i].transform.position);
        }
        nodeCount = i;
        if (lr != null)
        {
            lr.SetPosition(i, player.transform.position);
        }
    }

    void CreateNode()
    {
        //CALCULATES THE DISTANCE BETWEEN THE PLAYER AND THE HOOK, GENERATES A NODE EVERY TIME THAT DISTANCE PASSES A CERTAIN VALUE
        //EACH NODE IS SPAWNED WITH A HINGE JOINT THAT CONNECTS TO THE NODE PREVIOUS
        
        Vector2 pos2Create = player.transform.position - lastNode.transform.position;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += (Vector2)lastNode.transform.position;
        GameObject obj = (GameObject)Instantiate(nodePrefab, pos2Create, Quaternion.identity);
        obj.transform.SetParent(transform);
        lastNode.GetComponent<HingeJoint2D>().connectedBody = obj.GetComponent<Rigidbody2D>();
        lastNode = obj;
        Nodes.Add(lastNode);
 
        vertexCount++;
     }

    void RayCheck()
    {
        //THIS FUNCTION ACTS AS FAKE COLLISION
        //A SMALL RAY IS CAST IN 3 DIRECTIONS OF THE HOOK TO READ A TAG ON A GAMEOBJECT
        //IF THE TAG IS NOT SET TO 'GRABABLE' THE HOOK IS DESTROYED
        pos = new Vector2(transform.position.x, transform.position.y);
        Vector3 direction = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        RaycastHit2D fakeCollisionup= Physics2D.Raycast(pos, (transform.TransformDirection(Vector2.up)), rayCheckDist);
        RaycastHit2D fakeCollisionleft = Physics2D.Raycast(pos, (transform.TransformDirection(Vector2.left)), rayCheckDist);
        RaycastHit2D fakeCollisionright = Physics2D.Raycast(pos, (transform.TransformDirection(Vector2.right)), rayCheckDist);

        //COMBINING THESE IF STATEMENTS TOGETHER WOULD CREATE NUMEROUS ERRORS, INCLUDING NOT SPAWNING NODES FOR THE ROPES

        //RAY LOOKS UP
        if (fakeCollisionup.collider != null && fakeCollisionup.transform.gameObject.tag == "NOGRAB")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        }
        else if (fakeCollisionup.collider != null && fakeCollisionup.transform.gameObject.tag == "Coin")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        }
        else if (fakeCollisionup.collider != null && fakeCollisionup.transform.gameObject.tag == "Death")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        }

        //RAY LOOKS LEFT
        if (fakeCollisionleft.collider != null && fakeCollisionleft.transform.gameObject.tag == "NOGRAB")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        } else if (fakeCollisionleft.collider != null && fakeCollisionleft.transform.gameObject.tag == "Coin")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        } else if (fakeCollisionleft.collider != null && fakeCollisionleft.transform.gameObject.tag == "Death")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        }


        //RAY LOOKS RIGHT
        if (fakeCollisionright.collider != null && fakeCollisionright.transform.gameObject.tag == "NOGRAB")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        } else if (fakeCollisionright.collider != null && fakeCollisionright.transform.gameObject.tag == "Coin")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        } else if (fakeCollisionright.collider != null && fakeCollisionright.transform.gameObject.tag == "Death")
        {
            Destroy(gameObject);
            player.GetComponent<SegmentedRopeHook>().head.SetActive(false);
        }
    }
}
