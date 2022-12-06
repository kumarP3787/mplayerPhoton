using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.Demo.Asteroids;
using Photon.Pun.UtilityScripts;
using System.Collections;

public class PlayerList : MonoBehaviourPunCallbacks
{

    [Header("UI References")]
    public TMP_Text PlayerNameText;
    public Image PlayerColorImage;
    public Button PlayerReadyButton;
    public Image PlayerReadyImage;
    private int ownerId;
    private object isPlayerReady;

    public void Initialize(int playerId, string playerName)
    {      
        PlayerNameText.text = "000" + playerId + " / " + playerName;
    }

   
    public void SetPlayerReady(bool playerReady)
    {
        PlayerReadyButton.GetComponentInChildren<TMP_Text>().text = playerReady ? "Ready!" : "Ready?";
        PlayerReadyImage.enabled = playerReady;
    }

}

