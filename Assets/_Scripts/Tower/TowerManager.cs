using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject head; 
    private GameObject currentTarget = null;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private float delay;
    [SerializeField] private GameObject bullet;
    private bool isShooting = false;
    [SerializeField] public int cost;
  
    void Start()
    {
        //defaultHeadPosition = head.transform;
    }

    private void FixedUpdate()
    {
        if (currentTarget != null)
        {
            head.transform.LookAt(currentTarget.transform);
            if (!isShooting)
                StartCoroutine("Shoot");
        }
            
    }

    public void ChangeTarget(GameObject target)
    {
        currentTarget = target;

        /*if (currentTarget == null)
            head.transform.rotation = new Quaternion();*/
    }

    IEnumerator Shoot ()
    {
        isShooting = true;
        GameObject shoot = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        shoot.GetComponent<DamageDealer>().target = currentTarget;
        yield return new WaitForSeconds(delay);
        isShooting = false;
    }


}
