using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIController : MonoBehaviour {

    [SerializeField] SpriteRenderer FaderSprite, CreditSprite, StartSprite, ButtonSprite;

    private Color TransparentColor = new Color(1f, 1f, 1f, 0f);

	// Use this for initialization
	void Start () {
        CreditSprite.color = TransparentColor;
        StartSprite.color = TransparentColor;
        ButtonSprite.color = TransparentColor;
        StartCoroutine(StartUICoroutine());
	}

    IEnumerator StartUICoroutine() {
        yield return new WaitForSeconds(1f);

        // Show credit sprite
        // TODO: Audio
        yield return StartCoroutine(FadeSprite(CreditSprite, 1f, Color.white));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeSprite(CreditSprite, 1f, TransparentColor));
        yield return new WaitForSeconds(1f);

        // Show start page
        yield return StartCoroutine(FadeSprite(StartSprite, 1f, Color.white));
        yield return StartCoroutine(FadeSprite(ButtonSprite, 1f, Color.white));

        // Wait for click
        yield return new WaitUntil(() => (Input.anyKeyDown || Input.GetMouseButtonDown(0)));

        // Start Game
        StartCoroutine(FadeSprite(StartSprite, 1f, TransparentColor));
        yield return StartCoroutine(FadeSprite(ButtonSprite, 1f, TransparentColor));
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    IEnumerator FadeSprite(SpriteRenderer sprite, float duration, Color end) {
        Color start = sprite.color;
        float t = 0f;
        while (t < duration) {
            sprite.color = Color.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
