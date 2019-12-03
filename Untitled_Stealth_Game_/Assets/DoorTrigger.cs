using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] public bool isOpen = false;
    [SerializeField] private int timeOpen;
    // Start is called before the first frame update

    private void Start()
    {
        timeOpen = 4;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("In DoorKey col");
            isOpen = true;
            StartCoroutine(OpenDoor(timeOpen));
        }

    }
    private IEnumerator OpenDoor(int openTime_)
    {
        yield return new WaitForSeconds(openTime_);
        isOpen = false;
    }
}
