using UnityEngine;

public class NavStargate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ship(Clone)") 
        {
            Resources.maxCoins += 50;
            if (Resources.maxFuel < 100)
                Resources.maxFuel += 25;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Ship") 
        {
            Resources.maxCoins -= 50;
            if (Resources.maxFuel > 50)
                Resources.maxFuel -= 25;
        }
    }
}
