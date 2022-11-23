using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;
    public bool isGameEnded;
    public GameObject LastCamLocation;


    void Start()
    {
        target_offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isGameEnded)
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .125f);
        else
            transform.position = Vector3.Lerp(transform.position, LastCamLocation.transform.position, .02f);
    }
}
