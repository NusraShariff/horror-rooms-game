using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightPickup : MonoBehaviour
{
    public Camera playerCamera;
    public float pickupDistance = 8f;
    public Light playerFlashlight;

    private bool collected;

    void Update()
    {
        if (collected || Keyboard.current == null ||
            !Keyboard.current.eKey.wasPressedThisFrame)
            return;

        if (playerCamera == null)
            return;

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        RaycastHit[] hits = Physics.RaycastAll(
            ray,
            pickupDistance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Collide
        );

        foreach (RaycastHit hit in hits)
        {
            Transform hitTransform = hit.transform;

            if (hitTransform == transform ||
                hitTransform.IsChildOf(transform) ||
                transform.IsChildOf(hitTransform))
            {
                collected = true;
                gameObject.SetActive(false);

                if (playerFlashlight != null)
                    playerFlashlight.enabled = true;

                Debug.Log("Flashlight collected!");
                return;
            }
        }

        Debug.Log("Flashlight not detected. Aim directly at it.");
    }
}