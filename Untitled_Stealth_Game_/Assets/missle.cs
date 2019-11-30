using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missle : MonoBehaviour
{
    public GameObject player;
    private Vector3 orientation;
    public int moveSpeed;
    public bool homing;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        if (moveSpeed <= 0) { moveSpeed = 1; }
        direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5f);
        if (homing)
        {
            direction = player.transform.position - transform.position;
            transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            Vector3 velocity = direction.normalized * moveSpeed / 5;

            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            Vector3 velocity = direction.normalized * moveSpeed / 5;
            transform.position += velocity * Time.deltaTime;
        }
    }
}
