using UnityEngine;

public class ProtectionFactory : MonoBehaviour
{

    public float damageAmount = 10f;
    public float damageInterval = 1f;
    public float nextDamageTime = 0f;
    public float health = 500f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null && enemyScript.enabled)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= 4f && Time.time >= nextDamageTime)
                {
                    enemyScript.health -= damageAmount;
                    health -= damageAmount; // Reduce factory health as well
                    Resources.coins += damageAmount; // Reward coins for damaging enemies
                    nextDamageTime = Time.time + damageInterval;

                    if (enemyScript.health <= 0f)
                    {
                        Destroy(enemy); // Destroy the enemy if health is depleted
                    }
                }
            }
        }

        if (health <= 0f)
        {
            Destroy(gameObject); // Destroy this object when health is depleted
        }
    }
}
