using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Spawner
{
    public float speed = 20f;

    public GameObject player;
    public PlayerController playerController;

    private void Awake()
    {
        isNPC = 1;
    }

    new void Start()
    {
        base.Start();
        player.transform.position =  new Vector3(target.position.x + Mathf.Cos(npcNumber * (Mathf.Deg2Rad * (360f / (npcNumber + 1)))) * radius,
            target.position.y + Mathf.Sin(npcNumber * (Mathf.Deg2Rad * (360f / (npcNumber + 1)))) * radius,
            target.position.z);

        playerController = player.GetComponent<PlayerController>();
        SetCaciPosition();
        StartCoroutine("MusicStop");

    }
    void Update()
    {
        if(isMusic && playerController.startTimer < 0)
            SetCaciPosition();
    }

    void SetCaciPosition()
    {
        for (int i = 0; i < npcNumber; i++)
        {
            float x = target.position.x + Mathf.Cos(angles[i]) * radius;
            float y = target.position.y + Mathf.Sin(angles[i]) * radius;
            float z = target.position.z;

            npcs[i].transform.position = new Vector3(x, y, z);

            angles[i] -= speed * Time.deltaTime;
        }
    }
}
