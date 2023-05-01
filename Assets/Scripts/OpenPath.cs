using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPath : MonoBehaviour
{
    public int enemies;
    public GameObject[] bridge;

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
