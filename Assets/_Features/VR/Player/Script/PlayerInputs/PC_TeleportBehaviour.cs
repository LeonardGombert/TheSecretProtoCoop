﻿#if UNITY_STANDALONE
using Gameplay.VR.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.PC.Player
{
    public class PC_TeleportBehaviour : MonoBehaviour
    {
        [Tooltip("Key that fires the Teleportation input.")] [SerializeField] KeyCode teleportKeyCode = default;

        [Tooltip("The point from which the laser emits.")] [SerializeField] Transform teleportOrigin = null;
        TeleportManager teleportationManager = null;

        private void Awake()
        {
            teleportationManager = GetComponent<TeleportManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(teleportKeyCode))
            {
                teleportationManager.pointerOrigin = teleportOrigin;
                teleportationManager.TallRayPointer(null);
            }

            if (Input.GetKeyUp(teleportKeyCode))
            {
                teleportationManager.TryTeleporting();
            }
        }
    }
}
#endif