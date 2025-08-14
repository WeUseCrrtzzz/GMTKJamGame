using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuildSystem : MonoBehaviour
{

    public int BuildingID = 0;
    public float GridX = 2f;
    public float GridZ = 2f;
    public GameObject[] buildPrefabs;
    public GameObject buildPrefab;
    public GameObject shipPrefab;
    public float[] buildCosts;
    public TextMeshProUGUI[] priceTags;
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

        GameObject[] playerBlocks = GameObject.FindGameObjectsWithTag("PlayerBlock");

        if (isNotColliding && collidingWithGround)
        {
            target.GetComponent<Renderer>().material.color = Color.green; // Change color to green if valid position
        }
        else
        {
            target.GetComponent<Renderer>().material.color = Color.red; // Change color to red if invalid position
        }

        if (playerBlocks.Length <= 0) 
        {
            target.GetComponent<Renderer>().material.color = Color.green;
            BuildingID = 9;
        }

        if (BuildingID == 9 && isNotColliding && !EventSystem.current.IsPointerOverGameObject()) 
        {
            Debug.Log("Can Place Ship");
            if (Input.GetKeyDown("mouse 0")) 
            {
                BuildingBlock block = Instantiate(shipPrefab, new Vector3(snappedX, worldPosition.y, snappedZ), Quaternion.identity).GetComponent<BuildingBlock>();
                BuildingID = 0; // Reset BuildingID after placing the ship
            }
        }

        if (isNotColliding && collidingWithGround && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetKeyDown("mouse 0")) 
            {
                // Create the prefab at the snapped position
                if (BuildingID != 0) 
                {
                    float baseCost = buildCosts[BuildingID - 1];
                    float price = GetDiscountedCost(baseCost);

                    if (Resources.coins >= price) 
                    {
                        BuildingBlock block = Instantiate(buildPrefab, new Vector3(snappedX, worldPosition.y, snappedZ), Quaternion.identity).GetComponent<BuildingBlock>();
                        block.ID = BuildingID - 1;
                        Resources.coins -= price;
                    } else
                    {
                        Debug.Log("Not enough coins to build this structure.");
                        // can display a message to the player or play a sfx
                    }

                    //if (Resources.coins >= buildCosts[BuildingID - 1]) 
                    //{
                    //    Instantiate(buildPrefabs[BuildingID - 1], new Vector3(snappedX, worldPosition.y, snappedZ), Quaternion.identity);
                    //    Resources.coins -= buildCosts[BuildingID - 1];
                    //}
                }
            }
        }

        if (Input.GetKeyDown("mouse 1")) 
        {
            BuildingID = 0;
        }

        foreach (TextMeshProUGUI priceTag in priceTags)
        {
            if (priceTag != null)
            {
                int index = System.Array.IndexOf(priceTags, priceTag);
                if (index >= 0 && index < buildCosts.Length)
                {
                    float discountedCost = GetDiscountedCost(buildCosts[index]);
                    priceTag.text = discountedCost.ToString();
                }
            }
        }
    }

    public void SetBuildingID(int id)
    {
        BuildingID = id;
    }

    // returns the price after applying ship upgrade discounts
    float GetDiscountedCost(float baseCost)
    {
        float discount = 0f;

        try
        {
            discount = ShipUpgradeManager.BuildCostDiscount;
        }
        catch { discount = 0f; }

        return Mathf.Max(0f, baseCost - discount); // never below 0
    }
}
