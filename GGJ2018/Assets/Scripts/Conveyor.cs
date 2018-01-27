using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {

    public float Speed;
    public bool Direction; // true: forward

    [SerializeField] Transform StartGearTransform, EndGearTransform;
    [SerializeField] Vector3 SpawnOffset;

    [SerializeField] GameObject SpawnUnit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
