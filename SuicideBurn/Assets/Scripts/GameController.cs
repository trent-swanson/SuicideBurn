using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject pausePanel;

	bool isPaused = false;

	public void Pause() {
		isPaused = !isPaused;
		if (isPaused)
			pausePanel.SetActive(true);
		else
			pausePanel.SetActive(false);
	}

	public void Exit() {

	}
}
