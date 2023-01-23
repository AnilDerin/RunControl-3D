using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{

    public Animator _Anim;

    public void SelfPassive()
    {
        _Anim.SetBool("isSaved", false);

    }

}
