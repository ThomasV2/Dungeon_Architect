using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim.SetFloat("walking", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetButtonDown("Validation"))
    {
        Application.LoadLevel("Main");
    }
	}
}
