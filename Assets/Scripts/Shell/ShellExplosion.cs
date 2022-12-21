using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask TankMask;
    public ParticleSystem ExplosionParticles;       
    public AudioSource ExplosionAudio;              
    public float MaxDamage = 100f;                  
    public float ExplosionForce = 1000f;            
    public float MaxLifeTime = 2f;                  
    public float ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position,ExplosionRadius,TankMask);

        foreach (var collider in colliders)
        {
            Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();

            if(!targetRigidbody)
                continue;
            targetRigidbody.AddExplosionForce(ExplosionForce,transform.position,ExplosionRadius);

            TankHealth tankHealth = targetRigidbody.GetComponent<TankHealth>();

            if(!tankHealth)
                continue;
            float damage = CalculateDamage(targetRigidbody.position);
            tankHealth.TakeDamage(damage);
        }

        ExplosionParticles.transform.parent = null;
        ExplosionParticles.Play();
        ExplosionAudio.Play();

        Destroy(ExplosionParticles.gameObject,ExplosionParticles.duration);
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionTarget = targetPosition - transform.position;
        float explosionDistance = explosionTarget.magnitude;

        float relativeDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;
        float damage = relativeDistance * MaxDamage;
        damage = Mathf.Max(0f , damage);
        return damage;
    }
}