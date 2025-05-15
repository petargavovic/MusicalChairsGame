using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sandwich : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().energy = 1;
            GetComponent<Animator>().Play("sandwich_despawn");
//            gameObject.SetActive(false);
GameObject.Find("SandwichSpawner").GetComponent<SandwichSpawner>().spawned = false;
            GameObject.Find("SandwichSpawner").GetComponent<SandwichSpawner>().audioManager.Play("eat");

            GameObject.Find("SandwichSpawner").GetComponent<SandwichSpawner>().StartCoroutine("Spawn");
        }
    }
}
