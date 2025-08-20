using UnityEngine;
using TMPro;

public class BuildingBlock : MonoBehaviour
{

    public GameObject[] blockPrefabs;
    public float[] buildTimes;
    public int ID;
    public TextMeshProUGUI timerText;

    private float buildTimer;
    private float effectiveBuildTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // make sure upgrades never goes below 1 second
        float baseTime = (ID < buildTimes.Length) ? buildTimes[ID] : 0f;
        effectiveBuildTime = Mathf.Max(1f, baseTime - ShipUpgradeManager.BuildTimeReduction);
    }

    // Update is called once per frame
    void Update()
    {
        buildTimer += Time.deltaTime;
        if (buildTimer >= effectiveBuildTime /* buildTimes[ID]*/) 
        {
            Instantiate(blockPrefabs[ID], transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (timerText != null)
        {
            float remaining = Mathf.Max(0f, effectiveBuildTime - buildTimer);
            timerText.text = Mathf.RoundToInt(remaining).ToString();
        }
        //timerText.text = Mathf.RoundToInt(buildTimes[ID] - buildTimer).ToString();
    }
}
