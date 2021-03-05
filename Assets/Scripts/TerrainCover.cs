using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
*Project Name: Be watar, my friend
*Create Date: 12/26
*Author: Suz
*Update Record: 
*
*/

/// <summary>
/// 地形遮挡类
/// </summary>
public class TerrainCover : MonoBehaviour
{
	[SerializeField]private float rendSpeed = 5;

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player") {
			StartCoroutine(FadeInCoroutine());
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			StartCoroutine(FadeOutCoroutine());
		}
	}

	
	
	IEnumerator FadeOutCoroutine() {
		float alpha = 1;
		while (alpha>0) {
			alpha -= Time.deltaTime * rendSpeed;
			Debug.Log(alpha);
			GetComponent<Tilemap>().color = new Color(1, 1, 1, alpha);
			yield return new WaitForSeconds(0);
		}
		
	}
	
	IEnumerator FadeInCoroutine() {
		float alpha = 0;
		while (alpha < 1) {
			alpha += Time.deltaTime * rendSpeed;
			GetComponent<Tilemap>().color = new Color(1, 1, 1, alpha);
			yield return new WaitForSeconds(0);
		}
		
	}

	
}
