﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : Entity {

	public bool spin = true;
	float _angularDrag;

	void Start() {
		_sprite.flipX = Random.value > 0.5f;

		if (spin) {
			_rigidbody.rotation = Random.value * 360f;
			_rigidbody.angularVelocity = Random.Range(-1f, 1f) * 30;
			_angularDrag = _rigidbody.angularDrag;
			_rigidbody.angularDrag = 0;
		}
	}

	public override void attachTo(Rigidbody2D rb, Collider2D c) {
		base.attachTo(rb, c);
		_rigidbody.angularDrag = _angularDrag;
	}
}
