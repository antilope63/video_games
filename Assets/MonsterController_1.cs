using UnityEngine;
using UnityEngine.AI;

public class MonsterController_1 : MonoBehaviour
{
    public Transform player;
    public float walkSpeed = 2.0f;
    public float runSpeed = 4.0f;
    public float detectionRange = 10.0f;
    public float attackRange = 2.0f;

    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // S'assurer que le NavMeshAgent est actif
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.updatePosition = true;
            agent.updateRotation = true;
        }
        else
        {
            Debug.LogError("NavMeshAgent n'est pas sur un NavMesh ou est null !");
        }
    }

    void Update()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            Debug.Log("Distance to player: " + distanceToPlayer);

            if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
            {
                Debug.Log("Player detected. Chasing player...");
                agent.isStopped = false;
                agent.speed = runSpeed;
                agent.SetDestination(player.position);

                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                Debug.Log("Set isRunning to true");
            }
            else if (distanceToPlayer <= attackRange)
            {
                Debug.Log("Player in attack range.");
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                Debug.Log("Set isRunning to false");
                // Ici tu peux ajouter du code pour l'animation d'attaque
            }
            else
            {
                Debug.Log("Player not in detection range.");
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                Debug.Log("Set isRunning to false");
            }
        }
        else
        {
            Debug.LogError("NavMeshAgent is not on NavMesh or is null.");
        }
    }
}
