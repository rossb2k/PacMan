using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private bool isFrozen = false;
    private float freezeTimer = 0f;

    public float chaseSpeed = 5f;
    public float freezeDuration = 5f;
    public float roamingSpeed = 2f;
    public float roamingDistance = 10f;

    private Vector3 randomRoamingPoint;
    private bool isRoaming = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player is tagged as "Player."
        StartCoroutine(StartRoaming());
    }

    void Update()
    {
        if (!isFrozen)
        {
            if (isRoaming)
            {
                agent.speed = roamingSpeed;
                if (Vector3.Distance(transform.position, randomRoamingPoint) < 1f)
                {
                    isRoaming = false;
                    StartCoroutine(StartRoaming());
                }
                else
                {
                    agent.SetDestination(randomRoamingPoint);
                }
            }
            else
            {
                agent.speed = chaseSpeed;
                agent.SetDestination(target.position);
            }
        }
        else
        {
            // Handle freezing behavior here.
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                isFrozen = false;
                agent.speed = chaseSpeed;
                agent.SetDestination(target.position); // Set destination to player's position to keep moving.
            }
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        agent.speed = 0f;
        freezeTimer = freezeDuration;
    }

    IEnumerator StartRoaming()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        randomRoamingPoint = RandomNavSphere(transform.position, roamingDistance, -1);
        isRoaming = true;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}