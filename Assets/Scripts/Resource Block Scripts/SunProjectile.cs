using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SunProjectile : MonoBehaviour
{
    [Header("HP / Decay")]
    public float maxHealth = 25f;
    public float health = 25f;
    public float selfDotPerSecond = 1f;     // loses 1 HP / sec

    [Header("Sun DOT Area")]
    public float damagePerTick = 5f;        // damage applied to nearby blocks
    public float damageInterval = 1f;       // tick every X seconds
    public float damageRadius = 2.5f;       // DOT radius

    [Header("Movement")]
    public float gridStep = 1f;             // distance per “tile” in world units (match MapGenerator.gridSpacing)
    public float moveInterval = 5f;        // moves down 1 tile per 5s
    public Vector3 moveDirection = new Vector3(0f, 0f, -1f); // “down” in your top-down setup

    [Header("Tags")]
    public string sunCollectorTag = "SunGenerator"; // your collector object tag

    float _nextDamageTime;
    float _nextMoveTime;

    void Awake()
    {
        // Ensure trigger collider exists for Sun-Collector activation
        var col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = damageRadius;
    }

    void Update()
    {
        // Self-decay
        health -= selfDotPerSecond * Time.deltaTime;
        if (health <= 0f) { Destroy(gameObject); return; }

        // Area damage tick
        if (Time.time >= _nextDamageTime)
        {
            DoAreaDamage();
            _nextDamageTime = Time.time + damageInterval;
        }

        // Grid-hop movement
        if (Time.time >= _nextMoveTime)
        {
            TryStep();
            _nextMoveTime = Time.time + moveInterval;
        }
    }

    void DoAreaDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (var hit in hits)
        {
            // 1) Damage anything with Health
            var h = hit.GetComponent<Health>();
            if (h != null)
                h.health -= damagePerTick;

            // 2) If a Sun-Collector enters, "activate" it with our damage value
            if (hit.CompareTag(sunCollectorTag))
            {
                var gen = hit.GetComponent<SunGenerator>();
                if (gen != null) gen.Activate(damagePerTick);
            }
        }
    }

    void TryStep()
    {
        Vector3 target = transform.position + moveDirection.normalized * gridStep;

        // If it hits a block with Health, destroy both (per spec)
        Collider[] hits = Physics.OverlapBox(target, Vector3.one * 0.45f, Quaternion.identity);
        foreach (var c in hits)
        {
            if (c.attachedRigidbody != null && c.attachedRigidbody.gameObject == gameObject) continue;

            var h = c.GetComponent<Health>();
            if (h != null)
            {
                h.health = 0f; // kill the block
                Destroy(gameObject); // and the projectile
                return;
            }
        }

        // Otherwise move down one tile
        transform.position = target;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(sunCollectorTag))
        {
            var gen = other.GetComponent<SunGenerator>();
            if (gen != null) gen.Deactivate();
        }
    }
}
