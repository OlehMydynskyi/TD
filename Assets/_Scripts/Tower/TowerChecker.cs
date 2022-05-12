using UnityEngine;

public class TowerChecker : MonoBehaviour
{
    private TowerManager towerManager;
    [SerializeField] private float radiusOfVisibility;
    [SerializeField] private LayerMask enemies;
    private GameObject currentTarget;

    protected void Start()
    {
        towerManager = GetComponent<TowerManager>();   
    }

    private void FixedUpdate()
    { 
        if(Physics.CheckSphere(transform.position, radiusOfVisibility, enemies) && currentTarget == null)
        {
            currentTarget = Physics.OverlapSphere(transform.position, radiusOfVisibility, enemies)[0].gameObject;
            towerManager.ChangeTarget(currentTarget);
        }
        else if (currentTarget != null)
        {
            if(Vector3.Distance(transform.position, currentTarget.transform.position) > radiusOfVisibility || currentTarget.activeInHierarchy == false)
            {
                //Debug.Log("Set null");
                currentTarget = null;
                towerManager.ChangeTarget(currentTarget);
            }   
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusOfVisibility);
    }
}