using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKeys : MonoBehaviour
{
    [SerializeField] public AllKeysCollectedBool setBool;
    [SerializeField] public Audio audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            setBool.GetComponent<AllKeysCollectedBool>().keyCount++;

            audio.playSound(4);
            gameObject.SetActive(false);//or move.
        }
          
    }
}
