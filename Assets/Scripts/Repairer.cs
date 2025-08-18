using UnityEngine;
using System.Collections;

public class Repairer : MonoBehaviour
{
    public int repairAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBlock") || other.CompareTag("RegenResourceBlock"))
        {
            Health healthComponent = other.GetComponent<Health>();
            if (healthComponent != null)
            {
                StartCoroutine(RepairOverTime(healthComponent));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBlock") || other.CompareTag("RegenResourceBlock"))
        {
            Health healthComponent = other.GetComponent<Health>();
            if (healthComponent != null)
            {
                StopCoroutine(RepairOverTime(healthComponent));
            }
        }
    }

    private IEnumerator RepairOverTime(Health healthComponent)
    {
        while (healthComponent.health < healthComponent.maxHealth)
        {
            healthComponent.health += repairAmount;
            if (healthComponent.health > healthComponent.maxHealth)
            {
                healthComponent.health = healthComponent.maxHealth;
            }
            yield return new WaitForSeconds(healthComponent.repairInterval);
        }
    }
}
