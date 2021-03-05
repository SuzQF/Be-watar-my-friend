using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
*Project Name: Be watar, my friend
*Create Date: 12/24
*Author: Suz
*Update Record: 
*
*/
/// <summary>
/// 玩家物理状态列举
/// </summary>
public enum PlayerPhysicsStatus {
	Ice,
	Liquid,
	Steam,
}

/// <summary>
/// 玩家形状状态列举
/// </summary>
public enum PlayerShapeStatus {
	Wider,
	Square,
	Taller,
}


/// <summary>
/// 玩家操作类
/// </summary>
public class PlayerController : MonoBehaviour {
	public static PlayerController instance;

	public Animator animator;
	public Rigidbody2D rg;

	public Text GM;


	public const float IceMaxJumpSpeed = 6.5f;
	public const float LiquidMaxJumpSpeed = 7.2f;
	public const float SteamMaxJumpSpeed = 8.0f;

	//玩家的形状及物理状态
	public PlayerPhysicsStatus PhysicsStatus = PlayerPhysicsStatus.Liquid;
	public PlayerShapeStatus ShapeStatus = PlayerShapeStatus.Square;

	//玩家属性
	public bool CanHoldSwitch = true;
	public float JumpForce = 600;
	public bool CanShapeChange = true;
	public float moveSpeed = 600;
	//玩家max属性
	//坐标轴速度
	public float maxJumpSpeed = 6.2f;
	public float maxHorizontalSpeed = 2f;
	public float maxVerticalSpeed = 12f;

	//玩家状态
	public bool isJumping;
	public bool isFalling;
	public bool isGround;
	public bool isNextWall;

	public GameObject currentPlayer;
	private bool isFallToSqueeze = false;
	float maxv = 0;
	public bool isFallToBreak = false;
	public int GM4count = 0;
	public int GM5count = 0;
	float alpha = 1;

	private void Awake() {
		instance = this;
		rg = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}



	private void Update() {
		//transform.position = currentPlayer.transform.position;
		Move();
		Jump();
		animator.SetFloat("VerticalSpeed", Mathf.Abs(rg.velocity.y));
		animator.SetFloat("HorizontalSpeed", Mathf.Abs(rg.velocity.x));
		MoveStatusChange();

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			GetComponent<SpriteRenderer>().flipX = false;
		}

