﻿
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Photon.Pun;
using UnityEngine.UI;
namespace Gameplay
{
    public class MainMenuBehavior : MonoBehaviour
    {
        public Transform playerVR;
        public Vector3Variable playerVRPos;
        [SerializeField] private CallableFunction _JoinRoom;
        [SerializeField] private CallableFunction _CreateRoom;
        [SerializeField] private IntVariable _sceneID;
        [SerializeField] private BoolVariable _isMobile;
        [SerializeField] private GameObject lobbyMobile;
        [SerializeField] private GameObject lobbyVR;
        [SerializeField] private Text codeVR;
        [SerializeField] private Text codeMobile;
        private int index = -1;
        

        private void OnEnable()
        {
            lobbyMobile.SetActive(false);
            lobbyVR.SetActive(false);
            index = 2;
        }

        public void SetIndex(int ID) => index = ID;
        public void JoinRoom() { _JoinRoom.Raise(codeMobile.text); }

        [Button]
        public void CreateRoom()
        {
            int roomName = Random.Range(0, 10);
            codeVR.text = roomName.ToString();
            _CreateRoom.Raise(roomName.ToString());

        }

        public void OpenScene()
        {
            if (_isMobile.Value)
            {
                SceneManager.LoadSceneAsync("GameSceneMobile", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("MainMenu");
                _sceneID.Value = index + 1;
            }
            else
            {
                SceneManager.LoadSceneAsync("GameSceneVR", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("MainMenu");
                _sceneID.Value = index;
            }
        }

        public void OpenCanvas()
        {
            if (_isMobile.Value)
            {
                lobbyMobile.SetActive(true);
            }
            else
            {
                lobbyVR.SetActive(true);

            }
        }

        

    }
}

