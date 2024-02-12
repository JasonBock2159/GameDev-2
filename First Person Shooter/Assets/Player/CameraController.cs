using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CameraController : MonoBehaviour
{
    //Debug
   // public TMP_Text debug_text;
    
    //Camera variables
    public Camera cam;
    private Vector2 look_input = Vector2.zero;
    private float look_speed = 60;
    private float horizontal_look_angle = 0;
    public bool invert_x = false;
    public bool invert_y = false;
    private int invert_x_factor = 1;
    private int invert_y_factor = 1;
    [Range(0.01f, 1f)]public float sensitivity;

   






    // Start is called before the first frame update
    void Start()
    {
       // Hide the mouse.
       Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Invert Camera
        if (invert_x) invert_x_factor = -1;
        if(invert_y) invert_y_factor=-1;

        //Get Components
       // character_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       


        Look();
    }

  

    public void GetLookInput(InputAction.CallbackContext context)
    {
        look_input = context.ReadValue<Vector2>();
    }

   
    private void Look()
    {
        // Left Right Movement
        transform.Rotate(Vector3.up, look_input.x * look_speed * Time.deltaTime * invert_x_factor *sensitivity);

        //Up Down Movement
        float angle = look_input.y * look_speed * Time.deltaTime * invert_y_factor * sensitivity;
        horizontal_look_angle-=angle;
        horizontal_look_angle = Mathf.Clamp(horizontal_look_angle, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(horizontal_look_angle,0,0);
    }

    
}

