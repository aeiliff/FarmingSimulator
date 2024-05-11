using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Cow : MonoBehaviour {
	public Animator animator;
	public int sickTime;
	
	void Start () {
		animator = GetComponent<Animator>();
		StartCoroutine(MyCoroutine());	
	}
	
	// game loop
	IEnumerator MyCoroutine() {
		yield return new WaitForSeconds(sickTime);
		//animator.Play("SickCow");
		animator.SetBool("IsSick", true);
		animator.SetBool("IsIdle", false);

		yield return new WaitForSeconds(30);
		if (animator.GetBool("IsSick")) {
			animator.SetBool("IsSick", false);
			animator.SetBool("IsDead", true);
		}
		else {
			GameManager.instance.AddPoints(10);
			CureCow();
		}
	}

	public void CureCow() {
		animator.SetBool("IsSick", false);
		animator.SetBool("IsIdle", true);
	}
}
