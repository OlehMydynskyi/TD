using UnityEngine;

public class SingleDD : DamageDealer
{

    protected override void TakeDamage(Collider other)
    {
        base.TakeDamage(other);
        other.GetComponent<EnemyManager>().GetDamage(damage, damageType);
    }
}
