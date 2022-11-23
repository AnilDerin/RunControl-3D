using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreeHero : MonoBehaviour
{

    public NavMeshAgent _Navmesh;
    public SkinnedMeshRenderer _Renderer;
    public Material _Mat;
    public Animator _Animator;
    public GameManager _gm;
    public GameObject Target;
    bool isTouching;

    void LateUpdate()
    {
        if (isTouching)
            _Navmesh.SetDestination(Target.transform.position);


    }


    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BottomPlayers") || other.CompareTag("Player"))
        {
            ChangeMaterialAndAnimationTrigger();
            isTouching = true;


        }
        else if (other.CompareTag("Enemy"))
        {
            Vector3 newPos = new Vector3(transform.position.x, .23f, transform.position.z);
            _gm.CreateDestroyEffect(newPos, false, false);
            gameObject.SetActive(false);

        }
    }


    void ChangeMaterialAndAnimationTrigger()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = _Mat;
        _Renderer.materials = mats;
        _Animator.SetBool("Attack", true);

    }

}
