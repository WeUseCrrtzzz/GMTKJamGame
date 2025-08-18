using UnityEngine;

public class CameraMovement : MonoBehaviour
{

public GameObject sun;

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
        if (transform.position.z < sun.transform.position.z -38f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, sun.transform.position.z -38f);
        }
        if (transform.position.z > sun.transform.position.z - 8f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, sun.transform.position.z - 8f);
        }

        Camera cam = GetComponent<Camera>();
        if (cam != null && cam.orthographic == false)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                cam.fieldOfView -= scroll * 10f;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 5f, 20f);
            }
        }
    }
}
