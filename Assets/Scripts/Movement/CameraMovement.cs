using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shiftMultiplier;
    [SerializeField] private float sensibility;
    private float multiplier = 1f;

    void Update()
    {
        movement();
        rotation();
    }

    void movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            multiplier = shiftMultiplier;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            multiplier = 1f;

        if (Input.GetKey(KeyCode.W))
            transform.Translate(this.transform.forward * speed * multiplier * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(this.transform.forward * -speed * multiplier * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(this.transform.right * speed * multiplier * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(this.transform.right * -speed * multiplier * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.Q))
            transform.Translate(this.transform.up * -speed * multiplier * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.E))
            transform.Translate(this.transform.up * speed * multiplier * Time.deltaTime, Space.World);
    }


    float xRotation = 1, yRotation = 1;
    void rotation()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");


        yRotation += x * sensibility * Time.deltaTime;

        xRotation -= y * sensibility * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

    }
}
