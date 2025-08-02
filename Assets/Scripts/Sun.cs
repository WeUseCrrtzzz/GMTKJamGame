using UnityEngine;

public class Sun : MonoBehaviour
{

    public float damagePerSecond = 5f;
    public float damageToSelf = 1f; // Amount of damage the sun takes per second
    public float damageRadius = 10f;
    private float nextDamageTime = 0f;
    public float damageInterval = 1.0f;
    private GameObject healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextDamageTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
            foreach (var hitCollider in hitColliders)
            {
                Health healthComponent = hitCollider.GetComponent<Health>();
                if (healthComponent != null)
                {
                    healthComponent.health -= damagePerSecond * Time.deltaTime;
                }
            }
            nextDamageTime = Time.time + damageInterval;
            Health selfHealthComponent = GetComponent<Health>();
            if (selfHealthComponent != null)
            {
                selfHealthComponent.health -= damageToSelf * Time.deltaTime;
            }
        }
    }
}
