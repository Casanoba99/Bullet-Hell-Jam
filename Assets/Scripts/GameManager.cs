 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager manager;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(manager);
        }
        else Destroy(gameObject);
    }
    #endregion

    int clip = 0;

    [Header("Music")]
    public AudioSource music;
    public AudioClip menuClip;
    public AudioClip[] gameplayClip;

    void Start()
    {
        music.clip = menuClip;
        music.Play();
    }

    private void Update()
    {
        if (!music.isPlaying)
        {
            StartGameplay();
        }
    }

    public void StartGameplay()
    {
        music.loop = false;
        clip = Random.Range(0, 1);
        music.clip = gameplayClip[clip];
        music.Play();
    }
}
