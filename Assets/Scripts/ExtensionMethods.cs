using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static Vector2 flat(this Vector3 vector) {
        return new Vector2(vector.x, vector.z);
    }
}
