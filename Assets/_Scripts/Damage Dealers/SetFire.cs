using UnityEngine;

public class SetFire : SingleDD
{
    [SerializeField] private float fireDamage;

    protected override void TakeDamage(Collider other)
    {
        base.TakeDamage(other);
        other.GetComponent<EnemyManager>().StartCoroutine("Burning", fireDamage);
    }
}