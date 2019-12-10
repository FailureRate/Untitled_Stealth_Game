using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Tilemap map;
    public float speed = 5;
    public GameObject player;
    private bool moveToHit;
    private int targetNode;
    private int countdown;
    public List<GameObject> distinations = new List<GameObject>();
    static int randomIndex = 0;
    AStar aStarPathfinder = new AStar();
    public enum State { MOVE, IDLE };
    State state;

    // List<Node> pathDisplay = new List<Node>();

    // Use this for initialization
    void Start()
    {
        targetNode = PickNode();
        moveToHit = false;
        state = State.IDLE;
        aStarPathfinder.Init(map);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = new Vector3(distinations[targetNode].transform.position.x, distinations[targetNode].transform.position.y, 0);
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        if (map.GetNode(Mathf.FloorToInt(playerPosition.x + 0.5f), Mathf.FloorToInt(playerPosition.y + 0.5f)) != null)
        {
            if (map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f)) != null)
            {
                // handle mouse input
                countdown++;

                if (countdown >= 50)
                {
                    moveToHit = true;
                }

                if (state == State.IDLE && moveToHit == true)
                {
                    moveToHit = false;
                    state = State.MOVE;

                    Node targetNode = map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));

                    if (targetNode.tileType == Node.TileType.GRASS)
                    {
                        StartCoroutine(SearchPathAndMove(targetNode));
                    }
                    else
                    {
                        state = State.IDLE;
                        Debug.Log("WALL - can't move to here");
                    }


                }
            }
        }

    }

    IEnumerator SearchPathAndMove(Node target)
    {
        Vector3 position = transform.position;
        Node start = map.GetNode(Mathf.FloorToInt(position.x + 0.5f), Mathf.FloorToInt(position.y + 0.5f));

        // Get the shortest path between start and target using astar algorithm
        List<Node> path = aStarPathfinder.Search(start, target);

        foreach (Node n in path)
        {
            Vector3 nextPos = new Vector3(n.x, n.y, 0);
            Vector3 dir = nextPos - transform.position;
            Vector3 moved = Vector3.zero;
            while (dir.magnitude > moved.magnitude)
            {
                position += speed * dir.normalized * Time.deltaTime;
                moved += speed * dir.normalized * Time.deltaTime;
                transform.position = position;
                yield return null;
            }

            yield return null;
        }

        state = State.IDLE;

        targetNode = PickNode();

    }
    int PickNode()
    {

        if (distinations[targetNode + 1] == null)
        {
            randomIndex = 0;
            moveToHit = false;
            countdown = 0;
            return randomIndex;
        }
        else
        {
            randomIndex++;
            moveToHit = false;
            countdown = 0;
            return randomIndex;
        }
    }

}
