using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject main;
    public GameObject settings;

    [Header("Audio")]
    public AudioMixer mixer;

    public void Play()
    {
        // Fundido a negro.
        GameManager.manager.StartGameplay();
        SceneManager.LoadScene(1);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void Music(float volum)
    {
        mixer.SetFloat("Music", volum);
    }

    public void Sound(float volum)
    {
        mixer.SetFloat("Sound", volum);
    } 
}
