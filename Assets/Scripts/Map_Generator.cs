using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map_Generator : MonoBehaviour {

    public GameObject Player_Prefab;
    public GameObject Ground_Prefab;
    public GameObject Finish_Prefab;
    public GameObject Camera;
    public GameObject[] item_list;
    public int Size = 0;

    private List<GameObject> Items = new List<GameObject>();
    private GameObject current_player;
    private GameObject current_item;
	// Use this for initialization
	void Start () 
    {
        GameObject ground;
	    int i;
        current_player = Instantiate(Player_Prefab);
        for (i = 0; i < Size; i++)
        {
            ground = Instantiate(Ground_Prefab);
            ground.transform.position = new Vector3(i * 2.0f, 0, 0);
        }
        ground = Instantiate(Finish_Prefab);
        ground.transform.position = new Vector3(i * 2.0f, 0, 0);
        foreach (GameObject item in Items)
        {
            Instantiate(item);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (current_player != null)
            Camera.transform.position = new Vector3(current_player.transform.position.x + 2f, current_player.transform.position.y + 7.5f, current_player.transform.position.z + -7.5f);
        else
            Camera.transform.position = new Vector3(current_item.transform.position.x + 2f, current_item.transform.position.y + 7.5f, current_item.transform.position.z + -7.5f);
    }

    public void PutMode()
    {
        Destroy(current_player);

    }
}
