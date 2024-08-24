using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.SceneManagement;

public class ConnetToServer : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.");
        PhotonNetwork.JoinLobby();
    }
    
    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList().");
        SceneManager.LoadScene("Lobby");
    }
}
