using UnityEngine;

public class AIControl : MonoBehaviour, IGoTo
{
    GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent agent;
    Animator anim;


    // Use this for initialization
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToDestination();
    }

    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    public void GoToDestination()
    {
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetTrigger("isWalking");
    }
}