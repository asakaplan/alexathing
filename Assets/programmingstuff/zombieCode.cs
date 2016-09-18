using UnityEngine;
using System.Collections;

public class zombieCode : MonoBehaviour {
	public float speed = 3;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dist = GameObject.Find("OVRPlayerController").transform.position-transform.position;
		Vector3 mov = dist/1000;
		//transform.Translate (mov);
	}
}
