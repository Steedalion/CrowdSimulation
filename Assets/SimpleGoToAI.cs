using UnityEngine;
using UnityEngine.AI;

public class SimpleGoToAI : MonoBehaviour, IGoTo
{
    public Transform destination;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToDestination();
    }

    public void GoToDestination()
    {
        agent.SetDestination(destination.position);
    }
}