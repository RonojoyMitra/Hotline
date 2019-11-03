using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Target, Player;
    [SerializeField] float CameraLagSpeed = 12.0f;
    [SerializeField] Vector3 Offset;  
    [SerializeField] float CameraRadius = 5.0f;
    [SerializeField] float AimingRadius = 10.0f;

    Vector3 MousePos;
    Vector3 TargetCenter;
    Vector3 DirectionVector;
    Vector3 CamPos;  

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        TargetCenter = Target.position;
    }

    void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.y = TargetCenter.y;

        DirectionVector = MousePos - TargetCenter;
        DirectionVector.Normalize();

        if(Input.GetKey(KeyCode.LeftShift))
        {
            CamPos = DirectionVector * AimingRadius;
        }
        else
        {
            CamPos = DirectionVector * CameraRadius;
        }
        
        CamPos.y = 27;
             
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CamPos, 0.4f);
        Player.rotation = Quaternion.LookRotation(DirectionVector);
    }

    void FixedUpdate()
    {
        Vector3 TargetPosition = Player.position + Offset;
        Vector3 SmoothedPosition = Vector3.Lerp(Target.position, TargetPosition, CameraLagSpeed * Time.deltaTime);
        Target.position = SmoothedPosition;
    }
}
