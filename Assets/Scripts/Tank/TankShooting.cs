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

    
    private string _fireButton;         
    private float _currentLaunchForce;  
    private float _chargeSpeed;         
    private bool _isFired;                


    private void OnEnable()
    {
        _currentLaunchForce = MinLaunchForce;
        AimSlider.value = MinLaunchForce;
    }


    private void Start()
    {
        _fireButton = "Fire" + PlayerNumber;

        _chargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.

        AimSlider.value = MinLaunchForce;

        if(_currentLaunchForce >= MaxLaunchForce && !_isFired){
            _currentLaunchForce = MaxLaunchForce;
            Fire();
        }
        else if(Input.GetButtonDown(_fireButton)){
            _isFired = false;
            _currentLaunchForce = MinLaunchForce;

            ShootingAudio.clip = ChargingClip;
            ShootingAudio.Play();
        }
        else if(Input.GetButton(_fireButton) && !_isFired){
            _currentLaunchForce+=_chargeSpeed * Time.deltaTime;
            AimSlider.value = _currentLaunchForce;
        }
        else if(Input.GetButtonUp(_fireButton) && !_isFired){
            Fire();
        }
    }


    private void Fire()
    {
        _isFired = true;

        Rigidbody shellRigidbodyInstance = Instantiate(Shell , FireTransform.position,FireTransform.rotation) as Rigidbody;
        shellRigidbodyInstance.velocity = _currentLaunchForce * FireTransform.forward;

        ShootingAudio.clip = FireClip;
        ShootingAudio.Play();

        _currentLaunchForce = MinLaunchForce;
    }
}