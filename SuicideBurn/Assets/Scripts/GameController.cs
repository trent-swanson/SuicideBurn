using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject pausePanel;
    public GameObject gameoverPanel;

	bool isPaused = false;
    public bool isDead = false;

	public void Pause() {
		isPaused = !isPaused;
		if (isPaused)
			pausePanel.SetActive(true);
		else
			pausePanel.SetActive(false);
	}

    public void GameOver()
    {
            gameoverPanel.SetActive(true);
    }

	public void Exit() {

	}
}
