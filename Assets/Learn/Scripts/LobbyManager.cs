using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LobbyManager : MonoBehaviour, ILobbyCallbacks
{

    private TypedLobby sqlLobby = new TypedLobby("RandomSqlLobby", LobbyType.SqlLobby);

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        Helper.networkEvents += OnMaster;
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        Helper.networkEvents -= OnMaster;
    }

    /// <summary>
    /// After Connect To Master Join Lobby. 
    /// </summary>
    /// <param name="eventName"></param>
    private void OnMaster(NetworkEventNames eventName)
    {
        if(eventName == NetworkEventNames.OnMaster)
        {
            Debug.Log("Masater Connected");
           // PhotonNetwork.JoinLobby(); 
            PhotonNetwork.JoinLobby(sqlLobby);
        }
    }



    public void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.CurrentLobby.Name);
        Helper.networkEvents?.Invoke(NetworkEventNames.LobbyJoined);
    }

    public void OnLeftLobby()
    {
     
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log(PhotonNetwork);
        foreach (var r in lobbyStatistics)
        {
            Debug.Log(r.RoomCount);
        }
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        Debug.Log(PhotonNetwork.CountOfRooms);
        foreach (var r in roomList) 
        {
            Debug.Log(r.Name);
        }
    }

   
}
