using UnityEngine;

public class SubtleParallax : MonoBehaviour
{
    // Cu cât valoarea e mai mare (între 0 și 1), cu atât fundalul se va mișca mai mult
    // 0 = stă fix cu camera (infinit)
    // 0.2 = se mișcă ușor în sens invers, dând senzația de distanță
    [Range(0f, 1f)]
    public float parallaxEffectMultiplier = 0.2f;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        // Luăm transform-ul camerei părinte
        cameraTransform = transform.parent;

        if (cameraTransform != null)
        {
            lastCameraPosition = cameraTransform.position;
        }
    }

    // Folosim Interpolarea în LateUpdate pentru a elimina complet trepidația
    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Vedem cât s-a mișcat camera în acest cadru
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // În loc să lăsăm fundalul să meargă 100% cu camera, 
        // îl împingem puțin înapoi în funcție de mișcarea camerei
        transform.localPosition -= new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0);

        lastCameraPosition = cameraTransform.position;
    }
}