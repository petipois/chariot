using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
	public float timeToDestruct = 2f;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, timeToDestruct);
	}
}
