using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerInput : MonoBehaviour
{
    float X, Y;
    Animator anim;
    Coroutine shootCoro;

    [Header("Movement")]
    public float speed;
    public Vector2 inputVector;

    [Header("Shot")]
    public float timeShot;
    public float forceShot;
    public Transform shotPos;
    public GameObject proyectile;
    public CinemachineVirtualCamera vCam;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Rotation();

        if (Input.GetMouseButton(0))
        {
            Start_Shoot();
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        }
        else
        {
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
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
    #endregion


    void Start_Shoot()
    {
        shootCoro ??= StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        GameObject shot = Instantiate(proyectile, shotPos.position, shotPos.rotation, shotPos.transform);
        shot.GetComponent<Rigidbody2D>().AddForce(shotPos.up * forceShot, ForceMode2D.Impulse);
        shot.transform.parent = null;

        yield return new WaitForSeconds(timeShot);
        shootCoro = null;
    }

    public void Dead()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
