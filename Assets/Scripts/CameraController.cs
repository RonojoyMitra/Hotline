using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Target, Player;
    [SerializeField] float CameraLagSpeed = 12.0f;
    [SerializeField] float CameraRadius = 5.0f;
    [SerializeField] float AimingRadius = 10.0f;
    [SerializeField] float CameraHeight = 27.0f;

    Vector3 MousePos;
    Vector3 DirectionVector;
    Vector3 CamPos;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.y = Target.position.y;

        DirectionVector = MousePos - Target.position;
        DirectionVector.Normalize();

        if(Input.GetKey(KeyCode.LeftShift))
        {
            CamPos = Player.position + (DirectionVector * AimingRadius);
        }
        else
        {
            CamPos = Player.position + (DirectionVector * CameraRadius);
        }

        CamPos.y = CameraHeight;    

        Player.rotation = Quaternion.LookRotation(DirectionVector);
        Target.position = Vector3.Lerp(Target.position, CamPos, CameraLagSpeed * Time.deltaTime);       
    }   
}
