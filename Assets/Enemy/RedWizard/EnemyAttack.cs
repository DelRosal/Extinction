using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    [SerializeField] private GameObject fireballs;

    private Animator animator;
    private float timer = Mathf.Infinity;
    [SerializeField]
    private float health = 3;
    private bool recieveDamage;
    [SerializeField]
    private float damageAnimationTime = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(RecurrentAttack());
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject, 1);
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        recieveDamage = true;

        animator.SetTrigger("Damaged");

        StartCoroutine(RecieveDamage());
    }

    IEnumerator RecieveDamage()
    {
        yield return new WaitForSeconds(damageAnimationTime);
        recieveDamage = false;
    }

    IEnumerator RecurrentAttack()
    {
        int i = 0;
        while (true)
        {
            if (recieveDamage) yield return new WaitForSeconds(3);

            animator.SetTrigger("attack");
            GameObject actualFireball = Instantiate(fireballs, startingPoint.position, Quaternion.identity);
            actualFireball.GetComponent<proyectil>().Direction(Mathf.Sign(-transform.localScale.x));
            if (i == 9) { i = 0; }
            else { i++; }
            yield return new WaitForSeconds(3.0f);
        }

    }
}
