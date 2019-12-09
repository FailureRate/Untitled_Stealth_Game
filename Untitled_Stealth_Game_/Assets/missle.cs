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
            float dt = Time.deltaTime;

            Vector3 targetVelocity = enemy.gameObject.GetComponent<basicEnemy>().velocity;
            Vector3 vr = targetVelocity - velocity;
            Vector3 sr = player.transform.position - transform.position;
            float tc = sr.magnitude / vr.magnitude;
            Vector3 st = player.transform.position + targetVelocity * tc;

            Vector3 dir = st - transform.position;
            dir.Normalize();


            float dotProduct = Vector3.Dot(dir, orientation.normalized);
            float desiredAngle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg; // desired angle in degrees
            float actualAngle = maxAngularSpeed * dt; // actual angle in degrees


            if (desiredAngle > actualAngle)
            {
                Vector3 rightNormal = new Vector3(orientation.y, -orientation.x, 0);
                bool isRightDir = Vector3.Dot(rightNormal, dir) > 0;
                if (isRightDir)
                {
                    actualAngle = -actualAngle;
                }
                orientation = Quaternion.Euler(0, 0, actualAngle) * orientation;
            }
            else
            {
                orientation = dir;
            }
            UpdateOrientation();

            Vector3 pos = transform.position;
            velocity = orientation * moveSpeed;
            pos += velocity * dt;

            transform.position = pos;
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
    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
