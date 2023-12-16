using UnityEngine;
using System.IO;

public class TrackingViewer : MonoBehaviour
{
    public Vector3[] trackPoints; // Tableau de points modifiable dans l'inspecteur
    public string filePath = "Assets/TEMP/Sebastien/PlayerTracking/PlayerTrackingData_2.txt"; // Chemin du fichier



    private void OnDrawGizmos()
    {
        LoadTrackingData(); // Charger les données du fichier à chaque rafraîchissement de l'éditeur
        DrawTrackingLine();
    }

    private void LoadTrackingData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Le fichier n'existe pas : " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Debug.LogWarning("Le fichier est vide : " + filePath);
            return;
        }

        // Initialiser le tableau avec la taille correspondant au nombre de lignes
        trackPoints = new Vector3[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            // Diviser la ligne en valeurs (assurez-vous que le séparateur est correct)
            string[] values = lines[i].Split(',');

            if (values.Length >= 3)
            {
                // Utiliser CultureInfo.InvariantCulture pour interpréter les virgules correctement
                float x = float.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture);
                float y = float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture);
                float z = float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture);

                trackPoints[i] = new Vector3(x, y, z);
            }
        }
    }

    private void DrawTrackingLine()
    {
        if (trackPoints == null || trackPoints.Length < 2)
        {
            Debug.LogWarning("Pas assez de points");
            return;
        }

        // Définir une couleur pour les Gizmos (par exemple, rouge)
        Gizmos.color = Color.yellow;

        // Dessiner la ligne en reliant chaque point au suivant
        for (int i = 0; i < trackPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(trackPoints[i], trackPoints[i + 1]);
        }
    }

}