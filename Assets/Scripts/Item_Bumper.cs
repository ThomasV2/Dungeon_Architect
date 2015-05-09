using UnityEngine;
using System.Collections;

public class Item_Bumper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
		{
			Debug.Log("Contact Bumper !");
			Contact( other.gameObject.GetComponent<Player_Controller>());
		}
	}
	
	void Contact(Player_Controller thePlayer){
		// verifier que la case cible est dispo
		// map.getCaseTypeAt()
		thePlayer.Teleport( transform.position + 2*Vector3.right );
	}


}
