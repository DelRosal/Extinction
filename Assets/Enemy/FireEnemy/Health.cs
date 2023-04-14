using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float startHealth;
    [SerializeField]
    private float currentHealth;

    private Animator animate;
    private bool _dead;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth=startHealth;        
    }

    public void TakeDamage(float _damage)
    {
        currentHealth= Mathf.Clamp(currentHealth-_damage,0,startHealth);

        if (currentHealth>0)
        {
        }
        else{
            if (!_dead)
            {
                animate.SetTrigger("die");
                GetComponent<enemyPatrol>().enabled=false;
                _dead= true;

            }
        }
    }

}
