using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour {
    public bool buildGraph = false;
    public bool drawGraph = false;
    public bool nodesVisible = false;
    public bool edgesVisisble = false;
    public float raycastLength = 50f;
    public Node[] nodeList;
    public LayerMask nodeMask = 3072;

    private void Awake() {
        //nodeMask = LayerMask.GetMask("Wall", "Node");
    }

    // Start is called before the first frame update
    void Start() {
        nodeList = FindObjectsOfType<Node>();
    }

    // Update is called once per frame
    void Update() {

    }

    void AllNodesVisible(bool isVisisble) {
        foreach(Node node in nodeList) {
            node.GetComponent<MeshRenderer>().enabled = isVisisble;
        }
    }

    private void OnValidate() {
        
    }

    private void OnDrawGizmos() {
        if(nodeList == null) {
            BuildGraph();
        }

        if(buildGraph) {
            BuildGraph();
            buildGraph = false;
        }

        if(edgesVisisble) {
            AllNodesVisible(nodesVisible);
        }
    }

    private void BuildGraph() {
        nodeList = FindObjectsOfType<Node>();
        int indexCount = 0;

        //Debug.Log("Finding neighbors...");

        foreach(Node node in nodeList) {
            node.name = "Node " + indexCount;
            node.indexInMap = indexCount;
            node.nodeMap = this;
            node.FindNeighbors();

            indexCount++;
        }

        //Debug.Log("Finished finding neighbors.");
    }
}
