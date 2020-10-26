using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public int ScoreFind;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DETECTS IF THE PLAYER HAS COLLIDED WITH THE COIN
        //COIN IS DELETED AND SCORE INCREASED BY 1
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("Canvas").GetComponent<UIHandler>().score = GameObject.Find("Canvas").GetComponent<UIHandler>().score + 1;
            Destroy(this.gameObject);
        }
    }
}
