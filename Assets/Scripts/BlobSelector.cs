using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BlobSelector : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    public GameObject selectedBlob;

    private static BlobSelector _instance;
    public static BlobSelector instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject[] allBlobs;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        selectedBlob = GameObject.FindGameObjectWithTag("1");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedBlob = GameObject.FindGameObjectWithTag("1");
        if (Input.GetKeyDown(KeyCode.Alpha2))
            selectedBlob = GameObject.FindGameObjectWithTag("2");
        if (Input.GetKeyDown(KeyCode.Alpha3))
            selectedBlob = GameObject.FindGameObjectWithTag("3");

        foreach (var blob in allBlobs)
        {
            if(blob != selectedBlob)
            {
                blob.GetComponent<PlayerController>().enabled = false;
            }
            else
            {
                blob.GetComponent<PlayerController>().enabled = true;
            }            
        }

        virtualCamera.m_Follow = selectedBlob.transform;
        //selectedBlob.GetComponent<PlayerController>().focused = virtualCamera.m_Follow = selectedBlob.transform;
    }
}
