#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[ExecuteInEditMode]
public class PlayerBasePlacer : MonoBehaviour
{
    public GameObject playerBasePrefab; // Référence prefab de PlayerBase

#if UNITY_EDITOR
    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        // Vérifie la combinaison touche + clic gauche pour le placement
        if (e.type == EventType.MouseDown && e.button == 0 && e.modifiers == EventModifiers.Shift)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Place le PlayerBase à l'emplacement du clic
                PlacePlayerBase(hit.point);
            }

            e.Use(); // Empêche la sélection d'autres objets dans la scène
        }

        // Vérifie la combinaison touche + clic droit pour la rotation
        if (e.type == EventType.MouseDown && e.button == 1 && e.modifiers == EventModifiers.Shift)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Modifie la rotation du PlayerBase pour faire face à l'endroit du clic
                RotatePlayerBase(hit);
            }

            e.Use(); // Empêche la sélection d'autres objets dans la scène
        }
    }

    static void PlacePlayerBase(Vector3 position)
    {
        PlayerBasePlacer playerBasePlacer = FindObjectOfType<PlayerBasePlacer>();

        if (playerBasePlacer.playerBasePrefab == null)
        {
            Debug.LogError("Prefab manquant");
            return;
        }

        GameObject existingPlayerBase = GameObject.Find("PlayerBase");
        if (existingPlayerBase != null)
        {
            DestroyImmediate(existingPlayerBase);
        }

        // Crée une nouvelle instance de PlayerBase
        GameObject newPlayerBase = Instantiate(playerBasePlacer.playerBasePrefab, position, Quaternion.identity);

        // Obtient la position du mesh du joueur pour placer depuis le Mesh (et pas au pivot entre la cam et le mesh)
        Transform playerTransform = newPlayerBase.transform.Find("Player");
        if (playerTransform != null)
        {
            // Ajuste la position du prefab en fonction de la position du joueur
            newPlayerBase.transform.position = position - playerTransform.localPosition;
        }

        newPlayerBase.name = "PlayerBase";
        Selection.activeGameObject = newPlayerBase;

        HandleUtility.AddDefaultControl(0); // Empêche de cliquer sur le mesh après placement

        Debug.Log("GG y a un nouveau PlayerBase");
    }

    static void RotatePlayerBase(RaycastHit hit)
    {
        PlayerBasePlacer playerBasePlacer = FindObjectOfType<PlayerBasePlacer>();

        if (playerBasePlacer.playerBasePrefab == null)
        {
            Debug.LogError("Prefab manquant");
            return;
        }

        GameObject existingPlayerBase = GameObject.Find("PlayerBase");
        if (existingPlayerBase != null)
        {
            Vector3 lookAtPosition = hit.point;
            lookAtPosition.y = existingPlayerBase.transform.position.y;
            existingPlayerBase.transform.LookAt(lookAtPosition);
            Debug.Log("TOURNE");
        }
    }

#endif
}