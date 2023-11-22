#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[ExecuteInEditMode]
public class PlayerBasePlacer : MonoBehaviour
{
    public GameObject playerBasePrefab; // Ref prefab de PlayerBase
    public KeyCode placementKey = KeyCode.LeftControl; // Touche du clavier pour activer le placement

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

        // V�rifie la combinaison touche + clic
        if (e.type == EventType.MouseDown && e.button == 0 && e.modifiers == EventModifiers.Control)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Place le PlayerBase � l'emplacement du clic
                PlacePlayerBase(hit.point);
            }

            e.Use(); // Emp�che la s�lection d'autres objets dans la sc�ne
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

        //Delete l'ancien PlayerBase (si il y en a un)
        GameObject existingPlayerBase = GameObject.Find("PlayerBase");
        if (existingPlayerBase != null)
        {
            DestroyImmediate(existingPlayerBase);
        }

        // Cr�er une nouvelle instance de PlayerBase
        GameObject newPlayerBase = Instantiate(playerBasePlacer.playerBasePrefab, position, Quaternion.identity);
        newPlayerBase.name = "PlayerBase";

        Debug.Log("GG y a un nouveau PlayerBase");
    }
#endif
}