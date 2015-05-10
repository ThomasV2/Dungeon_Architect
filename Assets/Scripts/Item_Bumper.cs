using UnityEngine;
using System.Collections;

public class Item_Bumper : MonoBehaviour {

	float CST_DEACTIVATION_TIME = 3f;

	float lastActivationTime = 0f;
	bool isOn = true;

	private Map_Generator generator;
	// Use this for initialization
	void Start () {
		generator = GameObject.Find("Generator").GetComponent<Map_Generator>();	
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
		if ( generator.CheckCanEnter( transform.position + 2*Vector3.right ) ){
			// on verifie si il a eu le temps de se réactiver
			if( !isOn ){
				if( Time.time > lastActivationTime + CST_DEACTIVATION_TIME ){
					isOn = true;
				}
			}
			// teleport si actif
			if( isOn ){
				thePlayer.Teleport( transform.position + 2*Vector3.right );
				lastActivationTime = Time.time;
				isOn = false;
			}

		}
	}


}
