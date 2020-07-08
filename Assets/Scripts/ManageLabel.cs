using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageLabel : MonoBehaviour
{

    [SerializeField] LayerMask mask;
	Transform detectionCenter;

    int numOfBoxes = 0;
    TextMeshPro text;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
		detectionCenter = transform.GetChild(0);
    }

    void Update()
    {

        Collider[] check = Physics.OverlapBox(detectionCenter.position, Vector3.one * 2.5f, Quaternion.identity, mask);

        int boxCount = 0;

        foreach (Collider coll in check)
        {
            if (coll.gameObject.GetComponent<Box>())
                boxCount++;
        }

        numOfBoxes = boxCount;

        text.text = "" + numOfBoxes;
    }

}
