using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageController : MonoBehaviour {


    private Rigidbody m_rigidbody;
    public bool Crushed;
    public bool InHand;
    [SerializeField]
    private GameObject originalLuggage;
    [SerializeField]
    private GameObject brokenLuggage;
    private AudioSource m_crackSoundSource;

	// Use this for initialization
	void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        brokenLuggage.SetActive(false);
        m_crackSoundSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Crush()
    {
        Crushed = true;
        Debug.Log("Box Crushed");
        brokenLuggage.SetActive(true);
        brokenLuggage.transform.position = originalLuggage.transform.position;
        brokenLuggage.transform.rotation = originalLuggage.transform.rotation;
        if (!m_crackSoundSource.isPlaying)
        {
            m_crackSoundSource.Play();
        }
        Destroy(originalLuggage);
        Invoke("DestroyObject", 10f);
    } 

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
