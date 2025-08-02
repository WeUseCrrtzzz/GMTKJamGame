using UnityEngine;
using TMPro;

public class Resources : MonoBehaviour
{

    //[SerializeField] public static int labour = 0;
    [SerializeField] public static float coins = 0;
    [SerializeField] public static float health = 100;
    public static float fuel = 0;

    private TextMeshProUGUI coinText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI fuelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinText = transform.Find("Money").GetComponent<TextMeshProUGUI>();
        healthText = transform.Find("Health").GetComponent<TextMeshProUGUI>();
        fuelText = transform.Find("Fuel").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Money: " + coins.ToString();
        healthText.text = "Health: " + health.ToString();
        fuelText.text = "Fuel: " + fuel.ToString();
    }
}
