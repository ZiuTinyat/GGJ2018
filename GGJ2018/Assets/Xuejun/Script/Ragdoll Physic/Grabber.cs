using UnityEngine;
using Sirenix.OdinInspector;


public class Grabber : MonoBehaviour
{
    [ReadOnly]
    public ConfigurableJoint Joint;
    [ReadOnly]
    public FixedJoint Joint2;

    //public Grab MyGrab;
    private GrabHandler m_grabHandler;
    private Rigidbody m_rb;
    private Rigidbody m_rbToGrab;

    private void Start()
    {
        m_grabHandler = transform.root.GetComponent<GrabHandler>();
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!m_grabHandler.Grabbing && !Joint)
        {
            Destroy(Joint);
        }

        if (!m_grabHandler.Grabbing && !Joint2)
        {
            Destroy(Joint2);
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_grabHandler.Grabbing = true;
            Debug.Log("Mouse Down, Start Grab");
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

        if (Joint2)
        {
            Destroy(Joint2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (m_grabHandler && m_grabHandler.m_grabStrength >= 0.5f && m_grabHandler.Grabbing &&
        //    !Joint && collision.gameObject.layer ==10 )
        //{
        //    if (collision.rigidbody)
        //    {
        //        foreach (var grabber in m_grabHandler.m_grabbers)
        //        {
        //            if (grabber.Joint && grabber.Joint.connectedBody != null)
        //            {
        //                return;
        //            }
        //        }
        //        m_grabHandler.StartGrab(collision.rigidbody);
        //    }

        //    Joint = m_rb.gameObject.AddComponent<ConfigurableJoint>();
        //    Joint.xMotion = ConfigurableJointMotion.Locked;
        //    Joint.yMotion = ConfigurableJointMotion.Locked;
        //    Joint.zMotion = ConfigurableJointMotion.Locked;
        //    Joint.angularXMotion = ConfigurableJointMotion.Locked;
        //    Joint.angularYMotion = ConfigurableJointMotion.Locked;
        //    Joint.angularZMotion = ConfigurableJointMotion.Locked;
        //    Joint.projectionMode = JointProjectionMode.PositionAndRotation;
        //    Joint.anchor = transform.InverseTransformPoint(collision.contacts[0].point);
        //    if (collision.gameObject.GetComponent<Rigidbody>())
        //    {
        //        Joint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
        //    }


        if (m_grabHandler && m_grabHandler.m_grabStrength >= 0.5f && m_grabHandler.Grabbing && !Joint2 && collision.gameObject.layer == 10)
        {
                if (collision.rigidbody)
                {
                    foreach (var grabber in m_grabHandler.m_grabbers)
                    {
                        if (grabber.Joint2 && grabber.Joint2.connectedBody != null)
                        {
                            return;
                        }
                    }
                    m_grabHandler.StartGrab(collision.rigidbody);
                }

            Joint2 = m_rb.gameObject.AddComponent<FixedJoint>();
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
                Joint2.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
                if (collision.gameObject.GetComponentInParent<LuggageController>())
                {
                    LuggageController controller = collision.gameObject.GetComponentInParent<LuggageController>();
                    collision.gameObject.GetComponentInParent<LuggageController>().InHand = true;
                    Debug.Log(collision.gameObject.GetComponentInParent<LuggageController>().InHand);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (collision.gameObject.GetComponentInParent<LuggageController>())
            {
                if (collision.gameObject.GetComponentInParent<LuggageController>().InHand)
                {
                    collision.gameObject.GetComponentInParent<LuggageController>().InHand = false;
                }
            }
        }
    }
}
