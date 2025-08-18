using UnityEngine;
using TMPro;

public class Resources : MonoBehaviour
{

    //[SerializeField] public static int labour = 0;
    public static float maxCoins = 100;
    [SerializeField] public static float coins = 100;
    public float maxHealth = 200;
    [SerializeField] public static float health = 200;
    public static float maxFuel = 50;
    public static float fuel = 0;

    private TextMeshProUGUI coinText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI fuelText;
    private GameObject healthBar;
    private GameObject fuelBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinText = transform.Find("Money").GetComponent<TextMeshProUGUI>();
        healthText = transform.Find("Health").GetComponent<TextMeshProUGUI>();
        fuelText = transform.Find("Fuel").GetComponent<TextMeshProUGUI>();
        healthBar = transform.Find("HealthBar").gameObject;
        fuelBar = transform.Find("FuelBar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Health playerHealth = GameObject.Find("Ship(Clone)")?.GetComponent<Health>();
        if (playerHealth != null)
        {
            maxHealth = playerHealth.maxHealth;
            health = playerHealth.health;
            healthText.text = "Health: " + health.ToString();
            healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);
        }

        coinText.text = "Money: " + coins.ToString() + " / " + maxCoins;
        
        fuelText.text = "Fuel: " + fuel.ToString();
        
        fuelBar.transform.localScale = new Vector3(fuel / 100, 1, 1);

        //if (coins > 100) coins = 100;
        if (health > maxHealth) health = maxHealth;
        if (fuel > maxFuel) fuel = maxFuel;

    }
}
