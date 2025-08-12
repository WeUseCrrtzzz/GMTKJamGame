using UnityEngine;
using UnityEngine.UI;

public class DDUSlider : MonoBehaviour
{

    public SolJumpManager jumpManager;
    public int currentDDU;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDDU = jumpManager.currentDDU;

        Slider slider = gameObject.GetComponent<Slider>();
        slider.value = currentDDU * 0.001f;
        Debug.Log(currentDDU);
    }
}
