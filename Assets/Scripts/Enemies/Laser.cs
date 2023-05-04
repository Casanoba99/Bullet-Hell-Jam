using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Clear"))
        {
            GetComponentInParent<Animator>().SetBool("Laser", false);
        }
    }
}
