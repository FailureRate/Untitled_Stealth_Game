using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] public List<GameObject> pool;   
    [SerializeField] public GameObject prefab;
    [SerializeField] public int amountOfObjects;
   

    public static BulletPool instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < amountOfObjects; i++)
        {
            GameObject pro = Instantiate(prefab);
            pro.SetActive(false);
            pool.Add(pro);
        }
    }

    public void SpawnObjectsFromPool(Vector3 pos_, Quaternion rot_)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = pos_;
                pool[i].transform.rotation = rot_;
                pool[i].SetActive(true);
                break;//just need one at a time
            }
        }

    }
}
