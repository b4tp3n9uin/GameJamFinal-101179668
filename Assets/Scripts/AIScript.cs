using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    Animator anim;
    NavMeshAgent navAgent;

    [SerializeField]
    Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        anim.SetInteger("State", 0); //Walk
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(_target.position);

        if (Vector3.Distance(_target.position, transform.position) < 4.1f)
        {
            anim.SetInteger("State", 1); //Idle when near player.
        }
        else 
        {
            anim.SetInteger("State", 0);
        }
    }
}
