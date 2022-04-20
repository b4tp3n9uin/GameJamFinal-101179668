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

    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

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
            anim.SetInteger("State", 0); // Move when far
        }
    }

    public void IncreaseSpeed()
    {
        navAgent.speed += 0.5f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.Damage();
        }
    }
}
