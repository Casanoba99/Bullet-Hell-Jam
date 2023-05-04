using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenPath : MonoBehaviour
{
    public bool playerIn = false;
    public int enemies;
    public GameObject[] bridge;

    private void Start()
    {
        for (int i = 0; i < bridge.Length; i++)
        {
            bridge[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (enemies <= 0)
        {
            for (int i = 0; i < bridge.Length; i++)
            {
                bridge[i].SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = false;
        }
    }
}
