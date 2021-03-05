using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*Project Name: Be watar, my friend
*Create Date: 12/24
*Author: Suz
*Update Record: 
*
*/

/// <summary>
///
/// </summary>
public class FireTerrain : TerrainBase
{
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			collision.GetComponent<PlayerController>().Warm();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.GetComponent<PlayerController>().Warm();
		}
	}

	
}
