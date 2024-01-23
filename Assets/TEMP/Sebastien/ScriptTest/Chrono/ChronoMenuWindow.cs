using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ChronoMenuWindow : EditorWindow
{
    private float startTime;
    private List<float> laps = new List<float>();
    private List<string> segmentNames = new List<string>();
    private bool isRunning;
    private bool startOnSceneLoad = false;
    private float lastSavedTime;

    private bool segmentNamesFoldout = true; // Variable pour déplier/replier la liste des noms de segments
    private bool parametersFoldout = true; // Variable pour déplier/replier la liste des noms de segments


    [MenuItem("Tools/Chrono")]
    private static void OpenChronoMenu()
    {
        GetWindow<ChronoMenuWindow>("Chrono");
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        EditorApplication.update += OnEditorUpdate;

        if (startOnSceneLoad)
        {
            StartChronometer();
        }
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.update -= OnEditorUpdate;
    }

    private void OnGUI()
    {

        GUILayout.Space(20);

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.richText = true;
        style.fontSize = 16;

        GUILayout.Label("Chrono Tool", style);
        GUILayout.Space(20);


        if (GUILayout.Button(isRunning ? "Stop" : "Start"))
        {
            if (isRunning)
            {
                StopChronometer();
            }
            else
            {
                if (!startOnSceneLoad)
                {
                    StartChronometer();
                }
            }
        }

        if (GUILayout.Button("Reset"))
        {
            ResetChronometer();
        }

        if (isRunning)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            GUILayout.Label("<size=16><b>Temps écoulé : </b></size>" + elapsedTime.ToString("F3"),style);

            if (GUILayout.Button("Add Segment"))
            {
                AddLap(elapsedTime);
            }

            Repaint();
        }
        else
        {
            style.fontSize = 16;  
            GUILayout.Label("<size=16><b>Last saved time: </b></size>" + lastSavedTime.ToString("F3"), style);
            GUILayout.Space(20); 

        }

        GUILayout.Label("Segment List : ");
        GUILayout.Space(20);

        for (int i = 0; i < laps.Count; i++)
        {
            string segmentName = i < segmentNames.Count ? segmentNames[i] : "DefaultSegment";
            GUILayout.Label(segmentName + " : " + laps[i].ToString("F3"));
        }

        // Section pour la gestion des noms de segment
        segmentNamesFoldout = EditorGUILayout.Foldout(segmentNamesFoldout, "Segment Names List");
        GUILayout.Space(50);


        if (segmentNamesFoldout)
        {
            for (int i = 0; i < segmentNames.Count; i++)
            {
                GUILayout.BeginHorizontal();
                segmentNames[i] = EditorGUILayout.TextField("Segment " + i + ":", segmentNames[i]);

                if (GUILayout.Button("-", GUILayout.Width(20f)))
                {
                    RemoveSegmentName(i);
                }

                GUILayout.EndHorizontal();
            }

            // Bouton pour ajouter un nouveau nom de segment
            if (GUILayout.Button("Add Segment Name"))
            {
                AddSegmentName("NewSegment");
            }
        }

        // Section pour la gestion des noms de segment
        parametersFoldout = EditorGUILayout.Foldout(parametersFoldout, "Parameters");

        if (parametersFoldout)
        {
            startOnSceneLoad = GUILayout.Toggle(startOnSceneLoad, "Launch on start");

        }
    }

    private void AddLap(float lapTime)
    {
        laps.Add(lapTime);
    }

    private void ResetLaps()
    {
        laps.Clear();
    }

    private void StartChronometer()
    {
        startTime = Time.realtimeSinceStartup;
        isRunning = true;
    }

    private void StopChronometer()
    {
        isRunning = false;
        lastSavedTime = Time.realtimeSinceStartup - startTime;
    }

    private void ResetChronometer()
    {
        isRunning = false;
        lastSavedTime = 0f;
        ResetLaps();
    }

    private void RemoveSegmentName(int index)
    {
        if (index >= 0 && index < segmentNames.Count)
        {
            segmentNames.RemoveAt(index);
        }
    }

    private void AddSegmentName(string defaultName)
    {
        segmentNames.Add(defaultName);
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode && isRunning && startOnSceneLoad)
        {
            StopChronometer();
        }
    }

    private void OnEditorUpdate()
    {
        float elapsedTime = Time.realtimeSinceStartup - startTime;

        if (EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            if (Event.current != null && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space)
            {
                AddLap(elapsedTime);
            }
        }
    }
}
