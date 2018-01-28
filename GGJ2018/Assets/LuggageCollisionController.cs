using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LuggageCollisionController : MonoBehaviour {

    UnityEvent bagCollision;
    public LuggageController LuggageController;
    [SerializeField]
    private float m_relativeVelocity = 5;

    // Use this for initialization
    void Start () {
        LuggageController = GetComponentInParent<LuggageController>();
        if (bagCollision == null)
        {
            bagCollision = new UnityEvent();
        }
        bagCollision.AddListener(LuggageController.Crush);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !LuggageController.Crushed)
        {
            if (collision.relativeVelocity.magnitude >= m_relativeVelocity)
            {
                bagCollision.Invoke();
            }
        }
    }
}
