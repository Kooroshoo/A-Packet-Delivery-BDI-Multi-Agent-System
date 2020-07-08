using UnityLogic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Drone : Agent
{
    public string kbPath = "KBs/PrologFile";
    public string kbName = "KbName";

    [SerializeField]
    GameObject[] landingAreasNorth;
    [SerializeField]
    GameObject[] landingAreasSouth;
    [SerializeField]
    GameObject[] landingAreasEast;
    [SerializeField]
    GameObject[] landingAreasWest;

    [SerializeField]
    GameObject railBotN;
    [SerializeField]
    GameObject railBotS;
    [SerializeField]
    GameObject railBotE;
    [SerializeField]
    GameObject railBotW;

    [SerializeField]
    GameObject chargingStation;

    [SerializeField]
    LayerMask mask;

    private NavMeshAgent nav;
    private Transform droneModel;
    private Vector3 droneModelStartingPosition;
    private Animator anim;


    void Start()
    {
        Init(kbPath, kbPath);
        nav = GetComponent<NavMeshAgent>();
        droneModel = transform.GetChild(0);
        droneModelStartingPosition = droneModel.localPosition;
        anim = GetComponent<Animator>();
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

    public IEnumerator TakeOff()
    {
        anim.SetBool("isActive", true);
        float heightGoal = 30f;
        Vector3 increase = new Vector3(0, 0.2f, 0);

        while (droneModel.localPosition.y < heightGoal)
        {
            droneModel.localPosition += increase;
            yield return null;
        }
    }

    public IEnumerator Land()
    {
        float heightGoal = droneModelStartingPosition.y + 1;

        RaycastHit hit;

        if (Physics.Raycast(droneModel.position + Vector3.up, -Vector3.up, out hit, 50.0f, mask))
        {
            Artifact go = hit.transform.gameObject.GetComponent<Artifact>();
            if (go == null)
            {
                heightGoal = 1;
            }
            else
            {
                heightGoal = 5;
            }
        }

        Vector3 decrease = new Vector3(0, 0.2f, 0);
        float rotSpeed = 2f;

        while (droneModel.localPosition.y > heightGoal)
        {
            droneModel.localPosition -= decrease;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotSpeed);
            yield return null;
        }

        anim.SetBool("isActive", false);
    }

    public void PickUp(GameObject go)
    {
        go.transform.SetParent(droneModel);
        go.gameObject.transform.localRotation = Quaternion.identity;
        go.gameObject.transform.localPosition = new Vector3(0, -3, 0);
    }

    public void DropDown()
    {
        Artifact go = GetComponentInChildren<Artifact>();
        if (go == null)
        {
            return;
        }
        go.gameObject.transform.parent = null;
        go.gameObject.transform.localScale = Vector3.one;
        go.gameObject.transform.rotation = Quaternion.identity;
    }

    public GameObject GetLandingZone(object startingArea, object destinationArea)
    {
        GameObject landingZone;

        switch (startingArea.ToString())
        {
            default:
            case "north":
                switch (destinationArea.ToString())
                {
                    default:
                    case "north":
                        landingZone = landingAreasNorth[0];
                        break;
                    case "south":
                        landingZone = landingAreasNorth[1];
                        break;
                    case "east":
                        landingZone = landingAreasNorth[2];
                        break;
                    case "west":
                        landingZone = landingAreasNorth[3];
                        break;
                }
                break;
            case "south":
                switch (destinationArea.ToString())
                {
                    default:
                    case "north":
                        landingZone = landingAreasSouth[0];
                        break;
                    case "south":
                        landingZone = landingAreasSouth[1];
                        break;
                    case "east":
                        landingZone = landingAreasSouth[2];
                        break;
                    case "west":
                        landingZone = landingAreasSouth[3];
                        break;
                }
                break;
            case "east":
                switch (destinationArea.ToString())
                {
                    default:
                    case "north":
                        landingZone = landingAreasEast[0];
                        break;
                    case "south":
                        landingZone = landingAreasEast[1];
                        break;
                    case "east":
                        landingZone = landingAreasEast[2];
                        break;
                    case "west":
                        landingZone = landingAreasEast[3];
                        break;
                }
                break;
            case "west":
                switch (destinationArea.ToString())
                {
                    default:
                    case "north":
                        landingZone = landingAreasWest[0];
                        break;
                    case "south":
                        landingZone = landingAreasWest[1];
                        break;
                    case "east":
                        landingZone = landingAreasWest[2];
                        break;
                    case "west":
                        landingZone = landingAreasWest[3];
                        break;
                }
                break;
        }

        return landingZone;
    }

    public GameObject GetRailBot(object area)
    {
        GameObject railBot;

        switch (area.ToString())
        {
            default:
            case "north":
                railBot = railBotN;
                break;
            case "south":
                railBot = railBotS;
                break;
            case "east":
                railBot = railBotE;
                break;
            case "west":
                railBot = railBotW;
                break;
        }

        return railBot;
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
