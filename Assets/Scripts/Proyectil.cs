using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Proyectil : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private float _tiempoDeAutodestruccion = 5;

    private GUIManager _gui;

    void Start()
    {

        Destroy(gameObject, _tiempoDeAutodestruccion);

        //ESTO VA A CAMBIAR
        GameObject guiGO = GameObject.Find("GUIManager");
        Assert.IsNotNull(guiGO, "No hay GUIManager");
        _gui = guiGO.GetComponent<GUIManager>();
        Assert.IsNotNull(_gui, "GUIManager no tiene componente");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, _speed * Time.deltaTime, 0);
    }

    void OnCollisionEnter(Collision c)
    {
        _gui._text.text = "Collision ENTER " + transform.name;
    }
}
