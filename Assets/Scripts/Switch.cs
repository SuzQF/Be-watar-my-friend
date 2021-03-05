using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*Project Name: Be watar, my friend
*Create Date: 12/27
*Author: Suz
*Update Record: 
*
*/

/// <summary>
///
/// </summary>
public class Switch : MonoBehaviour
{
	public Sprite switched;
	public MovePuzzle puzzleInControl;
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name=="Player") {
			if (FindObjectOfType<PlayerController>().PhysicsStatus==PlayerPhysicsStatus.Steam) {
				return;
			}
			else {
				GetComponent<SpriteRenderer>().sprite = switched;
				puzzleInControl.ChangeCollider();
				puzzleInControl.isSwitched = true;
				GetComponent<CircleCollider2D>().enabled = false;
			}
		}
	}
}
