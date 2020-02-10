using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the spinny animation
public class InvViewer : MonoBehaviour {
    Transform[] items;
    public float spinSpeed;

    // Start is called before the first frame update
    void Start() {
        items = new Transform[] { };
    }
    
    void FixedUpdate() {
        //Debug.Log(items.Length);
        foreach(Transform item in items) {
            Vector3 target = item.eulerAngles;
            //Debug.Log(target + " - " + item.eulerAngles);
            target.y = Mathf.MoveTowardsAngle(target.y, target.y + 10, Time.deltaTime * spinSpeed);
            item.eulerAngles = Vector3.MoveTowards(item.eulerAngles, target, Time.deltaTime * spinSpeed);
        }
    }

    public void updateItemList(List<GameObject> itemList) {
        List<Transform> itemTransforms = new List<Transform> { };
        foreach(GameObject itemGameObject in itemList) {
            itemTransforms.Add(itemGameObject.transform);
        }

        items = itemTransforms.ToArray();

    }
}
