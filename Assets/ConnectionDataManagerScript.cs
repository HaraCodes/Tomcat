using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionDataManagerScript : UnityTransport
{
    [SerializeField] private Button Btn;
    [SerializeField] private TMP_InputField Address;
    [SerializeField] private TMP_InputField Port;
    [SerializeField] private TMP_InputField ServerAddress;
    [SerializeField] private GameObject manager;
    /*[SerializeField] private Text Port;
    [SerializeField] private Text ServerListenAddress;*/

    private void Awake()
    {
        Btn.onClick.AddListener(() =>
        {
            ConnectionData.Address = Address.text;
            ConnectionData.ServerListenAddress = ServerAddress.text;
            ConnectionData.Port = System.Convert.ToUInt16(int.Parse(Port.text));
            manager.SetActive(false);
        });

       
    }
}
