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

    Vector3 GivePosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BottomPlayers") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("EmptyChars"))
            {
                ChangeMaterialAndAnimationTrigger();
                GetComponent<AudioSource>().Play();
                isTouching = true;
            }
        }
        else if (other.CompareTag("igneliKutu"))
        {
            _gm.CreateDestroyEffect(GivePosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Testere"))
        {
            _gm.CreateDestroyEffect(GivePosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("PervaneIgneler"))
        {
            _gm.CreateDestroyEffect(GivePosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Balyoz"))
        {
            _gm.CreateDestroyEffect(GivePosition(), true);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            _gm.CreateDestroyEffect(GivePosition(), false, false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("LastTriggerForFreeHeroes"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<Animator>().SetBool("Attack", false);
        }
    }

    void ChangeMaterialAndAnimationTrigger()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = _Mat;
        _Renderer.materials = mats;

        _Animator.SetBool("Attack", true);
        gameObject.tag = "BottomPlayers";
        if (GameManager.CurrentCharCount == 0)
            GameManager.CurrentCharCount += 2;
        else
            GameManager.CurrentCharCount++;
    }
}
