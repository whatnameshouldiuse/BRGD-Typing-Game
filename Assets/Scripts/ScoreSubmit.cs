using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Typerspace;

public class ScoreSubmit : MonoBehaviour
{
    public TMP_InputField NameInput;
    public GameObject SubmitButton;
    public GameObject SuccessText;
    public GameObject ScoreReporter;

    // Start is called before the first frame update
    void Start()
    {
        NameInput.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Submit();
    }

    public void Submit()
    {
        Leaderboard.Record(NameInput.text, (int)ScoreReporter.GetComponent<ScoreReporter>().score);

        SuccessText.GetComponent<TextMeshProUGUI>().enabled = !SuccessText.GetComponent<TextMeshProUGUI>().enabled;
        NameInput.GetComponent<Image>().enabled = !NameInput.GetComponent<Image>().enabled;
        SubmitButton.GetComponent<Image>().enabled = !SubmitButton.GetComponent<Image>().enabled;
    }
}
