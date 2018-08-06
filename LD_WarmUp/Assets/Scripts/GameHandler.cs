using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public bool setting_isPaused;

	// Use this for initialization
	void Start () {
        SetIsPaused(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIsPaused(bool set)
    {
        this.setting_isPaused = set;
        if (set)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = set;
    }
}
