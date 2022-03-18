using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.

public class EnemyMover : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float speed;
    private Transform target;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float damage;
    [SerializeField] private float delay;
    private bool isAttack = false;
    private Animator animator;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, 100f, targetMask) && target == null)
            target = Physics.OverlapSphere(transform.position, 100f, targetMask)[0].transform;
        else if (!Physics.CheckSphere(transform.position, 100f, targetMask))
            return;

        if (Vector3.Distance(gameObject.transform.position, target.position) > 2f)
        {
            animator.SetBool("Move", true);
            navMeshAgent.speed = speed;
            navMeshAgent.destination = target.position;
        }
           
        if (Vector3.Distance(gameObject.transform.position, target.position) < 2f)
        {
            navMeshAgent.speed = 0;
            gameObject.transform.LookAt(target); 
            animator.SetBool("Move", false);
            if (!isAttack)
            {
                isAttack = true;
                animator.SetBool("Attack", true);
            }     
        }
    }

    private void Attack()
    {
        if (target != null)
        {
            target.GetComponent<TargetManager>().GetDamage(damage);
        }
                
    }

    IEnumerator AttackDelay()
    {
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(delay);
        isAttack = false;
    }
}
