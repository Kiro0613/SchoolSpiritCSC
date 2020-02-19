using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public NodeMap nodeMap;
    public int indexInMap;

    public bool isVisisble;
    public bool findNeighbors = false;
    public bool showLines = false;
    public List<Node> neighbors;

    private void Awake() {
        nodeMap = GetComponentInParent<NodeMap>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnValidate() {
        GetComponent<MeshRenderer>().enabled = isVisisble;
    }
    
    public void FindNeighbors() {
        neighbors.Clear();
        neighbors.Add(this);

        foreach(Node node in nodeMap.nodeList) {
            if(!node.Equals(this)) {
                //Change the layer so it doesn't detect itself
                gameObject.layer = 12;
                node.gameObject.layer = 12;

                if(Physics.Linecast(transform.position, node.transform.position, out RaycastHit hit, nodeMap.nodeMask)) {
                } else {
                    neighbors.Add(node);
                }

                gameObject.layer = 11;
                node.gameObject.layer = 11;
            }
        }
    }

    public void DrawNeighbors() {
        foreach(Node neighbor in neighbors) {
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    private void OnDrawGizmos() {
        if(findNeighbors) {
            FindNeighbors();
            findNeighbors = false;
        }

        if(showLines || nodeMap.edgesVisisble) {
            Gizmos.color = showLines ? Color.cyan : Color.magenta;
            DrawNeighbors();
        }
    }
}
