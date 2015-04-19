﻿using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject target;
	public string[] attacks;
	int attacknumber = 0;
	Ray shootRay;
	RaycastHit shootHit;
	public float range = 5f;
	int shootableMask;
	public int damagePerHit = 20;
	AudioSource playerAudio;
	public AudioClip hitClip;
	public AudioClip swingClip;
	public bool attackPerforming = false;
	// Use this for initialization
	void Start () {
		playerAudio = GetComponent <AudioSource> ();
		attacks = new string[3];
		attacks[0] = "attack1";
		attacks[1] = "attack2";
		attacks[2] = "attack3";
		shootableMask = LayerMask.GetMask ("Shootable");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Fire1")>0) {
			StartCoroutine(attack());
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

		}
	}

	IEnumerator  attack() {

		if (attackPerforming) {
			yield return new WaitForSeconds(0);
		} else {
			attackPerforming = true;
			target.animation.Play (attacks[attacknumber]);
			attacknumber++;
			if (attacknumber == 3) {
				attacknumber = 0;
			}
			if(Physics.Raycast (shootRay, out shootHit, range))
			{
				Debug.DrawLine(shootRay.origin, shootHit.point);
				ZombieHealth enemyHealth = shootHit.collider.GetComponent <ZombieHealth> ();
				if(enemyHealth != null)
				{
					enemyHealth.TakeDamage (damagePerHit);
					playerAudio.clip = hitClip;
					playerAudio.Play();
				} else {
					playerAudio.clip = swingClip;
					playerAudio.Play();
				}
				
			} else {
				playerAudio.clip = swingClip;
				playerAudio.Play();
			}
			yield return new WaitForSeconds(0.5f);
			target.animation.Play ("idle");
			attackPerforming = false;
		}
	}


	IEnumerator  powerattack() {
		target.animation.Play ("powerAttack");
		yield return new WaitForSeconds(1.7f);
		target.animation.Play ("idle");
	}

	IEnumerator  finishattack() {
		target.animation.Play ("finishAttack");
		yield return new WaitForSeconds(1.5f);
		target.animation.Play ("idle");
	}
}
