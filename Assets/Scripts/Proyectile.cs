using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public float timeDestroy;
    public GameObject light;
    public SpriteRenderer sprite;
    public ParticleSystem particle;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Clear"))
        {
            Hit();
        }
    }

    public void Hit()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        sprite.enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
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
