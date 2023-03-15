using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy_Death : MonoBehaviour
{
    private GUIManager _gui;

    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private float _tiempoDeAutodestruccion = 5;

    private void Start()
    {
        Destroy(gameObject, _tiempoDeAutodestruccion);

        GameObject guiGO = GameObject.Find("GUIManager");
        Assert.IsNotNull(guiGO, "No hay GUIManager");
        _gui = guiGO.GetComponent<GUIManager>();
        Assert.IsNotNull(_gui, "GUIManager no tiene componente");
    }

    void Update()
    {
        transform.Translate(0, -_speed * Time.deltaTime, 0);
    }

    // We write a function for the character to collect items
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            collision.GetComponent<SpriteRenderer>().enabled = false;
            _speed = 0;
            _gui.contador++;
            //We activate the animation of the Acron_collection when the item disapears
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject, 0.5f);
            Destroy(collision.gameObject, 0.5f);
        }

    }
}
