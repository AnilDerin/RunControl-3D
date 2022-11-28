using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BottomPlayer : MonoBehaviour
{
    NavMeshAgent _Navmesh;
    public GameManager _gm;
    public GameObject Target;

    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_gm.enemyCount != 0)
            _Navmesh.SetDestination(Target.transform.position);
        else
            _Navmesh.isStopped = true;
    }

    Vector3 GivePosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("igneliKutu"))
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
        else if (other.CompareTag("EmptyChars"))
        {
            _gm.Characters.Add(other.gameObject);
        }
    }
}
