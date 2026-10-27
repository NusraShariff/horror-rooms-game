using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPickup : MonoBehaviour
{
    public GameObject ghost;
    public GameObject exitDoor;
    public GhostChase ghostChase;
    public Camera playerCamera;
    public float pickupDistance = 3f;

    private bool collected;

    void Start()
    {
        KeyManager.hasKey = false;
        
        if (ghost != null)
            ghost.SetActive(false);
    }

    void Update()
    {
        if (collected || Keyboard.current == null ||
            !Keyboard.current.eKey.wasPressedThisFrame)
            return;

        if (playerCamera == null)
        {
            Debug.LogWarning("Assign the Player Camera in KeyPickup.");
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            if (hit.transform == transform ||
                hit.transform.IsChildOf(transform))
            {
                collected = true;
                KeyManager.hasKey = true;
                gameObject.SetActive(false);

                if (ghost != null)
                    ghost.SetActive(true);

                if (ghostChase != null)
                    ghostChase.StartChasing();

                if (exitDoor != null)
                    exitDoor.SetActive(false);

                Debug.Log("Key collected! The ghost has appeared!");
            }
        }
    }
}