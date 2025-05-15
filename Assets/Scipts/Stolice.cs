using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stolice : Spawner
{
    public GameObject stolicaPrefab;
    public List<Sprite> spritesList;

    // Start is called before the first frame update

    // Update is called once per frame

    private new void Start()
    {
        base.Start();
        for (int i = 0; i < npcNumber; i++)
        {
            float x = target.position.x + Mathf.Cos(angles[i]) * radius;
            float y = target.position.y + Mathf.Sin(angles[i]) * radius;
            float z = target.position.z + 0.2f;

            npcs[i].transform.position = new Vector3(x, y, z);
            npcs[i].name = i.ToString();

            int sprite = 0;
            switch (Mathf.Rad2Deg * angles[i]) {
                case float n when (n <= 60):
                    sprite = 0;
                    break;
                case float n when (n > 60 && n <= 120):
                    sprite = 1;
                    break;
                case float n when (n > 120 && n <= 180):
                    sprite = 2;
                    break;
                case float n when (n > 180 && n <= 240):
                    sprite = 3;
                    break;
                case float n when (n > 240 && n <= 300):
                    sprite = 4;
                    break;
                case float n when (n > 300):
                    sprite = 5;
                    break;
            }
            npcs[i].GetComponent<SpriteRenderer>().sprite = spritesList[sprite];
        }
    }

    void Update()
    {
        
    }

}
