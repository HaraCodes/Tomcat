using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomModelOrientationScript : MonoBehaviour
{
    public Transform Orientation;
    void Start()
    {
        transform.rotation = Orientation.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Orientation.rotation;
    }
}
