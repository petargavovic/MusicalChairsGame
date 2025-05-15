using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SandwichSpawner : MonoBehaviour
{
    public GameObject sandwich;
    List<Vector3> spawnList = new List<Vector3>();
    public Transform playerTransform;
    public AudioManager audioManager;

    public bool spawned = true;
    void Start()
    {
        foreach(Transform t in transform)
        {
            spawnList.Add(t.position);
        }
        StartCoroutine("Spawn");
    }

    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(4f, 8f));
        float distance = 0, furthestDistance = 0;
        int element = 0;
        for (int i = 0; i < spawnList.Count(); i++)
        {
            distance = Vector3.Distance(playerTransform.position, spawnList[i]);

            if (distance > furthestDistance)
            {
                element = i;
                furthestDistance = distance;
            }
        }
        Instantiate(sandwich, spawnList.ElementAt(element), Quaternion.identity, null);
        spawned = true;
    }
}
