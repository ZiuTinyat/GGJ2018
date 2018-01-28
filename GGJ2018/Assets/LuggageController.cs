using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageController : MonoBehaviour {

    private Rigidbody m_rigidbody;
    public bool Crushed;

    [SerializeField]
    private GameObject originalLuggage;

    [SerializeField]
    private GameObject brokenLuggage;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        brokenLuggage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Crush()
    {
        Crushed = true;
        Debug.Log("Box Crushed");
        brokenLuggage.SetActive(true);
        brokenLuggage.transform.position = originalLuggage.transform.position;
        brokenLuggage.transform.rotation = originalLuggage.transform.rotation;
        Destroy(originalLuggage);
        Invoke("DestroyObject", 10f);
    } 

    private void DestroyObject() {
        Destroy(gameObject);
    }

}
