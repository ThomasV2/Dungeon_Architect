using UnityEngine;
using System.Collections;

public partial class Player_Controller : MonoBehaviour {

    const float ROTATION_EXP = 10f;
    const float TRANSLATION_EXP = 10f;
    const float WALKING_ANIMATION_EXP = 10f;

	public float initSpeed = 6f;

	public float speed ;

    private Map_Generator generator;
    private Rigidbody rigid;
    private Animator anim;

	// Use this for initialization
	void Start () {
		speed = initSpeed;
        rigid = this.gameObject.GetComponent<Rigidbody>();
        generator = GameObject.Find("Generator").GetComponent<Map_Generator>();
        anim = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        bool isWalking = (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);

        if (isWalking)
        {
            float targetAngle = Mathf.Atan2(h, v) * 180f / Mathf.PI;
            Vector3 eulerAngles = this.transform.localEulerAngles;
            float currentAngle = eulerAngles.y;
            float angleDiff = (targetAngle - currentAngle);
            while (angleDiff >= 180f)
                angleDiff -= 360f;
            while (angleDiff < -180f)
                angleDiff += 360f;
            eulerAngles.y += angleDiff * (1f - Mathf.Exp(-ROTATION_EXP * Time.deltaTime));
            this.transform.localEulerAngles = eulerAngles;

            float sqrMagnitude = h * h + v * v;
            if (sqrMagnitude > 1f)
            {
                float magnitude = Mathf.Sqrt(sqrMagnitude);
                h /= magnitude;
                v /= magnitude;
            }

            rigid.velocity = new Vector3(h * speed, 0, v * speed);
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }

        float walkingTarget = isWalking ? 1f : 0f;
        float walkingCurrent = anim.GetFloat("walking");
        walkingCurrent += (walkingTarget - walkingCurrent) * (1f - Mathf.Exp(-WALKING_ANIMATION_EXP * Time.deltaTime));
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
