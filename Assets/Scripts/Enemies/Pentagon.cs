using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagon : MonoBehaviour
{
    bool canMove = true;

    Transform currentP;
    AudioSource source;
    Coroutine shotCoro, stopCoro;

    public int life;
    public OpenPath path;

    [Header("Movement")]
    public int point;
    public float torque;
    public float stopTime;
    public float speed;
    public Transform[] points;

    [Header("Drop ammo")]
    public int probability;
    public GameObject ammo;

    [Header("Shoot")]
    public float timeShot;
    public float forceShot;
    public Transform[] shotPos;
    public GameObject projectile;

    void Start()
    {
        source = GetComponent<AudioSource>();
        currentP = points[point];
    }

    void Update()
    {
        if (!MenuPause.mPause.pause && canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentP.position, speed * Time.deltaTime);

            if (transform.position == currentP.position)
            {
                Start_Stop();
            }
            else
            {
                transform.Rotate(Vector3.forward, torque * Time.deltaTime);
                Start_Shot();
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
        if (collision.CompareTag("Suelo"))
        {
            path = collision.GetComponent<OpenPath>();

        }

        if (collision.CompareTag("Player P"))
        {
            life--;
            collision.GetComponent<Proyectile>().Hit();
            if (life <= 0)
            {
                canMove = false;
                source.Play();
                transform.GetChild(0).gameObject.SetActive(false);

                path.enemies--;
                int n = Random.Range(0, (probability * 2) + 1);
                if (n <= probability) _ = Instantiate(ammo, transform.position, ammo.transform.rotation, null);

                Destroy(gameObject, 1);
            }
        }
    }

    void Start_Stop()
    {
        stopCoro ??= StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(stopTime);

        point++;

        if (point >= points.Length)
        {
            point = 0;
        }

        currentP = points[point];

        stopCoro = null;
    }

    public void Start_Shot()
    {
        shotCoro ??= StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        for (int j = 0; j < shotPos.Length; j++)
        {
            GameObject clone = Instantiate(projectile, shotPos[j].position, shotPos[j].rotation, shotPos[j].transform);
            clone.GetComponent<Rigidbody2D>().AddForce(shotPos[j].up * forceShot, ForceMode2D.Impulse);
            clone.transform.parent = null;
        }

        yield return new WaitForSeconds(timeShot);

        shotCoro = null;
    }
}
