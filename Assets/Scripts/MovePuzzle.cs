using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*Project Name: 
*Create Date: 
*Author: 
*Update Record: 
*
*/

/// <summary>
///
/// </summary>
public class MovePuzzle : MonoBehaviour {
	public bool isSwitched = false;
	public Vector3 goalPosition;
	public float speed;

	private void Update() {
		if (isSwitched&&transform.position!=goalPosition) {
			Debug.Log("Switched"+ speed * Time.deltaTime	);
			transform.position =  Vector3.Lerp(transform.position, goalPosition, speed * Time.deltaTime);
		}
	}

	internal void ChangeCollider() {
		GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
	}
}
