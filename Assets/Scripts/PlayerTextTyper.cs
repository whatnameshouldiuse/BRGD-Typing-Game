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

    List<string> keyList = new List<string>(); //A list of accepted key inputs (should only be letters)

    // Start is called before the first frame update
    void Start()
    {
        textBox = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();    //Get text box
        inputBox = this.gameObject.GetComponentInChildren<TMP_InputField>();    //Get input field
        textBox.text = "";                                                      //Empty output text box
        //keyList = ["A", "B", "C"];
        inputBox.ActivateInputField();                                          //Activate text input without clicking
    }

    // Update is called once per frame
    void Update()
    {
        //Re-activate the input field if the player clicked off of it for some reason
        inputBox.ActivateInputField();

        //If a character was entered into the input field, copy it over to the output text via strings
        //Delete character from input field once done
        if (inputBox.text != null)
        {
            textBox.text = textBox.text.ToString() + inputBox.text.ToString();
            //string tempString = inputBox.text.ToString();
            //string outputString = textBox.text.ToString() + tempString;
            //textBox.text = outputString;
            inputBox.text = null;
        }
    }
}
