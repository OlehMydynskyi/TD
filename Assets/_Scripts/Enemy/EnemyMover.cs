using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float speed;
    private Transform target;
    [SerializeField] private LayerMask targetMask;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        target = Physics.OverlapSphere(transform.position, 100f, targetMask)[0].transform;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;
            

        if (Vector3.Distance(gameObject.transform.position, target.position) > 0.5f)
        {
            navMeshAgent.speed = speed;
            navMeshAgent.destination = target.position;
        }
           
    }
}
