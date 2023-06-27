using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerCam : NetworkBehaviour
{
    public float sensX;
    public float sensY;

    public Transform Orientation;

    public float rotationX;
    public float rotationY;

    
    void Start()
    {
        if (!IsOwner) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        float mouseX= Input.GetAxis("Mouse X") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * sensY;

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

        Orientation.rotation = Quaternion.Euler(rotationX, rotationY, 0);

    }
}
