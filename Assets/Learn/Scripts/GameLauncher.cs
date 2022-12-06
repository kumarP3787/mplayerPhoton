using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLauncher : MonoBehaviourPunCallbacks
{
    #region Variables
    [Header("Stats")]
    [SerializeField] TMP_Text ping;
    [SerializeField] TMP_Text connectionStatus;
    [SerializeField] TMP_Text roomName;

    [Header("Login Panel")]
    [SerializeField] GameObject loginPanel;
    [SerializeField] TMP_InputField playerNameInput;

    [Header("Selection Panel")]
    [SerializeField] GameObject selectionPanel;

    [Header("Create Room Panel")]
    [SerializeField] GameObject createRoomPanel;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField maxPlayerInputField;

    [Header("Room List Panel")]
    [SerializeField] GameObject roomListPanel;
    [SerializeField] GameObject roomListEntryPrefab;
    [SerializeField] Transform roomcontentList;

    [Header("Player List Panel")]
    [SerializeField] GameObject playerListPanel;
    [SerializeField] GameObject playerListEntryPrefab;
    [SerializeField] Transform playercontentList;
    [SerializeField] Button startGameButton;

    [Header("Error Panel")]
    [SerializeField] GameObject errorPanel;
    [SerializeField] TMP_Text errorText;

    string playerName;

    #endregion

    #region UNITY
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        playerNameInput.text = "Player" + Random.Range(1000, 10000);
    }
    void Update()
    {
        ping.text = "Ping: " + PhotonNetwork.GetPing() + "ms";
        connectionStatus.text = "Connection Status: " + PhotonNetwork.NetworkClientState;

    }
    #endregion


    // ---------------------------------------------------------- //


    #region PUN CALLBACKS
    public override void OnConnectedToMaster()
    {
        SetActivePanel(selectionPanel.name);
        PhotonNetwork.JoinLobby();

    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby joined");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join room "+PhotonNetwork.InLobby);

        roomName.text = PhotonNetwork.CurrentRoom.Name;
        SetActivePanel(playerListPanel.name);
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(playerListEntryPrefab, playercontentList);
            entry.GetComponent<PlayerList>().Initialize(p.ActorNumber, p.NickName);
        }
    }

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        foreach(var x in  lobbyStatistics)
        {
            Debug.Log(x.RoomCount);
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        Debug.Log("<b>" + roomList.Count + " : this is room count</b>");
        foreach (var x in roomList)
        {
            Debug.Log(x.Name + "This is room name");
        }
       /*foreach (Transform trans in roomcontentList)
        {
            Destroy(trans.gameObject);
        }*/

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;

            GameObject entry = Instantiate(roomListEntryPrefab, roomcontentList);
            entry.GetComponent<RoomListItem>().Initialize(roomList[i].Name, (byte)roomList[i].PlayerCount, roomList[i].MaxPlayers);
        }


       /* foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
                continue;

            GameObject entry = Instantiate(roomListEntryPrefab, roomcontentList);
            entry.GetComponent<RoomListItem>().Initialize(info.Name, (byte)info.PlayerCount, info.MaxPlayers);
        }*/
        
    }

    public override void OnLeftRoom()
    {
        SetActivePanel(selectionPanel.name);

        foreach (Transform entry in playercontentList)
        {
            Destroy(entry.gameObject);
        }
    }

    public override void OnLeftLobby()
    {
        Debug.Log("<b>player left the lobby</b>");
    }


    #endregion

    // ---------------------------------------------------------- //

    #region UI CALLBACKS


    public void OnLeaveButtonClicked()
    {
        SetActivePanel(selectionPanel.name);
        PhotonNetwork.LeaveRoom();
    }



    public void OnCreateRoomButtonClicked()
    {
        string roomName = roomNameInputField.text;
        roomName = (roomName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : roomName;

        byte maxPlayers;
        byte.TryParse(maxPlayerInputField.text, out maxPlayers);
        maxPlayers = (byte)Mathf.Clamp(maxPlayers, 2, 8);

        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers, PlayerTtl = 10000 };
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public void OnJoinRoomButtonClicked()
    {
      //  PhotonNetwork.JoinRoom();
    }

    public void OnLoginButtonClicked()
    {

        playerName = playerNameInput.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            SetActivePanel(errorPanel.name);
        }
    }

    public void OnCreateRoomClicked()
    {
        SetActivePanel(createRoomPanel.name);
    }

    public void OnRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }

        SetActivePanel(roomListPanel.name);

    }


    public void SetActivePanel(string activePanel)
    {
        loginPanel.SetActive(activePanel.Equals(loginPanel.name));
        selectionPanel.SetActive(activePanel.Equals(selectionPanel.name));
        createRoomPanel.SetActive(activePanel.Equals(createRoomPanel.name));
        roomListPanel.SetActive(activePanel.Equals(roomListPanel.name));
        playerListPanel.SetActive(activePanel.Equals(playerListPanel.name));
        errorPanel.SetActive(activePanel.Equals(errorPanel.name));

    }


    public void OnBackButtonClicked()
    {
        SetActivePanel(selectionPanel.name);
    }

    public void OnOkButtonClicked()
    {
        SetActivePanel(loginPanel.name);
    }


    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();
        SetActivePanel(loginPanel.name);
    }

    #endregion
}
