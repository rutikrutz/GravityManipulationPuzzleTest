using UnityEngine;
using UnityEngine.InputSystem;

public class HoloGram_Handler : MonoBehaviour
{
    [SerializeField] private GameObject HoloGram;
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private Camera Cam;

    [SerializeField] private InputActionReference IAR_holoGramMovement;
    [SerializeField] private InputActionReference IAR_Enter;

    [SerializeField, Range(0, 10)]
    private float RotationRate = 5;

    Quaternion Desired_Rotation;

    public static Vector3 Current_Gravity_Dir =
        new Vector3(0, -1, 0);
    private void Start()
    {
        Current_Gravity_Dir = new Vector3(0, -1, 0);
    }
    void Update()
    {
        if (IAR_holoGramMovement.action.IsPressed())
        {
            HoloGram.SetActive(true);

            Vector2 Input_Dir =  IAR_holoGramMovement.action.ReadValue<Vector2>();

            Vector3 up = -Current_Gravity_Dir.normalized;

            Vector3 camForward = Vector3.ProjectOnPlane( Cam.transform.forward,up).normalized;

            Vector3 camRight =  Vector3.ProjectOnPlane( Cam.transform.right, up).normalized;

            // RIGHT Side 
            if (Input_Dir.x >= 0.5f)
            {
                Desired_Rotation =Quaternion.Euler(0, 0, 0);

                    Vector3 origin = mainPlayer.transform.position + up * 0.5f;
                    Vector3 dir = camRight;
                if (IAR_Enter.action.WasPressedThisFrame())
                {

                    if (Physics.Raycast(origin, dir, out RaycastHit hit, 30f))
                    {
                        Current_Gravity_Dir = -hit.normal;
                    }
                }
                Debug.DrawLine(mainPlayer.transform.position,dir * 30, Color.green, 1);
            }

            // LEFT SIDE
            else if (Input_Dir.x <= -0.5f)
            {
                Desired_Rotation =Quaternion.Euler(180, 180, 0);

                    Vector3 origin = mainPlayer.transform.position + up * 0.5f;
                    Vector3 dir = -camRight;
                if (IAR_Enter.action.WasPressedThisFrame())
                {

                    if (Physics.Raycast(origin, dir, out RaycastHit hit, 30f))
                    {
                        Current_Gravity_Dir = -hit.normal;
                    }
                }
                Debug.DrawLine(mainPlayer.transform.position,dir * 30, Color.green, 1);
            }

            // FRONT side
            else if (Input_Dir.y >= 0.5f)
            {
                Desired_Rotation =Quaternion.Euler(-90, -90, 0);

                    Vector3 origin = mainPlayer.transform.position + up * 0.5f;
                    Vector3 dir = camForward;
                if (IAR_Enter.action.WasPressedThisFrame())
                {

                    if (Physics.Raycast(origin, dir, out RaycastHit hit, 30f))
                    {
                        Current_Gravity_Dir = -hit.normal;
                    }
                }
                Debug.DrawLine(mainPlayer.transform.position, dir * 30, Color.green, 1);

            }

            // BACK side
            else if (Input_Dir.y <= -0.5f)
            {
                Desired_Rotation = Quaternion.Euler(90, 0, -90);

                    Vector3 origin = mainPlayer.transform.position + up * 0.5f;
                    Vector3 dir = -camForward;

                if (IAR_Enter.action.WasPressedThisFrame())
                {
                    if (Physics.Raycast(origin, dir, out RaycastHit hit, 30f))
                    {
                        Current_Gravity_Dir = -hit.normal;
                    }
                }
                Debug.DrawLine(mainPlayer.transform.position, dir * 30, Color.green, 1);
            }

            Quaternion baseRot =Quaternion.LookRotation(
                    camForward,
                    up
                );

            Quaternion targetRot =baseRot * Desired_Rotation;

            HoloGram.transform.rotation =
                Quaternion.Lerp(
                    HoloGram.transform.rotation,
                    targetRot,
                    RotationRate * Time.deltaTime
                );
        }
        else
        {
            HoloGram.SetActive(false);
        }
    }
}