using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

	public static int personCount;
	public static int score;

	[Range(0, 1000)]
	public static float fuel;

	public delegate void UpdateAction();
    public static event UpdateAction OnUpdateFuel;
	public static event UpdateAction OnUpdateScore;

	public static void UpdateFuel(float amount) {
		fuel += amount;
		OnUpdateFuel();
	}

	public static void UpdateScore(int amount) {
		score += amount;
		OnUpdateScore();
	}
}
