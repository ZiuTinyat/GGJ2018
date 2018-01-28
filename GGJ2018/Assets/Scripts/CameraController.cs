using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance { get; private set; }

    public bool FollowEnabled { get; private set; }
    [SerializeField] float DirectionBias = 0f;
    [SerializeField] float SpeedCoef = 1f;
    private Vector3 PlayerRelCenterPos;

    public Transform PlayerAnchor;

    private void Awake () {
        if (!Instance) Instance = this;
        else Debug.LogWarning("Multiple CameraController detected");
    }

    public void ResetCamera() {
        transform.position = new Vector3(PlayerAnchor.position.x, transform.position.y, transform.position.z);
        PlayerRelCenterPos = transform.position - PlayerAnchor.position;
    }

    // Use this for initialization
    IEnumerator Start () {
        //player = PlayerController.Instance;
        yield return null;
        //transform.position = new Vector3(PlayerAnchor.position.x, transform.position.y, transform.position.z);
        PlayerRelCenterPos = transform.position - PlayerAnchor.position;

        FollowEnabled = true;
	}

    public void SetFollowEnabled(bool enabled) {
        if (enabled != FollowEnabled) {
            FollowEnabled = enabled;
            // possible other stuff
        }
    }

    private void MoveToUpdate (Vector3 pos) {
        Vector3 rel = pos - transform.position;
        if (rel.magnitude < 0.05f) return;
        transform.Translate(Mathf.Sqrt(rel.magnitude * 2f) * rel.normalized * SpeedCoef * Time.deltaTime); // sqrt
        //transform.Translate(rel * SpeedCoef * Time.deltaTime); // Linear
        //transform.Translate(rel.magnitude * rel * SpeedCoef * Time.deltaTime); // square
    }

    private void FollowUpdate () {
            MoveToUpdate(PlayerAnchor.position + PlayerRelCenterPos + Vector3.left * DirectionBias);
    }
	
	// Update is called once per frame
	void Update () {
        if (FollowEnabled) {
            FollowUpdate();
        }
	}
}
