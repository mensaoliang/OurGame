using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject jumpCloud;
    public GameObject binaryStar;
    public GameObject JumplingCloudBeginnerGuidePanel;
    public GameObject StarsBeginnerGuidePanel;

    private static GameObject itemBeingDragged;
    private Vector3 startPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 tem = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition = new Vector3(tem.x, tem.y, 0f);
        if (gameObject.CompareTag("jumpCloudButton"))    //generate a jump cloud
        {   
            ////////////////////new added codes by Zheng////////////////////
            if (PlayerPrefs.GetInt("FirstUseCloud") == 1)
            {
                JumplingCloudBeginnerGuidePanel.SetActive(true);
                PlayerPrefs.SetInt("FirstUseCloud", 0);
            }
            else
            {
                JumplingCloudBeginnerGuidePanel.SetActive(false);
                itemBeingDragged = Instantiate(jumpCloud, startPosition,
                                        Quaternion.identity) as GameObject;
            }
            ////////////////////////////////////////////////////////////////
        }
        else if (gameObject.CompareTag("binaryStarButton")) //generate a binary star
        {
            ////////////////////new added codes by Zheng////////////////////
            if (PlayerPrefs.GetInt("FirstUseStars") == 1)
            {
                StarsBeginnerGuidePanel.SetActive(true);
                PlayerPrefs.SetInt("FirstUseStars", 0);
            }
            else
            {
                StarsBeginnerGuidePanel.SetActive(false);
                itemBeingDragged = Instantiate(binaryStar, startPosition,
                                        Quaternion.identity) as GameObject;
                itemBeingDragged.GetComponent<LineRenderer>().SetWidth(0f, 0f);
                itemBeingDragged.GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
            }
            ////////////////////////////////////////////////////////////////
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemBeingDragged == null) return;   //if this is the first time to use an item, then we dont drag this item
        Vector3 tem = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        itemBeingDragged.transform.position = new Vector3(tem.x, tem.y, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemBeingDragged == null) return;   //if this is the first time to use an item, then we dont drag this item
        if (Vector3.Distance(itemBeingDragged.transform.position,
                    startPosition) < 1)//too near
        {
            Destroy(itemBeingDragged);
        }
        if (itemBeingDragged.tag == "binaryStar")       //show the line renderer and the star com
        {
            itemBeingDragged.GetComponent<LineRenderer>().SetWidth(0.8f, 0.4f);
            itemBeingDragged.GetComponent<Transform>().GetChild(1).gameObject.SetActive(true);
        }
        Destroy(itemBeingDragged, 10f);
        itemBeingDragged = null; 

    }
}
