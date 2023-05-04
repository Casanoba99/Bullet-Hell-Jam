using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    Animator anim;
    Coroutine shotCoro, timerCoro;

    public int life;
    public OpenPath path;

    [Header("Shoot")]
    public int amountShots;
    public float time2Shoot;
    public float timeShot;
    public float forceShot;
    public Transform[] shotPos;
    public GameObject projectile;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (path.playerIn) Start_LoadShoot();
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
                path.enemies--;
                Destroy(gameObject);
            }
        }
    }

    void Start_LoadShoot()
    {
        timerCoro ??= StartCoroutine(LoadShot());
    }

    IEnumerator LoadShot()
    {
        yield return new WaitForSeconds(time2Shoot);
        anim.SetTrigger("Shot");
        timerCoro = null;
    }

    public void Start_Shot()
    {
        shotCoro ??= StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        for (int i = 0; i < amountShots; i++)
        {
            for (int j = 0; j < shotPos.Length; j++)
            {
                GameObject clone = Instantiate(projectile, shotPos[j].position, shotPos[j].rotation, shotPos[j].transform);
                clone.GetComponent<Rigidbody2D>().AddForce(shotPos[j].up * forceShot, ForceMode2D.Impulse);
                clone.transform.parent = null;
            }

            yield return new WaitForSeconds(timeShot);
        }

        shotCoro = null;
    }
}
