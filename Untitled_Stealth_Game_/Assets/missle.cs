using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missle : MonoBehaviour
{
    public GameObject player;
    public GameObject explosion;
    private Vector3 orientation;
    public int moveSpeed;
    public float maxAngularSpeed;
    public bool homing;
    private Vector3 direction;

    private int countdown;
    // Start is called before the first frame update
    void Start()
    {
        if (moveSpeed <= 0) { moveSpeed = 1; }
        direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        //Destroy(this.gameObject, 5f);
        if (homing)
        {
          
            float turningAvialable = maxAngularSpeed * dt;
            // To do: Get the direction(dir) towards the target. The direction should be a unit vector.
            direction = player.transform.position - transform.position;
            float angle = Mathf.Acos(Vector3.Dot(direction, orientation) / (direction.magnitude * orientation.magnitude) * Mathf.Rad2Deg);

            //Debug.Log(angle);
            Debug.Log(turningAvialable);

            if (angle > turningAvialable)
            {
                direction.x = (orientation.x * Mathf.Cos(angle * Mathf.Deg2Rad)) - (orientation.y * Mathf.Sin(angle));
                direction.y = (orientation.x * Mathf.Sin(angle * Mathf.Deg2Rad)) + (orientation.y * Mathf.Cos(angle));
                Quaternion.Euler(direction);
            }
            direction.Normalize();
            // Update orientation 
            orientation = direction;



            Vector3 pos = transform.position;
            Vector3 velocity = orientation * moveSpeed;
            transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            //velocity = direction.normalized * moveSpeed / 5;

            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            Vector3 velocity = direction.normalized * moveSpeed / 5;
            transform.position += velocity * Time.deltaTime;
            if (countdown >= 150) { Instantiate(explosion, transform.position, transform.rotation); Destroy(this.gameObject); }
        }
        countdown++;
    }
}






//// To do: Update the current position (pos) using S_final = S_initial + v * t
//pos += velocity* dt;

//transform.position = pos;
//    }

//    void UpdateOrientation()
//{
//    Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
//    transform.eulerAngles = angle;
//}