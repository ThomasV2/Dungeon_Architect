using UnityEngine;
using System.Collections;

public partial class Player_Controller : MonoBehaviour {

	public float initSpeed = 10f;

	public float speed = 10f;

    private Map_Generator generator;
    private Rigidbody rigid;
	// Use this for initialization
	void Start () {
        rigid = this.gameObject.GetComponent<Rigidbody>();
        generator = GameObject.Find("Generator").GetComponent<Map_Generator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = speed * Input.GetAxis("Horizontal");
        float v = speed * Input.GetAxis("Vertical");
        //transform.Translate(h, 0, v);
		rigid.velocity = new Vector3(h, 0, v);
		
		UpdateFonc();

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
