using UnityEngine;
using TMPro;

public class DistanceTexts : MonoBehaviour
{

    public TextMeshProUGUI DDUText;
    public TextMeshProUGUI DistanceToCenterText;
    public SolJumpManager jumpManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DDUText.text = "DDU : " + jumpManager.currentDDU;
        DistanceToCenterText.text = "DistanceToCenter : " + (1000f - jumpManager.currentDDU);
    }
}
