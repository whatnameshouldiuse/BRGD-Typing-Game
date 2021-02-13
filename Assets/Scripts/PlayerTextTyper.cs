using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTextTyper : MonoBehaviour
{
    public string textTyped;    //The text typed by the player
    public TextMeshProUGUI textBox; //The text display (provisional, may be implemented permanently)

    // Start is called before the first frame update
    void Start()
    {
        textBox = this.gameObject.GetComponent<TextMeshProUGUI>();  //Get text box
        textBox.text = "Sample Text";                           //Placeholder text
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
