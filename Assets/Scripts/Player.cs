using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager _GameManager;
    public bool isEnded;
    public GameObject FinalPos;
    public Cam _cam;


    void FixedUpdate()
    {
        if (!isEnded)
            transform.Translate(Vector3.forward * .9f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()

    {
        if (isEnded)
        {
            transform.position = Vector3.Lerp(transform.position, FinalPos.transform.position, .02f);
        }

        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(transform.position.x, -1.1f, 1.1f) - .1f, transform.position.y,
                    transform.position.z), .3f);
                }

                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(transform.position.x, -1.1f, 1.1f) + .1f, transform.position.y,
                    transform.position.z), .3f);
                }
            }
        }

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Toplama") || other.CompareTag("Cikarma") || other.CompareTag("Bolme"))
        {
            int number = int.Parse(other.name);
            _GameManager.AdamYonetimi(other.tag, number, other.transform);
            other.GetComponent<BoxCollider>().enabled = false;
        }

        else if (other.CompareTag("EndGameTrigger"))
        {
            _cam.isGameEnded = true;
            _GameManager.TriggerEnemies();
            isEnded = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direk") || collision.gameObject.CompareTag("igneliKutu")
            || collision.gameObject.CompareTag("PervaneIgneler"))
        {

            if (transform.position.x > 0)
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);

        }
    }
}