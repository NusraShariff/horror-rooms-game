using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 3f;
    public float interactDistance = 3f;
    public bool requiresKey = false;
    public GameObject doorMessage;
    public TMP_Text messageText;

    private bool isOpen;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Transform player;
    private Coroutine messageRoutine;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;

        if (doorMessage != null)
            doorMessage.SetActive(false);
    }

    void Update()
    {
        if (player == null || Keyboard.current == null)
            return;

        if (!Keyboard.current.eKey.wasPressedThisFrame)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > interactDistance)
            return;

        if (requiresKey && !KeyManager.hasKey)
        {
            ShowMessage();
            return;
        }

        isOpen = !isOpen;
    }

    void LateUpdate()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            openSpeed * Time.deltaTime
        );
    }

    void ShowMessage()
    {
        if (doorMessage == null)
            return;

        if (messageText != null)
            messageText.text = "KEY REQUIRED!";

        doorMessage.SetActive(true);

        if (messageRoutine != null)
            StopCoroutine(messageRoutine);

        messageRoutine = StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);

        if (doorMessage != null)
            doorMessage.SetActive(false);

        messageRoutine = null;
    }
}
