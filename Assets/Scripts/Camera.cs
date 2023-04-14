using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float playerLimitX;
    [SerializeField]
    private float xSmoothTime;
    private float xTargetPos;
    private float xOffset;
    [SerializeField]
    private float counterSpeed;
    private float counter;
    [SerializeField]
    private float playerPLimitY = 2f;
    [SerializeField]
    private float playerNLimitY = 2f;

    [SerializeField]
    private Vector3 stgDimensionsP;
    [SerializeField]
    private Vector3 stgDimensionsN;

    [SerializeField]
    private Transform[] limitsX;

    private void Start()
    {

    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        stgDimensionsP = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        stgDimensionsN = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        Vector3 newCameraPos = transform.position;
        Vector3 targetPos = target.position;
        targetPos.z = transform.position.z;

        if (targetPos.y >= stgDimensionsN.y + playerPLimitY && targetPos.y <= stgDimensionsP.y - playerNLimitY)
        {
            targetPos.y = transform.position.y;
        }
        else
        {
            if (targetPos.y < stgDimensionsN.y + playerPLimitY)
            {
                targetPos.y += (transform.position.y - stgDimensionsN.y) - playerPLimitY;
            }
            else if (targetPos.y > stgDimensionsP.y - playerNLimitY)
            {
                targetPos.y += (transform.position.y - stgDimensionsP.y) + playerNLimitY;
            }
        }

        if (horizontal > 0)
        {
            counter += Time.deltaTime * counterSpeed;
        }
        else if (horizontal < 0)
        {
            counter -= Time.deltaTime * counterSpeed;
        }

        if (counter > 1) counter = 1;
        else if (counter < 0) counter = 0;

        xOffset = Mathf.Lerp(-playerLimitX, playerLimitX, counter);
        targetPos.x += xOffset;

        Vector3 smoothPos = Vector3.Lerp(newCameraPos, targetPos, 1);

        smoothPos.x = Mathf.Clamp(smoothPos.x, limitsX[0].position.x, limitsX[1].position.x);

        transform.position = smoothPos;
    }
}