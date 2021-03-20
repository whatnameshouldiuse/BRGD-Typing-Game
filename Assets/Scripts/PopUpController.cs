using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public string popText;  //The text the player has to type to destroy this popup
    public bool topLayer;   //Boolean for if this popup is in the active layer the player interacts with
    public float score;     //The base score value for this popup's word
    public bool isWinner;   //Boolean for if the popup is being destroyed
    public Vector2 size;    //Scaling vector for popup
    public Vector2 sizeMod; //Change vector for popup's scaling
    public float rot;       //Rotation around z-axis

    // Start is called before the first frame update
    void Start()
    {
        //Assign popup text and set visualization
        //popText = "example";
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = popText;

        //Make this popup active (for now, until spawning/handling is finished)
        topLayer = true;

        //Set the popup's score (for now, until scoring is fleshed out)
        score = 100;

        //Set up scaling vectors
        size = transform.localScale;
        sizeMod = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //If the popup is being deleted
        if (isWinner && transform.localScale.x > 0)
        {
            sizeMod += new Vector2(Time.deltaTime, Time.deltaTime);
            transform.localScale = size - sizeMod;
        }
    }

    //When the player successfully destroys the popup, this method runs
    public void Winner(float scoreMod)
    {
        isWinner = true;    //Set deletion bool for animation

        //Calculate the points the player earns and add them to the score total
        GameObject.Find("Win/Loss Handler").GetComponent<GameOverController>().playerScore += (score * scoreMod);

        //Once score is sent off, reduce ad count in handler object by one
        GameObject.Find("Popup Handler").GetComponent<TextComparison>().adsLeft--;

        //Begin timer for animation before deletion
        StartCoroutine("DeleteTimer");
    }

    public IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
