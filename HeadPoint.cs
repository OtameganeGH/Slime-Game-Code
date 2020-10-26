using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPoint : MonoBehaviour
{
    bool ropeActive;
    public GameObject curHook;
    public int nodeNum;
    public List<GameObject> Nodes = new List<GameObject>();
   
    void Update()
    {
        //FINDS DETAILS ABOUT THE HOOK AND THE ROPE
        ropeActive = GetComponentInParent< SegmentedRopeHook > ().ropeActive;        
        curHook = GameObject.FindGameObjectWithTag("Hook");
        if (curHook != null)
        {           
            nodeNum = curHook.GetComponent<SegmentedRope>().nodeCount;
            Nodes = curHook.GetComponent<SegmentedRope>().Nodes;
        }
       
        //THE HEAD CONNECTED TO THE ROPE FINDS A NODE AND ROTATES TO LOOK AT IT
        if(Nodes.Count> 0)
        {
            if (ropeActive == true && Nodes.Count > 4 && Nodes != null)
            {
                float AngleRad = Mathf.Atan2(Nodes[nodeNum - 3].transform.position.y - transform.position.y, Nodes[nodeNum - 3].transform.position.x - transform.position.x);
                float AngleDeg = (180 / Mathf.PI) * AngleRad;

                this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
            }
        }
    }
}
