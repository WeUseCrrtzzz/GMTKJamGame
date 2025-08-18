using UnityEngine;
using TMPro;

public class Sun : MonoBehaviour
{

    public float damagePerSecond = 5f;
    public float damageToSelf = 1f; // Amount of damage the sun takes per second
    public float damageRadius = 10f;
    private float nextDamageTime = 0f;
    public float damageInterval = 1.0f;
    public TextMeshProUGUI healthText;
    private GameObject healthBar;

    public float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Health selfHealthComponent = GetComponent<Health>();

        if (Time.time >= nextDamageTime)
        {
            //Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
            //foreach (var hitCollider in hitColliders)
            //{
            //    Health healthComponent = hitCollider.GetComponent<Health>();
            //    if (healthComponent != null)
            //    {
            //        healthComponent.health -= damagePerSecond * Time.deltaTime;
            //    }
            //}
            nextDamageTime = Time.time + damageInterval;
            if (selfHealthComponent != null)
            {
                selfHealthComponent.health -= damageToSelf;
            }
        }

        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);

        healthText.text = selfHealthComponent.health.ToString();
    }
}
