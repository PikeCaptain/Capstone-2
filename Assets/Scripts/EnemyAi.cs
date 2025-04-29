using UnityEngine;
using System.Collections;
using System;
using JetBrains.Annotations;

public class EnemyAi : MonoBehaviour
{
    public Transform patrolPoint;
    private enum EnemyState { Idle, Patrol, Chase, Attack }
    private EnemyState enemyState;
    private Animator anim;
    private float distanceToTarget;
    private Coroutine idleToPatrol;
    public Transform target;  // Assuming this is set via inspector or another script
    public UnityEngine.AI.NavMeshAgent ai;  // Assuming this is set via inspector or another script
    public event Action OnDeath; // Event that is triggered when the zombie dies
    [SerializeField]
    private int health = 5;
    public bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDead = false;

        patrolPoint = transform;
        target = GameObject.FindWithTag("Player").transform;

        // Initialize the enemy state
        enemyState = EnemyState.Idle;

        // Assign Animator and NavMeshAgent
        anim = GetComponent<Animator>();
        ai = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Set the initial distance to the target
        distanceToTarget = Mathf.Abs(Vector3.Distance(target.position, transform.position));

        // Set initial state to Idle
        SwitchState(0);

        // This is just a placeholder for whatever health system you have.
        if (health <= 0)
        {
            Die();
        }
    }

    // Coroutine to switch from Idle to Patrol after 5 seconds
    private IEnumerator SwitchToPatrol()
    {
        yield return new WaitForSeconds(5);
        enemyState = EnemyState.Patrol;
        idleToPatrol = null;
    }

    // Method to switch animation state
    private void SwitchState(int newState)
    {
        if (anim.GetInteger("State") != newState)
        {
            anim.SetInteger("State", newState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            // Update distance to target
            distanceToTarget = Mathf.Abs(Vector3.Distance(target.position, transform.position));

            // Switch based on the enemy's current state
            switch (enemyState)
            {
                case EnemyState.Idle:
                    SwitchState(0);
                    ai.SetDestination(transform.position);  // Stay at the current position while idle

                    // If idleToPatrol is null, start the patrol coroutine
                    if (idleToPatrol == null)
                    {
                        idleToPatrol = StartCoroutine(SwitchToPatrol());
                    }
                    break;

                case EnemyState.Patrol:
                    // Calculate distance to patrol point
                    float distanceToPatrolPoint = Mathf.Abs(Vector3.Distance(patrolPoint.position, transform.position));

                    // If far from patrol point, continue patrolling, else go back to idle
                    if (distanceToPatrolPoint > 2)
                    {
                        SwitchState(1);
                        ai.SetDestination(patrolPoint.position);  // Move towards patrol point
                    }
                    else
                    {
                        SwitchState(0);
                    }

                    // If within 15 units of the target, start chasing
                    if (distanceToTarget <= 15)
                    {
                        enemyState = EnemyState.Chase;
                    }
                    break;

                case EnemyState.Chase:
                    SwitchState(2);
                    ai.SetDestination(target.position);  // Move towards the target

                    // If within 5 units of the target, switch to attack
                    if (distanceToTarget <= 5)
                    {
                        enemyState = EnemyState.Attack;
                    }
                    // If distance to target is greater than 15, go back to idle
                    else if (distanceToTarget > 15)
                    {
                        enemyState = EnemyState.Idle;
                    }
                    break;

                case EnemyState.Attack:
                    SwitchState(3);

                    // If within 5-15 units of target, switch back to chase
                    if (distanceToTarget > 5 && distanceToTarget <= 15)
                    {
                        enemyState = EnemyState.Chase;
                    }
                    // If distance to target is greater than 15, go back to idle
                    else if (distanceToTarget > 15)
                    {
                        enemyState = EnemyState.Idle;
                    }
                    break;

                default:
                    SwitchState(0); // Default case, should never hit this unless there's an issue
                    break;
                   
            }
        }
        else
        {
            ai.SetDestination(patrolPoint.position);
        }

    }

    // Call this method to damage the zombie
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        health -= damage;
        anim.SetTrigger("Hits");
        StartCoroutine(HitDelay());
        if (health <= 0)
        {
            Debug.Log("Zombie die");
            Die();
        }
    }

    private IEnumerator HitDelay()
    {
        ai.SetDestination(patrolPoint.position);
        yield return new WaitForSeconds(1.5f);
        ai.SetDestination(target.position);
    }

    // Handle the zombie's death
    private void Die()
    {
        isDead = true;

        anim.SetTrigger("Death");

        // Trigger the OnDeath event
        OnDeath?.Invoke();

        // Optionally, add animations or sounds here
        Destroy(gameObject, 3f); // Destroy the zombie object
    }

}
