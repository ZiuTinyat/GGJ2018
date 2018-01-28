using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        int layerMask = 1 << 9;
        Physics.Raycast(ray, out hitInfo, 100, layerMask);
        Vector3 hitPoint = hitInfo.point;
        transform.position = new Vector3(hitPoint.x, hitPoint.y, transform.position.z);
	}
}
