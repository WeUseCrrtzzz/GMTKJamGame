using UnityEngine;

public class RegenerableBio : MonoBehaviour
{

    public float value = 100f;

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

            if (genScript.enabled && Time.time >= genScript.nextRegenerationTime)
            {
                int regenerationAmount = genScript.generationAmount;
                // Regenerate resources
                Resources.radiation += regenerationAmount;
                value -= regenerationAmount;
                //Resources.labour += regenerationAmount;
                //Resources.hope += regenerationAmount;

                genScript.nextRegenerationTime = Time.time + genScript.regenerationInterval;
            }
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
                    value -= enemyScript.regenerationAmount;
                    enemyScript.nextRegenerationTime = Time.time + enemyScript.regenerationInterval;
                }
            }
        }

        if (value <= 0f)
        {
            Destroy(gameObject); // Destroy this object when value is depleted
        }
    }
}

