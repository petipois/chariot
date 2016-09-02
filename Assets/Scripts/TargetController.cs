using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour {
	// destination
	//public Transform[] patrolPoints;
	//public float moveSpeed = 5f;
	//int currentPoint;
	public Image destinationImage;
	Image test;
	public AudioClip pickedUp;
	[SerializeField]
	public int money,randomMin, randomMax;
	public int addedTime;
	public string[] targetDestination;
	Text destinationText;
	int destinationNum;
	GameManager gm;
	ChariotController cc;
	string message;
	// Use this for initialization
	void Start()
	{
		addedTime = Random.Range (randomMin, randomMax);
		test = GameObject.FindGameObjectWithTag ("Test").GetComponent<Image> ();
		gm = GameObject.FindObjectOfType<GameManager> ();
		cc = GameObject.FindObjectOfType<ChariotController> ();
		destinationText = GameObject.FindGameObjectWithTag ("Destination").GetComponent<Text> ();
		message = "";
		RandomDestination ();
	}
	void RandomDestination(){
		destinationNum = Random.Range (0,targetDestination.Length);
	}
	void Update()
	{
		destinationText.text = message;

	}
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Chariot") {
			
			transform.parent = cc.pickUpPoint.transform;
			transform.position = cc.pickUpPoint.transform.position;
			cc.hasPassenger = true;
			message = targetDestination [destinationNum];
			test.sprite = destinationImage.sprite;
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Chariot")
		AudioSource.PlayClipAtPoint (pickedUp, this.transform.position);
	}
}
