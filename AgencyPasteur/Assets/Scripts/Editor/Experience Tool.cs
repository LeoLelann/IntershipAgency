using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

public class ExperienceTool : EditorWindow
{
    private string FolderPath = "Assets/Resources/ScriptableObject/";
    private string _extention = ".asset";
    private Vector2 _windowSize;
    private string _chosenExp;
    Vector2 _scrollPos;
    private SCMix _mixList;
    private SCHeat _heatList;

    #region SetUp
    [MenuItem("Pasteur/ExperienceTool")]
    private static void Init()
    {
        ExperienceTool window = GetWindowWithRect<ExperienceTool>(new Rect(0, 0, 1000, 300), false);
        window.Show();
    }
    private void OnGUI()
    {
        LoadPaths();
        OnGUIUpdate();
    }

    private void LoadPaths()
    {
        SCMix pathm = Resources.Load<SCMix>("ScriptableObject/Mix");
        _mixList = pathm;
        SCHeat pathh = Resources.Load<SCHeat>("ScriptableObject/Heat");
        _heatList = pathh;
    }
    private void OnGUIUpdate()
    {
        EditorGUILayout.BeginVertical();
        DisplayExpList();
        DisplayExp();
        EditorGUILayout.EndVertical();
    }
    #endregion
    #region Display
    private void DisplayExpList()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Exp List:", new GUIStyle(GUI.skin.label) { fontSize = 14, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginVertical();
            switch (i)
            {
                case (0):
                    GUILayout.Label("Mix :", new GUIStyle(GUI.skin.label) { fontSize = 12, alignment = TextAnchor.MiddleCenter });
                   if( GUILayout.Button("Edit", new GUIStyle(GUI.skin.button) { fontSize = 10, fontStyle = FontStyle.Bold }))
                    _chosenExp = "Mix";
                    break; 
                case (1):
                    GUILayout.Label("Heat :", new GUIStyle(GUI.skin.label) { fontSize = 12, alignment = TextAnchor.MiddleCenter });
                    if(GUILayout.Button("Edit", new GUIStyle(GUI.skin.button) { fontSize = 10, fontStyle = FontStyle.Bold }))
                    _chosenExp = "Heat";
                    break;
                case (2):
                    GUILayout.Label("Dilute :", new GUIStyle(GUI.skin.label) { fontSize = 12, alignment = TextAnchor.MiddleCenter });
                    if(GUILayout.Button("Edit", new GUIStyle(GUI.skin.button) { fontSize = 10, fontStyle = FontStyle.Bold }))
                    _chosenExp = "Dilute";
                    break;
            }  
            //DisplayListElement(_floors.Floors[i], i);
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    private void DisplayExp()
    {

        EditorGUILayout.BeginVertical();

        GUILayout.Label(_chosenExp, new GUIStyle(GUI.skin.label) { fontSize = 15, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
        switch (_chosenExp)
        {
            case ("Mix"):
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(180));
                if (_mixList.Mixed.Count > 0)
                {
                    for (int i = 0; i < _mixList.Mixed.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Mix " + i);
                        for(int j = 0; j < _mixList.Mixed[i].State.Length; j++)
                        {
                           _mixList.Mixed[i].State[j]=(Glassware.glasswareState)EditorGUILayout.EnumPopup(_mixList.Mixed[i].State[j]);
                        }
                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("X", new GUIStyle(GUI.skin.button) { fixedHeight = 18, fontSize = 10, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter }))
                        {
                            _mixList.Mixed.RemoveAt(i);
                        }
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndScrollView();
                if (GUILayout.Button("Add Mix Result", new GUIStyle(GUI.skin.button) { fixedHeight = 16, fontSize = 11, fontStyle = FontStyle.Bold }))
                {
                    _mixList.Mixed.Add(new Mix());
                }
                GUI.backgroundColor = new Color(0, .53f, .22f);
                if (GUILayout.Button("Save", new GUIStyle(GUI.skin.button) { fixedHeight = 16, fontSize = 11, fontStyle = FontStyle.Bold, }))
                {
                    Save();
                }
                break;
            case ("Heat"):
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(180));
                if (_heatList.Heated.Count > 0)
                {
                    for (int i = 0; i < _heatList.Heated.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Heat " + i);
                        for (int j = 0; j < _heatList.Heated[i].State.Length; j++)
                        {
                            _heatList.Heated[i].State[j] = (Glassware.glasswareState)EditorGUILayout.EnumPopup(_heatList.Heated[i].State[j]);
                        }
                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("X", new GUIStyle(GUI.skin.button) { fixedHeight = 18, fontSize = 10, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter }))
                        {
                            _heatList.Heated.RemoveAt(i);
                        }
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndScrollView();
                if (GUILayout.Button("Add Heat Result", new GUIStyle(GUI.skin.button) { fixedHeight = 16, fontSize = 11, fontStyle = FontStyle.Bold }))
                {
                    _heatList.Heated.Add(new Heat());
                }
                GUI.backgroundColor = new Color(0, .53f, .22f);
                if (GUILayout.Button("Save", new GUIStyle(GUI.skin.button) { fixedHeight = 16, fontSize = 11, fontStyle = FontStyle.Bold, }))
                {
                    Save();
                }
                break;
        }
       
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndVertical();
    }
    #endregion
    private void Save()
    {
        switch (_chosenExp)
        {
            case ("Mix"):
                string path = $"{FolderPath}Mix{_extention}";
                EditorUtility.SetDirty(_mixList);
                if (!File.Exists(path))
                {
                    AssetDatabase.CreateAsset(_mixList, path);
                }
                break;
            case ("Heat"):
                string path2 = $"{FolderPath}Mix{_extention}";
                EditorUtility.SetDirty(_heatList);
                if (!File.Exists(path2))
                {
                    AssetDatabase.CreateAsset(_heatList, path2);
                }
                break;
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}


