using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [Range(0f, 1f)]
    public float volume;
    private AudioSource dis;
    public bool menu;
    public bool ddol;
    public int scene;
    public AudioClip[] pieces;

    public static Music instance;

    void Start()
    {
        dis = gameObject.GetComponent<AudioSource>();
        dis.volume = volume;
        dis.Play();
//        StartCoroutine("Menu");
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

}