		if (maxv > rg.velocity.y) {
			maxv = rg.velocity.y;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log(rg.velocity.x + "    " + rg.velocity.y);
		}
	}

	private void MoveStatusChange() {
		if (rg.velocity.y < -0.1f) {
			SetMoveStatus(2);
		}
		if (isFalling && rg.velocity.y <= 0.1f && rg.velocity.y > -0.1f) {
			SetMoveStatus(0);
		}

		//max起跳速度
		if (rg.velocity.y >= maxJumpSpeed) {
			rg.velocity = new Vector2(rg.velocity.x, maxJumpSpeed);
		}

		//max水平速度
		if (rg.velocity.x > maxHorizontalSpeed) {
			rg.velocity = new Vector2(maxHorizontalSpeed, rg.velocity.y);
		}
		if (rg.velocity.x < -maxHorizontalSpeed) {
			rg.velocity = new Vector2(-maxHorizontalSpeed, rg.velocity.y);
		}

		//max坠落速度
		if (rg.velocity.y <= -7.65) {
			isFallToBreak = true;
		}
		if (rg.velocity.y <= -maxVerticalSpeed) {
			isFallToSqueeze = true;
			rg.velocity = new Vector2(rg.velocity.x, -maxVerticalSpeed);
			Debug.Log("Fall to Squeeze");
		}
	}

	private void SetMoveStatus(int status) {
		switch (status) {
			case 0:
				isGround = true;
				isJumping = false;
				isFalling = false;
				animator.SetBool("isGround", true);
				animator.SetBool("isJumping", false);
				animator.SetBool("isFalling", false);
				break;
			case 1:
				isGround = false;
				isJumping = true;
				isFalling = false;
				animator.SetBool("isGround", false);
				animator.SetBool("isJumping", true);
				animator.SetBool("isFalling", false);
				break;
			case 2:
				isGround = false;
				isJumping = false;
				isFalling = true;
				animator.SetBool("isGround", false);
				animator.SetBool("isJumping", false);
				animator.SetBool("isFalling", true);
				break;
			default:
				break;
		}
	}

	private void Jump() {
		if ((Input.GetKeyDown(KeyCode.W) ||
			Input.GetKeyDown(KeyCode.UpArrow)) &&
			!isJumping && !isFalling) {
			animator.SetTrigger("Jump");
			SetMoveStatus(1);
			rg.AddForce(new Vector2(0, JumpForce));
		}
	}

	private void Move() {
		float moveH = Input.GetAxis("Horizontal");

		rg.AddForce(new Vector2(moveH * Time.deltaTime * moveSpeed, 0));
	}

	private void ShapeChange() {
		switch (ShapeStatus) {
			case PlayerShapeStatus.Wider:
				transform.localScale = new Vector3(2, 0.5f, 1); 
				break;
			case PlayerShapeStatus.Square:
				transform.localScale = new Vector3(1, 1, 1);
				break;
			case PlayerShapeStatus.Taller:
				transform.localScale = new Vector3(0.5f, 2f, 1);
				break;
			default:
				break;
		}
	}


	/// <summary>
	/// 被横向挤压
	/// </summary>
	public void BeHorizontalSqueezed() {
		if (CanShapeChange == false) {
			return;
		}
		if (ShapeStatus < PlayerShapeStatus.Taller) {
			ShapeStatus += 1;
		}
		ShapeChange();

		//变形动画
	}

	/// <summary>
	/// 被纵向挤压
	/// </summary>
	public void BeVerticalSqueezed() {
		Debug.Log("beVerticalSqueeze");
		if (CanShapeChange == false) {
			return;
		}
		if (ShapeStatus > PlayerShapeStatus.Wider) {
			ShapeStatus -= 1;
		}
		ShapeChange();
		//变形动画
	}

	/// <summary>
	/// 加热
	/// </summary>
	public void Warm() {
		if (PhysicsStatus == PlayerPhysicsStatus.Steam) {
			return;
		}
		PhysicsStatus += 1;
		PropertyChange();
		//变形动画
	}

	/// <summary>
	/// 制冷
	/// </summary>
	public void Cool() {
		if (PhysicsStatus == PlayerPhysicsStatus.Ice) {
			return;
		}
		PhysicsStatus -= 1;
		PropertyChange();
		//变形动画
	}



	/// <summary>
	/// 改变玩家属性
	/// </summary>
	private void PropertyChange() {
		switch (PhysicsStatus) {
			case PlayerPhysicsStatus.Ice:
				GetComponent<SpriteRenderer>().color = Color.blue;
				CanHoldSwitch = true;
				maxJumpSpeed = IceMaxJumpSpeed;
				CanShapeChange = false;
				break;
			case PlayerPhysicsStatus.Liquid:
				GetComponent<SpriteRenderer>().color = Color.white;
				CanHoldSwitch = true;
				maxJumpSpeed = LiquidMaxJumpSpeed;
				CanShapeChange = true;
				break;
			case PlayerPhysicsStatus.Steam:
				GetComponent<SpriteRenderer>().color = Color.red;
				CanHoldSwitch = false;
				maxJumpSpeed = SteamMaxJumpSpeed;
				CanShapeChange = true;
				break;
			default:
				break;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log(maxv);
		maxv = 0;
		if (collision.gameObject.tag == "Ground" && isFalling) {
			if (isFallToSqueeze) {
				BeVerticalSqueezed();
				isFallToSqueeze = false;
			}
			animator.SetBool("isGround", true);
			rg.velocity = new Vector2(rg.velocity.x, 0);
			SetMoveStatus(0);
		}
		if (isFallToBreak && collision.gameObject.name == "BreakableTerrain" &&
			PhysicsStatus == PlayerPhysicsStatus.Ice) {
			Destroy(collision.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "MEven") {
			switch (collision.gameObject.name) {
				case "M":
					FindObjectOfType<MEven>().M = true;
					break;
				case "E":
					FindObjectOfType<MEven>().E = true;
					break;
				case "v":
					FindObjectOfType<MEven>().v = true;
					break;
				case "e":
					FindObjectOfType<MEven>().e = true;
					break;
				case "n":
					FindObjectOfType<MEven>().n = true;
					break;
				default:
					break;
			}
			Destroy(collision.gameObject);
		}
		if (collision.gameObject.tag == "GM") {
			alpha = 1;
			switch (collision.gameObject.name) {
				case "1":
					GM.text = "我的朋友，放轻松些，别去畏惧冒险和新事物。";
					Destroy(GameObject.Find("2"));
					break;
				case "2":
					GM.text = "逃避未知是人类的第一天性，但这样就会错过很多丰富多彩的冒险。";
					Destroy(GameObject.Find("1"));
					break;
				case "3":
					GM.text = "诱人的东西总是难以触碰，但时机尚未成熟。别心急，朋友。";
					break;
				case "4":
					GM4count += 1;
					if (GM4count == 2) {
						if (ShapeStatus == PlayerShapeStatus.Wider) {
							GM.text = "我的朋友，希望你水做的脑子刚才没有被摔坏！";
						}
					}
					else {
						return;
					}
					break;
				case "5":
					GM5count += 1;
					if (GM5count == 2) {
						GM.text = "虽然不太轻松，但确实必须承认，不论是水还是人，都有自己的能力上限，这不丢人。";
					}
					else {
						return;
					}
					break;
				case "6":
					GM.text = "人生就是如此，可望不可及的东西往往比可及的东西多得多……得多得多得多得多得多。";
					break;
				case "7":
					GM.text = "那句话怎么说来着？滴水石穿！好像不太对……总之，我惊叹于你的力量！";
					break;
				case "8":
					GM.text = "一鼓作气！那个宝贝还在那里等着你呢！";
					break;
				case "9":
					if (ShapeStatus == PlayerShapeStatus.Square) {
						GM.text = "不知道你有没有在意过三层夹心的汉堡，尤其是里面那层薄薄的肉饼……";
					}
					else if (ShapeStatus == PlayerShapeStatus.Wider) {
						GM.text = "老实说我都有些不认得你了，但外表并不重要，你说对么？";
						return;
					}
					break;
				case "10":
					GM.text = "朋友，如果我是你，我会考虑收起腹部让自己在夹缝中等待。";
					break;
				case "11":
					GM.text = "水的优势在于何处，看来在心中已经有了答案呀……";
					break;
				case "12":
					if (PhysicsStatus != PlayerPhysicsStatus.Steam) {
						GM.text = "朋友，我相信你在你水做的脑子里已经有了想法，对吗？";
					}
					break;
				case "13":
					if (PhysicsStatus == PlayerPhysicsStatus.Ice) {
						GM.text = "希望周围没有想吃雪糕的小孩子……";
					}
					break;
				case "14":
					if (PhysicsStatus == PlayerPhysicsStatus.Ice) {
						GM.text = "没关系，虽然你跳得矮了些，但你变得更加坚硬了嘛！";
					}
					break;
				case "15":
					if (PhysicsStatus == PlayerPhysicsStatus.Steam) {
						GM.text = "朦胧而又缥缈的外形就像是空中的云，说不定可以飞起来呢？";
					}
					else {
						return;
					}
					break;
				case "16":
					if (PhysicsStatus != PlayerPhysicsStatus.Steam) {
						GM.text = "它彻底明白了如何活得像一团水！";
					}
					else {
						return;
					}
					break;
				case "17":
					if (PhysicsStatus == PlayerPhysicsStatus.Steam) {
						GM.text = "你好像……太轻了点，想个办法增加你的重量吧！";
					}
					break;
				case "18":
					GM.text = "足够的撞击力需求足够的高度，我想对你而言应该不难理解。";
					break;
			}
			StartCoroutine(Fadeout());
			Destroy(collision.gameObject);
		}
	}

	IEnumerator Fadeout() {
		bool over = false;
		while (!over) {
			over = true;
			yield return new WaitForSeconds(3);
		}
		GM.text = "";
		yield break;
	}
}
