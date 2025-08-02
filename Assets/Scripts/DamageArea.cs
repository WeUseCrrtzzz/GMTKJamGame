using UnityEngine;

public class DamageArea : MonoBehaviour
{

    public int damageAmount = 10;
    public float damageInterval = 1.0f;
    private float nextDamageTime = 0f;

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
        if (Time.time >= nextDamageTime)
        {
            Health healthComponent = other.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.health -= damageAmount;
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
    
}
