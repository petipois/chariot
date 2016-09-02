using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	
	public void OpenShop(){
		//TODO
	}
	public void LoadAScene(string levelName)
	{
		SceneManager.LoadScene (levelName);
	}
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
