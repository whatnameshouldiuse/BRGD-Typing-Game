using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimator : MonoBehaviour
{
    public Sprite[] frames;                 //Array for holding all frames
    public SpriteRenderer spriteRenderer;   //Wizard sprite renderer
    public float fps;                       //Frames per second
    public bool isBopping;                  //Boolean for if the wizard is bopping

    //Song BPM is 128
    //Resulting fps should be 2.13333333 repeating?

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        fps = 2 * 2.13333333333333333333f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBopping)
        {
            int index = (int)(Time.time * fps) % frames.Length;
            spriteRenderer.sprite = frames[index];
        }
    }
}
