using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Map_Generator : MonoBehaviour {

    //TODO même ordre que les Tiles !
    public GameObject[] Prefab;
    public GameObject Camera;
    public List<Tile_Type> item_list;
    public Text time_score;

    private List<GameObject> Items = new List<GameObject>();
    private GameObject current_player;
    private GameObject current_item;
    private int index_select = 0;
    private int current_x = 1;
    private int current_y = 0;
    private bool isBuild = false;
    private bool isWait = false;
    private float timer;
    private float velocity = 0f;
    private float score;
    private GameObject parent;
    #region Map
    public enum Tile_Type
    {
        Invalid = -1,
        Wall,
        Ground,
        Begin,
        Finish,
        Spike_sac,
        Spike_trap,
		Bump_sac,
		Bump_obj,
		Terre
    };

        
	int[,] map = new int[20,20] 
            {
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 1, 5, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 7, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 3, 1, 1, 0, -1},
            {-1, 2, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, -1},
            {-1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, -1},
            {-1, 0, 0, 0, 0, 1, 1, 6, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, -1},
            {-1, 0, 0, 0, 0, 1, 1, 4, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
            };
    #endregion

        // Use this for initialization
    void Start() {            
		Generate();        
	}
    void Generate()
    {
        parent = new GameObject();
        parent.name = "Parent";
        GameObject ground;
        int y = 0;
        int x;
		int typeItem;

        while (y < 20) 
		{
            x = 0;
            while (x < 20)
            {			
				typeItem = map[y,x];
				switch ( typeItem )
				{
                case (int)Tile_Type.Invalid:
					ground = Instantiate(Prefab[(int)Tile_Type.Wall]);
					ground.transform.position = new Vector3(x, 0, y);
                    ground.transform.parent = parent.transform;
                    break;

				case (int)Tile_Type.Wall:
				case (int)Tile_Type.Ground:
				case (int)Tile_Type.Finish:
					ground = Instantiate(Prefab[typeItem]);
                    ground.transform.position = new Vector3(x, 0, y);
                    ground.transform.parent = parent.transform;
                    break;

                case (int)Tile_Type.Begin:
                    ground = Instantiate(Prefab[(int)Tile_Type.Begin]);
                    ground.transform.position = new Vector3(x, 0, y);
                    ground.transform.parent = parent.transform;
                    current_player = ground;
                    ground = Instantiate(Prefab[(int)Tile_Type.Ground]);
                    ground.transform.position = new Vector3(x, 0, y);
                    ground.transform.parent = parent.transform;
                    break;

                case (int)Tile_Type.Spike_sac:
				case (int)Tile_Type.Spike_trap:
				case (int)Tile_Type.Bump_obj:
				case (int)Tile_Type.Bump_sac:
					ground = Instantiate(Prefab[typeItem]);
                    ground.transform.position = new Vector3(x, 0, y);
                    ground.transform.parent = parent.transform;
                    ground = Instantiate(Prefab[(int)Tile_Type.Ground]);
                    ground.transform.position = new Vector3(x, 0, y);
                    ground.transform.parent = parent.transform;
                    break;

                default:
                    break;
                }
                ++x;
            }
            ++y;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isWait)
        {
            time_score.text = "Please, change the Player and press A";
            if (Input.GetButtonDown("Validation"))
            {
                current_item = null;
                Destroy(parent.gameObject);
                Generate();
                isWait = false;
            }

        }
        else
        {
            if (current_player != null)
                Camera.transform.position = new Vector3(current_player.transform.position.x + 2f, current_player.transform.position.y + 7.5f, current_player.transform.position.z + -7.5f);
            else if (current_item != null)
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
                    index_select = index_select > 0 ? index_select - 1 : item_list.Count - 1;
                    GameObject new_item = Instantiate(Prefab[(int)item_list[index_select]]);
                    new_item.transform.position = current_item.transform.position;
                    Destroy(current_item);
                    current_item = new_item;
                }
                else if (Input.GetButtonDown("Next"))
                {
                    index_select = index_select < item_list.Count - 1 ? index_select + 1 : 0;
                    GameObject new_item = Instantiate(Prefab[(int)item_list[index_select]]);
                    new_item.transform.position = current_item.transform.position;
                    Destroy(current_item);
                    current_item = new_item;
                }
                if (Input.GetButtonDown("Validation") && checkValidation())
                {
                    current_item.transform.position = new Vector3(current_item.transform.position.x, 0f, current_item.transform.position.z);
                    map[(int)current_item.transform.position.z, (int)current_item.transform.position.x] = (int)item_list[index_select];
                    item_list.RemoveAt(index_select);
                    if (item_list.Count <= 0)
                    {
                        isBuild = false;
                        isWait = true;
                        /*current_item = null;
                        Destroy(parent.gameObject);
                        Generate();*/
                    }
                    else
                        current_item = Instantiate(Prefab[(int)item_list[0]]);
                }
            }
            timer += Time.deltaTime;
            if (!isBuild)
            {
                score -= Time.deltaTime;
                time_score.text = score.ToString("0");
                if (score <= 0)
                {
                    item_list.Clear();
                    PutMode();
                }
            }
        }
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

	public bool CheckCanEnter( Vector3 destination)
	{
		Debug.Log("Contact Bumper !");
		if (map[(int)destination.z, (int)destination.x] == (int)Tile_Type.Wall)
			return false;
		return true;
	}

    public void PutMode()
    {
		Vector3 playerPos = current_player.transform.position;
        Destroy(current_player);
        if (item_list.Count > 0)
        {

			Destroy(parent.gameObject);
			// on regenere la map pour avoir les sacs
			Generate();
			isBuild = true;
			// TODO: eviter de créer le joueur pour le redétruire
			Destroy(current_player);

			current_item = Instantiate(Prefab[(int)item_list[0]]);
			current_item.transform.position = playerPos; //new Vector3(1f, 0.5f, 1f);
			current_x = (int)playerPos.x;
			current_y = (int)playerPos.z;

		}
		else
        {
            isBuild = false;
            isWait = true;
            /*current_item = null;
            Destroy(parent.gameObject);
            Generate();*/
        }
    }


}
