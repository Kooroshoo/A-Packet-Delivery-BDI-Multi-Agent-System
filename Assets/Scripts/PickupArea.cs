using UnityLogic;
using UnityEngine;

public class PickupArea : Agent
{

    public string kbPath = "KBs/PrologFile";
    public string kbName = "KbName";

    private GameObject[] drones;

    enum Area { NORTH, SOUTH, EAST, WEST }

    [SerializeField] Area area;

    void Start()
    {
        Init(kbPath, kbPath);

        drones = GameObject.FindGameObjectsWithTag("Drone");

        // for example: "area(south)"
        string areabelief = "area(" + area.ToString().ToLower() + ")";

        // adds it to the KB of the pickupArea
        AddBelief(areabelief);
    }

    public GameObject GetDrone()
    {
        return drones[Random.Range(0, drones.Length)];
    }

    public void Destroy(GameObject g)
    {
        if (g.GetComponent<Box>().destination == this.gameObject)
        {
            Debug.Log(g.name + " has been delivered correctly!");
            DestroyImmediate(g);
        }
        else
        {
            Debug.LogError("A problem has occurred with " + g.name + ". It has been delivered to the wrong destination.");
        }
    }

    // print whatever you want on the console from a UnityProlog plan
    public void PrintLog(object str)
    {
        Debug.Log(str.ToString());
    }
}