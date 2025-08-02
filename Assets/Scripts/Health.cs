using UnityEngine;

public class Health : MonoBehaviour
{

    public float health = 100;
    private GameObject healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = transform.Find("Health").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) 
        {
            Destroy(gameObject);
        }

        if (healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(health / 90f, 1, 1);
        }
    }
}
