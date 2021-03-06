﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour {

	public static InputManager inst;

	public float horizontal;
	public bool click;

	//Quaternion _initAttitudeInv = Quaternion.identity;

	void Awake () {
		Assert.IsNull(inst);
		inst = this;
	}

	
	void Start() {
		if (Application.platform == RuntimePlatform.Android) {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			//Input.gyro.enabled = true;
			//_initAttitudeInv = Quaternion.Inverse(Input.gyro.attitude);
		}
	}

	void Update () {
		horizontal = 0;
		click = false;

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0) {
				Touch touch = Input.touches[0];
				if (touch.phase != TouchPhase.Canceled) {
					float x = touch.position.x / Screen.width;
					if (x < 0.3f) {
						horizontal = -1;
					} else if (x > 0.7f) {
						horizontal = 1;
					}
				}
			}

			/*
			Quaternion attitude = _initAttitudeInv * Input.gyro.attitude;
			_angles = attitude.eulerAngles;
			_angle = attitude.eulerAngles.z - 180f;
			_angle = (180f - Mathf.Abs(_angle)) * Mathf.Sign(_angle);
			const float thresh = 10;
			if (Mathf.Abs(_angle) > thresh) {
				horizontal = _angle / 20f;
			}
			*/


		} else {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				horizontal = -1;
			}

			if (Input.GetKey(KeyCode.RightArrow)) {
				horizontal = 1;
			}

			if (Input.GetMouseButton(0)) {
				horizontal = -1;
			}

			if (Input.GetMouseButton(1)) {
				horizontal = 1;
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	/*
	void OnGUI() {
		if (Application.platform != RuntimePlatform.Android) return;
		GUIStyle style = new GUIStyle();
		style.fontSize = 40;
		//GUI.Label(new Rect(100, 100, 200, 100), "" + _angles, style);
		//GUI.Label(new Rect(100, 200, 200, 100), "" + _angle, style);
		GUI.Label(new Rect(100, 100, 200, 100), "" + debug, style);
	}
	*/
}
