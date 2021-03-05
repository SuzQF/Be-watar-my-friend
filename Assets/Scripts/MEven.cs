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
public class MEven : MonoBehaviour
{
	public bool M = false;
	public bool E = false;
	public bool v = false;
	public bool e = false;
	public bool n = false;

	public GameObject MEven_Soft;

	private void Update() {
		if (M&&E&&v&&e&&n) {
			MEven_Soft.SetActive(true);
			MEven_Soft.transform.position = FindObjectOfType<PlayerController>().transform.position;
			FindObjectOfType<PlayerController>().GM.text = "Thanks For Playing Our First Game!";
		}
	}

}
