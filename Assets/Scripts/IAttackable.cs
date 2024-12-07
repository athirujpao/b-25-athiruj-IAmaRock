using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public GameObject RockPrefab { get; set; }
    void TakeDamage(int damage);
}
