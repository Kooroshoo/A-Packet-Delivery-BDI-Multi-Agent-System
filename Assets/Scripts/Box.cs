using UnityLogic;
using UnityEngine;

public class Box : Artifact
{
    public string kbPath = "KBs/prologfile";
    public string kbName = "kbName";

    public GameObject start, destination;

    void Awake()
    {
        Init(kbPath, kbName);
    }
}
