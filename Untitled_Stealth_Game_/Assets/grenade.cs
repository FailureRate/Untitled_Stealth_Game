using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public Vector3 destination;
    private Vector3 orientation;

    public GameObject player;
    public int speedVar;
    public int count;
    public bool Thrown = false;
    public bool destinationLocked;
    // Start is called before the first frame update
    void Start()
    {
        if (speedVar <= 0) { speedVar = 1; }   
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationLocked == false)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = transform.position.z - Camera.main.transform.position.z;
            destination = Camera.main.ScreenToWorldPoint(pos);
            destinationLocked = true;
        }

        if (Thrown == true)
        {
            Vector3 direction = destination - transform.position;
            Vector3 velocity = direction * speedVar;
            transform.position += velocity * Time.deltaTime;
            if (count >= 200) { Thrown = false; }
            count++;
        }
        if (Thrown == false) { transform.position = player.transform.position; count = 0; destinationLocked = false; }
        if (Input.GetMouseButtonDown(1) && this.tag == "Grenade") { Thrown = true;}

    }
}
