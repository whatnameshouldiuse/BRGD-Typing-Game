using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimationScript : MonoBehaviour
{

    public float animScale;     //how strongly the sprite will animate
    public float animSpeed;     //how quickly the sprite will animate

    public float x;             //transform variables for xy coordinates, z axis rotation, and scale
    public float y;
    public float rot;
    public Vector2 size;

    public float newX;                 //modified transform variables for each frame
    public float newY;
    public float newRot;


    // Start is called before the first frame update
    void Start()
    {
        //Set initial parameters (determines where the sprite animates around)
        x = transform.position.x;
        y = transform.position.y;
        rot = transform.rotation.eulerAngles.z;
        size = transform.localScale;

        animScale = 0.25f;
        animSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(x, y + (Mathf.Sin(Time.time * animSpeed) * animScale));
        transform.rotation = Quaternion.Euler(0, 0, rot + (Mathf.Sin(Time.time * 2 * animSpeed) * 5 * animScale));
        transform.localScale = size * (1 + (Mathf.Sin(Time.time * animSpeed) * 0.25f * animScale));
    }
}
