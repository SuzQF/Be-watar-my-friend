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

/// <summary>
///
/// </summary>
public class CameraController : MonoBehaviour
{
	private void FixedUpdate() {
		transform.position = new Vector3( FindObjectOfType<PlayerController>().transform.position.x,
															FindObjectOfType<PlayerController>().transform.position.y,
															transform.position.z);
	}
}
