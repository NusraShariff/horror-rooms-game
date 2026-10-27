using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class FinalDoorController : MonoBehaviour
{
    public TMP_InputField codeInputField;
    public TMP_Text resultText;
    public GameObject keypadPanel;
    public float openAngle = 90f;
    public float openSpeed = 3f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen;
    private bool playerNearby;
    private Coroutine messageRoutine;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        if (keypadPanel != null)
            keypadPanel.SetActive(false);

        if (resultText != null)
            resultText.text = "";
    }

void Update()
{
    if (Keyboard.current == null)
        return;

    if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
    {
        if (keypadPanel != null)
            keypadPanel.SetActive(true);

        if (codeInputField != null)
        {
            codeInputField.text = "";
            codeInputField.ActivateInputField();
            codeInputField.Select();
        }
    }

    if (keypadPanel != null && keypadPanel.activeSelf)
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame ||
            Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        {
            SubmitCode();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            keypadPanel.SetActive(false);
    }
}

    public void SubmitCode()
    {
        if (codeInputField == null || resultText == null)
            return;

        if (codeInputField.text == CodeManager.correctCode)
        {
            resultText.color = Color.green;
            resultText.text = "CORRECT CODE! EXIT UNLOCKED!";
            keypadPanel.SetActive(false);
            isOpen = true;
        }
        else
        {
            resultText.color = Color.red;
            resultText.text = "INVALID CODE! TRY AGAIN.";

            if (messageRoutine != null)
                StopCoroutine(messageRoutine);

            messageRoutine = StartCoroutine(ClearMessage());
            codeInputField.text = "";
            codeInputField.ActivateInputField();
        }
    }

    IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(2f);

        if (resultText != null)
            resultText.text = "";

        messageRoutine = null;
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

public void SetPlayerNearby(bool nearby)
{
    playerNearby = nearby;

    if (!nearby && keypadPanel != null)
        keypadPanel.SetActive(false);
}

}