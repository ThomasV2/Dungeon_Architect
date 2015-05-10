using UnityEngine;
using System.Collections;

public partial class Player_Controller : MonoBehaviour {

	public float slowTimer = 0f;
	public bool isSlowed = false;

	// ralentir le joueur
	public void Slowed(float newSpeed, float duration){
		speed = newSpeed;
		slowTimer = duration;
		isSlowed = true;
	}

	//teleportation ou bumper
	public void Teleport(Vector3 destination){
		Debug.Log("Teleport ! " + destination);
		transform.position = destination ;
	}


	// Pour les timers
	void UpdateFonc(){
		if( isSlowed ){
			slowTimer = slowTimer - Time.deltaTime;
			if( slowTimer <= 0 ){
				slowTimer = 0f;
				isSlowed = false;
				speed = initSpeed;
			}
		}

	}
}
