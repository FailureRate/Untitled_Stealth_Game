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
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    { 
        if (gren.GetComponent<grenade>().Thrown == true)
        {
            direction = transform.position - gren.transform.position;
            Vector3 velocity = direction.normalized * speedVar/5;
            transform.position += velocity;
        }
        else if (playerDetected)
        {
            direction =  player.transform.position - transform.position;
            transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            Vector3 velocity = direction.normalized * speedVar / 5;
            transform.position += velocity;
        }

    }
}
