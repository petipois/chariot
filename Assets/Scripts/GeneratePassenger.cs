using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePassenger : MonoBehaviour {
	public GameObject[] passenger;
	public float xMin, xMax, zMin, zMax;
	int choice;
	//[SerializeField]
	//float seconds, again = 1f;
	// Use this for initialization
	void Start () {
		//InvokeRepeating ("CreatePassenger", seconds, again);
		//CreatePassenger();
	}
	
	// Update is called once per frame
	public void CreatePassenger () {
		choice = Random.Range (0, passenger.Length);
		Vector3 randomPos = new Vector3 (Random.Range(xMin,xMax), 1, Random.Range(zMin,zMax));
		Instantiate (passenger[choice],randomPos, passenger[choice].transform.rotation);
	}
}
