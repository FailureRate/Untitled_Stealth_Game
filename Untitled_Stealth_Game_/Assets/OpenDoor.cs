using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] public GameObject openPosition;
    [SerializeField] public DoorTrigger doorTriggerObj;
    [SerializeField] private int speed;
    private Vector3 close;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
        close = transform.position;//starting position
    }

    // Update is called once per frame
    void Update()
    {
        if (doorTriggerObj.isOpen == true) {
            OpenTheDoor();
        }
        if (doorTriggerObj.isOpen == false)
        {
            CloseDoor();
        }

    }
    private void OpenTheDoor() {
        direction = openPosition.transform.position - transform.position;        
        Vector3 velocity = direction.normalized * speed / 5;
        transform.position += velocity;
    }

    private void CloseDoor()
    {
        direction = close - transform.position;
        Vector3 velocity = direction.normalized * speed / 5;
        transform.position += velocity;
    }
}
