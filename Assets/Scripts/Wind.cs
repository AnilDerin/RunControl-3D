using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public GameObject Pervane;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BottomPlayers"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 0, 0), ForceMode.Impulse);
        }
    }
}
