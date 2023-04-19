using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectil : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _lifetime = 2;

    private bool _hit;
    private float _direction;
    private BoxCollider2D boxCollider;
    private Animator animator;

    // Audio source to the collision
    [SerializeField] private AudioSource collisonSound;

    // Start is called before the first frame update
    void Awake()
    {
        animator= GetComponent<Animator>();
        boxCollider= GetComponent<BoxCollider2D>();
    }

    void Start(){
        StartCoroutine(DestroyProyectile());
    }

    IEnumerator DestroyProyectile(){
        yield return new WaitForSeconds(_lifetime);
        _hit=true;
        boxCollider.enabled=false;
        animator.SetTrigger("explode");
    }

    // Update is called once per frame
    void Update()
    {
        if(_hit)
        {return;}
        transform.Translate(_direction*_speed*Time.deltaTime,0,0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Ground")){
        _hit=true;
        boxCollider.enabled=false;
        animator.SetTrigger("explode");
        if (!collision.CompareTag("Ground")) collisonSound.Play();
        }
    }

    public void Direction(float dir){
        
        _direction=dir;
        gameObject.SetActive(true);
        _hit=false;
        boxCollider.enabled=true;

        float localScaleX= transform.localScale.x;
        if(Mathf.Sign(localScaleX)!=_direction)
        {
            localScaleX=-localScaleX;
        }
       
        transform.localScale= new Vector3 (localScaleX,
                                            transform.localScale.y,
                                            transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
