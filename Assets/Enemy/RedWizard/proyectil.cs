using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectil : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private bool _hit;
    private float _direction;
    private BoxCollider2D boxCollider;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator= GetComponent<Animator>();
        boxCollider= GetComponent<BoxCollider2D>();

        
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
        _hit=true;
        boxCollider.enabled=false;
        animator.SetTrigger("explode");
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
