using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
 public class Launcher : MonoBehaviourPunCallbacks
 {
     #region Private Serializable Fields

     #endregion

     #region Private Fields

     private string gameVersion = "1";

     #endregion

     #region MonoBehaviour CallBacks

     private void Awake()
     {
         PhotonNetwork.AutomaticallySyncScene = true;
     }

     private void Start()
     {
         Connect();
     }

     #endregion

     #region Public Methods


     public void Connect()
     {
         if (PhotonNetwork.IsConnected)
         {
             PhotonNetwork.JoinRandomRoom();
         }
         else
         {
             PhotonNetwork.GameVersion = gameVersion;
             PhotonNetwork.ConnectUsingSettings();
         }
     }

     #endregion
 }
