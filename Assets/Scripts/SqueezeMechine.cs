using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
*Project Name: Be watar, my friend
*Create Date: 12/26
*Author: Suz
*Update Record: 
*
*/

public enum SqueezeDirection {
	Up,
	Down,
	Right,
	Left,
}


/// <summary>
///
/// </summary>
public class SqueezeMechine : MonoBehaviour {
	public Animator animator;
	public SqueezeDirection direction;

	//public float axisDistance;


	//public Vector3 basePosition;
	//public Vector3 goalPosition;

	//public bool isTriggered = false;
	//public bool isSqueezeOver = false;
	//public float speed = 6f;

	//private void Awake() {
	//	basePosition = transform.position;
	//	switch (direction) {
	//		case SqueezeDirection.Up:
	//			goalPosition = new Vector3(basePosition.x, basePosition.y + axisDistance, basePosition.z);
	//			break;
	//		case SqueezeDirection.Down:
	//			goalPosition = new Vector3(basePosition.x, basePosition.y - axisDistance, basePosition.z);
	//			break;
	//		case SqueezeDirection.Right:
	//			goalPosition = new Vector3(basePosition.x + axisDistance, basePosition.y, basePosition.z);
	//			break;
	//		case SqueezeDirection.Left:
	//			goalPosition = new Vector3(basePosition.x - axisDistance, basePosition.y, basePosition.z);
	//			break;
	//		default:
	//			break;
	//	}
	//}

	//void Update() {
	//	Squeeze();
	//	StatusJudge();
	//	Back();
	//}

	//private void Back() {
	//	if (isSqueezeOver && transform.position != basePosition) {
	//		Debug.Log("Back");
	//		switch (direction) {
	//			case SqueezeDirection.Up:
	//				transform.Translate(new Vector3(0, -Time.deltaTime * speed, 0));
	//				break;
	//			case SqueezeDirection.Down:
	//				transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
	//				break;
	//			case SqueezeDirection.Right:
	//				transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0));
	//				break;
	//			case SqueezeDirection.Left:
	//				transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
	//				break;
	//			default:
	//				break;
	//		}
	//	}
	//}

	//private void Squeeze() {
	//	if (isTriggered && transform.position != goalPosition) {
	//		Debug.Log("Squeeze");
	//		switch (direction) {
	//			case SqueezeDirection.Up:
	//				transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
	//				break;
	//			case SqueezeDirection.Down:
	//				transform.Translate(new Vector3(0, -Time.deltaTime * speed, 0));
	//				break;
	//			case SqueezeDirection.Right:
	//				transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
	//				break;
	//			case SqueezeDirection.Left:
	//				transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0));
	//				break;
	//			default:
	//				break;
	//		}
	//	}
	//}

	//private void StatusJudge() {
	//	//挤压机朝右
	//	if (direction == SqueezeDirection.Right) {
	//		if (transform.position.x > goalPosition.x) {
	//			transform.position = goalPosition;
	//			isSqueezeOver = true;
	//			isTriggered = false;
	//		}
	//		if (transform.position.x < basePosition.x) {
	//			transform.position = basePosition;
	//			isSqueezeOver = false;
	//		}
	//	}

	//	//挤压机朝左
	//	else if (direction == SqueezeDirection.Left) {
	//		if (transform.position.x < goalPosition.x) {
	//			transform.position = goalPosition;
	//			isTriggered = false;
	//			isSqueezeOver = true;
	//		}
	//		if (transform.position.x > basePosition.x) {
	//			transform.position = basePosition;
	//			isSqueezeOver = false;
	//		}
	//	}
		

	//	//挤压机朝上
	//	else if (direction == SqueezeDirection.Up) {
	//		if (transform.position.y > goalPosition.y) {
	//			transform.position = goalPosition;
	//			isTriggered = false;
	//			isSqueezeOver = true;
	//		}
	//		if (transform.position.y < basePosition.y) {
	//			transform.position = basePosition;
	//			isSqueezeOver = false;
	//		}
	//	}

	//	//挤压机朝下
	//	else if (direction == SqueezeDirection.Down) {
	//		if (transform.position.y < goalPosition.y) {
	//			transform.position = goalPosition;
	//			isTriggered = false;
	//			isSqueezeOver = true;
	//		}
	//		if (transform.position.y > basePosition.y) {
	//			transform.position = basePosition;
	//			isSqueezeOver = false;
	//		}
	//	}

	//}
	
	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("Squeeze Mechine");
		if (collision.name == "Player") {
			animator.SetTrigger("Triggered");
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			if ((direction == SqueezeDirection.Up && collision.gameObject.GetComponent<PlayerController>().isNextWall ||
				direction == SqueezeDirection.Down && collision.gameObject.GetComponent<PlayerController>().isGround) &&
				collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0.1f &&
				collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > -0.1f) 
				{
				collision.gameObject.GetComponent<PlayerController>().BeVerticalSqueezed();
			}
			else if ((direction == SqueezeDirection.Right ||
				direction == SqueezeDirection.Left) &&
				collision.gameObject.GetComponent<Rigidbody2D>().velocity.x < 0.1f &&
				collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > -0.1f && 
				collision.gameObject.GetComponent<PlayerController>().isNextWall) 
				{
				collision.gameObject.GetComponent<PlayerController>().BeHorizontalSqueezed();
			}
		}
		OnTriggerEnter2D(collision.gameObject.GetComponent<Collider2D>());
	}
}
