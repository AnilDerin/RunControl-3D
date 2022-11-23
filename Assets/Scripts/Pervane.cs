using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public BoxCollider WindZone;
    public Animator animator;
    public float SecondsOfWait;

    public void AnimationStatus(string status)
    {
        if (status == "true")
        {
            animator.SetBool("Start", true);
            WindZone.enabled = true;
        }
        else
        {
            animator.SetBool("Start", false);
            WindZone.enabled = false;
            StartCoroutine(TriggerAnim());
        }
    }

    IEnumerator TriggerAnim()
    {
        yield return new WaitForSeconds(SecondsOfWait);
        AnimationStatus("true");
    }
}
