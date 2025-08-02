using UnityEngine;

public class SunGenerator : MonoBehaviour
{

    public float generationAmount = 10; // Amount to generate
    public bool isActive = false; // Indicates if the generator is active
    public float regenerationInterval = 5f; // Time in seconds between regenerations
    public float nextRegenerationTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && Time.time >= nextRegenerationTime)
        {
            Regenerate();
            nextRegenerationTime = Time.time + regenerationInterval;
        }
    }

    void Regenerate()
    {
        Resources.fuel += generationAmount;
    }

    public void Activate()
    {
        isActive = true;
    }
}
