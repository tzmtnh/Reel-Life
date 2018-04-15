﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Hook : MonoBehaviour {

	enum State { Idle, GoingDown, GoingUp }

	public static Hook inst;

	public float reactionSpeed = 20;

	public LineRenderer rope;

	State _state = State.Idle;
	Collider2D _collider;

	Rigidbody2D _rigidbody;
	public Rigidbody2D rigidbody {
		get { return _rigidbody; }
	}

	void updateLeftRight() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			_rigidbody.AddForce(new Vector2(-reactionSpeed, 0));
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			_rigidbody.AddForce(new Vector2(reactionSpeed, 0));
		}
	}

	void Awake() {
		Assert.IsNull(inst);
		inst = this;

		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<Collider2D>();

		rope.SetPosition(0, _rigidbody.transform.position);
	}

	void FixedUpdate() {
		switch (_state) {
			case State.Idle:
				_rigidbody.gravityScale = 0;
				if (Input.GetKeyDown(KeyCode.Space)) {
					_state = State.GoingDown;
				}
				break;

			case State.GoingDown:
				_rigidbody.gravityScale = 1;
				updateLeftRight();
				break;

			case State.GoingUp:
				_rigidbody.gravityScale = -1;
				updateLeftRight();
				break;

			default:
				Debug.LogError("Unhandled Rob State");
				break;
		}
	}

	void Update() {
		rope.SetPosition(0, _rigidbody.transform.position);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Entiry")) {
			Entiry entiry = collision.collider.GetComponent<Entiry>();
			Assert.IsNotNull(entiry);
			entiry.attachTo(_rigidbody, _collider);
			if (_state == State.GoingDown) {
				_state = State.GoingUp;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (_state == State.GoingDown && collision.CompareTag("Ground")) {
			_state = State.GoingUp;
		} else if (_state == State.GoingUp && collision.CompareTag("SeaLevel")) {
			_state = State.Idle;
			GameManager.inst.changeState(GameManager.GameState.Ninja);
		}
	}
}
