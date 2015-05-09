using UnityEngine;
using System.Collections;

public class Item_Controller : MonoBehaviour {

    public Map_Generator.Tile_Type trap;

    private Map_Generator generator;
	// Use this for initialization
	void Start () {
        generator = GameObject.Find("Generator").GetComponent<Map_Generator>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        generator.item_list.Add(trap);
        Destroy(this.gameObject);
    }
}
