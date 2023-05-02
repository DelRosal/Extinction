using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private Transform attackPoint2;
    [SerializeField]
    private float attackSize;
    [SerializeField]
    private LayerMask attackLayer;
    [SerializeField]
    private Animator anim;

    private int simbol;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") < 0) simbol = -1;
            else if (Input.GetAxisRaw("Horizontal") > 0) simbol = 1;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerAttack();
        }
    }

    void PlayerAttack()
    {
        anim.SetTrigger("Bite");

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll((simbol > 0 ? attackPoint.position : attackPoint2.position), attackSize, attackLayer);

        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log(enemy.name);
            enemy.gameObject.GetComponent<EnemyDamaged>().TakeDamage();
            Debug.Log("Hit enemy");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawSphere((simbol > 0 ? attackPoint.position : attackPoint2.position), attackSize);
    }
}
