﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollSceneScript : MonoBehaviour {
    public GameObject environment;  //collection of all kinds of ground
    public GameObject coin;
    public GameObject power;
    public GameObject bomb;
    public GameObject player;   //To check if a bomb is in front of the player and in the screen, we need to know the position of player
    public GameObject explosion;    //explosion effect displayed when player shake the phone to remove the bomb
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
    public void IncDiamondCount()
    {
        diamond++;
        diamondCount.text = diamond.ToString();
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
        diamond = 0;
        diamondCount.text = diamond.ToString();
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
        else if (roadName.StartsWith("PlainH") || roadName.StartsWith("VineGround") || roadName.StartsWith("PlainS"))//plainHighGround or VineGround or PlainStep
            baseY = -1.25f;
        for (int st = centerX - 10; st < centerX + 12; st++)
        {
            if (roadName.StartsWith("PlainSt"))//the following two scenes have different height grounds
            {
                if (st > centerX - 3f) baseY = 1.25f;
                else if (st > centerX + 4f) baseY = 2.75f;
            }
            else if (roadName.StartsWith("PlainSH"))
            {
                if (st > centerX - 3f) baseY = 2.75f;
                else if (st > centerX + 4f) baseY = -1.25f;
            }
            int genChance = Random.Range(0, 2);//50% chance generate coins on a certain position
            if (genChance == 0) continue;//dont generate coins here
            int genNum = Random.Range(0, 3);//how many coins will generated here, 3 at most, 1 at least
            for (float i = baseY; i < baseY + genNum + 0.1 && i < 3f; i += 1)
            {
                int kind = Random.Range(0, 101); // 4% bombs, 3% powers, 93% coins
                if (kind < 4)//bombs
                    CurrentCoins.AddLast(Instantiate(bomb, new Vector3((float)st, i, 0),
                                            Quaternion.identity) as GameObject);
                else if (kind < 7)//powers
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
			if (diamond >= 1) 
			{
                bool suc = false;
                for (LinkedListNode<GameObject> it = CurrentCoins.First; it != null;)
				{
                    if (it.Value.transform.position.x > player.transform.position.x + 13f)
                        break;  //if the rest items are all out of screen, just stop the for loop
                    if (it.Value.tag == "bomb")  //only destroy bombs within the screen
					{
                        GameObject expEffect = Instantiate(explosion, it.Value.transform.position, Quaternion.identity) as GameObject;
                        Destroy(expEffect, 1);  //display the explosion effect at the same positon for 1s
                        DestroyImmediate(it.Value);
						LinkedListNode<GameObject> itnext = it.Next;
						CurrentCoins.Remove(it);
						it = itnext;
                        suc = true; //successfully used the ability
					}
					else it = it.Next;
				}
                if (suc)
                {
                    diamond--;
                    diamondCount.text = diamond.ToString();
                }
                else print("No bomb!!!!!!!!!!!!!!!!1");
			}
            //int count = 0;
            else print("no diamond!:: " + diamond.ToString());
        }
	}
}
