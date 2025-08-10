using UnityEngine;

public class HunterUpgrade : MonoBehaviour
{
    public float radius = 3.5f;
    public float damagePerSecond = 10f;
    public float tickInterval = 0.5f;
    float nextTick;

    void Update()
    {
        if (MainMenu.IsPaused) return;

        if (Time.time < nextTick) return;
        nextTick = Time.time + tickInterval;

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var h in hits)
        {
            if (h.CompareTag("Enemy"))
            {
                var hp = h.GetComponent<Health>();
                if (hp != null) hp.health -= damagePerSecond * tickInterval;
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.3f, 0.8f, 1f, 0.2f);
        Gizmos.DrawSphere(transform.position, radius);
    }
#endif
}