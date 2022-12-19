using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int PlayerNumber = 1;       
    public Rigidbody Shell;            
    public Transform FireTransform;    
    public Slider AimSlider;           
    public AudioSource ShootingAudio;  
    public AudioClip ChargingClip;     
    public AudioClip FireClip;         
    public float MinLaunchForce = 15f; 
    public float MaxLaunchForce = 30f; 
    public float MaxChargeTime = 0.75f;

    /*
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    */

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
    }
}