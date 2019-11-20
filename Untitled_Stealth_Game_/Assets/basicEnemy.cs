using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public GameObject gren;
    private Vector3 orientation;
    public float speedVar;
    public GameObject player;
    public bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gren.GetComponent<grenade>().Thrown == true)
        {
            Vector3 direction = transform.position - gren.transform.position;
            Vector3 velocity = direction.normalized * speedVar/5;
            transform.position += velocity;
        }
        else if (playerDetected)
        {
            Vector3 direction =  player.transform.position - transform.position;
            Vector3 velocity = direction.normalized * speedVar / 5;
            transform.position += velocity;
        }
    }
}
