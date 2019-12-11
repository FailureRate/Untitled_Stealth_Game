using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missle : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject explosion;
    private Vector3 orientation;
    public int moveSpeed;
    private float maxAngularSpeed = 90.0f;
    public bool homing;
    private Vector3 direction;
    public Vector3 velocity;

    private int countdown;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        if (moveSpeed <= 0) { moveSpeed = 1; }
        direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(this.gameObject, 5f);
        if (homing)
        {
            float turningAvialable = maxAngularSpeed * Time.deltaTime;
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
            Vector3 velocity = orientation * moveSpeed / 2;
            transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            //velocity = direction.normalized * moveSpeed / 5;

            transform.position += velocity * Time.deltaTime;
            if (countdown >= 400) { Destroy(this.gameObject); }
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            Vector3 velocity = direction.normalized * moveSpeed;
            transform.position += velocity * Time.deltaTime;
            if (countdown >= 150) { Instantiate(explosion, transform.position, transform.rotation); Destroy(this.gameObject); }
        }
        countdown++;
    }
    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
