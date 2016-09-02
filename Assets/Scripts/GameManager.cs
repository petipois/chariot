using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	public Text objectiveText, scoreText, timeText, finalScoreText, finalCoinsText, bestScoreText, achievementText, coinsText;
	public GameObject startingPanel, GameStartCanvas, gameOverPanel, chariotPlayer, EntireGame;
	[SerializeField]
	float levelTime;

	int score = 0;
	int bestScore;
	int money = 0;
	int moneyGained;
	AudioSource asource;
	public AudioClip coinClip;
	int tempCoinCount;
	public float timeBetweenCoinIncrement = 0.05f;
	int passengers = 0;
	[SerializeField]
	int targetScore;
	bool paused, levelRestart;
	Vector3 startingPos;
	GeneratePassenger gp;
	// Use this for initialization
	void Awake () {
		asource = GameObject.FindObjectOfType<MusicPlayer> ().GetComponent<AudioSource>();
		bestScore = PlayerPrefs.GetInt("Best");
		moneyGained = PlayerPrefs.GetInt ("Money");

		tempCoinCount = money;

		InitializeGameValues ();
	}
	void InitializeGameValues()
	{
		levelTime = Random.Range (10, 60);
		targetScore = Random.Range (10, 50);
		startingPos = new Vector3 (0, 1, 0);
		objectiveText.text = "Deliver " + targetScore + " passengers";
		CreatePlayer ();
		paused = true;
	}
	void PauseGame()
	{
		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
	public void StartGame()
	{
		paused = false;
		Time.timeScale = 1;
		startingPanel.SetActive (false);
		GameStartCanvas.SetActive (true);
		EntireGame.SetActive (true);
		gp = GameObject.FindObjectOfType<GeneratePassenger> ();
		gp.CreatePassenger ();

	}
	void CreatePlayer()
	{
		Instantiate (chariotPlayer, startingPos, Quaternion.identity);
	}
	// Update is called once per frame
	void Update () {

		if (levelRestart) {
			startingPanel.SetActive (true);
			GameStartCanvas.SetActive (false);
			EntireGame.SetActive (false);
		}
		levelTime -= Time.deltaTime;
		timeText.text = Mathf.RoundToInt(levelTime).ToString ();
		scoreText.text = score + " / " + targetScore;
		coinsText.text = tempCoinCount.ToString ();
		if (tempCoinCount < money) {
			if(!IsInvoking("IncrementTemporaryCoins")){
				coinsText.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
				InvokeRepeating ("IncrementTemporaryCoins", 0f, timeBetweenCoinIncrement);
			}
		}
		if (levelTime <= 0) {
			GameOver ();
		}

		//cannot go over a minute
		if (levelTime > 60) {
			levelTime = 60;
		}
	}
	public void passengerDelivered()
	{
		score++;
		passengers++;
		gp.CreatePassenger ();
	
	}
	public void receiveFare(int coins)
	{
		money += coins;
	}
	void IncrementTemporaryCoins()
	{
		tempCoinCount++;
		asource.PlayOneShot (coinClip);
		if (tempCoinCount >= money) {
			CancelInvoke ();
			coinsText.transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}
	public void AddTime(float addedTime)
	{
		levelTime += addedTime;
	}
	public void RestartLevel()
	{
		levelRestart = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

	}
	public void LevelSelect()
	{
		SceneManager.LoadScene ("LevelSelect");
	}

	void GameOver()
	{
		gameOverPanel.SetActive(true);
		Time.timeScale = 0;
		if (score > bestScore) {
			PlayerPrefs.SetInt ("Best", score);
		}

		if (score >= targetScore) {
			achievementText.text = "Mission accomplished!";
		} else {
			achievementText.text = "Try again!";
		}
		finalScoreText.text = score + " / " + targetScore;
		bestScoreText.text = bestScore.ToString ();
		finalCoinsText.text = money.ToString ();
	}
}
