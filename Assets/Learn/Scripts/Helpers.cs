using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;



public static class Helper
{
    public static Action<NetworkEventNames> networkEvents;
    public static Action<string,byte> RoomCreateRequest;
}


public enum NetworkEventNames
{
    None = 0,
    OnMaster = 6,
    LobbyJoined = 12,
    LobbyLeft = 18,
    RoomJoined = 24,
    RoomLeft = 30,

}



