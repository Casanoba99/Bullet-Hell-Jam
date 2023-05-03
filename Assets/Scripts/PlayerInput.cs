using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    bool ammo = true;
    bool isDash = false;
    bool damage = true;
    int currentShots;
    float X, Y;
    Animator anim;
    Coroutine shootCoro, dashCoro;

    public new GameObject light;

    [Header("Movement")]
    public float speed;
    public Vector2 inputVector;

    [Header("Dash")]
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    public GameObject clearArea;
    public Rigidbody2D rb;
    public TrailRenderer trail;

    [Header("Shot")]
    public int maxShots;
    public float timeShot;
    public float forceShot;
    public Transform shotPos;
    public GameObject projectile;
    public TextMeshProUGUI shotsTMP;
    public CinemachineVirtualCamera vCam;

    void Start()
    {
        anim = GetComponent<Animator>();
        vCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        trail.emitting = false;
        currentShots = maxShots;
        shotsTMP.text = currentShots.ToString();
    }

    void Update()
    {
        if (ammo)
        {
            if (!isDash)Move();
            Rotation();
        }

        if (Input.GetMouseButton(0) && currentShots > 0)
        {
            Start_Shoot();
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        }
        else if (currentShots <= 0 && ammo)
        {
            anim.SetTrigger("Fall");
            anim.SetBool("Floor", false);
            ammo = false;
        }
        else
        {
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Start_Dash();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy P") && damage)
        {
            collision.GetComponent<Proyectile>().Hit();
            currentShots -= 2;
            shotsTMP.text = currentShots.ToString();
        }

        //if (collision.CompareTag("Enemy"))
        //{
        //    collision.GetComponent<Proyectile>().Hit();
        //    currentShots -= 5;
        //    shotsTMP.text = currentShots.ToString();
        //}

        if (collision.CompareTag("End"))
        {
            // Cargar nivel
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo") && ammo)
        {
            anim.SetBool("Floor", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            anim.SetTrigger("Fall");
            anim.SetBool("Floor", false);
        }
    }

    #region Movement
    void Move()
    {
        X = Input.GetAxisRaw("Horizontal");
        Y = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(X, Y);
        transform.position += speed * Time.deltaTime * (Vector3)inputVector;
    }

    void Rotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Start_Dash()
    {
        dashCoro ??= StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        damage = false;
        isDash = true;
        rb.velocity = new Vector2(inputVector.x * dashPower, inputVector.y * dashPower);
        trail.emitting = true;
        clearArea.SetActive(true);

        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        trail.emitting = false;
        damage = true;
        isDash = false;
        clearArea.SetActive(false);

        yield return new WaitForSeconds(dashCooldown);

        light.SetActive(false);
        yield return new WaitForSeconds(.05f);
        light.SetActive(true);

        dashCoro = null;
    }
    #endregion


    void Start_Shoot()
    {
        shootCoro ??= StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        GameObject shot = Instantiate(projectile, shotPos.position, shotPos.rotation, shotPos.transform);
        shot.GetComponent<Rigidbody2D>().AddForce(shotPos.up * forceShot, ForceMode2D.Impulse);
        shot.transform.parent = null;
        currentShots--;
        shotsTMP.text = currentShots.ToString();

        yield return new WaitForSeconds(timeShot);
        shootCoro = null;
    }

    public void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
