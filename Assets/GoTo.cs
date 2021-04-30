using UnityEngine;
using UnityEngine.AI;

public class GoTo : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
    }

}
