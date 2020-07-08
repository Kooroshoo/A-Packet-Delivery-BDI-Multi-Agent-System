using UnityLogic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections.Generic;

public class SortingBot : Agent
{
    public string kbPath = "KBs/PrologFile";
    public string kbName = "KbName";

    [SerializeField]
    GameObject[] exchangeAreas;
    [SerializeField]
    GameObject[] exchangeBots;

    private NavMeshAgent nav;
    
    [SerializeField]
    GameObject chargingStation;


    void Start()
    {
        Init(kbPath, kbPath);
        nav = GetComponent<NavMeshAgent>();
    }

    public IEnumerator Goto(GameObject go)
    {
        nav.enabled = true;
        nav.isStopped = false;

        nav.SetDestination(go.transform.position);

        while (!nav.enabled || nav.pathPending)
        {
            yield return null;
        }

        while (!nav.enabled || nav.remainingDistance > 1.2f)
        {
            yield return null;
        }

        nav.isStopped = true;
        nav.enabled = false;
    }

    public void PickUp(GameObject go)
    {
        go.transform.SetParent(transform);
        go.gameObject.transform.localPosition = new Vector3(0, 3, 0);
        go.gameObject.transform.localRotation = Quaternion.identity;
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

    public GameObject GetExchangeArea(object areaName)
    {
        GameObject area;

        switch (areaName.ToString())
        {
            default:
            case "north":
            area = exchangeAreas[0];
            break;
            case "south":
            area = exchangeAreas[1];
            break;
            case "east":
            area = exchangeAreas[2];
            break;
            case "west":
            area = exchangeAreas[3];
            break;
        }

        return area;
    }

    public GameObject GetRailBot(object areaName)
    {
        GameObject bot;

        switch (areaName.ToString())
        {
            default:
            case "north":
            bot = exchangeBots[0];
            break;
            case "south":
            bot = exchangeBots[1];
            break;
            case "east":
            bot = exchangeBots[2];
            break;
            case "west":
            bot = exchangeBots[3];
            break;
        }

        return bot;
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
