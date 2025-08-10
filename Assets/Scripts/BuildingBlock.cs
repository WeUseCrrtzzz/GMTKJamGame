using UnityEngine;
using TMPro;

public class BuildingBlock : MonoBehaviour
{

    public GameObject[] blockPrefabs;
    public float[] buildTimes;
    public int ID;
    public TextMeshProUGUI timerText;

    private float buildTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buildTimer += Time.deltaTime;
        if (buildTimer >= buildTimes[ID]) 
        {
            Instantiate(blockPrefabs[ID], transform.position, transform.rotation);
            Destroy(gameObject);
        }

        timerText.text = Mathf.RoundToInt(buildTimes[ID] - buildTimer).ToString();
    }
}
