using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDripSpin : MonoBehaviour
{

    public GameObject curHook;
    public int nodeNum;
    public List<GameObject> Nodes = new List<GameObject>();
    

    // DROPPLET CONENCTED TO HOOK ROTATES TOWARDS FIRST NODE TO APPEAR AS PART OF THE LINE
    void Update()
    {   curHook = GameObject.FindGameObjectWithTag("Hook");
        nodeNum = curHook.GetComponent<SegmentedRope>().nodeCount;
        Nodes = curHook.GetComponent<SegmentedRope>().Nodes;

        float AngleRad = Mathf.Atan2(Nodes[1].transform.position.y - transform.position.y, Nodes[1].transform.position.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg +90 );

    }
}
