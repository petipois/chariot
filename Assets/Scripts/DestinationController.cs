using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DestinationController : MonoBehaviour {
	GameManager gm;
	MeshRenderer destRend;
	Text destinationText;
	public Text addedTimeText;
	TargetController tc;
	public GameObject deliveryEffect;
	public AudioClip delivered;
	string message;

	void Start()
	{
		destRend = this.GetComponent<MeshRenderer> ();
		gm = GameObject.FindObjectOfType<GameManager> ();
		message = this.tag;
		destinationText = GameObject.FindGameObjectWithTag ("Destination").GetComponent<Text> ();
	}
	void Update()
	{
		if (destinationText.text == message) {
			destRend.enabled = true;
			this.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(0f,255f,0f,0.5f));
		} else {
			destRend.enabled = false;
		}
	}
	void OnTriggerEnter(Collider other)
	{ 
		if (other.gameObject.tag == "Passenger" && destinationText.text == message) {
			tc = other.gameObject.GetComponent<TargetController> ();
			Instantiate (deliveryEffect, this.transform.position, this.transform.rotation);
			AudioSource.PlayClipAtPoint (delivered, this.transform.position);
			gm.AddTime (tc.addedTime);
			StartCoroutine (showAddedTime ());
			Destroy (other.gameObject);
			gm.passengerDelivered ();
			gm.receiveFare (tc.money);
		} 
	}
	IEnumerator showAddedTime()
	{
		addedTimeText.text = "+" + tc.addedTime + "s";
		Debug.Log ("+"+tc.addedTime+"s");
		yield return new WaitForSeconds (2f);
		addedTimeText.text = "";
	}
}
