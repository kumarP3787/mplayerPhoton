using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
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



    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        Helper.networkEvents += InLobby;
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        Helper.networkEvents -= InLobby;
    }


    private void InLobby(NetworkEventNames eventName)
    {
        if (eventName == NetworkEventNames.LobbyJoined)
        {
            SetActivePanel(selectionPanel.name);
        }
    }

    public void OnCreateRoomClicked()
    {
        SetActivePanel(createRoomPanel.name);
    }


    public void OnCreateRoomButtonClicked()
    {
        string roomName = roomNameInputField.text;
        roomName = (roomName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : roomName;
        byte maxPlayers;
        byte.TryParse(maxPlayerInputField.text, out maxPlayers);
        maxPlayers = (byte)Mathf.Clamp(maxPlayers, 2, 8);
        Helper.RoomCreateRequest?.Invoke(roomName, maxPlayers);
    }


    public void OnRoomListButtonClicked()
    {
       
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

}
