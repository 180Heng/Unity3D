using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public Transform Sun;
	public Transform Mercury;
	public Transform Venus;
	public Transform Earth;
	public Transform Moon;
	public Transform Mars;
	public Transform Jupiter;
	public Transform Saturn;
	public Transform Uranus;
	public Transform Neptune;


	// Use this for initialization
	void Start () {
		Sun.position = Vector3.zero;
		Mercury.position = new Vector3(3, 0, 0);
		Venus.position = new Vector3(6, 0, 0);
		Earth.position = new Vector3(9, 0, 0);
		Moon.position = new Vector3(11, 0, 0);
		Mars.position = new Vector3(13, 0, 0);
		Jupiter.position = new Vector3(17, 0, 0);
		Saturn.position = new Vector3(20, 0, 0);
		Uranus.position = new Vector3(23, 0, 0);
		Neptune.position = new Vector3(25, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		Mercury.RotateAround(Sun.position, new Vector3(0, 0, 1), 10 * Time.deltaTime);
		Venus.RotateAround(Sun.position, new Vector3(0, 1, 0), 17 * Time.deltaTime);
		Earth.RotateAround(Sun.position, new Vector3(0, 0, 1), 30 * Time.deltaTime);
		Moon.RotateAround(Earth.position, new Vector3(0, 1, 0), 359 * Time.deltaTime);
		Mars.RotateAround(Sun.position, new Vector3(0, 1, 2), 15 * Time.deltaTime);
		Jupiter.RotateAround(Sun.position, new Vector3(0, 1, 3), 20 * Time.deltaTime);
		Saturn.RotateAround(Sun.position, new Vector3(0, 2, 3), 40 * Time.deltaTime);
		Uranus.RotateAround(Sun.position, new Vector3(0, 3, 1), 25 * Time.deltaTime);
		Neptune.RotateAround(Sun.position, new Vector3(0, 3, 2), 11 * Time.deltaTime);
	}

}
