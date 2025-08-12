using UnityEngine;

public class Repairer : MonoBehaviour
{

    public int repairAmount = 10;
    public float repairInterval = 1.0f;
    private float nextRepairTime = 0f;

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
        if (other.CompareTag("PlayerBlock") || other.CompareTag("RegenResourceBlock")) 
        {
            if (Time.time >= nextRepairTime)
            {
                Health healthComponent = other.GetComponent<Health>();
                if (healthComponent != null && healthComponent.health < healthComponent.maxHealth)
                {
                    healthComponent.health += repairAmount;
                    if (healthComponent.health > healthComponent.maxHealth)
                    {
                        healthComponent.health = healthComponent.maxHealth;
                    }
                    nextRepairTime = Time.time + repairInterval;
                    
                }
            }
        }
    }
}
