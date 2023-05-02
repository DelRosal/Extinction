using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class enemyPatrol : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private int health = 3;
    [Header("Patrol")]
    [SerializeField]
    private Transform _right;
    [SerializeField]
    private Transform _left;

    private bool recieveDamage;
    [SerializeField]
    private float damageAnimationTime = 1f;

    [Header("FireEnemy")]
    [SerializeField] private Transform _enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float _speed;
    private Vector3 initScale;
    private bool movingLeft;

    //Collision with enemy sound
    [SerializeField] private AudioSource collisonSound;
    // We write a function for the character to collect items
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collisonSound.Play();

        }

    }


    void Awake()
    {
        initScale = _enemy.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("die");
            Destroy(gameObject, 1);
        }

        if (recieveDamage) return;
        if (movingLeft)
        {
            if (_enemy.position.x >= _left.position.x)
            {
                DirectionMovement(-1);
            }
            else
            {
                DirectionChange();
            }

        }
        else
        {
            if (_enemy.position.x <= _right.position.x)
            {
                DirectionMovement(1);
            }
            else
            {
                DirectionChange();
            }
        }

    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;
    }

    private void DirectionMovement(int _direction)
    {

        _enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        _enemy.position = new Vector3(_enemy.position.x + Time.deltaTime * _direction * _speed,
                                    _enemy.position.y,
                                    _enemy.position.z);

    }

    public void TakeDamage()
    {
        anim.SetTrigger("Damaged");
        health -= 1;
        recieveDamage = true;
        StartCoroutine(RecieveDamage());
    }

    IEnumerator RecieveDamage()
    {
        yield return new WaitForSeconds(damageAnimationTime);
        recieveDamage = false;
    }
}
