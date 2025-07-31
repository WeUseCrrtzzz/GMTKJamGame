using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * 5f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 5f);
        }

        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 10f)
        {
            transform.position = new Vector3(10f, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
        if (transform.position.z > 10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
        }
    }
}
