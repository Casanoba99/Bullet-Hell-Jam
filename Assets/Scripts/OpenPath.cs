using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenPath : MonoBehaviour
{
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
}