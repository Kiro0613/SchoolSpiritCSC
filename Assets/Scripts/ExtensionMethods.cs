using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Flat {
    public enum V2 {
        xy, xz,
        yx, yz,
        zx, zy
    }

    public static Vector2 flat(this Vector3 vector, V2 parts = V2.xz) {
        switch(parts) {
            case V2.xy: return new Vector2(vector.x, vector.y);
            case V2.xz: return new Vector2(vector.x, vector.z);
            case V2.yx: return new Vector2(vector.y, vector.x);
            case V2.yz: return new Vector2(vector.y, vector.z);
            case V2.zx: return new Vector2(vector.z, vector.x);
            case V2.zy: return new Vector2(vector.z, vector.y);
            default: throw new BadVectorComponents("Flat() could not create a V2 with specified components.");
        }
    }

    public static float flatDistTo(this Vector2 start, Vector2 end) {
        return (end - start).magnitude;
    }

    public static float flatDistTo(this Vector3 start, Vector3 end, V2 parts = V2.xz) {
        return (end.flat(parts) - start.flat(parts)).magnitude;
    }
}

public class BadVectorComponents : Exception {
    public BadVectorComponents() {

    }

    public BadVectorComponents(string message) : base(message) {

    }

    public BadVectorComponents(string message, Exception inner) : base(message, inner) {

    }
}
