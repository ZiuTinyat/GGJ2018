using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    private AudioSource m_successSoundSource;

    private void Start()
    {
        m_successSoundSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Luggage.Tag) {
            GameController.AddScore(Luggage.ValueTable[other.GetComponent<Luggage>().Type]);
            if (!m_successSoundSource.isPlaying)
            {
                m_successSoundSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == Luggage.Tag) {
            GameController.AddScore(-Luggage.ValueTable[other.GetComponent<Luggage>().Type]);
        }
    }
}
