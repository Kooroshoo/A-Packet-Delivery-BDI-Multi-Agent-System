using System.Collections;
using UnityLogic;
using UnityEngine;

public class GameManager : Agent
{
    [SerializeField]
    GameObject pfBox;

    [SerializeField]
    GameObject[] pickupAreas;

    private int number = 0;

    public string kbPath = "KBs/PrologFile";
    public string kbName = "KbName";

    void Start()
    {
        Init(kbPath, kbPath);
    }

    public GameObject SpawnBox(GameObject startPickupArea, GameObject destPickupArea)
    {
        GameObject boxObject = Instantiate(pfBox, startPickupArea.transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
        boxObject.name = "Box " + ++number;

        Box box = boxObject.GetComponent<Box>();
        box.start = startPickupArea;
        box.destination = destPickupArea;

        Debug.Log("Created " + boxObject.name + ". Start is " + startPickupArea.transform.parent.name + ". Destination is " + destPickupArea.transform.parent.name + ".");
        return boxObject;
    }

    public GameObject GetArea(int index)
    {
        return pickupAreas[index];
    }

    public GameObject GetArea()
    {
        return pickupAreas[Random.Range(0, pickupAreas.Length)];
    }

    public IEnumerator WaitForSeconds(float seconds)
    {
        float time = 0;

        while (time < seconds)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    // print whatever you want on the console from a UnityProlog plan
    public void PrintLog(object str)
    {
        Debug.Log(str.ToString());
    }
}
