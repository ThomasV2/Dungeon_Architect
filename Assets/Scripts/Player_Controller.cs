using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

    public float Speed = 0.2f;

    private Map_Generator generator;
    private Rigidbody rigid;
	// Use this for initialization
	void Start () {
        rigid = this.gameObject.GetComponent<Rigidbody>();
        generator = GameObject.Find("Generator").GetComponent<Map_Generator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Speed * Input.GetAxis("Horizontal");
        float v = Speed * Input.GetAxis("Vertical");
        transform.Translate(h, 0, v);
        rigid.velocity = Vector3.zero;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Debug.Log("Fini !");
            generator.PutMode();
        }
    }
}
