using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiffReadout : MonoBehaviour
{
    public int diff;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        diff = GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff;
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        diff = GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff;

        if (diff == 0)
        {
            text.text = "Difficulty:" + "\n" + "Easy";
        }

        if (diff == 1)
        {
            text.text = "Difficulty:" + "\n" + "Medium";
        }

        if (diff == 2)
        {
            text.text = "Difficulty:" + "\n" + "Hard";
        }
    }
}
