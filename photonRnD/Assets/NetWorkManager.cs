using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI statusText = null;
    [SerializeField] private TMP_InputField roomInput = null;
    [SerializeField] private TMP_InputField NickNameInput = null;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        
    }

    private void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }

    public void DisConnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 끊김");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInput.text);
    }

    public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
