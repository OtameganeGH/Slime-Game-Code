using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float  moveSpeed, distance1, distance2;
    public bool change = true,  isHorizontal = true;
    public Vector3 Point1, Point2;

    void Start()
    {
        //THIS IS FOR MOVING PLATOFRMS
        //THIS SETS TWO BOUNDS BASED ON OBJECTS CURRENT POSITION FOR OBJ TO MOVE BETWEEN
        //OBJECT CAN EITHER MOVE HORIZONTALLY OR VERTICALLY
        if (isHorizontal == true) {
            Point1 =  new Vector3(transform.position.x - distance1, transform.position.y, transform.position.z);
            Point2 =  new Vector3(transform.position.x + distance2, transform.position.y, transform.position.z);
                }
        if (isHorizontal == false){
            Point1 =  new Vector3(transform.position.x, transform.position.y + distance1, transform.position.z);
            Point2 =  new Vector3(transform.position.x, transform.position.y - distance2, transform.position.z);
                }
    }
   
    void Update()
    {
        //SWITCHES DIRECTION OF OBJ
        if (isHorizontal == true)
        {
            if (transform.position.x < Point1.x)//0
            {
                change = true;
            }
            if (transform.position.x > Point2.x)//8
            {
                change = false;
            }
        }
        if (isHorizontal == false)
        {
            if (transform.position.y < Point2.y)
            {
                change = true;
            }
            if (transform.position.y > Point1.y)
            {
                change = false;
            }
        }

        //MOVES OBJ IN A DIRECTION
        if (isHorizontal == true)
        {
            if (change == true)//right
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            if (change == false)//left
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
        }
        else if (isHorizontal == false) {
            if (change == true)//up
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            }
            if (change == false)//down
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            }

        }
    }
}
