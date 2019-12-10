using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllKeysCollectedBool : MonoBehaviour
{
    [SerializeField] public int keyCount;//static
    // Start is called before the first frame update
    void Start()
    {
        keyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCount == 5) {
            gameObject.SetActive(false);//or move
        }
    }
}
