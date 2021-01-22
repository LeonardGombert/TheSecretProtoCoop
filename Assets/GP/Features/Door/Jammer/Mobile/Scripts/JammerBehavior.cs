﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Video;
namespace Gameplay
{
    public class JammerBehavior : MonoBehaviour, IKillable
    {
        private bool state;

        [SerializeField] private CallableFunction _destroyJammer;
        [SerializeField] private CallableFunction _sendOnJammerDestroyedToOthers;
        [SerializeField] private List<SwitcherBehavior> switchers;
        private Camera cam;
        private VideoPlayer video;
        [Range(0, 10)] public int ID;
        
        [SerializeField] 
        private bool State 
        {
            get
            {
                return state;
            }
            set
            {
                state = value;

                if (state)
                {
                    for (int i = 0; i < switchers.Count; i++)
                    {
                        switchers[i].State = 0;
                        switchers[i].ShowLines(false);
                    }
                }
                else
                {
                    for (int i = 0; i < switchers.Count; i++)
                    {
                        switchers[i].State = 1;
                        switchers[i].ShowLines(true);
                    }
                }
            }
        }

        public void OnEnable() => JammerManager.jammers.Add(this);
        public void OnDisable() => JammerManager.jammers.Remove(this);

        private IEnumerator Start()
        {

            yield return new WaitForSeconds(.5f);
            video = FindObjectOfType<VideoPlayer>();
            cam = video.GetComponent<Camera>();
            if (cam.gameObject.GetComponent<VideoPlayer>() != null)
                video = cam.gameObject.GetComponent<VideoPlayer>();
            State = true;
        }

        public void AddSwitcher(SwitcherBehavior switcher) => switchers.Add(switcher);

        public void SetState(bool value) => State = value;

        [Button]
        public void Die() 
        {
            _destroyJammer.Raise(ID);
            _sendOnJammerDestroyedToOthers.Raise(ID);

        }

        public void Die(Vector3 force = default)
        {
            throw new System.NotImplementedException();
        }

        public void StopGlitch()
        {
            if(video != null)
            {
                video.enabled = false;
            }

        }
    }
}

