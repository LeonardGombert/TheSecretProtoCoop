﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class PlayerBehavior : MonoBehaviour, IKillable
    {
        public GameEvent _refreshScene;

        public RoomManager currentRoom { get; set; }

        private bool _isDead;
        public bool isDead { get { return _isDead; } set { _isDead = value; } }

        [SerializeField] private Transform rigTransform;

        [SerializeField] private GameEvent playerHitTrap, raiseAlarm;

        [SerializeField] private CallableFunction _gameOver;

        [SerializeField] private CallableFunction _sendPlayerPosAndRot;

        [SerializeField] private Vector3Variable _playerPosition;
        [SerializeField] private QuaternionVariable _playerRotation;

        public void Die(Vector3 direction = default)
        {
            if (!isDead)
            {
                raiseAlarm.Raise();
                _gameOver.Raise();
                playerHitTrap.Raise();

                isDead = true;
            }
        }

        void Update()
        {
            // Rotation
            _playerRotation.Value = transform.localRotation;

            // Position
            if (currentRoom != null)
            {
                // Sets the Position of the Player in the Room to the position of the Player in the World
                currentRoom.room.LocalPlayer.position = rigTransform.position;

                // Sets the Position Variable to the Local Position of the Player (Relative to the Room)
                _playerPosition.Value = currentRoom.room.LocalPlayer.localPosition;
            }

            _sendPlayerPosAndRot.Raise();
        }

        public void ResetPlayer() => isDead = false;
    }
}
