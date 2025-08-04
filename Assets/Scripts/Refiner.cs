using UnityEngine;

public class Refiner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RegenResourceBlock")) 
        {
            RegenerableBio bio = other.GetComponent<RegenerableBio>();
            if (bio != null)
            {
                bio.refined = true; // Set the refined state to true
            }
        }
        if (other.CompareTag("ResourceBlock")) 
        {
            RegenerableBio bio = other.GetComponent<RegenerableBio>();
            if (bio != null)
            {
                bio.refined = true; // Set the refined state to true
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RegenResourceBlock")) 
        {
            RegenerableBio bio = other.GetComponent<RegenerableBio>();
            if (bio != null)
            {
                bio.refined = false; // Reset the refined state when exiting the trigger
            }
        }

        if (other.CompareTag("ResourceBlock")) 
        {
            RegenerableBio bio = other.GetComponent<RegenerableBio>();
            if (bio != null)
            {
                bio.refined = false; // Reset the refined state when exiting the trigger
            }
        }
    }
}
