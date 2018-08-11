using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    protected Vector3 startPosition;
    public GameObject levelGenerator;
    public float distance;

	// Use this for initialization
	void Start () {
        startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // generate more level once the bullet has traveled its max distance
        var currentPosition = this.transform.position;

        if (distance <= Vector3.Distance(startPosition, currentPosition)) {
            Debug.Log("HELLO HELLO");
            GameObject newLand = Instantiate(levelGenerator, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
	}
}
