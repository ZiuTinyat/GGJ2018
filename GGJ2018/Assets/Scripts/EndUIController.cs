using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour {

    [SerializeField] SpriteRenderer FaderSprite, NBTVSprite, RIPSprite, SubtitleSprite;
    [SerializeField] Text MoneyText;

    private Color TransparentColor = new Color(1f, 1f, 1f, 0f);

    private Color MoneyTextColor;

    // Use this for initialization
    void Start() {
        NBTVSprite.color = TransparentColor;
        RIPSprite.color = TransparentColor;
        SubtitleSprite.color = TransparentColor;
        MoneyTextColor = MoneyText.color;
        MoneyText.color = TransparentColor;
        StartCoroutine(StartUICoroutine());
    }

    IEnumerator StartUICoroutine() {
        yield return new WaitForSeconds(1f);

        // Show end page
        StartCoroutine(FadeSprite(NBTVSprite, 1f, Color.white));
        StartCoroutine(FadeSprite(SubtitleSprite, 1f, Color.white));
        yield return StartCoroutine(FadeSprite(RIPSprite, 1f, Color.white));
        yield return StartCoroutine(FadeText(MoneyText, 1f, TransparentColor));

        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("start");
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

    IEnumerator FadeText(Text UI, float duration, Color start) {
        int money = GameController.Money;
        UI.text += money.ToString() + ".00";
        float t = 0f;
        while (t < duration) {
            UI.color = Color.Lerp(start, MoneyTextColor, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
