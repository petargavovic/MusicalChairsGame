using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [Range(0f, 1f)]
    public float volume;

//    public static AudioManager instance;

    void Awake()
    {
/*        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
*/
        int i = 0;
 //       DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.GetComponents<AudioSource>()[i];
            //      s.source.clip = s.clip;
            //    s.source.pitch = s.pitch;
            i++;
        }
    }
   
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = s.pitch;
        s.source.volume = s.volume * volume;
        s.source.Play();
    }
}
