using UnityEngine;
using System.Collections;

public class Item_Pic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
		{
			Debug.Log("Contact !");
			Contact( other.gameObject.GetComponent<Player_Controller>());
		}
	}
	
	void Contact(Player_Controller thePlayer){
		thePlayer.Slowed( 5f, 3f );
	}
}
