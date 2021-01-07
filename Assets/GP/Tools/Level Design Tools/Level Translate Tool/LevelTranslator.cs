﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;


namespace Tools.LevelDesign
{
    public class LevelTranslator : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private RectTransform switchablesParent;
        [SerializeField] private List<GameObject> prefabsVR;
        [SerializeField] private List<GameObject> prefabsMobile;
        private List<GameObject> switchables;
        public TextAsset json;
        [HideInInspector]
        [SerializeField] private LevelSaver.ListOfISwitchableElement elements;
        //private Image newImage;
        private GameObject newObject;
        public void TranslateLevelPosition()
        {
            switchables.Clear();
            elements = JsonUtility.FromJson<LevelSaver.ListOfISwitchableElement>(json.ToString());
            //RectTransform parent = Object.Instantiate(switchablesParent, canvas.transform) as RectTransform;
            for (int i = 0; i < elements.list.Count; i++)
            {
                Vector3 position = elements.list[i].position;
                Quaternion rotation = elements.list[i].rotation;
                Vector3 scale = elements.list[i].scale;
  
                for (int j = 0; j < prefabsVR.Count; j++)
                {
                    if (elements.list[i].prefab.GetInstanceID() == prefabsVR[j].GetInstanceID())
                    {
                        for (int k = 0; k < parent.childCount; k++)
                        {
                            for (int l = 0; l < parent.GetChild(k).childCount; l++)
                            {
                                if (parent.GetChild(k).GetChild(l).position == position)
                                {

#if UNITY_EDITOR
                                    newObject = PrefabUtility.InstantiatePrefab(prefabsMobile[j] as GameObject, parent.GetChild(k)) as GameObject;
                                    newObject.GetComponent<Gameplay.ISwitchable>().State = parent.GetChild(k).GetChild(l).GetComponent<Gameplay.ISwitchable>().State;
                                    newObject.GetComponent<Gameplay.ISwitchable>().Power = parent.GetChild(k).GetChild(l).GetComponent<Gameplay.ISwitchable>().Power;
                                    switchables.Add(parent.GetChild(k).GetChild(l).gameObject);
                                    break;
#endif

                                }
                            }

                        }

                    }
                }



                newObject.transform.position = position;
                newObject.transform.rotation = rotation;
                newObject.transform.localScale = scale;

            }


            for (int i = 0; i < switchables.Count; i++)
            {
                DestroyImmediate(switchables[i]);
            }

        }
    }
}

