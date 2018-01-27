using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    [SerializeField]
    private Rigidbody m_hand;


    public Rigidbody NearbyGrabbleRb;
    //[SerializeField]
    //private Rigidbody m_rightHand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GrabNearbyGrabbleThing ()
    {
        
    }

    public void TryGrab()
    {
        if (NearbyGrabbleRb)
        {
            GrabNearbyGrabbleThing();
        }
    }
}
