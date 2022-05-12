using System.Collections;
using UnityEngine;
using Object_Pooling;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject head; 
    private GameObject currentTarget = null;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private float delay;
    [SerializeField] private DamageDealer bullet;
    private bool isShooting = false;
    [SerializeField] public int cost;
    private ObjectPool objectPool;
  
    void Start()
    {
        objectPool = ObjectPool.Instance;
    }

    private void FixedUpdate()
    {
        if (currentTarget != null && currentTarget.activeInHierarchy == true)
        {
            head.transform.LookAt(currentTarget.transform);
            if (!isShooting)
                StartCoroutine("Shoot");
        }
            
    }

    public void ChangeTarget(GameObject target)
    {
        currentTarget = target;
    }

    IEnumerator Shoot ()
    {
        isShooting = true;
        objectPool.GetObject(bullet.GetComponent<DamageDealer>()).OnSpawn(shootPoint, currentTarget);
        yield return new WaitForSeconds(delay);
        isShooting = false;
    }


}