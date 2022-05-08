using UnityEngine;

public class RangeDD : DamageDealer
{
    [SerializeField] private float radiusOfDamage;
    private Collider[] enemies;

    protected override void TakeDamage(Collider other)
    {
        base.TakeDamage(other);
        enemies = Physics.OverlapSphere(other.transform.position, radiusOfDamage, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
            enemies[i].GetComponent<EnemyManager>().GetDamage(damage, damageType);
    }
}
