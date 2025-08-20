using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Repairer : MonoBehaviour
{
    public int repairAmount = 10;

    // Global book-keeping across all Repairers
    private static readonly Dictionary<Health, int> overlapCount = new();            // how many Repairers cover this Health
    private static readonly Dictionary<Health, Coroutine> runningRepair = new();      // the single active coroutine per Health

    // Local tracking so we can clean up properly on disable/destroy
    private readonly HashSet<Health> insideNow = new();

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.CompareTag("PlayerBlock") || other.CompareTag("RegenResourceBlock")))
            return;

        Health h = other.GetComponent<Health>();
        if (h == null) return;

        insideNow.Add(h);

        // Increase coverage count
        if (!overlapCount.ContainsKey(h)) overlapCount[h] = 0;
        overlapCount[h]++;

        // If this is the FIRST Repairer covering this health, start the one-and-only coroutine
        if (!runningRepair.ContainsKey(h))
        {
            Coroutine c = h.StartCoroutine(RepairOverTime(h));
            runningRepair[h] = c;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.CompareTag("PlayerBlock") || other.CompareTag("RegenResourceBlock")))
            return;

        Health h = other.GetComponent<Health>();
        if (h == null) return;

        HandleExit(h);
    }

    private void OnDisable()
    {
        // If this Repairer is turned off/destroyed while overlapping, make sure we decrement counts
        if (insideNow.Count == 0) return;

        foreach (var h in insideNow)
            HandleExit(h);

        insideNow.Clear();
    }

    private void HandleExit(Health h)
    {
        insideNow.Remove(h);

        if (!overlapCount.ContainsKey(h)) return;

        overlapCount[h] = Mathf.Max(0, overlapCount[h] - 1);

        // If no Repairers remain, stop the single coroutine
        if (overlapCount[h] == 0)
        {
            if (runningRepair.TryGetValue(h, out var c) && c != null)
            {
                // Coroutine was started on the Health component itself
                h.StopCoroutine(c);
            }
            runningRepair.Remove(h);
            overlapCount.Remove(h);
        }
    }

    // Runs ONCE per Health while at least one Repairer overlaps it
    private IEnumerator RepairOverTime(Health healthComponent)
    {
        // safety: if Health goes away, exit
        while (healthComponent != null)
        {
            // If there are no Repairers covering this health anymore, exit
            if (!overlapCount.TryGetValue(healthComponent, out int count) || count <= 0)
                yield break;

            // Apply a single tick of repair (non-stacking)
            if (healthComponent.health < healthComponent.maxHealth)
            {
                healthComponent.health = Mathf.Min(
                    healthComponent.maxHealth,
                    healthComponent.health + repairAmount
                );
            }

            // Wait according to the block's own repair interval
            yield return new WaitForSeconds(healthComponent.repairInterval);
        }
    }
}
