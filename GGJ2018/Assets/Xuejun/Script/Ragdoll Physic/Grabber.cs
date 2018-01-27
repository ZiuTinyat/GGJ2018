using UnityEngine;
using Sirenix.OdinInspector;


public class Grabber : MonoBehaviour
{
    [ReadOnly]
    public ConfigurableJoint Joint;

    //public Grab MyGrab;
    private GrabHandler m_grabHandler;
    private Rigidbody m_rb;
    private Rigidbody m_rbToGrab;

    private void Start()
    {
        m_grabHandler = transform.root.GetComponent<GrabHandler>();
        m_rb = GetComponent<Rigidbody>();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.rigidbody.gameObject.layer == 10)
    //    {
    //        m_rbToGrab = collision.rigidbody;
    //    }
    //}

    private void Update()
    {
        if (!m_grabHandler.Grabbing && !Joint)
        {
            Destroy(Joint);
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_grabHandler.Grabbing = true;
            Debug.Log("Mouse Down, Start Grab");
            StartGrab();
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_grabHandler.Grabbing = false;
            Debug.Log("Mouse Up, Realse Grab");
            RealseGrab();
        }
    }

    public void RealseGrab()
    {
        if(Joint)
        {
            Destroy(Joint);
        }
    }

    public void StartGrab()
    {
        if (m_rbToGrab)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = m_rbToGrab;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_grabHandler && m_grabHandler.m_grabStrength >= 0.5f && m_grabHandler.Grabbing &&
            !Joint && collision.gameObject.layer ==10 )
        {
            if (collision.rigidbody)
            {
                foreach (var grabber in m_grabHandler.m_grabbers)
                {
                    if (grabber.Joint && grabber.Joint.connectedBody != null)
                    {
                        return;
                    }
                }

                m_grabHandler.StartGrab(collision.rigidbody);
            }

            Joint = m_rb.gameObject.AddComponent<ConfigurableJoint>();
            Joint.xMotion = ConfigurableJointMotion.Locked;
            Joint.yMotion = ConfigurableJointMotion.Locked;
            Joint.zMotion = ConfigurableJointMotion.Locked;
            Joint.angularXMotion = ConfigurableJointMotion.Locked;
            Joint.angularYMotion = ConfigurableJointMotion.Locked;
            Joint.angularZMotion = ConfigurableJointMotion.Locked;
            Joint.projectionMode = JointProjectionMode.PositionAndRotation;
            Joint.anchor = transform.InverseTransformPoint(collision.contacts[0].point);
            if (collision.rigidbody) Joint.connectedBody = collision.rigidbody;
        }
    }
}
