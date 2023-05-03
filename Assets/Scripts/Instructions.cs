using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject controls;
    private bool canHide;
    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(HideControls());
    }

    void Update()
    {
        if (!canHide) return;
        if (Input.GetAxis("Horizontal") != 0) { controls.SetActive(false); }
    }

    IEnumerator HideControls()
    {
        yield return new WaitForSeconds(2);
        canHide = true;
    }
}
