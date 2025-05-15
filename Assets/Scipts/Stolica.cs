using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stolica : MonoBehaviour
{
    public bool taken = false;
    public GameObject nextLevel;
    private void OnTriggerEnter(Collider other)
    {
        NPC npc = GameObject.Find("NPCSpawner").GetComponent<NPC>();
        if (!npc.isMusic && !npc.won)
        {
            if (other.CompareTag("Player"))
            {
                print(other.name);
                if (!taken)
                {
                    if (GameObject.Find("SpawnerSettings").GetComponent<SpawnerSettings>().npcNumber == 1)
                    {
                        //igra je gotova

                        
                        other.GetComponent<PlayerController>().winScreen.gameObject.SetActive(true);
                        other.GetComponent<PlayerController>().timer = 99999;
                        npc.audioManager.Play("win");
                        GameObject.Find("NPCSpawner").GetComponent<NPC>().won = true;
                        StartCoroutine("GoToMenu");
                    }
                    else
                    {

                        Instantiate(nextLevel, GameObject.Find("Canvas").transform);
                        GameObject.Find("NPCSpawner").GetComponent<NPC>().won = true;
                    }
                }
                taken = true;
            }
            else if(other.tag == "NPC")
            {
                PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                taken = true;
                playerController.takenCount++;
                other.GetComponent<NPCController>().inChair = true;
                if (GameObject.Find("SpawnerSettings").GetComponent<SpawnerSettings>().npcNumber <= playerController.takenCount +1)
                    playerController.timer = 0;
            }
        }
    }
    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
