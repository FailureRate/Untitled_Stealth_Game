using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public GameObject gren;
    public GameObject missle;
    private Vector3 orientation;
    public float speedVar;
    public GameObject player;
    public bool playerDetected;
    private Vector3 direction;
    public bool followingPath;
    public Vector3 velocity;

    public Vector3[] pathNodes;
    [SerializeField]
    private int pathnode;
    private int countdown;
    public Rigidbody2D rb;
    private bool InRange;
    private bool detected;

    public bool missleHome;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
        pathnode = 0;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gren.GetComponent<grenade>().Thrown == true && (InRange == true || detected == true))
        {
                rb.velocity = Vector2.zero;
                direction = transform.position - gren.transform.position;
                transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
                velocity = direction.normalized * speedVar;
                rb.velocity += new Vector2(direction.x, direction.y) * (speedVar+0.3f);
            //transform.position += velocity;
            detected = true;
            playerDetected = true;
        }
        else if (playerDetected)
        {
            rb.velocity = Vector2.zero;
            movetoward(player.transform.position);
            //intercept();
            followingPath = false;
            if (countdown >= 300)
            {
                countdown = 0;
                missle.GetComponent<missle>().player = player;
                missle.GetComponent<missle>().homing = missleHome;
                missle.GetComponent<missle>().enemy = this.gameObject;
                Instantiate(missle, transform.position, transform.rotation);
            }
            countdown++;
        }
        else if (pathnode < pathNodes.Length && followingPath)
        {
            rb.velocity = Vector2.zero;
            movetoward(pathNodes[pathnode]);
            if ((pathNodes[pathnode].x + 0.5 > transform.position.x && transform.position.x > pathNodes[pathnode].x - 0.5) && (pathNodes[pathnode].y + 0.5 > transform.position.y && transform.position.y > pathNodes[pathnode].y - 0.5))
            { pathnode++; rb.velocity = Vector2.zero; }
            if (pathnode >= pathNodes.Length) { pathnode = 0; }
        }
        else
        {
            followingPath = true;
            detected = false;
        }

        if (gren.transform.position.x <= transform.position.x + 5 && gren.transform.position.x >= transform.position.x - 5 && gren.transform.position.y <= transform.position.y + 5 && gren.transform.position.y >= transform.position.y - 5)
        {
            InRange = true;
        }
        else { InRange = false; }

    }

    void movetoward(Vector3 position)
    {
        direction = position - transform.position;
        transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
        rb.velocity += new Vector2(direction.x, direction.y) * speedVar;
        //transform.position += velocity;
    }

    void intercept()
    {
        Vector2 vr = player.GetComponent<playerMovement>().rb.velocity - rb.velocity;
        Vector3 sr = player.transform.position - transform.position;
        float tc = sr.magnitude / vr.magnitude;
        Vector3 st = player.transform.position + transform.position * tc;
        direction = st - transform.position;
        transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
        rb.velocity += new Vector2(direction.x, direction.y) * speedVar;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BOOM"))
        {
            Destroy(this.gameObject);
        }
    }
}
