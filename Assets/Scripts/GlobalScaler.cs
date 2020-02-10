using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScaler : MonoBehaviour {
    public float xScale;
    public bool lockX;
    public bool useCurrentX;
    public float yScale;
    public bool lockY;
    public bool useCurrentY;
    public float zScale;
    public bool lockZ;
    public bool useCurrentZ;

    // Start is called before the first frame update
    void Start() {
        Transform originalParent = transform.parent;

        Vector3 originalLocalScale = transform.localScale;
        transform.SetParent(null);
        Vector3 originalGlobalScale = transform.localScale;

        Vector3 newScale = new Vector3(
            lockX ? originalGlobalScale.x : useCurrentX ? originalLocalScale.x : xScale,
            lockY ? originalGlobalScale.y : useCurrentY ? originalLocalScale.y : yScale,
            lockZ ? originalGlobalScale.z : useCurrentZ ? originalLocalScale.z : zScale
        );

        transform.localScale = newScale;
        transform.SetParent(originalParent);
    }
}
