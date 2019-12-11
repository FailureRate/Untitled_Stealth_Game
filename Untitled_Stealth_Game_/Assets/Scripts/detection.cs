using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detection : MonoBehaviour
{
    public basicEnemy enemy;
    private int countdown;
    private bool playerLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLeft == true)
        {
            countdown++;
            if (countdown >= 200) { enemy.playerDetected = false; countdown = 0; playerLeft = false; }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.playerDetected = true;
            playerLeft = false;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerLeft = true;
        }
    }
}
