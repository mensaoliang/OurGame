using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollSceneScript : MonoBehaviour {
    public GameObject environment;  //collection of all kinds of ground
    public GameObject coin;
    public GameObject power;
    public GameObject bomb;
	public Text diamondCount;
    private Transform trans;
	private int diamond; 
    private Transform scrollerTrans;
    private GameObject[] grounds;
    private LinkedList<GameObject> CurrentRoads = new LinkedList<GameObject>();
    private LinkedList<GameObject> CurrentCoins = new LinkedList<GameObject>();
	// Use this for initialization
	int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}
	void Start () {
        scrollerTrans = GetComponent<Transform>();
        //initialize the grounds collection
        trans = environment.GetComponent<Transform>();
        grounds = new GameObject[trans.childCount];
        //loop through the collection and create the individual ground
        for (int i = 0; i < trans.childCount; i++)
            grounds[i] = trans.GetChild(i).gameObject;
        CreateNewRoad();
	}
	void CreateNewRoad()
    {
        if (CurrentRoads.Count == 0)//empty
        {
            int randInd = Random.Range(0, trans.childCount);//random generate road type
            CurrentRoads.AddLast(Instantiate(grounds[randInd], new Vector3(26.4f, -4f, 0f),
                                        Quaternion.identity) as GameObject);//generate a road
            CreateNewCoins(CurrentRoads.Last.Value);//generate coins on this road
            randInd = Random.Range(0, trans.childCount);
            CurrentRoads.AddLast(Instantiate(grounds[randInd], new Vector3(53.6f, -4f, 0f),
                                        Quaternion.identity) as GameObject);
            CreateNewCoins(CurrentRoads.Last.Value);
            randInd = Random.Range(0, trans.childCount);
            CurrentRoads.AddLast(Instantiate(grounds[randInd], new Vector3(80.8f, -4f, 0f),
                                        Quaternion.identity) as GameObject);
            CreateNewCoins(CurrentRoads.Last.Value);
            randInd = Random.Range(0, trans.childCount);
            CurrentRoads.AddLast(Instantiate(grounds[randInd], new Vector3(108f, -4f, 0f),
                                        Quaternion.identity) as GameObject);
            CreateNewCoins(CurrentRoads.Last.Value);
        }
        else
        {
            int randInd = Random.Range(0, trans.childCount);
            Vector3 lastRoad = CurrentRoads.Last.Value.transform.position;
            CurrentRoads.AddLast(Instantiate(grounds[randInd], new Vector3(lastRoad.x + 27.2f, lastRoad.y, lastRoad.z),
                                        Quaternion.identity) as GameObject);
            CreateNewCoins(CurrentRoads.Last.Value);
            DestroyCoins(CurrentRoads.First.Value);
            Destroy(CurrentRoads.First.Value);
            CurrentRoads.RemoveFirst();
        }
    }
    void CreateNewCoins(GameObject road)//create coins on this road
    {
        string roadName = road.name;
        int centerX = Mathf.RoundToInt(road.transform.position.x);
        float baseY = 100f;//the correct lowest y value of coins on this road
        if (roadName.StartsWith("PlainG"))//plainGround
            baseY = -3.25f;
        else if (roadName.StartsWith("PlainH") || roadName.StartsWith("VineGround"))//plainHighGround or VineGround
            baseY = -1.25f;
        for (int st = centerX - 10; st < centerX + 12; st++)
        {
            int genChance = Random.Range(0, 2);//50% chance generate coins on a certain position
            if (genChance == 0) continue;//dont generate coins here
            int genNum = Random.Range(0, 3);//how many coins will generated here, 3 at most, 1 at least
            for (float i = baseY; i < baseY + genNum + 0.1; i += 1)
            {
                int kind = Random.Range(0, 101); // 5% bombs, 5% powers, 90% coins
                if (kind < 5)//bombs
                    CurrentCoins.AddLast(Instantiate(bomb, new Vector3((float)st, i, 0),
                                            Quaternion.identity) as GameObject);
                else if (kind < 11)//powers
                    CurrentCoins.AddLast(Instantiate(power, new Vector3((float)st, i, 0),
                                            Quaternion.identity) as GameObject);
                else
                    CurrentCoins.AddLast(Instantiate(coin, new Vector3((float)st, i, 0),
                                            Quaternion.identity) as GameObject);
            }
        }
    }
    void DestroyCoins(GameObject road)//destroy all coins on road
    {
        float endX = road.transform.position.x + 14f;
        while (CurrentCoins.Count > 0 && CurrentCoins.First.Value.transform.position.x < endX)
        {
            Destroy(CurrentCoins.First.Value);
            CurrentCoins.RemoveFirst();
        }
    }
	// Update is called once per frame
	void Update () {
        if (CurrentRoads.Count > 0 && scrollerTrans.position.x > CurrentRoads.First.Value.transform.position.x)
            CreateNewRoad();
        if (Input.GetKeyDown(KeyCode.B))
        {
			diamond = IntParseFast (diamondCount.text);
			if (diamond >= 1) 
			{
				for (LinkedListNode<GameObject> it = CurrentCoins.First; it != null;)
				{
					if (it.Value.tag == "bomb")
					{
						DestroyImmediate(it.Value);
						LinkedListNode<GameObject> itnext = it.Next;
						CurrentCoins.Remove(it);
						it = itnext;
					}
					else it = it.Next;
				}
				diamond = diamond - 1;
				diamondCount.text = diamond.ToString ();
			}
            //int count = 0;
        }
	}
}
