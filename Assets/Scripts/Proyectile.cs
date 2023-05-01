using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public float timeDestroy;
    public GameObject light;
    public ParticleSystem particle;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    public void Hit()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        light.SetActive(false);
        particle.Play();

        StartCoroutine(HitDestoy());
    }

    IEnumerator HitDestoy()
    {
        yield return new WaitForSeconds(particle.main.duration);
        Destroy(gameObject);
    }
}
