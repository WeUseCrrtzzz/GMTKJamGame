using UnityEngine;

public class SunGenerator : MonoBehaviour
{

    public float generationAmount = 1; // Amount to generate
    public float fuelGenerationAmount = 1; // Amount to generate
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
        if (MainMenu.IsPaused) return;

        if (isActive && Time.time >= nextRegenerationTime)
        {
            Regenerate();
            nextRegenerationTime = Time.time + regenerationInterval;
        }
    }

    void Regenerate()
    {
        Resources.fuel += fuelGenerationAmount;
        Resources.coins += generationAmount;
    }

    public void Activate(float amount)
    {
        isActive = true;
        generationAmount = amount;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
