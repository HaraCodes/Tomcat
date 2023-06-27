using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NewCamPosScript : NetworkBehaviour
{
    public Transform cameraPos;
    void Start()
    {
        if (!IsOwner) return;
        transform.position = cameraPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        transform.position = cameraPos.position;
    }
}
