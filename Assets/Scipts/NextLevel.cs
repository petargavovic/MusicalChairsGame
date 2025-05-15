using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    float timer = 5;

    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        text.text = "Следећи ниво за... "+ Mathf.RoundToInt(timer).ToString() + "с";
        if (timer < 0)
        {
            GameObject.Find("SpawnerSettings").GetComponent<SpawnerSettings>().npcNumber--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
