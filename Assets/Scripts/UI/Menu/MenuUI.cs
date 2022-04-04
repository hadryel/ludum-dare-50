using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Slider Slider;
    public AudioSource MusicSource;

    float PreviousVolume;

    // Start is called before the first frame update
    void Start()
    {
        PreviousVolume = PlayerPrefs.GetFloat("Volume");
        Slider.value = PreviousVolume;
        UpdateVolume();

    }

    // Update is called once per frame
    void Update()
    {
        if(PreviousVolume != Slider.value)
        {
            PreviousVolume = Slider.value;
            PlayerPrefs.SetFloat("Volume", PreviousVolume);
            PlayerPrefs.Save();
            UpdateVolume();
        }
    }

    public void UpdateVolume()
    {
        MusicSource.volume = PreviousVolume;
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
