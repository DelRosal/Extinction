using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



public class Pared : MonoBehaviour
{

    private Transform _transform;

    [SerializeField]
    private GameObject _ataqueOriginal;

    Vector3 posicionActual;

    Vector3 objectSize;

    void Start()
    {
        Debug.Log("START");

        _transform= GetComponent<Transform>();

        posicionActual = transform.position;

        objectSize = GetComponent<Renderer>().bounds.size;

        Assert.IsNotNull(_transform, "Es necesario para movimiento tener un transform");
        Assert.IsNotNull(_ataqueOriginal,"Dispario no puede ser nulo");
        //Assert.IsNotEqual(0,_speed, "Velocidad debe ser mayor a 0");
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {


            GameObject enemigo_actual = Instantiate(_ataqueOriginal, 
                        transform.position, 
                        transform.rotation);
 
            float _nXPos = (float) Random.Range((float)(-((objectSize.x/2) - 0.1)), (float)((objectSize.x/2) - 0.1));

            enemigo_actual.transform.position = new Vector3(_nXPos, posicionActual.y, posicionActual.z);

        }

    }

}
