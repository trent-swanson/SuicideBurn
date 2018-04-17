﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

	public static int personCount;
	public static int score;

	[Range(0, 1000)]
	public static float fuel;

	public delegate void UpdateFuelAction();
    public static event UpdateFuelAction OnUpdateFuel;

	public static void UpdateFuel(float amount) {
		fuel += amount;
		OnUpdateFuel();
	}
}
