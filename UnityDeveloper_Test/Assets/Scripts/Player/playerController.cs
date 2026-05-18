using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class playerController : MonoBehaviour
{

    CharacterController CC;
    Animator Animator_Controller;
    [SerializeField] private InputActionReference IAR_Move;
    [SerializeField] private InputActionReference IAR_Jump;
    [SerializeField] private Camera Cam;
    [SerializeField, Range(2, 10), Tooltip("The Speed of PLayer")] private float moveSpeed;
    [SerializeField, Range(2, 10), Tooltip("The Speed by Which the Player will rotate")] private float LookRotationSpeed;
    [SerializeField, Range(0, 10), Tooltip("Gravatational Force")] private float Gravity_Force;
    [SerializeField, Range(0, 10), Tooltip("Jump Force")] private float Jump_Force;
    [HideInInspector]public float Vertical_velocity;

    public static bool CanWalk = true;
    public  bool CanRotate = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CC = this.transform.GetComponent<CharacterController>();
        Animator_Controller = this.transform.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        ApplyGravity();


    }
    // Update is called once per frame
    void Update()
    {
       
       // AlignToSurface();
        if (!CanWalk) return;
        PlayerMovement();
        PlayerRotation();
        Jump();
        
    }

    private void PlayerMovement()
    {
        Vector2 Input_Rot = IAR_Move.action.ReadValue<Vector2>();

        Vector3 FinalMovement = Vector3.zero;
        if (Input_Rot.sqrMagnitude >= 0.01f)
        {

        }
        Vector3 up = -HoloGram_Handler.Current_Gravity_Dir.normalized;
        Vector3 CamForward = Vector3.ProjectOnPlane(Cam.transform.forward, up).normalized;
        Vector3 CamRight = Vector3.ProjectOnPlane(Cam.transform.right, up).normalized; 
         FinalMovement = (CamForward * Input_Rot.y + CamRight * Input_Rot.x).normalized;
       
         FinalMovement -= HoloGram_Handler.Current_Gravity_Dir * Vertical_velocity;
         CC.Move(FinalMovement * moveSpeed * Time.deltaTime);


            Animator_Controller.SetBool("running", Input_Rot.sqrMagnitude >= 0.01f);
        
    }
    private void PlayerRotation()
    {
        if (!CanRotate) return;
        Vector2 Input_Rot = IAR_Move.action.ReadValue<Vector2>();
        if (Input_Rot.sqrMagnitude <= 0.01f) return;
        float TargetAngle = Mathf.Atan2(Input_Rot.x, Input_Rot.y) * Mathf.Rad2Deg + Cam.transform.eulerAngles.y; 
      
       Vector3 CamForward = Vector3.ProjectOnPlane(Cam.transform.forward,-HoloGram_Handler.Current_Gravity_Dir).normalized;
       Vector3 CamRight = Vector3.ProjectOnPlane(Cam.transform.right,-HoloGram_Handler.Current_Gravity_Dir).normalized;

        Vector3 moveDir = CamForward * Input_Rot.y + CamRight * Input_Rot.x;
        moveDir.Normalize();
      
        Quaternion TargetRotation = Quaternion.LookRotation(moveDir,-HoloGram_Handler.Current_Gravity_Dir.normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, LookRotationSpeed * Time.deltaTime);


    }
    

    public static void Enable_walk()
    {
        CanWalk = true;
    }
    private void Jump()
    {
        if (IAR_Jump.action.WasPerformedThisFrame() && CC.isGrounded)
        {
            Vertical_velocity += Jump_Force ;
        }
      //  Animator_Controller.SetBool("in Air",!CC.isGrounded);
    }

    private void ApplyGravity()
    {
        if(Vertical_velocity >= -2)
        {

        Vertical_velocity -= Gravity_Force * Time.deltaTime;
        }
    }

    public void AlignToSurface()
    {
        Vector3 GravityDir = HoloGram_Handler.Current_Gravity_Dir.normalized;
        Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, -GravityDir);
        transform.rotation = Quaternion.Slerp(
       transform.rotation,
       surfaceRotation,
       LookRotationSpeed * Time.deltaTime
   );
    }
}

