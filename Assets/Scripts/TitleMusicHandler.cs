using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusicHandler : MonoBehaviour
{
    public AudioSource source;  //The audiosource
    public bool playing;        //Bool for if the song is meant to be playing

    // Start is called before the first frame update
    void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();   //Assign audiosource
        playing = true;
        source.volume = 0;

        //Generate an array with all objects tagged as "TitleMusic"
        GameObject[] dupes = GameObject.FindGameObjectsWithTag("TitleMusic");

        //If the array length > 1, another title music player already exists and this one should be deleted
        if (dupes.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject); //Make persistent to avoid scene load clipping of music
    }

    // Update is called once per frame
    void Update()
    {
        //Raise volume over several seconds (should take 1/float seconds)
        if (playing && source.volume < 1)
        {
            source.volume += 0.5f * Time.deltaTime;
        }

        //Cap volume in case of overflow
        if (playing && source.volume > 1)
        {
            source.volume = 1;
        }
    }

    //If the game calls to stop the title music (i.e. when the main game starts properly), mute and destroy
    public void StopMusic()
    {
        source.volume = 0;
        Destroy(this.gameObject);
    }
}
