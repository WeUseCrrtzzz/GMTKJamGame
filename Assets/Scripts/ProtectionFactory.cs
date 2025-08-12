using UnityEngine;

public class ProtectionFactory : MonoBehaviour
{

    public float damageAmount = 10f;
    public float generationAmount = 4f;
    public float damageInterval = 1f;
    public float nextDamageTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            if (Time.time >= nextDamageTime)
            {
                Health enemyHealth = other.GetComponent<Health>();
                Health thisHealth = gameObject.GetComponentInParent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.health -= damageAmount;
                    thisHealth.health -= damageAmount; // Reduce factory health as well
                    Resources.coins += generationAmount; // Reward coins for damaging enemies
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }
}
