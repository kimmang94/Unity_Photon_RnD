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

    /// <summary>
    /// 서버 Connect기능
    /// </summary>
    /// <returns></returns>
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// 마스터 서버 Connect기능
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }

    /// <summary>
    /// DisConnect기능
    /// </summary>
    public void DisConnect()
    {
        PhotonNetwork.Disconnect();
    }

    /// <summary>
    /// DisConnect확인
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 끊김");
    }

    /// <summary>
    /// 방만들기 기능
    /// </summary>
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 });
    }
    /// <summary>
    /// 룸에 들어가기 기능
    /// </summary>
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInput.text);
    }

    /// <summary>
    /// 룸을 만들거나 들어가는 기능
    /// </summary>
    public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    }

    /// <summary>
    /// 랜덤으로 룸에 들어가는 기능
    /// </summary>
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// 룸 떠나기기능
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    /// <summary>
    /// 정보 확인 기능
    /// </summary>
    [ContextMenu("정보")]
    private void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
                Debug.Log(playerStr);
            }
        }
        else
        {
            Debug.Log("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            Debug.Log("방 개수 : " + PhotonNetwork.CountOfRooms);
            Debug.Log("모든 방에 있는 인원 수 : " +  PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("로비에 있는지? : " + PhotonNetwork.InLobby);
            Debug.Log("연결 되었는지? : " + PhotonNetwork.IsConnected);
        }
    }
}
