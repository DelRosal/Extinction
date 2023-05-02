using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDamaged : MonoBehaviour
{
    [SerializeField]
    private enemyPatrol patrolScript;
    [SerializeField]
    private Health patrol2Script;
    [SerializeField]
    private EnemyAttack wizardScript;

    public void TakeDamage()
    {
        if (patrolScript) { patrolScript.TakeDamage(); }
        else if (wizardScript) wizardScript.TakeDamage();
        else Debug.Log("No component");
    }
}
