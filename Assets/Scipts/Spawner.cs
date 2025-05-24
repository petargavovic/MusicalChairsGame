using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnerSettings settings;

    public Transform target;
    public float radius = 1f;
    public float[] angles;
    public GameObject npcPrefab;

    protected int npcNumber;
    protected int isNPC = 0;

    public GameObject[] npcs;

    public bool isMusic = true;
    public bool won = false;

    public AudioManager audioManager;

    public void Start()
    {
        npcNumber = GameObject.Find("SpawnerSettings").GetComponent<SpawnerSettings>().npcNumber;
        npcs = new GameObject[npcNumber];
        angles = new float[npcNumber];
        for (int i = 0; i < npcNumber; i++)
        {
            npcs[i] = Instantiate(npcPrefab, transform);
            angles[i] = i * (Mathf.Deg2Rad * (360f / (npcNumber+isNPC)));
            if (isNPC == 1)
            {
                StartCoroutine("AnimStart", i);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator AnimStart(int i)
    {
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        npcs[i].transform.GetChild(0).GetComponentInChildren<Animator>().enabled = true;
        npcs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>().enabled = true;
    }

    IEnumerator MusicStop()
    {
        float duzina= Random.Range(13f, 20f);
        yield return new WaitForSeconds(duzina-1);
        target.GetComponent<Animator>().Play("boss_active");
        yield return new WaitForSeconds(1);
        isMusic = false;
        GameObject.Find("Music").SetActive(false);
        foreach (GameObject npc in npcs)
        {
            npc.transform.GetComponentInChildren<Animator>().SetTrigger("stop");
            npc.GetComponent<NPCController>().GoToChair();
        }
        foreach (GameObject chair in npcs[0].GetComponent<NPCController>().AllChairs)
            chair.GetComponent<BoxCollider>().isTrigger = true;
    }
}
