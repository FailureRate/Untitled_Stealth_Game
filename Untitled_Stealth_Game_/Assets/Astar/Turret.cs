using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Tilemap map;
    public float speed = 5;
    //public GameObject hitSpot;
    public GameObject player;
    public bool moveToHit;
    public bool stopMovementForNow;
    public int countdown;

    AStar aStarPathfinder = new AStar();
    public enum State { MOVE, IDLE };
    State state;

    List<Node> pathDisplay = new List<Node>();

    // Use this for initialization
    void Start()
    {
        stopMovementForNow = false;
        moveToHit = false;
        state = State.IDLE;
        aStarPathfinder.Init(map);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        if(stopMovementForNow == true)
        //if (map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f)) != null)
        {
            // handle mouse input
            countdown++;

            if (countdown >= 200)
            {
                moveToHit = true;
            }

            if (state == State.IDLE && moveToHit == true)
            {
                state = State.MOVE;
                //Vector3 pos = Input.mousePosition;
                pos = player.transform.position;
                //pos = Camera.main.ScreenToWorldPoint(pos);

                Debug.Log("x: " + Mathf.FloorToInt(pos.x + 0.5f).ToString() + " y: " + Mathf.FloorToInt(pos.y + 0.5f).ToString());
                Node targetNode = map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));
                Debug.Log(targetNode.tileType.ToString());

                if (targetNode.tileType == Node.TileType.GRASS)
                {
                    StartCoroutine(SearchPathAndMove(targetNode));
                }
                else
                {
                    state = State.IDLE;
                    Debug.Log("WALL - can't move to here");
                }
                moveToHit = true;
                countdown = 0;

            }
        }

        //for (int i = 0; i < pathDisplay.Count - 1; i++)
        //{
        //    Node n1 = pathDisplay[i];
        //    Node n2 = pathDisplay[i + 1];
        //    Debug.DrawLine(new Vector3(n1.x, n1.y, 0), new Vector3(n2.x, n2.y, 0));
        //}
    }

    IEnumerator SearchPathAndMove(Node target)
    {
        Vector3 pos = transform.position;
        Node start = map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));

        // Get the shortest path between start and target using astar algorithm
        List<Node> path = aStarPathfinder.Search(start, target);
        pathDisplay = path;

        foreach (Node n in path)
        {
            Vector3 nextPos = new Vector3(n.x, n.y, 0);
            Vector3 dir = nextPos - transform.position;
            Vector3 moved = Vector3.zero;
            while (dir.magnitude > moved.magnitude)
            {
                pos += speed * dir.normalized * Time.deltaTime;
                moved += speed * dir.normalized * Time.deltaTime;
                transform.position = pos;
                yield return null;
            }

            yield return null;
        }

        // set state to IDLE in order to enable next movement
        state = State.IDLE;
    }

}
