using UnityEngine;
using System.Collections;

public partial class Player_Controller : MonoBehaviour {

	public float initSpeed = 10f;

	public float speed = 10f;

    private Map_Generator generator;
    private Rigidbody rigid;
    private Animator anim;
	// Use this for initialization
	void Start () {
        rigid = this.gameObject.GetComponent<Rigidbody>();
        generator = GameObject.Find("Generator").GetComponent<Map_Generator>();
        anim = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float h = speed * Input.GetAxis("Horizontal");
        float v = speed * Input.GetAxis("Vertical");
        if (h > 5f)
            this.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        else if (h < -5f)
            this.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
        else if (v > 5f)
            this.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        else if (v < -5f)
            this.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		rigid.velocity = new Vector3(h, 0, v);


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        bool isWalking = (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);
        float walkingTarget = isWalking ? 1f : 0f;
        float walkingCurrent = anim.GetFloat("walking");
        walkingCurrent += (walkingTarget - walkingCurrent) * (1f - Mathf.Exp(-10f * Time.deltaTime));
        anim.SetFloat("walking", walkingCurrent);

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
