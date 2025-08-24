using UnityEngine;

public class Health : MonoBehaviour
{

    public float health = 100;
    public float maxHealth = 100;
    private GameObject healthBar;
    public GameObject emptyBlock;
    public float nextDamageTime = 0f;
    public float damageInterval = 1.0f;
    public float nextRepairTime = 0f;
    public float repairInterval = 1.0f;
    public bool repairing = false;

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
            if (gameObject.name != ("Ship(Clone)") && !CompareTag("PlayerBlock")) 
            {
                Instantiate(emptyBlock, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }

        if (healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(0.9f, 0.9f, health / maxHealth * 0.9f);
        }

        if (repairing) 
        {
            var myMaterial = healthBar.transform.Find("Fill").GetComponent<Renderer>().material;
            myMaterial.EnableKeyword("_EMISSION");
        }
        else 
        {
            var myMaterial = healthBar.transform.Find("Fill").GetComponent<Renderer>().material;
            myMaterial.DisableKeyword("_EMISSION");
        }

    }
}
