using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Target;
    public bool isAttacking = false;
    public GameManager _gm;
    public bool isEndedEnemy;
    public NavMeshAgent _NavMesh;
    public Animator _Anim;

    void Start() { }

    public void TriggerAnimation()
    {
        isAttacking = true;
        _Anim.SetBool("AttackEnemy", true);
    }

    void LateUpdate()
    {
        if (isAttacking)
        {
            _NavMesh.SetDestination(Target.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BottomPlayers"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, .23f, transform.position.z);
            _gm.CreateDestroyEffect(yeniPoz, false, true);
            gameObject.SetActive(false);
        }
    }
}
