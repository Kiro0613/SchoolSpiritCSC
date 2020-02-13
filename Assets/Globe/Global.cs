using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
    public static Player.Player Plr;

    void Start() {
        Plr = GameObject.Find("player").GetComponent<Player.Player>();
    }

    void Update() {
        
    }



}
