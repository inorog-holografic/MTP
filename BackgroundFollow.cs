using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        lastCameraPosition = cameraTransform.position;
    }

    // Schimbăm în FixedUpdate sau folosim o sincronizare perfectă
    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Mutăm fundalul exact în poziția camerei pe X și Y, păstrând Z-ul lui original
        // Asta elimină acumularea de erori matematice mici (floating point errors)
        transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);

        lastCameraPosition = cameraTransform.position;
    }
}