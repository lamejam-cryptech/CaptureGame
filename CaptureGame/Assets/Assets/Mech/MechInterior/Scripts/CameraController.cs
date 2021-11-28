using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 startRotation;

    [SerializeField]
    private float leftMax;
    [SerializeField]
    private float rightMax;

    [SerializeField]
    private float upMax;
    [SerializeField]
    private float DownMax;

    [SerializeField]
    private float Sensitivity = 1;

    Vector2 previousMousePos = Vector2.zero;
    Vector3 cameraRotation;

    public void Start()
    {
        previousMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        cameraRotation = this.transform.localEulerAngles;
        Cursor.visible = false;
    }

    public void Update()
    {
        Vector2 currentMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouseDelta = (currentMousePos - previousMousePos) * Sensitivity;
        previousMousePos = currentMousePos;
        
        cameraRotation.x = clamp(cameraRotation.x - mouseDelta.y, startRotation.x - DownMax, startRotation.x + upMax);
        cameraRotation.y = clamp(cameraRotation.y + mouseDelta.x, startRotation.y - leftMax, startRotation.y + rightMax);

        this.transform.localEulerAngles = cameraRotation;
    }

    private float clamp(float x, float min, float max)
    {
        if (x < min)
        {
            return min;
        }
        else if(x > max)
        {
            return max;
        }

        return x;
    }
}
