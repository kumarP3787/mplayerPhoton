using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /// <summary>
    /// 
    /// </summary>
using PHT = ExitGames.Client.Photon.Hashtable;
public class RoomManager : MonoBehaviour, IInRoomCallbacks,IMatchmakingCallbacks
{
    public const string Key_1 = "C0";
    public const string Key_2 = "C1";
    private TypedLobby sqlLobby = new TypedLobby("RandomSqlLobby", LobbyType.SqlLobby);

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        Helper.RoomCreateRequest += CreateNewRoom;
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        Helper.RoomCreateRequest -= CreateNewRoom;

    }
    public void CreateNewRoom(string _roomName, byte _maxPlayer)
    {
        //RoomOptions options = new RoomOptions { MaxPlayers = _maxPlayer, PlayerTtl = 10000 };
        //PhotonNetwork.CreateRoom(_roomName, options, null);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = _maxPlayer;
        roomOptions.CustomRoomProperties = new PHT { { Key_1, 400 }, { Key_2, "Map3" } };
        roomOptions.CustomRoomPropertiesForLobby = new string[] { Key_1, Key_2 };
        //roomOptions.CustomRoomPropertiesForLobby = { ELO_PROP_KEY, MAP_PROP_KEY }; // makes "C0" and "C3" available in the lobby
        PhotonNetwork.CreateRoom(_roomName, roomOptions, sqlLobby);
    }



    public void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        Debug.Log("RoomCreated");

    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room create Failed" + message);
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
    

    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
      
    }

    public void OnLeftRoom()
    {
       
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
        
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
       
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
      
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        
    }



}
