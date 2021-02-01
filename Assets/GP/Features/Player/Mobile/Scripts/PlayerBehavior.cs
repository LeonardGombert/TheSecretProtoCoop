﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Mobile
{
    public class PlayerBehavior : MonoBehaviour
    {
        public RoomManager currentRoom { get; set; }

        [SerializeField] private Vector3Variable _playerPosition;
        [SerializeField] private QuaternionVariable _playerRotation;

        [SerializeField] private GameObjectVariable _player;

        [SerializeField] private BoolVariable _hidePlayer;

        [SerializeField] private Transform playerTransform;

        [SerializeField] private UnityEngine.Events.UnityEvent _OnStart;

        void Start()
        {
            _OnStart.Invoke();
            _player.Value = this.gameObject;
            playerTransform.localScale = new Vector3(2, 2, 2);
        }

        void Update()
        {
            if (!_hidePlayer.Value)
            {
                // Position
                if (currentRoom != null)
                {
                    // Sets the Local Position of the Player (Relative to the Room) to the Position Variable
                    currentRoom.room.LocalPlayer.localPosition = _playerPosition.Value;

                    // Sets the Position of the Player in the World to the Position of the Player in the Room;
                    playerTransform.position = currentRoom.room.LocalPlayer.position;
                }

                playerTransform.forward = _player.Value.transform.forward;

                // Rotation
                playerTransform.eulerAngles = new Vector3(-90, playerTransform.eulerAngles.y, 0);
            }
        }
    }
}

