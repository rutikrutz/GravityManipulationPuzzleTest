using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class playerController : MonoBehaviour
{

    CharacterController CC;
    Animator Animator_Controller;
    [SerializeField] private InputActionReference IAR_Move;
    [SerializeField] private Camera Cam;
    [SerializeField, Range(2, 10), Tooltip("The Speed of PLayer")] private float moveSpeed;
    [SerializeField, Range(2, 10), Tooltip("The Speed by Which the Player will rotate")] private float LookRotationSpeed;
    [SerializeField, Range(2, 10), Tooltip("Gravatational Force")] private float Gravity_Force;

    public static bool CanWalk = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CC = this.transform.GetComponent<CharacterController>();
        Animator_Controller = this.transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanWalk) return;
        PlayerMovement();
        PlayerRotation();

    }

    private void PlayerMovement()
    {
        Vector2 Input_Rot = IAR_Move.action.ReadValue<Vector2>();

        if (Input_Rot.sqrMagnitude >= 0.01f)
        {
            Animator_Controller.SetBool("running", true);

            CC.Move(transform.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            Animator_Controller.SetBool("running", false);
        }
    }
    private void PlayerRotation()
    {
        Vector2 Input_Rot = IAR_Move.action.ReadValue<Vector2>();
        if (Input_Rot.sqrMagnitude <= 0.01f) return;
        float TargetAngle = Mathf.Atan2(Input_Rot.x, Input_Rot.y) * Mathf.Rad2Deg + Cam.transform.eulerAngles.y; 
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, TargetAngle, 0), LookRotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
 
}

