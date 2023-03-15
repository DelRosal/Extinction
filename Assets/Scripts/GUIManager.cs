using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class GUIManager : MonoBehaviour
{

    [SerializeField]
    public TMP_Text _text;
    public float contador = 0;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_text, "Texto no puede ser nulo");

    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"Puntos: {contador}";
    }
}
