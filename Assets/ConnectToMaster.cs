using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToMaster :MonoBehaviour, IConnectionCallbacks
{

  
    public  void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public  void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    public void OnConnected()
    {
        Debug.Log("Connected"); 
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master Server");
        Helper.networkEvents?.Invoke(NetworkEventNames.OnMaster);
      
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
       
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
   
    }

    public void OnDisconnected(DisconnectCause cause)
    {
       
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
       
    }



    #region UNITY


    public void Connection()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }
    #endregion

}
