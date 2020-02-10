using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    //public float timer;
    //public float radius;
    public AudioSource audioSource;
    //public AudioClip sound;
    public float volume;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void play(AudioClip sound, float radius) {
        audioSource.clip = sound;
        audioSource.Play();

        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius, 1 << 9);
        foreach(Collider enemy in objectsInRange) {
            volume = ((radius - Vector3.Distance(transform.position, enemy.transform.position)) / radius) * 10;
            //enemy.gameObject.GetComponent<Enemy>().hearSound(gameObject);
            Debug.Log("Radius - " + radius + "\nDistance - " + Vector3.Distance(transform.position, enemy.transform.position) + "  |  Volume - " + volume);
        }
    }

    private void getEnemiesInRange() {
        
    }
}