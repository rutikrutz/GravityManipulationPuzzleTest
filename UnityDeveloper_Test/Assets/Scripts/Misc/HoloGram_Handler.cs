using UnityEngine;
using UnityEngine.InputSystem;

public class HoloGram_Handler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private GameObject HoloGram;
    [SerializeField]private GameObject mainPlayer;
    [SerializeField]private InputActionReference IAR_holoGramMovement;
    [SerializeField,Range(0,10)] private float RotationRate;
    Quaternion Desired_Rotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IAR_holoGramMovement.action.IsPressed())
        {
            HoloGram.SetActive(true);
            Vector2 Input_Dir = IAR_holoGramMovement.action.ReadValue<Vector2>();
            
            if(Input_Dir.x >= 0.5f)
            {
               
                Desired_Rotation = Quaternion.Euler(0,0,0);
            }
            else if(Input_Dir.x <= -0.5f)
            {
                Desired_Rotation = Quaternion.Euler(180, 180, 0);
              
            }
            else if(Input_Dir.y >= 0.5f)
            {

                Desired_Rotation = Quaternion.Euler(-90, -90, 0);
            }
            else if(Input_Dir.y <= -0.5f)
            {
                Desired_Rotation = Quaternion.Euler(90, 0, -90);

            }
                HoloGram.transform.rotation = Quaternion.Lerp(HoloGram.transform.rotation, mainPlayer.transform.rotation * Desired_Rotation, RotationRate * Time.deltaTime);
           
        }
        else {

                HoloGram.transform.rotation = Quaternion.Lerp(HoloGram.transform.rotation, mainPlayer.transform.rotation *  Quaternion.Euler( 0, 0 , -90) , RotationRate * Time.deltaTime);
                HoloGram.SetActive(false);
        }
    }
}
