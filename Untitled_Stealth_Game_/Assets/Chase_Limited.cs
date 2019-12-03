using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Limited : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float maxAngularSpeed;

    Vector3 orientation = Vector3.up;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        // To do: Complete Update() to chase the targe with the angular speed limit (maxAngularSpeed)
        // * recommended steps: 
        // 1. implement the chasing that you did in Lab3.1
        // 2. limit the rotation (orientation). The rotation angle cannot be over the max angle based on maxAngularSpeed.
        float dt = Time.deltaTime;

        float turningAvialable = maxAngularSpeed * dt;
        // To do: Get the direction(dir) towards the target. The direction should be a unit vector.
        Vector3 dir = target.gameObject.transform.position - transform.position;
        float angle = Mathf.Acos(Vector3.Dot(dir, orientation) / (dir.magnitude * orientation.magnitude) * Mathf.Rad2Deg);

        Debug.Log(angle);
        Debug.Log(turningAvialable);

        if (angle > turningAvialable)
        {
            dir.x = (orientation.x * Mathf.Cos(angle * Mathf.Deg2Rad)) - (orientation.y * Mathf.Sin(angle));
            dir.y = (orientation.x * Mathf.Sin(angle * Mathf.Deg2Rad)) + (orientation.y * Mathf.Cos(angle));
            Quaternion.Euler(dir);
        }
        dir.Normalize();
        // Update orientation 
        orientation = dir;
        UpdateOrientation();



        Vector3 pos = transform.position;
        Vector3 velocity = orientation * speed;
        // To do: Update the current position (pos) using S_final = S_initial + v * t
        pos += velocity * dt;

        transform.position = pos;
    }

    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
