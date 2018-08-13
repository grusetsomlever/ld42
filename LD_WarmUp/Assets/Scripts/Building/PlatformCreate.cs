using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Hello");
        Debug.Log(this.transform.eulerAngles);

        float fixY = this.transform.eulerAngles.y;
        fixY = fixY / 90;
        fixY = Mathf.Round(fixY) * 90;

        this.transform.eulerAngles = new Vector3(0, fixY, 0);

        Debug.Log(this.transform.eulerAngles);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
