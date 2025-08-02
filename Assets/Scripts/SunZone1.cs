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

    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.CompareTag("PlayerBlock") || 
            collision.gameObject.CompareTag("Enemy") || 
            collision.gameObject.CompareTag("ResourceBlock"))
        {
            Health healthComponent = collision.gameObject.GetComponent<Health>();
            if (healthComponent != null)
            {
                if (Time.time >= nextDamageTime)
                {
                    healthComponent.health -= damagePerSecond * Time.deltaTime;
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }
}
