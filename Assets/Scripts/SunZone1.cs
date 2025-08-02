using UnityEngine;

public class SunZone1 : MonoBehaviour
{

    public float damagePerSecond = 5f;
    private float nextDamageTime = 0f;
    public float damageInterval = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collision) 
    {
        if (collision.gameObject.CompareTag("PlayerBlock") || 
            collision.gameObject.CompareTag("Enemy") || 
            collision.gameObject.CompareTag("ResourceBlock") ||
            collision.gameObject.CompareTag("RegenResourceBlock"))
        {
            Health healthComponent = collision.gameObject.GetComponent<Health>();
            if (healthComponent != null)
            {
                if (Time.time >= nextDamageTime)
                {
                    healthComponent.health -= damagePerSecond;
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }

        if (collision.gameObject.CompareTag("SunGenerator"))
        {
            SunGenerator sunGenerator = collision.gameObject.GetComponent<SunGenerator>();
            if (sunGenerator != null)
            {
                sunGenerator.Activate();
            }
        }
    }

    void OnTriggerExit(Collider collision) 
    {
        if (collision.gameObject.CompareTag("SunGenerator"))
        {
            SunGenerator sunGenerator = collision.gameObject.GetComponent<SunGenerator>();
            if (sunGenerator != null)
            {
                sunGenerator.Deactivate();
            }
        }
    }
}
