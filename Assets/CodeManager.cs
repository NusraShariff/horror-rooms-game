using UnityEngine;
using TMPro;

public class CodeManager : MonoBehaviour
{
    public TMP_Text secretCodeText;
    public TMP_InputField codeInputField;
    public TMP_Text resultText;
    public GameObject keypadPanel;
    public GameObject finalDoor;

    public static string correctCode;

    void Awake()
    {
        correctCode = Random.Range(1000, 10000).ToString();

        if (secretCodeText != null)
            secretCodeText.text = correctCode;
    }

    public void CheckCode()
    {
        if (codeInputField == null)
            return;

        if (codeInputField.text == correctCode)
        {
            if (resultText != null)
                resultText.text = "CORRECT CODE! DOOR UNLOCKED!";

            if (finalDoor != null)
                finalDoor.GetComponent<DoorController>().enabled = true;

            if (keypadPanel != null)
                keypadPanel.SetActive(false);
        }
        else
        {
            if (resultText != null)
                resultText.text = "INVALID CODE! TRY AGAIN.";

            codeInputField.text = "";
        }
    }
}