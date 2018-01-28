using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageSpawner : MonoBehaviour {

    public static LuggageSpawner Instance;
    public static float SpawnInterval;
    public static bool AllowSpawn;
    
    public GameObject[] LuggageList;
    private bool Spawning = false;

    private void Awake() {
        Instance = this;
        SpawnInterval = 10f;
        AllowSpawn = true;
    }

    // Use this for initialization
    void Start () {
        
	}

    public void StartSpawn() {
        if (Spawning) return;
        Spawning = true;
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() {
        //float interval = 4f; // temporary
        do {
            yield return new WaitUntil(() => Spawning);
            SpawLuggage();
            yield return new WaitForSeconds(SpawnInterval);

        } while (AllowSpawn);

    }

    void SpawLuggage() {
        int index = Random.Range(0, LuggageList.Length);
        GameObject luggage = Instantiate(LuggageList[index], transform.position, Random.rotation, null);
        //luggage.GetComponent<Rigidbody>().AddForce(5f * Vector3.right, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
