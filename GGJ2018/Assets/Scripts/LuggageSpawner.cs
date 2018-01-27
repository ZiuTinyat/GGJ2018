using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageSpawner : MonoBehaviour {
    
    public GameObject[] LuggageList;
    public bool Spawing = true;

	// Use this for initialization
	void Start () {
		if (LuggageList.Length == 0) {
            Spawing = false;
        }
        StartCoroutine(SpawnCoroutine());
	}

    IEnumerator SpawnCoroutine() {
        float interval = 4f; // temporary
        do {
            yield return new WaitForSeconds(interval);
            yield return new WaitUntil(() => Spawing);
            SpawLuggage();

        } while (true);

    }

    void SpawLuggage() {
        int index = Random.Range(0, LuggageList.Length);
        GameObject luggage = Instantiate(LuggageList[index], transform.position, Random.rotation, null);
        luggage.GetComponent<Rigidbody>().AddForce(5f * Vector3.right, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
