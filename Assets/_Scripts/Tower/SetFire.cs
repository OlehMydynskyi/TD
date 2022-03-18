using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFire : DamageDealer
{
    [SerializeField] private float fireDamage;

    protected override void TakeDamage(Collider other)
    {
        base.TakeDamage(other);
        other.GetComponent<EnemyManager>().StartCoroutine("Burning", fireDamage);
    }
}
