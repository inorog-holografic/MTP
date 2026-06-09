using UnityEngine;

public class SubtleParallax : MonoBehaviour
{

    [Range(0f, 1f)]
    public float parallaxEffectMultiplier = 0.2f;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = transform.parent;

        if (cameraTransform != null)
        {
            lastCameraPosition = cameraTransform.position;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.localPosition -= new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0);

        lastCameraPosition = cameraTransform.position;
    }
}
