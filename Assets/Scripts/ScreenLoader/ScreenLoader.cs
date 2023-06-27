using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScreenLoader : NetworkBehaviour
{
    static string Winner;
    int Playercount = 0;
    bool isStarted = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("t"))
        {
            isStarted = !isStarted;
        }
    }
}
