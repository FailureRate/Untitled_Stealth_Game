using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    [Header("Box Collider")]GameObject[] doorFrames;
    [SerializeField]
    [Header("Open Position")] Transform[] openPositions; // this must be assigned correctly in editor
    [SerializeField]
    [Header("Closed Position")] Vector3[] closePositions;

    [SerializeField]
    [Header("Door Button")] BoxCollider2D doorTriggerObj;

    // variables
    [SerializeField]
    [Header("Door Speed")] float speed;
    [SerializeField]
    [Header("TimerOpen")] float timerMax = 5;
    [SerializeField]
    [Header("TimerOpen")] float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = timerMax;
        int i = 0;
        foreach (GameObject door in doorFrames)
        {
            closePositions[i] = door.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= timerMax)
        {
            CloseDoor();
        }
        else
        {
            timer += 1 * Time.deltaTime;
        }
    }
    private void OpenDoors() {
        int i = 0;
        foreach(GameObject door in doorFrames)
        {
            door.GetComponent<BoxCollider2D>().isTrigger = true;
            //Vector3 direction = openPositions[i].position - closePositions[i].position;
            //Vector3 velocity = direction.normalized * speed / 5;
            door.transform.position = openPositions[i].position;
            i++;
        }
        timer = 0;
    }

    private void CloseDoor()
    {
        int i = 0;
        foreach (GameObject door in doorFrames)
        {
            //Vector3 direction =  closePositions[i].position - openPositions[i].position;
            //Vector3 velocity = direction.normalized * speed / 5;
            door.transform.position = closePositions[i];
            i++;
            door.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Baddie")
        {
            OpenDoors();
        }
    }
}
