using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ActivateCamera(0);
    }

    public void SelectCamera(int i)
    {
        ActivateCamera(i);
    }

    private void ActivateCamera(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);

            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
