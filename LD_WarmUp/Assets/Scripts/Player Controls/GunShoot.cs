using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour {

    public GameObject projectile;
    public GameObject secondProjectile;
    public float distance = 20;
    public float force = 300;
    public AudioSource shootSound;
    public Music musicStatus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            position = Camera.main.ScreenToWorldPoint(position);

            GameObject shot = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            shot.GetComponent<Bullet>().backgroundMusicStatus = musicStatus;
            shot.GetComponent<Bullet>().distance = distance;
            shot.transform.LookAt(position);

            shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * force);

            shootSound.Play();
        }
        if (Input.GetMouseButtonDown(1)) {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            position = Camera.main.ScreenToWorldPoint(position);

            GameObject shot = Instantiate(secondProjectile, transform.position, Quaternion.identity) as GameObject;
            shot.GetComponent<Bullet>().distance = distance;
            shot.transform.LookAt(position);

            shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * force);

            shootSound.Play();
        }
    }
}
