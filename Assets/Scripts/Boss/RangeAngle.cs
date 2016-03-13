﻿using UnityEngine;
using System.Collections;


public class RangeAngle : MonoBehaviour {

	public Transform player;
	public Transform playerOnEnter;
	public Transform tongue;
	public Collider2D c;
	public float shotAngle;
	public bool fire = false;

	private BossController bossController;

	// Use this for initialization
	void Start () {
//		bossController = GameObject.Find<Testcontroller> ();
		bossController = GameObject.Find("Boss controller").GetComponent<BossController>();
		c = gameObject.GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (fire == false) {
			c.enabled = true;
			shotAngle = 57.296f * Mathf.Atan2((player.position.y - tongue.position.y), (player.position.x - tongue.position.x));
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag.Equals ("Player")){
			Debug.Log ("Player entered range.");
			//shotAngle = 57.296f * Mathf.Atan2((player.position.y - tongue.position.y), (player.position.x - tongue.position.x));
			playerOnEnter.position = new Vector2 (player.position.x, player.position.y);
			c.enabled = false;
			//tongue.eulerAngles = new Vector3 (0, 0, shotAngle+90);
			if (bossController.fire) {
				fire = true;
			} else {
				fire = false;
			}
		}
	}
		
}
