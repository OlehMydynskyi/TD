using UnityEngine;

public class DeathBox : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyManager>().OnDeath();
        }
    }
}
