using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Target, Player;
    [SerializeField] float CameraLagSpeed = 12.0f;
    [SerializeField] Vector3 Offset;
    Vector3 MousePos;
    Vector3 ScreenCenter;
    Vector3 TargetCenter;

    float DefaultFOV;    

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        ScreenCenter = new Vector3(Screen.width / 2, Camera.main.farClipPlane, Screen.height / 2);

        TargetCenter = Player.position + Offset;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 TargetPosition = Player.position + Offset;
        Vector3 SmoothedPosition = Vector3.Lerp(Target.position, TargetPosition, CameraLagSpeed * Time.deltaTime);
        Target.position = SmoothedPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MousePos = Input.mousePosition;

        Vector3 DirectionVector = ScreenCenter - MousePos;

        Vector3 WorldCameraOffset = Camera.main.ScreenToWorldPoint(DirectionVector);

        Target.position = TargetCenter + WorldCameraOffset;
    }
}
