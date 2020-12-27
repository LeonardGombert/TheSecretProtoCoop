﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build.Reporting;
using UnityEditor;
using Gameplay;
namespace Tools
{
    
    public class BuildPlayer : MonoBehaviour
    {
        [MenuItem("Build/Build Android")]
        public static void BuildAndroid()
        {
            BoolVariable isMobile = Resources.Load("isMobile") as BoolVariable;
            isMobile.Value = true;
            isMobile.SetDirty();
            Debug.Log(isMobile.Value);
            BuildPlayerOptions buildPlayerOptions = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(new BuildPlayerOptions());
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);

            BuildSummary summary = report.summary;
            if (summary.result == BuildResult.Succeeded)
            {
                isMobile.Value = false;
                isMobile.SetDirty();
                Debug.Log(isMobile.Value);
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                isMobile.Value = false;
                isMobile.SetDirty();
                Debug.Log(isMobile.Value);
                Debug.Log("Build failed");
            }
        }

        [MenuItem("Build/Build Test")]
        public static void BuildTest()
        {
            BoolVariable isMobile = Resources.Load("isMobile") as BoolVariable;
            EditorGUIUtility.PingObject(isMobile);
        }

    }

}
