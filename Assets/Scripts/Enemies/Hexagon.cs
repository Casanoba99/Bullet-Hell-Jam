using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    bool havePath = false;
    Transform currentP;
    Coroutine shotCoro, stopCoro;
    Animator anim;

    public int life;
    public OpenPath path;

    [Header("Drop ammo")]
    public int probability;
    public GameObject ammo;

    [Header("Movement")]
    public int point;
    public float torque;
    public float stopTime;
    public float speed;
    public Transform[] points;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentP = points[point];
    }

    void Update()
    {
        if (!MenuPause.mPause.pause)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentP.position, speed * Time.deltaTime);

            if (transform.position == currentP.position)
            {
                Start_Stop();
            }
            else
            {
                transform.Rotate(Vector3.forward, torque * Time.deltaTime);
            }
        }
        else
        {
            StopAllCoroutines();
            shotCoro = null;
            stopCoro = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo") && !havePath)
        {
            path = collision.GetComponent<OpenPath>();
            havePath = true;

        }

        if (collision.CompareTag("Player P"))
        {
            life--;
            collision.GetComponent<Proyectile>().Hit();
            if (life <= 0)
            {
                path.enemies--;
                int n = Random.Range(0, (probability * 2) + 1);
                if (n <= probability) _ = Instantiate(ammo, transform.position, ammo.transform.rotation, null);
                Destroy(gameObject);
            }
        }
    }

    void Start_Stop()
    {
        stopCoro ??= StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        anim.SetBool("Laser", false);
        yield return new WaitForSeconds(stopTime);

        point++;

        if (point >= points.Length)
        {
            point = 0;
        }

        currentP = points[point];

        anim.SetBool("Laser", true);

        stopCoro = null;
    }
}
