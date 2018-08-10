using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_music : MonoBehaviour {

    public AudioSource startMusic;
    public AudioSource loopMusic;
    private bool loopMusicStarted = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!startMusic.isPlaying && loopMusicStarted == false) {
            loopMusic.Play();
            loopMusicStarted = true;
        }
    }
}
