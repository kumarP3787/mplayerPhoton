using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    public TMP_Text roomNameText;
    public TMP_Text roomPlayersText;
    public Button joinRoomButton;


    private string roomName;
    public void Start()
    {
        joinRoomButton.onClick.AddListener(() =>
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }

            PhotonNetwork.JoinRoom(roomName);
        });
    }

    public void Initialize(string name, byte currentPlayers, byte maxPlayers)
    {
        roomName = name;

        roomNameText.text = name;
        roomPlayersText.text = currentPlayers + " / " + maxPlayers;
    }

    public void JoinRoomButtonClicked()
    {
        PhotonNetwork.JoinRoom(PhotonNetwork.CurrentRoom.Name);
    }

}
