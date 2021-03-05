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
public class BreakableTerrain : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name=="Player"&&
			collision.gameObject.GetComponent<Rigidbody2D>().velocity.y<=-7.65f) {
			Destroy(this);
		}
	}
}
