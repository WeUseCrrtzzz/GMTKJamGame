using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSystem : MonoBehaviour
{

    public int BuildingID = 0;
    public float GridX = 2f;
    public float GridZ = 2f;
    public GameObject[] buildPrefabs;
    public LayerMask layerMask;
    public LayerMask groundLayerMask;
    public GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 100f; // Set a distance from the camera
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Snap to grid
        float snappedX = Mathf.Round(worldPosition.x / GridX) * GridX;
        float snappedZ = Mathf.Round(worldPosition.z / GridZ) * GridZ;

        target.transform.position = new Vector3(
            snappedX,
            target.transform.position.y,
            snappedZ
        );

        // Define the box size (adjust as needed)
        Vector3 boxSize = target.transform.localScale / 2f;

        // Check if target is NOT colliding with any objects in layerMask
        bool isNotColliding = !Physics.CheckBox(
            target.transform.position,
            boxSize,
            target.transform.rotation,
            layerMask
        );

        bool collidingWithGround = Physics.CheckBox(
            target.transform.position,
            boxSize,
            target.transform.rotation,
            groundLayerMask
        );

        if (isNotColliding && collidingWithGround)
        {
            target.GetComponent<Renderer>().material.color = Color.green; // Change color to green if valid position
        }
        else
        {
            target.GetComponent<Renderer>().material.color = Color.red; // Change color to red if invalid position
        }

        if (isNotColliding && collidingWithGround && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetKeyDown("mouse 0")) 
            {
                

                // Create the prefab at the snapped position
                if (BuildingID != 0) 
                {
                    Instantiate(buildPrefabs[BuildingID - 1], new Vector3(snappedX, worldPosition.y, snappedZ), Quaternion.identity);
                }
            }
        }

        if (Input.GetKeyDown("mouse 1")) 
        {
            BuildingID = 0;
        }
    }

    public void SetBuildingID(int id)
    {
        BuildingID = id;
    }
}
