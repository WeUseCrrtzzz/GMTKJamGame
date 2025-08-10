using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public enum UpgradeKind { ResourceRate, Hunters, ShipCollector, BuildDiscount }
    public UpgradeKind kind;

    [Header("UI")]
    public Button button;
    public TextMeshProUGUI label;

    void Reset()
    {
        button = GetComponent<Button>();
        label = GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        if (button != null) button.onClick.AddListener(OnClick);
        InvokeRepeating(nameof(Refresh), 0f, 0.2f); // light polling is fine for a jam
    }

    void OnDisable()
    {
        if (button != null) button.onClick.RemoveListener(OnClick);
        CancelInvoke(nameof(Refresh));
    }

    void OnClick()
    {
        var mgr = ShipUpgradeManager.I;
        if (mgr == null) return;

        switch (kind)
        {
            case UpgradeKind.ResourceRate: mgr.BuyResourceRate(); break;
            case UpgradeKind.Hunters: mgr.BuyHunters(); break;
            case UpgradeKind.ShipCollector: mgr.BuyShipCollector(); break;
            case UpgradeKind.BuildDiscount: mgr.BuyBuildDiscount(); break;
        }
        Refresh();
    }

    void Refresh()
    {
        var mgr = ShipUpgradeManager.I;
        if (mgr == null || button == null || label == null) return;

        string txt;
        bool interactable = true;

        switch (kind)
        {
            case UpgradeKind.ResourceRate:
                // Levels: 0->1 ($75), 1->2 ($100), 2 MAX
                if (mgr.resLvl == 0) { txt = "Increase Resource Rate (+25%)  —  $75"; interactable = Resources.coins >= 75; }
                else if (mgr.resLvl == 1) { txt = "Increase Resource Rate (+50%)  —  $100"; interactable = Resources.coins >= 100; }
                else { txt = "Increase Resource Rate  —  MAX"; interactable = false; }
                break;

            case UpgradeKind.Hunters:
                if (!mgr.hasHunters) { txt = "Ship‑based Hunters  —  $50"; interactable = Resources.coins >= 50; }
                else { txt = "Ship‑based Hunters  —  OWNED"; interactable = false; }
                break;

            case UpgradeKind.ShipCollector:
                if (!mgr.hasShipCollector) { txt = "Ship‑based Sol Collector  —  $50"; interactable = Resources.coins >= 50; }
                else { txt = "Ship‑based Sol Collector  —  OWNED"; interactable = false; }
                break;

            case UpgradeKind.BuildDiscount:
                // Levels: 0->1 ($50), 1->2 ($75), 2 MAX
                if (mgr.buildLvl == 0) { txt = "Build Discount (-$5 & -2s)  —  $50"; interactable = Resources.coins >= 50; }
                else if (mgr.buildLvl == 1) { txt = "Build Discount (-$15 & -3s)  —  $75"; interactable = Resources.coins >= 75; }
                else { txt = "Build Discount  —  MAX"; interactable = false; }
                break;

            default:
                txt = name; break;
        }

        label.text = txt;
        button.interactable = interactable;
    }
}
