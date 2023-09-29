using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
public class Launcher : MonoBehaviourPunCallbacks
{
   
   #region [Private Serializable Fields]
   [Tooltip("룸당 최대 플레이어 수입니다. 방이 가득 차면 새로운 플레이어가 참여할 수 없으므로 새 방이 생성됩니다.")]
   [SerializeField]
   private byte maxPlayersPerRoom = 4;
   #endregion

   #region [Private Fields]

   /// <summary>
   /// 이 클라이언트의 버전 번호입니다. 사용자는 gameVersion으로 서로 구분됩니다(이를 통해 획기적인 변경이 가능함).
   /// </summary>
   private string gameVersion = "1";

   #endregion

   #region MonoBehaviour CallBacks

   private void Awake()
   {
      //PhotonNetwork.LoadLevel()을 사용할 수 있고 같은 방에 있는 모든 클라이언트가 자동으로 레벨을 동기화할 수 있도록 해줍니다.
      PhotonNetwork.AutomaticallySyncScene = true;
   }

   private void Start()
   {
      //초기화 단계에서 Unity가 GameObject에 대해 호출한 MonoBehaviour 메서드입니다.
      Connect();
   }

   #endregion

   #region [Public Methods]
   /// <summary>
   /// 연결 프로세스를 시작합니다.
   /// - 이미 연결되어 있는 경우 임의의 방에 참여하려고 시도합니다.
   /// - 아직 연결되지 않은 경우 이 애플리케이션 인스턴스를 Photon 클라우드 네트워크에 연결합니다.
   /// </summary>
   public void Connect()
   {
      //우리는 연결되어 있는지 확인하고, 연결되어 있으면 참여하고, 그렇지 않으면 서버에 대한 연결을 시작합니다.
      if (PhotonNetwork.IsConnected)
      {
         //이 시점에서 Random Room에 참여해 보아야 합니다. 실패하면 OnJoinRandomFailed()에서 알림을 받고 이를 생성합니다.
         PhotonNetwork.JoinRandomRoom();
      }
      else
      {
         // 가장 먼저 Photon 온라인 서버에 연결해야 합니다
         PhotonNetwork.ConnectUsingSettings();
         PhotonNetwork.GameVersion = gameVersion;
      }
   }
   

   #endregion
   
   #region MonoBehaviourPunCallbacks Callbacks

   public override void OnConnectedToMaster()
   {
      PhotonNetwork.JoinRandomRoom();
      Debug.Log("PUN 기본 튜토리얼/런처: OnConnectedToMaster()가 PUN에 의해 호출되었습니다.");
   }

   public override void OnDisconnected(DisconnectCause cause)
   {
      Debug.LogWarningFormat("PUN 기초 튜토리얼/런처: OnDisconnected()가 PUN에 의해 호출된 이유 {0}", cause);
   }

   public override void OnJoinRandomFailed(short returnCode, string message)
   {
      Debug.Log("PUN 기본 튜토리얼/Launcher:OnJoinRandomFailed()가 PUN에 의해 호출되었습니다. 사용할 수 있는 임의의 공간이 없으므로 하나를 만듭니다.\n호출: PhotonNetwork.CreateRoom");

      // 무작위 방에 참여하지 못했습니다. 방이 없거나 모두 꽉 찼을 수 있습니다. 걱정하지 마세요. 새 방을 만들겠습니다..
      PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
   }

   public override void OnJoinedRoom()
   {
      Debug.Log("\nPUN 기본 튜토리얼/런처: PUN에 의해 호출되는 OnJoinedRoom(). 이제 이 고객은 방에 있습니다.");
   }
   #endregion
}
