using UnityLogic;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RailBot : Agent
{
    public string kbPath = "KBs/PrologFile";
    public string kbName = "KbName";

    [SerializeField]
    GameObject exchangeArea;
    [SerializeField]
    GameObject[] landingAreas;
    [SerializeField]
    GameObject sortingBot;
    [SerializeField]
    GameObject chargingStation;

    private bool moveForward;
    private GameObject[] drones;
    private float distanceCovered;


    void Start()
    {
        Init(kbPath, kbPath);

        drones = GameObject.FindGameObjectsWithTag("Drone");
    }

    public IEnumerator Goto(GameObject go)
    {
        // check if the destination is ahead or behind the robot in order to decide if move forward or backward
        if (Vector3.Angle(transform.forward, go.transform.position - transform.position) < 90)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        if (this.gameObject.name == "RailBotN" || this.gameObject.name == "RailBotS")
        {
            // Do not exit from the coroutine until the robot is near the destination
            while (Mathf.Abs(transform.position.x - go.transform.position.x) > 0.2f)
            {
                if (moveForward)
                {
                    transform.position = transform.position + transform.forward * .3f;
                }
                else if (!moveForward)
                {
                    transform.position = transform.position - transform.forward * .3f;
                }
                yield return null;
            }
        }
        else
        {
            // Do not exit from the coroutine until the robot is near the destination
            while (Mathf.Abs(transform.position.z - go.transform.position.z) > 0.2f)
            {
                if (moveForward)
                {
                    transform.position = transform.position + transform.forward * .3f;
                }
                else if (!moveForward)
                {
                    transform.position = transform.position - transform.forward * .3f;
                }
                yield return null;
            }
        }
    }

    public void PickUp(GameObject go)
    {
        go.transform.SetParent(transform);
        go.gameObject.transform.localRotation = Quaternion.identity;
        go.gameObject.transform.localPosition = new Vector3(0, 3, 0);
    }

    public void DropDown(GameObject area)
    {
        Artifact go = GetComponentInChildren<Artifact>();
        if (go == null)
        {
            return;
        }
        go.gameObject.transform.parent = null;
        go.gameObject.transform.localScale = Vector3.one;
        go.gameObject.transform.rotation = Quaternion.identity;
        go.gameObject.transform.parent = area.transform;
        go.gameObject.transform.localPosition = Vector3.up * 2.5f;
    }

    public GameObject GetExchangeArea()
    {
        return exchangeArea;
    }

    public GameObject GetSortingBot()
    {
        return sortingBot;
    }

    public GameObject GetDrone()
    {
        return drones[Random.Range(0, drones.Length)];
    }

    public GameObject GetArea(object areaName)
    {
        GameObject landingZone;

        switch (areaName.ToString())
        {
            default:
            case "north":
                landingZone = landingAreas[0];
                break;
            case "south":
                landingZone = landingAreas[1];
                break;
            case "east":
                landingZone = landingAreas[2];
                break;
            case "west":
                landingZone = landingAreas[3];
                break;
        }

        return landingZone;
    }

    public GameObject GetChargingStation()
    {
        return chargingStation;
    }
    // print whatever you want on the console from a UnityProlog plan
    public void PrintLog(object str)
    {
        Debug.Log(str.ToString());
    }
}
