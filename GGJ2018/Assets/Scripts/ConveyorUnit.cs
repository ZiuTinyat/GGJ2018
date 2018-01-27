using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorUnit : MonoBehaviour {
    public float x = 0f;
    public float totX = 0f;
    public float percent { get { return x / totX; } }
    public new Rigidbody rigidbody { get { return GetComponent<Rigidbody>(); } }
}
