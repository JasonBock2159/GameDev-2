using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Pistol : MonoBehaviour
   
{
      public GunData gun_data;
public Camera cam;
private Ray ray;
    private int ammo_in_clip;
    // Start is called before the first frame update

    //Debug
    public TMP_Text debug_text;

    //Shooting
    private bool primary_fire_is_shooting=false;
    private bool primary_fire_hold = false;
    private float shoot_delay_timer = 0.0f;
void Start()
    {
        ammo_in_clip = gun_data.ammo_per_clip; 
    }

    // Update is called once per frame
    void Update()
    {
      debug_text.text= "Ammo In Clip:" + ammo_in_clip.ToString();
        PrimaryFire();

        if(shoot_delay_timer>0) shoot_delay_timer-= Time.deltaTime;
    }

    public void GetPrimaryFireInput(InputAction.CallbackContext context)
    {//Checking for initial button press
        if (context.phase == InputActionPhase.Started)
        {
            primary_fire_is_shooting = true;
        }
        if (gun_data.automatic)
        {//Check if hold action was complete
            if(context.interaction is HoldInteraction && context.phase== InputActionPhase.Performed)
            {
                primary_fire_hold=true;
            }
        }
        //Check if button was released
        if(context.phase== InputActionPhase.Canceled)
        {
            primary_fire_hold = false;
            primary_fire_is_shooting = false;
        }
    }

    public void GetSecondaryFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) SecondaryFire();
    }
    private void PrimaryFire()
    {
        if (shoot_delay_timer <= 0)
        {
            if (primary_fire_is_shooting || primary_fire_hold)
            {
               
                shoot_delay_timer=gun_data.primary_fire_delay;//Delay Shooting

                //Determine Spread
              
                Vector3 dir=Quaternion.AngleAxis(Random.Range(-gun_data.spread,gun_data.spread), Vector3.up)*cam.transform.forward;
                dir= Quaternion.AngleAxis(Random.Range(-gun_data.spread, gun_data.spread), Vector3.right) * cam.transform.forward;
                
                //RayCast
                ray = new Ray(cam.transform.position, dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, gun_data.range))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green, 0.05f);
                }
                ammo_in_clip--;
                if (ammo_in_clip <= 0) ammo_in_clip = gun_data.ammo_per_clip;

                primary_fire_is_shooting = false;
            }
        }
        
       
    }

    private void SecondaryFire()
    {

    }
}
