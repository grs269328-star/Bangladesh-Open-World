using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;

/// <summary>বাংলা ভাষার অস্থায়ী Host/Client পরীক্ষার প্যানেল।</summary>
public class NetworkBootstrapUI : MonoBehaviour
{
    private string address = "127.0.0.1";
    private ushort port = 7777;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(12, 12, 270, 245), GUI.skin.box);
        GUILayout.Label("বাংলাদেশ ওপেন ওয়ার্ল্ড");
        GUILayout.Label("সার্ভারের ঠিকানা:");
        address = GUILayout.TextField(address);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("হোস্ট শুরু")) StartHost();
        if (GUILayout.Button("যুক্ত হও")) StartClient();
        GUILayout.EndHorizontal();
        if (GUILayout.Button("সার্ভার শুরু")) NetworkManager.Singleton.StartServer();
        if (GUILayout.Button("সংযোগ বন্ধ")) NetworkManager.Singleton.Shutdown();
        GUILayout.Label("পোর্ট: " + port);
        GUILayout.EndArea();
    }

    private void ConfigureTransport()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport != null)
            transport.SetConnectionData(address, port, "0.0.0.0");
    }

    private void StartHost()
    {
        ConfigureTransport();
        NetworkManager.Singleton.StartHost();
    }

    private void StartClient()
    {
        ConfigureTransport();
        NetworkManager.Singleton.StartClient();
    }
}
