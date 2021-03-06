using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AlgorithmTester : MonoBehaviour
{
    GameObject inputBox;
    GameObject textBox;
    GameObject scoreBox;
    GameObject scoreBox2;

    TMP_InputField input;
    TextMeshProUGUI sample;
    TextMeshProUGUI score;
    TextMeshProUGUI score2;

    int secondScore;

    // Start is called before the first frame update
    void Start()
    {
        inputBox = GameObject.Find("Input");
        input = inputBox.GetComponent<TMP_InputField>();
        textBox = GameObject.Find("SampleText");
        sample = textBox.GetComponent<TextMeshProUGUI>();
        scoreBox = GameObject.Find("Score");
        score = scoreBox.GetComponent<TextMeshProUGUI>();
        scoreBox2 = GameObject.Find("Score 2");
        score2 = scoreBox2.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string test = input.text.ToString();
            Compare(test);
            secondScore = Compare2(test);
            score2.text = secondScore.ToString();
        }
    }

    void Compare(string input)
    {
        float hits = 0;         //How many correct characters
        float miss = 0;         //How mnay wrong characters
        float testScore = 0;        //The overall score for the popup
        string newText = input;    //A separate instance of the input text for modifying

        //Get the popup's text string
        string popString = sample.text;

        //Add characters to make both strings the same length to prevent out-of-index
        int charDiff = popString.Length - newText.Length;
        if (charDiff > 0)
        {
            for (int i = 0; i < charDiff; i++)
            {
                newText = newText + "~";
            }
        }
        if (charDiff < 0)
        {
            for (int i = 0; i > charDiff; i--)
            {
                popString = popString + "~";
            }
        }

        //print(newText);
        //print(popString);

        //Break up the string and compare character-for-character
        for (int i = 0; i < popString.Length; i++)
        {
            if (popString[i] == newText[i])
            {
                hits++;
                //print("hit");
            }
            else
            {
                miss++;
                //print("miss");
            }
        }

        //Calculate score
        testScore = hits / (hits + miss);
        //print(score);

        score.text = testScore.ToString();
    }

    int Compare2(string input)
    {
        string string1 = input;
        string string2 = sample.text.ToString();

        if (String.IsNullOrEmpty(string1))
        {
            if (!String.IsNullOrEmpty(string2))
                return string2.Length;

            return 0;
        }

        if (String.IsNullOrEmpty(string2))
        {
            if (!String.IsNullOrEmpty(string1))
                return string1.Length;

            return 0;
        }

        int length1 = string1.Length;
        int length2 = string2.Length;

        int[,] d = new int[length1 + 1, length2 + 1];

        int cost, del, ins, sub;

        for (int i = 0; i <= d.GetUpperBound(0); i++)
            d[i, 0] = i;

        for (int i = 0; i <= d.GetUpperBound(1); i++)
            d[0, i] = i;

        for (int i = 1; i <= d.GetUpperBound(0); i++)
        {
            for (int j = 1; j <= d.GetUpperBound(1); j++)
            {
                if (string1[i - 1] == string2[j - 1])
                    cost = 0;
                else
                    cost = 1;

                del = d[i - 1, j] + 1;
                ins = d[i, j - 1] + 1;
                sub = d[i - 1, j - 1] + cost;

                d[i, j] = Math.Min(del, Math.Min(ins, sub));

                if (i > 1 && j > 1 && string1[i - 1] == string2[j - 2] && string1[i - 2] == string2[j - 1])
                    d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
            }
        }

        return d[d.GetUpperBound(0), d.GetUpperBound(1)];
    }
}
