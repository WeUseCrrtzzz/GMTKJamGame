using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SunZone1 : MonoBehaviour
{

    public float damagePerSecond = 5f;
    public List<GameObject> collidingBlocks = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collidingBlocks = collidingBlocks.Where(item => item != null).ToList();

        foreach (GameObject block in collidingBlocks) 
        {
            if (block.GetComponentInParent<Health>().gameObject.CompareTag("PlayerBlock") || 
                block.GetComponentInParent<Health>().gameObject.CompareTag("Enemy") || 
                block.GetComponentInParent<Health>().gameObject.CompareTag("ResourceBlock") ||
                block.GetComponentInParent<Health>().gameObject.CompareTag("RegenResourceBlock"))
            {
                Health healthComponent = block.GetComponentInParent<Health>();
                if (healthComponent != null)
                {
                    if (Time.time >= healthComponent.nextDamageTime)
                    {
                        healthComponent.health -= damagePerSecond;
                        healthComponent.nextDamageTime = Time.time + healthComponent.damageInterval;
                    }
                }
            }

            if (block.CompareTag("SunGenerator"))
            {
                SunGenerator sunGenerator = block.GetComponent<SunGenerator>();
                if (sunGenerator != null)
                {
                    sunGenerator.Activate(damagePerSecond);
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.GetComponentInParent<Health>() != null)
        {
            if (collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("PlayerBlock") || 
                collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("Enemy") || 
                collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("ResourceBlock") ||
                collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("RegenResourceBlock") ||
                collision.gameObject.CompareTag("SunGenerator"))
            {
                collidingBlocks.Add(collision.gameObject);
            }
        }
            
    }

    void OnTriggerExit(Collider collision) 
    {
        if (collision.gameObject.GetComponentInParent<Health>() != null)
        {
            if (collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("PlayerBlock") || 
                collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("Enemy") || 
                collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("ResourceBlock") ||
                collision.gameObject.GetComponentInParent<Health>().gameObject.CompareTag("RegenResourceBlock"))
            {
                collidingBlocks.Remove(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("SunGenerator"))
        {
            SunGenerator sunGenerator = collision.gameObject.GetComponent<SunGenerator>();
            if (sunGenerator != null)
            {
                sunGenerator.Deactivate();
            }
            collidingBlocks.Remove(collision.gameObject);
        }
    }
}
