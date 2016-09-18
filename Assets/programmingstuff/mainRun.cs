using UnityEngine;
using System.Collections;
using WebSocketSharp;

public class mainRun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Debug.Log("test");
	}
	
	// Update is called once per frame

	void Update () {
		transform.Rotate (100 * Time.deltaTime, 0, 0);
		Debug.Log ("test2");
	
	}
}
