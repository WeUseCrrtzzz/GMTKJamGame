using UnityEngine;

public class ShipUpgradeManager : MonoBehaviour
{
    public static ShipUpgradeManager I;

    [Header("References")]
    public GameObject playerShip;              // assign your ship/root
    public GameObject hunterUpgradePrefab;        // small prefab that damages enemies around the ship
    public GameObject sunCollectorTriggerPrefab; // trigger with SunGenerator + tag "SunGenerator"

    [Header("Resource Rate")]
    public int resLvl = 0;                     // 0,1,2  ->  +0%, +25%, +50%
    public static float ResourceRateMultiplier => resLvlStatic switch { 0 => 1f, 1 => 1.25f, _ => 1.5f };
    static int resLvlStatic = 0;

    [Header("Build Discounts")]
    public int buildLvl = 0;                   // 0,1,2
    public static float BuildCostDiscount => buildLvlStatic switch { 0 => 0f, 1 => 5f, _ => 15f }; // dollars
    public static float BuildTimeReduction => buildLvlStatic switch { 0 => 0f, 1 => 2f, _ => 3f }; // seconds (for later)
    static int buildLvlStatic = 0;

    [Header("Unlock Flags")]
    public bool hasHunters = false;
    public bool hasShipCollector = false;

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    // ---------- BUY METHODS (hook to UI buttons) ----------

    public void BuyResourceRate()
    {
        // $75 → level 1 (+25%), $100 → level 2 (+50%)
        if (resLvl == 0 && Resources.coins >= 75) { Resources.coins -= 75; resLvl = 1; resLvlStatic = 1; }
        else if (resLvl == 1 && Resources.coins >= 100) { Resources.coins -= 100; resLvl = 2; resLvlStatic = 2; }
        else Debug.Log("Resource rate upgrade: insufficient funds or maxed.");
    }

    public void BuyHunters()
    {
        // $50: enable ship-based hunters (AoE tick)
        if (hasHunters) { Debug.Log("Hunters already purchased"); return; }
        if (Resources.coins < 50) { Debug.Log("Not enough Money for Hunters"); return; }

        Resources.coins -= 50;
        hasHunters = true;

        // Attach hunter aura to ship
        if (playerShip != null && hunterUpgradePrefab != null)
        {
            var aura = Instantiate(hunterUpgradePrefab, playerShip.transform);
            aura.transform.localPosition = Vector3.zero;
        }
    }

    public void BuyShipCollector()
    {
        // $50: attach SunCollector trigger so SunZone/SunProjectile can activate it
        if (hasShipCollector) { Debug.Log("Collector already purchased"); return; }
        if (Resources.coins < 50) { Debug.Log("Not enough Money for Ship Collector"); return; }

        Resources.coins -= 50;
        hasShipCollector = true;

        if (playerShip != null && sunCollectorTriggerPrefab != null)
        {
            var trig = Instantiate(sunCollectorTriggerPrefab, playerShip.transform);
            trig.transform.localPosition = Vector3.zero;
            // ensure "SunGenerator" tag + SunGenerator script component exist on prefab, and a SphereCollider (isTrigger = true)
        }
    }

    public void BuyBuildDiscount()
    {
        // $50 → L1 (-$5, -2s) ; $75 → L2 (-$15, -3s)
        if (buildLvl == 0 && Resources.coins >= 50) { Resources.coins -= 50; buildLvl = 1; buildLvlStatic = 1; }
        else if (buildLvl == 1 && Resources.coins >= 75) { Resources.coins -= 75; buildLvl = 2; buildLvlStatic = 2; }
        else Debug.Log("Build discount: insufficient funds or maxed.");
    }
}
