using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject pausePanel;
    public GameObject gameoverPanel;
	public Slider fuelSlider;

	bool isPaused = false;
    public bool isDead = false;

	 void OnEnable()
    {
        GameManager.OnUpdateFuel += UpdateFuel;
    }
    
    
    void OnDisable()
    {
        GameManager.OnUpdateFuel -= UpdateFuel;
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

	}

	void UpdateFuel() {
		fuelSlider.value = GameManager.fuel;
	}
}
