using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button ServerBtn;
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        ServerBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            disableUI();
        });

        HostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            disableUI();
        });

        ClientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            disableUI();
        });
    }

    void disableUI()
    {
        canvas.SetActive(false);
    }
}
