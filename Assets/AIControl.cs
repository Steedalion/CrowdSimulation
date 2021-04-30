using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour, IGoTo, IFlee
{
    public GameObject[] goalLocations;
public        Vector3 currentGoal;

    NavMeshAgent agent;
    Animator anim;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private float fleeRadius = 10;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private float detectionRadius = 5;


    // Use this for initialization
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        GoToDestination();
    }

    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            GoToDestination();
            Walk();
        }
    }

    public void GoToDestination()
    {
        currentGoal = goalLocations[Random.Range(0, goalLocations.Length)].transform.position;
        agent.SetDestination(currentGoal);
        Debug.Log( "Going to"+currentGoal);
    }

    public void DetectDanger(Vector3 dangerPosition)
    {
        Debug.Log("informed");
        FleeFromDanger(dangerPosition);
    }

    private void FleeFromDanger(Vector3 dangerPosition)
    {
        Vector3 awayFromDanger = (transform.position - dangerPosition).normalized;
        if (awayFromDanger.magnitude < detectionRadius)
        {
            Vector3 awayFromDangerDirection = (transform.position - dangerPosition).normalized;
            Vector3 goal = transform.position + awayFromDangerDirection * fleeRadius;
            Vector3 validGoal = GetAValidGoal(goal);
            RunTo(validGoal);
        }
    }

    private Vector3 GetAValidGoal(Vector3 goal)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(goal, path);
        Vector3 finalPointInPath = path.corners[path.corners.Length - 1];
        return finalPointInPath;
    }

    private void RunTo(Vector3 validGoal)
    {
        float runSpeed = 5;
        float runTurnSpeed = 999;
        anim.SetTrigger(IsRunning);
        anim.speed = runSpeed;
        agent.angularSpeed = runTurnSpeed;
    }

    private void Walk()
    {
        float walkSpeed = 2;
        float walkTurnSpeed = 12;
        anim.SetTrigger(IsWalking);
        anim.speed = walkSpeed;
        agent.angularSpeed = walkTurnSpeed;
    }
}

public interface IFlee
{
    void DetectDanger(Vector3 dangerPosition);
}