using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    [SerializeField] private GameObject [] fireballs;

    private Animator animator;
    private float timer=Mathf.Infinity;


    // Start is called before the first frame update
    void Awake()
    {
        animator=GetComponent<Animator>();
        StartCoroutine(RecurrentAttack());
    }

    IEnumerator RecurrentAttack()
    {
        int i=0;
        while(true)
        {
            
            animator.SetTrigger("attack");
            fireballs[i].transform.position=startingPoint.position;
            fireballs[i].GetComponent<proyectil>().Direction(Mathf.Sign(transform.localScale.x));
            if(i==9){i=0;}
            else{i++;}
            yield return new WaitForSeconds (3.0f);
        }
        
    }
}
