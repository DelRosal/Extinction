using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent (typeof(Rigidbody))]

public class Enemy_Jump : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _transform;

    [SerializeField]
    private float _forceJump= 3;
    [SerializeField]
    private float _range=10;
    [SerializeField]
    private float _speed=5;

    void Start()
    {
        _rigidbody=GetComponent<Rigidbody>();
        StartCoroutine(RecurrentJump());
        StartCoroutine(Range());

    }

    void Update()
    {}

    IEnumerator RecurrentJump()
    {
        while(true)
        {
            _rigidbody.AddForce(transform.up*_forceJump, ForceMode.Impulse);
            yield return new WaitForSeconds (0.8f);
        }
        
    }
    IEnumerator Range()
    {
        while (true)
        {
            transform.position= new Vector3 ((-_range/2)+Mathf.PingPong(Time.time * _speed, _range),
                                            transform.position.y,
                                            transform.position.z);
            yield return new WaitForSeconds (.01f);
        }
    }

}
