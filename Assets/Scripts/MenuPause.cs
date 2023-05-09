using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static MenuPause mPause;

    private void Awake()
    {
        if (mPause == null)
        {
            mPause = this;
        }
    }

    public bool pause;
    public GameObject panelP;

    void Start()
    {
        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

            if (pause)
            {
                Cursor.SetCursor(null, Vector2.zero, 0);
                panelP.SetActive(true);
            }
            else
            {
                ClosePause();
            }
        }
    }

    public void ClosePause()
    {
        pause = false;
        panelP.SetActive(false);
        //Cursor.SetCursor(GameManager.manager.cursor, new Vector2(64, 64), 0);
    }

    public void ReturnMenu()
    {
        GameManager gm = GameManager.manager;
        gm.music.loop = true;
        gm.music.clip = gm.menuClip;
        gm.music.Play();

        Cursor.SetCursor(null, Vector2.zero, 0);
        SceneManager.LoadScene(0);
    }
}
