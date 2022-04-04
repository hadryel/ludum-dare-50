using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPreferencesLoader : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }

    void Update()
    {
        
    }
}
