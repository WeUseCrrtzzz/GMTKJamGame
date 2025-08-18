using UnityEngine;

public class RegenerableBio : MonoBehaviour
{

    public float regenerationInterval = 5f; // Time in seconds between regenerations
    public float nextRegenerationTime = 0f;
    public float generationAmount = 1.5f; // Amount to regenerate
    public float damageAmount = 2; // Amount to regenerate
    public bool refined = false; // Indicates if the bio is refined

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] generators = GameObject.FindGameObjectsWithTag("Generator");
        foreach (GameObject generator in generators)
        {
            Generator genScript = generator.GetComponent<Generator>();

            float distance = Vector3.Distance(transform.position, generator.transform.position);
            if (distance > 4f) continue; // Skip if the generator is too far

            Generate();
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null && enemyScript.enabled)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= 4f && Time.time >= enemyScript.nextRegenerationTime)
                {
                    enemyScript.health += enemyScript.regenerationAmount;
                    if (refined)
                    {
                        gameObject.GetComponent<Health>().health -= enemyScript.regenerationAmount * 0.5f;
                    }
                    else
                    {
                        gameObject.GetComponent<Health>().health -= enemyScript.regenerationAmount;
                    }
                    enemyScript.nextRegenerationTime = Time.time + enemyScript.regenerationInterval;
                }
            }
        }

        if (gameObject.GetComponent<Health>().health <= 0f)
        {
            Destroy(gameObject); // Destroy this object when value is depleted
        }
    }


    void Generate()
    {
        if (Time.time >= nextRegenerationTime)
        {
            float regenerationAmount = generationAmount;
            // Regenerate resources
            //Resources.coins += regenerationAmount;
            Resources.coins += regenerationAmount * ShipUpgradeManager.ResourceRateMultiplier;
            if (refined)
            {
                gameObject.GetComponent<Health>().health -= damageAmount * 0.5f;
            }
            else
            {
                gameObject.GetComponent<Health>().health -= damageAmount;
            }
            nextRegenerationTime = Time.time + regenerationInterval;
        }
    }
}

