using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Proyectil : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private GameObject _proyectil;


    [SerializeField]
    private float _range=10;
    [SerializeField]
    private float _speed=5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Proyectil());
        StartCoroutine(Range());

    }

    // Update is called once per frame
    void Update()
    {}

    IEnumerator Proyectil()
    {
        while (true)
        {
            Instantiate(_proyectil,
                        transform.position,
                        transform.rotation);
            yield return new WaitForSeconds (1.2f);
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
