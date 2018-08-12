using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    protected Vector3 startPosition;
    public GameObject levelGenerator;
    public float distance;
    public bool setZeroRotate = false;

	// Use this for initialization
	void Start () {
        startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // generate more level once the bullet has traveled its max distance
        var currentPosition = this.transform.position;

        if (distance <= Vector3.Distance(startPosition, currentPosition)) {
            if (setZeroRotate == true) {
                GameObject newLand = Instantiate(levelGenerator, this.transform.position, Quaternion.identity) as GameObject;
            }
            else {
                GameObject newLand = Instantiate(levelGenerator, this.transform.position, Quaternion.identity) as GameObject;
                newLand.transform.eulerAngles = this.transform.eulerAngles;
            }
            
            Destroy(gameObject);
        }
	}
}
