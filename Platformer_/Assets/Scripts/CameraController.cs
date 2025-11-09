using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float sensitivity = 200f;
    public float yClamp = 80f;

    private float rotX;
    private float rotY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -yClamp, yClamp);

        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
        Player.rotation = Quaternion.Euler(0f, rotY, 0f);
        
    }
}
