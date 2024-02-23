using UnityEngine;
using System.Collections.Generic;


public class CullingInstancedDemo : MonoBehaviour
{
    // How many meshes to draw.
    public int instances;
    // Range to draw meshes within.
    [Range(1, 200)]
    public float rangeX = 1f;
    [Range(1, 200)]
    public float rangeY = 1f;
    [Range(1, 200)]
    public float rangeZ = 1f;
    // Material to use for drawing the meshes.
    public Material material;
    // mesh to draw
    public Mesh mesh;
    // turn culling on and off to see difference
    public bool cull = true;
    // show the bounds of the quad/octtree leaves with cubes
    public bool drawBounds;
    // subdivsion of quad/octtree
    public int depth = 3;
    // swap between octree and quadtree
    public bool Octree = true;
    // max draw distance for meshes
    public float maxDrawDistance = 125;



    [SerializeField] private bool _rotateToGroundNormal = false;
    [SerializeField] private bool _randomYAxisRotation = false;
    [SerializeField] private float _maxYRotation = 90;
    [SerializeField] private LayerMask _groundLayer;

    // quadtreedata ----------------------------------------------------------------------
    QuadTreeNode quadTree;
    // culling
    private Plane[] cameraFrustumPlanes;
    float cameraOriginalFarPlane;
    // matrices
    List<Matrix4x4> matricesVisible = new List<Matrix4x4>();
    List<Matrix4x4> matricesAll = new List<Matrix4x4>();
    // cached position for camera
    Matrix4x4 cachedPos;
    // mesh bounds
    Bounds bounds;
    // just for debug/visual reference
    List<Bounds> boundsListVisible = new List<Bounds>();
    GUIStyle style = new GUIStyle();

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        cameraFrustumPlanes = new Plane[6];
        // build a list of random matrices for every instance


        RaycastHit hit;

        for (int i = 0; i < instances; i++)
        {
            Vector3 rayTestPosition = GetRandomRayPosition();
            Ray ray = new Ray(rayTestPosition, Vector3.down);

            if (!Physics.Raycast(ray, out hit, rangeY * 2f ,_groundLayer)) continue;

            Quaternion rotation = GetRotation(-hit.normal);
            Vector3 position = hit.point;


            Vector3 scale = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));

            Matrix4x4 mat = Matrix4x4.TRS(position, rotation, scale);
            matricesAll.Add(mat);
        }
        SetBounds();
        SetupQuadTree();
    }

    // debug/visual stuff
    void OnGUI()
    {
        style.fontSize = 30;
        GUI.Label(new Rect(10, 10, 300, 100), "All Meshes: " + matricesAll.Count + "\n" + "Meshes Drawn: " + matricesVisible.Count, style);
    }
    // debug/visual stuff
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        Gizmos.DrawWireCube(transform.position, new Vector3(rangeX * 2f, rangeY * 2f, rangeZ * 2f));

        Vector3 rayTestPosition = GetRandomRayPosition();
        Ray ray = new Ray(rayTestPosition, Vector3.down);

        Gizmos.color = new Color(1, 1, 0, 1f);
        Gizmos.DrawRay(ray); 

        if (drawBounds)
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f);
            for (int i = 0; i < boundsListVisible.Count; i++)
            {
                Gizmos.DrawWireCube(boundsListVisible[i].center, boundsListVisible[i].size);
            }
            Gizmos.color = new Color(0, 0, 1, 0.3f);
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }


    void GetFrustomData()
    {
        // create the frustum planes
        cameraOriginalFarPlane = Camera.main.farClipPlane;
        Camera.main.farClipPlane = maxDrawDistance;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        // set back to normal 
        Camera.main.farClipPlane = cameraOriginalFarPlane;
        // clear lists
        boundsListVisible.Clear();
        matricesVisible.Clear();
        // get the list of quadtree leaves visible in the frustum planes
        if (cull)
        {
            quadTree.RetrieveLeaves(planes, boundsListVisible, matricesVisible);
        }
        else
        {
            quadTree.RetrieveAllLeaves(boundsListVisible);
        }

    }

    void SetBounds()
    {
        // grow bounds so all points are within it
        bounds = new Bounds(matricesAll[0].GetPosition(), Vector3.one);
        for (int i = 0; i < matricesAll.Count; i++)
        {
            bounds.Encapsulate(matricesAll[i].GetPosition());
        }
        bounds.Expand(10);
    }

    void SetupQuadTree()
    {
        // create a new quadtree
        quadTree = new QuadTreeNode(bounds, depth, Octree);

        //find a leaf to put every matrixs in
        for (int i = 0; i < matricesAll.Count; i++)
        {
            quadTree.FindLeafForPoint(matricesAll[i]);
        }
        // clear out empty leaves
        quadTree.ClearEmpty();
    }

    private void Update()
    {
        // // only if we moved the camera
        if (cachedPos != Camera.main.transform.localToWorldMatrix)
        {
            GetFrustomData();
            cachedPos = Camera.main.transform.localToWorldMatrix;
        }

        // Draw a bunch of meshes each frame.
        if (cull)
        {
            Graphics.DrawMeshInstanced(mesh, 0, material, matricesVisible);
        }
        else
        {
            Graphics.DrawMeshInstanced(mesh, 0, material, matricesAll);
        }
    }

    private Quaternion GetRotation(Vector3 normal)
    {
        Vector3 eulerIdentiy = Quaternion.identity.eulerAngles;
        eulerIdentiy.x += 90; //can be removed or changed, depends on your mesh

        if (_randomYAxisRotation) eulerIdentiy.y += Random.Range(-_maxYRotation, _maxYRotation);

        if (_rotateToGroundNormal)
        {
            return Quaternion.FromToRotation(Vector3.up, normal) * Quaternion.Euler(eulerIdentiy);
        }
        return Quaternion.Euler(eulerIdentiy);
    }

    private Vector3 GetRandomRayPosition()
    {
        return new Vector3(transform.position.x + Random.Range(-rangeX, rangeX), transform.position.y + rangeY, transform.position.z + Random.Range(-rangeZ, rangeZ));
    }
}