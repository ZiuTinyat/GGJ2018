using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GrabHandler : MonoBehaviour
{
    [BoxGroup("Bools")]
    public bool Grabbing;

    [BoxGroup("Bools")]
    public bool HoldSomething;

    [BoxGroup("Bools")]
    public bool HoldSomethingAnchored;

    [BoxGroup("Bools")]
    public bool CarrySomething;

    [BoxGroup("Grabbers"), ReadOnly]
    public Grabber[] m_grabbers;

    [BoxGroup("Grabbers")]
    public float m_grabStrength;

    private void Start()
    {
        m_grabbers = GetComponentsInChildren<Grabber>();
    }

    public void StartGrab(Rigidbody rigidbody)
    {
    }

    public void EndGrab()
    {

    }

}
