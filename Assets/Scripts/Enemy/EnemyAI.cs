using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float loseRange = 12f; // little hysteresis helps

    [Header("Chase")]
    [SerializeField] private float repathInterval = 0.2f;
    [SerializeField] private float repathDistance = 0.5f;

    private NavMeshAgent agent;
    private float nextRepathTime;
    private Vector3 lastTargetPos;

    private enum State { Idle, Chase }
    private State state = State.Idle;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Prevent the agent from rotating on X/Z axes (keeps model upright)
        agent.updateUpAxis = false;
    }

    void Start()
    {
        if (player != null) lastTargetPos = player.position;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // State transitions
        if (state == State.Idle && dist <= detectionRange)
        {
            state = State.Chase;
            lastTargetPos = player.position;
            agent.isStopped = false;
            agent.SetDestination(lastTargetPos);
            nextRepathTime = Time.time + repathInterval;
        }
        else if (state == State.Chase && dist > loseRange)
        {
            state = State.Idle;
            agent.ResetPath();
            agent.isStopped = true;
            return;
        }

        // Chase behavior
        if (state == State.Chase && Time.time >= nextRepathTime)
        {
            Vector3 tpos = player.position;

            if ((tpos - lastTargetPos).sqrMagnitude >= repathDistance * repathDistance)
            {
                agent.SetDestination(tpos);
                lastTargetPos = tpos;
            }

            nextRepathTime = Time.time + repathInterval;
        }
    }
}
