using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float StartingHealth = 100f;          
    public Slider Slider;                        
    public Image FillImage;                      
    public Color FullHealthColor = Color.green;  
    public Color ZeroHealthColor = Color.red;    
    public GameObject ExplosionPrefab;
    
    
    private AudioSource _explosionAudio;          
    private ParticleSystem _explosionParticles;   
    private float _currentHealth;  
    private bool _isDead;            


    private void Awake()
    {
        _explosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        _explosionAudio = _explosionParticles.GetComponent<AudioSource>();

        _explosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        _currentHealth = StartingHealth;
        _isDead = false;

        SetHealthUI();
    }
    

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        SetHealthUI();

        if(_currentHealth <= 0f && !_isDead){
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        Slider.value = _currentHealth;
        FillImage.color = Color.Lerp(ZeroHealthColor,FullHealthColor,_currentHealth/StartingHealth);
    }


    private void OnDeath()
    {
        _isDead = true;

        _explosionParticles.transform.position = transform.position;
        _explosionParticles.gameObject.SetActive(true);

        _explosionParticles.Play();
        _explosionAudio.Play();

        gameObject.SetActive(false);
    }
}