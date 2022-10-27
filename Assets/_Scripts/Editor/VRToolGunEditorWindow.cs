/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JLO_VR.ToolGun 
{
    public class VRToolGunEditorWindow : EditorWindow
    {
        #region Declarations

        public string basePath = "/JLO_VR/VR Tool Gun/Scripts/Base/";
        public string scriptsPath = "/JLO_VR/VR Tool Gun/Scripts/";

        public string modeToCreateName;
        public string modeEventToCreateName;

        public float buttonSpacing = 20;
        private Vector2 scrollPos;

        private GUILayoutOption[] buttonOptions =
        {
            GUILayout.Height(40)
        };

        #endregion  

        #region Unity Methods

        void OnGUI()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, new GUIStyle());

            GUILayout.Space(buttonSpacing);

            // Add ToolGun GUI
            if (GUILayout.Button("Add Tool Gun", buttonOptions))
                AddToolGunToScene();

            GUILayout.Space(buttonSpacing);
            HorizontalLine();
            GUILayout.Space(buttonSpacing);

            // Mode GUI
            GUILayout.Label("Enter new mode name (.cs file):");
            modeToCreateName = GUILayout.TextField(modeToCreateName);
            GUILayout.Space(10);
            if (GUILayout.Button("Create Mode", buttonOptions))
                CreateMode(modeToCreateName);

            GUILayout.Space(buttonSpacing);
            HorizontalLine();
            GUILayout.Space(buttonSpacing);

            // Mode Event GUI
            GUILayout.Label("Enter new mode\nevent name (.cs file):");
            modeEventToCreateName = GUILayout.TextField(modeEventToCreateName);
            GUILayout.Space(10);
            if (GUILayout.Button("Create Mode Event", buttonOptions))
                CreateModeEvent(modeEventToCreateName);

            GUILayout.Space(buttonSpacing);
            HorizontalLine();
            GUILayout.Space(buttonSpacing);

            if (GUILayout.Button("Populate All Modes", buttonOptions))
                PopulateAllModes();

            GUILayout.Space(buttonSpacing);
            HorizontalLine();
            GUILayout.Space(buttonSpacing);

            if (GUILayout.Button("Close", buttonOptions))
                Close();

            GUILayout.EndScrollView();
        }

        void OnInspectorUpdate()
        {
            Repaint();
        }

        #endregion

        #region Custom Methods

        // [MenuItem("JLO_VR/VR Tool Gun")]
        static void Init()
        {
            VRToolGunEditorWindow window = (VRToolGunEditorWindow)GetWindow(typeof(VRToolGunEditorWindow), true, "VR Tool Gun");
            window.Show();
        }

        private void AddToolGunToScene()
        {
            ToolGun toolGun = FindObjectOfType<ToolGun>();

            if (toolGun)
            {
                Debug.LogError("There is already a tool gun in the scene!");
                return;
            }

            GameObject tgObj = new GameObject("Tool Gun");
            tgObj.AddComponent<ToolGun>();
            GameObject modesObj = new GameObject("Modes");
            modesObj.transform.SetParent(tgObj.transform);
        }

        public void PopulateAllModes() 
        {
            ToolGun toolGun = FindObjectOfType<ToolGun>();

            if (!toolGun)
            {
                Debug.LogError("No tool gun present in scene." +
                    "Please be sure to add a tool gun using " +
                    "the \"Add Tool Gun\" button");
                return;
            }

            Transform modesTransform = toolGun.transform.GetChild(0);
            int childCount = modesTransform.childCount;
            if (childCount == 0)
            {
                Debug.LogError("No modes added as children to the \"Modes\" child of tool gun.");
                return;
            }

            // for (int i = 0; i < childCount; i++)
            // {
            //     Transform modeTransform = modesTransform.GetChild(i);
            //     Type type = Type.GetType("JLO_VR.ToolGun." + modeTransform.name + ", Assembly-CSharp");
            //
            //     TG_Mode mode = null;
            //     if (!(TG_Mode)modeTransform.GetComponent(type))
            //         mode = (TG_Mode)modeTransform.gameObject.AddComponent(type);
            //     else
            //         mode = modeTransform.GetComponent<TG_Mode>();
            //
            //     toolGun.AddMode(modeTransform.name, mode);
            // }

            Debug.Log("All modes populated.");
        }

        private void CreateMode(string modeToCreateName)
        {
            string path = WriteFile("Modes", modeToCreateName, ReadFile("TG_ModeClone"));
            Debug.Log(modeToCreateName + ".cs created!\nPath: " + path);
            WriteEnumToFile(modeToCreateName);

            // add mode gameobject to tool gun and mode dictionary
            ToolGun toolGun = FindObjectOfType<ToolGun>();
            if (!toolGun)
            {
                Debug.LogError("Unable to add mode GameObject to tool gun. " +
                    "No tool gun in scene.");
                return;
            }
            GameObject modeObj = new GameObject(modeToCreateName);
            modeObj.transform.SetParent(toolGun.transform.GetChild(0));
        }

        private void CreateModeEvent(string modeEventToCreateName)
        {
            string path = WriteFile("Mode Events", modeEventToCreateName, ReadFile("TG_ModeEventClone"));
            Debug.Log(modeEventToCreateName + ".cs created!\nPath: " + path);
        }

        private List<string> ReadFile(string filename)
        {
            List<string> retList = new List<string>();
            string path = Application.dataPath + basePath + filename + ".cs";
            foreach (string line in File.ReadLines(path))
                retList.Add(line);

            return retList;
        }

        private string WriteFile(string folder, string filename, List<string> contents)
        {
            string path = Application.dataPath + scriptsPath + "/" + folder + "/" + filename + ".cs";

            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream);

            for (int i = 0; i < contents.Count; i++)
            {
                string line = contents[i];
                if (i == 7)
                {
                    if (folder.Equals("Modes"))
                        line = line.Replace("TG_ModeClone", filename);
                    else if (folder.Equals("Mode Events"))
                        line = line.Replace("TG_ModeEventClone", filename);
                }
                writer.WriteLine(line);
            }

            writer.Close();

            return path;
        }

        private void WriteEnumToFile(string enumTypeName)
        {
            List<string> contents = ReadFile("TG_ModeEnum");
            string path = Application.dataPath + basePath + "/TG_ModeEnum.cs";

            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream);

            string enumNameUpper = enumTypeName.ToUpper();
            if (ContentsContainString(enumNameUpper, contents))
                return;

            contents.Insert(contents.Count - 2, "\t\t" + enumNameUpper);

            for (int i = 0; i < contents.Count; i++)
            {
                if (i >= 4 && i < contents.Count - 3 && contents.Count >= 8 &&
                        !contents[i].Contains(","))
                    writer.WriteLine(contents[i] + ",");
                else
                    writer.WriteLine(contents[i]);
            }

            writer.Close();
        }

        private bool ContentsContainString(string strToCheck, List<string> contents)
        {
            for (int i = 0; i < contents.Count; i++)
            {
                string line = contents[i];
                if (line.Contains(strToCheck))
                    return true;
            }

            return false;
        }

        private void AddModeToToolGun(string modeName)
        {
            List<string> contents = ReadFile("ToolGun");
            string path = Application.dataPath + basePath + "/ToolGun.cs";

            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream);

            if (ContentsContainString(modeName, contents))
                return;

            int declarationIndex = -1;
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i].Contains("Declarations"))
                {
                    declarationIndex = i;
                    break;
                }
            }

            string modeVarName = char.ToLower(modeName[0]) + modeName.Substring(1, modeName.Length);
            contents.Insert(declarationIndex + 1, "\t\tpublic " + modeName + modeVarName);

            for (int i = 0; i < contents.Count; i++)
                writer.WriteLine(contents[i]);

            writer.Close();
        }

        private void HorizontalLine()
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(2f));
            EditorGUI.DrawRect(r, Color.gray);
        }

        #endregion
    }
}