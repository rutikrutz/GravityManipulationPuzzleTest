using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputActionReference IAR_look;
    public GameObject Camera;
    public GameObject Target;
    public float camera_SpringArm_length = 4;
    public float RotationSpeed;
     float Camera_Yaw;
    [SerializeField] LayerMask layermask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Decollider();
        Vector2 InputLook = IAR_look.action.ReadValue<Vector2>();
        Camera_Yaw += InputLook.x * RotationSpeed * Time.deltaTime;

        Quaternion SurfaceRotation = Quaternion.FromToRotation(Vector3.up, -HoloGram_Handler.Current_Gravity_Dir.normalized);

        Quaternion angleRot = Quaternion.AngleAxis(Camera_Yaw,-HoloGram_Handler.Current_Gravity_Dir.normalized);
        transform.rotation = angleRot * SurfaceRotation;
        //this.transform.rotation = Quaternion.Euler(0,Camera_Yaw, 0);

        Camera.transform.position = this.transform.position + this.transform.forward * -camera_SpringArm_length;
        Camera.transform.rotation = this.transform.rotation;
    }

    private void Decollider()
    {
        if(Physics.Raycast(Camera.transform.position,Target.transform.position,out RaycastHit hit,camera_SpringArm_length,layermask))
        {
           camera_SpringArm_length = Mathf.Lerp(camera_SpringArm_length,hit.distance,2 * Time.deltaTime);
        }
        else
        {
            camera_SpringArm_length = Mathf.Lerp(camera_SpringArm_length,4, 5 * Time.deltaTime);
        }
    }
}
