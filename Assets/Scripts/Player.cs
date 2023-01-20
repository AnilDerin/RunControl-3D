using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager _GameManager;
    public bool isEnded;
    public GameObject FinalPos;
    public Cam _cam;
    public Slider _slider;
    public GameObject TargetDestination;

    void Start()
    {
        float Diff = Vector3.Distance(transform.position, TargetDestination.transform.position);
        _slider.maxValue = Diff;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isEnded)
            transform.Translate(Vector3.forward * .9f * Time.deltaTime);

        if (isEnded)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                FinalPos.transform.position,
                .02f
            );
            if (_slider.value != 0)
                _slider.value -= .01f;
        }
        else
        {
            float Diff = Vector3.Distance(transform.position, TargetDestination.transform.position);
            _slider.value = Diff;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        new Vector3(
                            Mathf.Clamp(transform.position.x, -1.1f, 1.1f) - .1f,
                            transform.position.y,
                            transform.position.z
                        ),
                        .4f
                    );
                }

                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        new Vector3(Mathf.Clamp(transform.position.x, -1.1f, 1.1f) + .1f, transform.position.y, transform.position.z), .4f);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (
            other.CompareTag("Carpma")
            || other.CompareTag("Toplama")
            || other.CompareTag("Cikarma")
            || other.CompareTag("Bolme")
        )
        {
            int number = int.Parse(other.name);
            _GameManager.MathLogics(other.tag, number, other.transform);
            other.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("EndGameTrigger"))
        {
            _cam.isGameEnded = true;
            _GameManager.TriggerEnemies();
            isEnded = true;
        }
        else if (other.CompareTag("EmptyChars"))
        {
            _GameManager.Characters.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.CompareTag("Direk")
            || collision.gameObject.CompareTag("igneliKutu")
            || collision.gameObject.CompareTag("PervaneIgneler")
        )
        {
            if (transform.position.x > 0)
                transform.position = new Vector3(
                    transform.position.x - .2f,
                    transform.position.y,
                    transform.position.z
                );
            else
                transform.position = new Vector3(
                    transform.position.x + .2f,
                    transform.position.y,
                    transform.position.z
                );
        }
    }
}
