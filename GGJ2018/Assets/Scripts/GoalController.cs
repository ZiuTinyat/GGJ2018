using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Luggage.Tag) {
            GameController.AddScore(Luggage.ValueTable[other.GetComponent<Luggage>().Type]);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == Luggage.Tag) {
            GameController.AddScore(-Luggage.ValueTable[other.GetComponent<Luggage>().Type]);
        }
    }
}
