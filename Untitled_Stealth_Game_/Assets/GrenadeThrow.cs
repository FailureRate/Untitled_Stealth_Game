using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gren;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 pos = Input.mousePosition;
            //Debug.Log("Pos:" + Camera.main.ScreenToWorldPoint(pos));
            //pos.z = transform.position.z - Camera.main.transform.position.z;
            //gren.GetComponent<grenade>().destination = Camera.main.ScreenToWorldPoint(pos);
            gren.GetComponent<grenade>().Thrown = true;
        }
    }
}
