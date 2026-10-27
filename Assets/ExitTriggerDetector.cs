using UnityEngine;

public class ExitTriggerDetector : MonoBehaviour
{
    public FinalDoorController finalDoor;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && finalDoor != null)
            finalDoor.SetPlayerNearby(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && finalDoor != null)
            finalDoor.SetPlayerNearby(false);
    }
}