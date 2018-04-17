using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject pausePanel;
    public GameObject gameoverPanel;
	public Slider fuelSlider;
	public Text scoreText;

	bool isPaused = false;

	 void OnEnable()
    {
        GameManager.OnUpdateFuel += UpdateFuel;
		GameManager.OnUpdatePersonCount += UpdateScore;
    }

    void OnDisable()
    {
        GameManager.OnUpdateFuel -= UpdateFuel;
		GameManager.OnUpdatePersonCount -= UpdateScore;
    }

	void Start() {
		UpdateScore();
	}

    public void Pause() {
		isPaused = !isPaused;
		if (isPaused)
			pausePanel.SetActive(true);
		else
			pausePanel.SetActive(false);
	}

    public void GameOver() {
        gameoverPanel.SetActive(true);
    }

	public void Exit() {
		Application.Quit();
	}

	void UpdateFuel() {
		fuelSlider.value = GameManager.fuel;
	}

	void UpdateScore() {
		scoreText.text = GameManager.score.ToString();
	}
}
