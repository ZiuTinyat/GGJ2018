using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour {

    [SerializeField]
    private float m_forceFactor = 100.0f;

    [SerializeField]
    private Transform m_targetTransform;

    private Rigidbody m_rb;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 heading  = m_targetTransform.position - transform.position;
        m_rb.AddForce(heading  * m_forceFactor);
    }
}
