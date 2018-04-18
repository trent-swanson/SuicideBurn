using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject pausePanel;
    public GameObject victoryPanel;
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

    public void Victory() {
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

	public void Exit() {
		Application.Quit();
	}

    public void RestartLevel() {
        Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    //remove
    public void GameOver() {    }
}
