using UnityEngine;

public class SunRadiusSetter : MonoBehaviour
{

    public GameObject sun;
    public GameObject[] sunRadiuses;

    public int sunAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Health sunHealth = sun.GetComponent<Health>();
        sunAmount = Mathf.RoundToInt(sunHealth.health * 0.01f);

        sunRadiuses[0].transform.localScale = new Vector3(sunRadiuses[0].transform.localScale.x, sunRadiuses[0].transform.localScale.y, (10 - sunAmount) * 0.5f);
        sunRadiuses[0].transform.position = new Vector3(sunRadiuses[0].transform.position.x, sunRadiuses[0].transform.position.y, ((10 * 2) + sunAmount) + (sun.transform.position.z - 30f));
        sunRadiuses[1].transform.localScale = new Vector3(sunRadiuses[1].transform.localScale.x, sunRadiuses[1].transform.localScale.y, (11 - sunAmount) * 0.5f);
        sunRadiuses[1].transform.localPosition = new Vector3(sunRadiuses[1].transform.localPosition.x, sunRadiuses[1].transform.localPosition.y, (-sunRadiuses[0].transform.localScale.z * 2f) + ((10 - sunAmount) * 0.25f) - 0.25f);
        sunRadiuses[2].transform.localScale = new Vector3(sunRadiuses[2].transform.localScale.x, sunRadiuses[2].transform.localScale.y, (13 - sunAmount) * 0.5f);
        sunRadiuses[2].transform.localPosition = new Vector3(sunRadiuses[2].transform.localPosition.x, sunRadiuses[2].transform.localPosition.y, (-sunRadiuses[1].transform.localScale.z * 2f) + ((10 - sunAmount) * -0.25f) - 0.25f);
    }
}
