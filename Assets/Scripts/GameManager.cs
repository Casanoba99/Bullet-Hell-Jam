using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct PlayerDead
    {
        public Vector2 position;
        public Quaternion quaternion;
    }

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

    [Header("Cursor")]
    public Texture2D cursor;

    [Header("Deads")]
    public GameObject deadPfb;
    public List<PlayerDead> deads;

    void Start()
    {
        music.clip = menuClip;
        music.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            string date = System.DateTime.Now.ToString("dd-MM-yy_HH-mm-ss");
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Screenshot_" + date + ".png");
        }

        if (!music.isPlaying)
        {
            StartGameplay();
        }
    }

    public void StartGameplay()
    {
        music.loop = false;
        clip = UnityEngine.Random.Range(0, 1);
        music.clip = gameplayClip[clip];
        music.Play();

        Cursor.SetCursor(cursor, new Vector2(cursor.width/ 2 , cursor.height / 2), 0);
    }

    public void AddPlayerDead(Vector2 pos, Quaternion rot)
    {
        PlayerDead dead;
        dead.position = pos;
        dead.quaternion = rot;
        deads.Add(dead);
    }

    public void InstantiateDead()
    {
        for (int i = 0; i < deads.Count; i++)
        {
            _ = Instantiate(deadPfb, deads[i].position, deads[i].quaternion);
        }
    }
}
