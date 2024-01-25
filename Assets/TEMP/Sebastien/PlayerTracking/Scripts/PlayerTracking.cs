using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Diagnostics;

public class PlayerTracking : MonoBehaviour
{
    private static int instanceCounter = 0;
    private int myInstanceNumber;
    private List<Vector3> playerPositions = new List<Vector3>();
    private int frameCount = 0;
    private Vector3 lastPlayerPosition; // Évite de sauvegarder des points lorsque le joueur est immobile
    public int framesInterval = 5; // Enregistre la position du joueur toutes les X frames
    public int minPointsToSave = 100; // Nombre minimal de points pour enregistrer

    private void Awake()
    {
        instanceCounter++;
        myInstanceNumber = instanceCounter;
        lastPlayerPosition = transform.position;
    }

    private void Update()
    {
        frameCount++;

        // Enregistre la position du joueur toutes les framesInterval frames
        if (frameCount % framesInterval == 0)
        {
            Vector3 playerPosition = transform.position;

            // Ajoute la position seulement si elle est différente de la précédente
            if (playerPosition != lastPlayerPosition)
            {
                playerPositions.Add(playerPosition);
                lastPlayerPosition = playerPosition;
            }
        }
    }

    private void OnApplicationQuit()
    {
        // Vérifie si le nombre minimal de points pour sauvegarder est atteint
        if (playerPositions.Count >= minPointsToSave)
        {
            SaveTrackingData();
            UnityEngine.Debug.Log(playerPositions.Count + " pts de position enregistrés");
        }
        else
        {
            UnityEngine.Debug.Log("Pas assez de points pour sauvegarder");
        }
    }

    private void SaveTrackingData()
    {
        string folderPath = "Assets/TEMP/Sebastien/PlayerTracking/";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        int fileNumber = FindNextFileNumber(folderPath);
        string fileName = "PlayerTrackingData_" + fileNumber + ".txt";
        string filePath = Path.Combine(folderPath, fileName);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Vector3 position in playerPositions)
            {
                // Conversion des nombres à virgule flottante
                writer.WriteLine(position.x.ToString(CultureInfo.InvariantCulture) + ","
                                 + position.y.ToString(CultureInfo.InvariantCulture) + ","
                                 + position.z.ToString(CultureInfo.InvariantCulture));
            }
        }
        UnityEngine.Debug.Log("Tracking data saved to: " + filePath);

    }

    private int FindNextFileNumber(string folderPath)
    {
        int fileNumber = 1;

        while (File.Exists(Path.Combine(folderPath, "PlayerTrackingData_" + fileNumber + ".txt")))
        {
            fileNumber++;
        }

        return fileNumber;
    }

}
