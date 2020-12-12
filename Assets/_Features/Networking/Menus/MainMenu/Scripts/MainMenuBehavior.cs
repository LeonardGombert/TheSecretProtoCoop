﻿
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Gameplay
{
    public class MainMenuBehavior : MonoBehaviour
    {
        public Transform playerVR;
        public Vector3Variable playerVRPos;
        [SerializeField] CallableFunction _JoinRoom;
        [SerializeField] CallableFunction _CreateRoom;
        [SerializeField] IntVariable _sceneID;
        public VisualTreeAsset visualTree;
        public VisualElement rootElement;
        private Button join;
        private int index = 1;

        public int mainMenuIndex = 6;

        private void OnEnable()
        {
            index = 2;
            rootElement = visualTree.CloneTree();

            join = rootElement.Q<Button>("JoinButton");

            //join.clickable.clicked += () => Debug.Log("Clicked");

            rootElement.Add(join);

        }

        private void Test(ChangeEvent<Button> value) => Debug.Log("Helo");
        public void JoinRoom(int ID) { _JoinRoom.Raise(); index = ID; }

        [Button]
        public void CreateRoom() => _CreateRoom.Raise();

        public void OpenScene()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                SceneManager.LoadScene(index, LoadSceneMode.Additive);
                SceneManager.UnloadScene(mainMenuIndex);
            }
            else
            {
                SceneManager.LoadScene(index, LoadSceneMode.Additive);
                SceneManager.UnloadScene(mainMenuIndex);
            }

            _sceneID.Value = index;
        }

        

    }
}

