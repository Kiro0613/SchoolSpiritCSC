using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitTextureToWindow : MonoBehaviour {
    public RenderTexture texture;

    // Start is called before the first frame update
    void Start() {
        texture.width = Screen.width;
        texture.height = Screen.height;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
