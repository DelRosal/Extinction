using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackSize;
    [SerializeField]
    private LayerMask attackLayer;
    [SerializeField]
    private Animator anim;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerAttack();
        }
    }

    void PlayerAttack()
    {
        anim.SetTrigger("Bite");

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackSize, attackLayer);

        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log("Hit enemy");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawSphere(attackPoint.position, attackSize);
    }
}
