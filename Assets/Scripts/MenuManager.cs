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
    public Slider musicSl;
    public Slider soundSl;

    private void Start()
    {
        musicSl.value = PlayerPrefs.GetFloat("Music");
        Music(PlayerPrefs.GetFloat("Music"));
        soundSl.value = PlayerPrefs.GetFloat("Sound");
        Sound(PlayerPrefs.GetFloat("Sound"));
    }

    public void Play(int scene)
    {
        GameManager.manager.StartGameplay();
        SceneManager.LoadScene(scene);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    #region Audio
    public void Music(float volum)
    {
        mixer.SetFloat("Music", volum);
        PlayerPrefs.SetFloat("Music", volum);
        PlayerPrefs.Save();
    }

    public void Sound(float volum)
    {
        mixer.SetFloat("Sound", volum);
        PlayerPrefs.SetFloat("Sound", volum);
        PlayerPrefs.Save();
    }
    #endregion
}
