using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SplineMesh {

    [ExecuteInEditMode]
    public class SimpleTentacleAnimation : MonoBehaviour
    {
        public Spline spline; 

        public List<SplineNode> nodes;

        public List<GameObject> nodesPosition = new List<GameObject>();

        private bool toUpdate = false;

        public bool triggerUpdate; 

        // Start is called before the first frame update
        void Start()
        {
            nodes = new List<SplineNode>();
            nodes = spline.nodes;
        }

        // Update is called once per frame
        void Update()
        {
            if (toUpdate)
            {
                NodeGameObjectsRefresher();
                toUpdate = false;
            }
        }

        private void OnValidate()
        {

            toUpdate = true; 
            
        }

        


        private void NodeGameObjectsRefresher()
        {
            nodesPosition = new List<GameObject>(); 

            nodes = spline.nodes;

            for (int i = 0; i < nodes.Count; i++)
            {
                GameObject nodePositon = new GameObject("GameObject");
                nodePositon.transform.parent = this.gameObject.transform; 
                nodesPosition.Add(nodePositon);
                nodesPosition[i].transform.position = nodes[i].Position;
            }
        }

    }

}