using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map_Generator : MonoBehaviour {

    public GameObject Player_Prefab;
    public GameObject Ground_Prefab;
    public GameObject Finish_Prefab;
    public GameObject Wall_Prefab;
    public GameObject Camera;
    public GameObject[] item_list;

    private List<GameObject> Items = new List<GameObject>();
    private GameObject current_player;
    private GameObject current_item;
    private int index_select = 0;
    private int current_x = 1;
    private int current_y = 0;
    private bool isBuild = false;
    private float timer;
    private float velocity = 0f;
    private Vector3 pos_begin;

    #region Map
    enum Tile_Type
    {
        Invalid = -1,
        Wall,
        Ground,
        Begin,
        Finish
    };

        int[,] map = new int[20,20] 
            {
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 3, 0, 0, 0, -1},
            {-1, 2, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
            };
    #endregion

        // Use this for initialization
    void Start()
        {
            GameObject ground;
	        int y = 0;
            int x;
            while (y < 20)
            {
                x = 0;
                while (x < 20)
                {
                    switch (map[y,x])
                    {
                        case (int)Tile_Type.Invalid:
                            break;

                        case (int)Tile_Type.Wall:
                            ground = Instantiate(Wall_Prefab);
                            ground.transform.position = new Vector3(x, 0, y);
                            break;

                        case (int)Tile_Type.Ground:
                            ground = Instantiate(Ground_Prefab);
                            ground.transform.position = new Vector3(x, 0, y);
                            break;

                        case (int)Tile_Type.Begin:
                            ground = Instantiate(Player_Prefab);
                            ground.transform.position = new Vector3(x, 0, y);
                            pos_begin = ground.transform.position;
                            current_player = ground;
                            ground = Instantiate(Ground_Prefab);
                            ground.transform.position = new Vector3(x, 0, y);
                            break;

                        case (int)Tile_Type.Finish:
                            ground = Instantiate(Finish_Prefab);
                            ground.transform.position = new Vector3(x, 0, y);
                            break;
                        
                        default:
                            break;
                    }
                    ++x;
                }
                ++y;
            }
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
            Camera.transform.position = new Vector3(Mathf.SmoothDamp(Camera.transform.position.x, current_item.transform.position.x + 2f, ref velocity, 0.1f), Mathf.SmoothDamp(Camera.transform.position.y, current_item.transform.position.y + 7.5f, ref velocity, 0.1f), Mathf.SmoothDamp(Camera.transform.position.z, current_item.transform.position.z + -7.5f, ref velocity, 0.1f));
        if (isBuild && timer > 0.1f)
        {
            if (Input.GetAxis("Horizontal") > 0.5f)
            {
                current_x = ((current_x < 20) ? current_x + 1 : current_x);
                timer = 0f;
            }
            else if (Input.GetAxis("Horizontal") < -0.5f)
            {
                current_x = ((current_x > 1) ? current_x - 1 : current_x);
                timer = 0f;
            }
            else if (Input.GetAxis("Vertical") > 0.5f)
            {
                current_y = ((current_y < 20) ? current_y + 1 : current_y);
                timer = 0f;
            }
            else if (Input.GetAxis("Vertical") < -0.5f)
            {
                current_y = ((current_y > 1) ? current_y - 1 : current_y);
                timer = 0f;
            }
            current_item.transform.position = new Vector3(current_x, current_item.transform.position.y, current_y);
           if (Input.GetButtonDown("Prev"))
            {
                index_select = index_select > 0 ? index_select - 1 : item_list.Length - 1;
                GameObject new_item = Instantiate(item_list[index_select]);
                new_item.transform.position = current_item.transform.position;
                Destroy(current_item);
                current_item = new_item;
            }
            else if (Input.GetButtonDown("Next"))
            {
                index_select = index_select < item_list.Length - 1 ? index_select + 1 : 0;
                GameObject new_item = Instantiate(item_list[index_select]);
                new_item.transform.position = current_item.transform.position;
                Destroy(current_item);
                current_item = new_item;
            }
            if (Input.GetButtonDown("Validation") && checkValidation())
            {
                current_item.transform.position = new Vector3(current_item.transform.position.x, 0f, current_item.transform.position.z);
                Items.Add(current_item);
                isBuild = false;
                current_item = null;
                current_player = Instantiate(Player_Prefab);
                current_player.transform.position = pos_begin;
            }
        }
        timer += Time.deltaTime;
    }

    bool checkValidation()
    {
        if (map[(int)current_item.transform.position.z, (int)current_item.transform.position.x] != (int)Tile_Type.Ground)
            return false;
        foreach (GameObject item in Items)
        {
            if ((item.transform.position.x == current_item.transform.position.x
                && item.transform.position.z == current_item.transform.position.z))
            {
                return false;
            }
        }
        return true;
    }

    public void PutMode()
    {
        Destroy(current_player);
        isBuild = true;
        index_select = 0;
        current_item = Instantiate(item_list[0]);
        current_item.transform.position = new Vector3(1f, 0.5f, 1f);
    }

}
