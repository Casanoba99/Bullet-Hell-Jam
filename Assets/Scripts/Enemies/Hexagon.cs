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
        if (path.playerIn)
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

    //public void Start_Shot()
    //{
    //    shotCoro ??= StartCoroutine(Shot());
    //}

    //IEnumerator Shot()
    //{
    //    laserOut = true;
    //    for (int j = 0; j < shotPos.Length; j++)
    //    {
    //        shotPos[j].gameObject.SetActive(true);
    //    }

    //    yield return new WaitForSeconds(timeShot);

    //    for (int j = 0; j < shotPos.Length; j++)
    //    {
    //        shotPos[j].gameObject.SetActive(false);
    //    }
    //    laserOut = false;

    //    shotCoro = null;
    //}
}
