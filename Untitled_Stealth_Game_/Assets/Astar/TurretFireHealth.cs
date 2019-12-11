using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFireHealth : MonoBehaviour
{
    [SerializeField]
    [Header("Health")] int health;
    [SerializeField]
    [Header("Countdown")] float countDown;
    [SerializeField]
    [Header("Rigidbody")] Rigidbody2D rb;
    [SerializeField]
    [Header("PlayerReffrence")] GameObject player;
    [SerializeField]
    [Header("BoxCollider")] BoxCollider2D collider;
    [SerializeField]
    [Header("Missle")] GameObject missle;
    [SerializeField]
    [Header("IsMissleHoming")] bool isMissleHoming;
    [SerializeField]
    [Header("PlayerAcknowledged")] bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            if (countDown >= 150)
            {
                countDown = 0;
                missle.GetComponent<missle>().player = player;
                missle.GetComponent<missle>().homing = isMissleHoming;
                missle.GetComponent<missle>().enemy = this.gameObject;
                Instantiate(missle, transform.position, transform.rotation);
            }
            countDown++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BOOM")
        {
            health--;
            if(health <= 0)
            {
                Destroy(this.gameObject);
            }
            playerDetected = true;
        }
    }
}
