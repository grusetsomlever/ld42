using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public AudioSource backgroundWind;
    public AudioSource Chords1;
    public AudioSource Chords2;
    public AudioSource Strings;
    public AudioSource bass;
    public AudioSource bassDrum;
    public AudioSource fullMusic;

    public int blocksCreated;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (blocksCreated == 7) {
            if (bass.loop == true) {
                bass.loop = false;
            }
            if (!bassDrum.isPlaying && !bass.isPlaying) {
                bassDrum.Play();
            }
        }
        if (blocksCreated == 8) {
            if (bassDrum.loop == true) {
                bassDrum.loop = false;
            }
            if (!bassDrum.isPlaying && !fullMusic.isPlaying) {
                fullMusic.Play();
            }
        }
    }

    public void addBlock() {
        blocksCreated += 1;
        if (blocksCreated == 2) {
            Chords1.Play();
        }
        else if (blocksCreated == 4) {
            Chords2.Play();
        }
        else if (blocksCreated == 5) {
            Strings.Play();
        }
        else if (blocksCreated == 6) {
            bass.Play();
        }
    }
}
