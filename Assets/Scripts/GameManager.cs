﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	public enum GameState { StartMenu, Fishing, Ninja, EndGame, Leaderboard }

	public static GameManager inst;
	public static event Action<GameState, GameState> onGameStateChanged;

	public GameState state;

	public void changeState(GameState newState, bool forceUpdate = false) {
		if (newState == state && forceUpdate == false) return;
		GameState oldState = state;
		state = newState;
		if (onGameStateChanged != null) {
			onGameStateChanged(oldState, newState);
		}
	}

	void Awake() {
		Assert.IsNull(inst);
		inst = this;
		changeState(GameState.StartMenu, true);
	}
}
