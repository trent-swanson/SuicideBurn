using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningFlash : MonoBehaviour {

	public float flashRate;
	public GameObject warningPanel;

	void Start() {
		InvokeRepeating("Flash",flashRate, flashRate);
	}

	void Flash() {
		if (warningPanel.activeSelf)
			warningPanel.SetActive(false);
		else
			warningPanel.SetActive(true);
	}
}
