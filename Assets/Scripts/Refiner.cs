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
        if (other.CompareTag("RegenResourceBlock") || other.CompareTag("ResourceBlock")) 
        {
            RegenerableBio bio = other.GetComponent<RegenerableBio>();
            if (bio != null)
            {
                bio.refined = true; // Set the refined state to true
                bio.gameObject.GetComponent<Renderer>().material.color = new Color(0.1f, 0.25f, 1f, 0.1f);
            }
        }
        if (other.CompareTag("Enemy")) 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.refined = true; // Set the refined state to true
                enemy.gameObject.GetComponent<Renderer>().material.color = new Color(0.1f, 0.25f, 1f, 0.1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RegenResourceBlock") || other.CompareTag("ResourceBlock")) 
        {
            RegenerableBio bio = other.GetComponent<RegenerableBio>();
            if (bio != null)
            {
                bio.refined = false; // Reset the refined state when exiting the trigger
                bio.gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        if (other.CompareTag("Enemy")) 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.refined = false; // Reset the refined state when exiting the trigger
                enemy.gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
