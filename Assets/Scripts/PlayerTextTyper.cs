using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTextTyper : MonoBehaviour
{
    public string textTyped;    //The text typed by the player
    public TextMeshProUGUI textBox; //The text display (provisional)
    public TMP_InputField inputBox; //The text input field for Unity (provisional)

    // Start is called before the first frame update
    void Start()
    {
        textBox = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();    //Get text box
        inputBox = this.gameObject.GetComponentInChildren<TMP_InputField>();    //Get input field
        textBox.text = "";                                                      //Empty output text box
        inputBox.ActivateInputField();                                          //Activate text input without clicking
    }

    // Update is called once per frame
    void Update()
    {
        //Re-activate the input field if the player clicked off of it for some reason
        inputBox.ActivateInputField();

        //If a character was entered into the input field, copy it over to the output text via strings
        //Delete character from input field afterward
        //As a result, "inputBox.text.ToString()" gives us the most recent player input on a per-character basis
        //We can then script around the input character using the "textTyped" string in any other script
        if (inputBox.text != null)
        {
            textTyped = inputBox.text.ToString();
            textBox.text = textBox.text.ToString() + textTyped;
            inputBox.text = null;
        }
    }
}