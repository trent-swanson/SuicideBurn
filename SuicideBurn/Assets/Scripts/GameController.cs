using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject pausePanel;
    public GameObject gameoverPanel;
	public Slider fuelSlider;
	public Text scoreText;
    public Text personText;

	bool isPaused = false;

	 void OnEnable()
    {
        GameManager.OnUpdateFuel += UpdateFuel;
		GameManager.OnUpdatePersonCount += UpdatePerson;
    }

    void OnDisable()
    {
        GameManager.OnUpdateFuel -= UpdateFuel;
		GameManager.OnUpdatePersonCount -= UpdatePerson;
    }

	void Start() {
		UpdateScore();
        UpdatePerson();
	}

    public void Pause() {
		isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
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

    void UpdatePerson()
    {
        personText.text = GameManager.personCount.ToString();
    }
}
