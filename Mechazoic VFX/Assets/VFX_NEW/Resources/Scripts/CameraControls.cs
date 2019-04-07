using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private float vertical = 0;
    private float horizontal = 0;

    public float speed = 2;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
    }

    private void Update()
    {
        //  horizontal rotation
        horizontal = speed * Input.GetAxis("Mouse X");
        transform.root.Rotate(0, horizontal, 0);


        //  old vertical rotation with no limits
        //transform.Rotate(vertical, 0, 0);

        //  vertical rotation with limits
        vertical -= Input.GetAxis("Mouse Y") * speed;
        vertical = Mathf.Clamp(vertical, -90, 90);
        transform.localEulerAngles = new Vector3(vertical, transform.localEulerAngles.y, 0);
    }
}
