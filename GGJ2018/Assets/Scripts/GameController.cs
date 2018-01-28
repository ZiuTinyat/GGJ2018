using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    public static int Score { get; private set; }
    public static int Money { get; private set; }
    private static int ScoreStartSize;

    public float TotTime = 90f;

    [SerializeField] Text ScoreText, TimeDownText;
    
    public static void AddScore(int add) {
        Score += add;
        Instance.ScoreText.text = Score.ToString();
        Instance.StopCoroutine(ScoreEffect());
        Instance.StartCoroutine(ScoreEffect());
    }

    public static void AddMoney(int money)
    {
        Money += money;
    }

    static IEnumerator GameProcess() {
        Instance.TimeDownText.text = Mathf.RoundToInt(Instance.TotTime).ToString();
        float time = Instance.TotTime;
        while (time > 0) {

            if (time < 10f && LuggageSpawner.AllowSpawn) { // stop spawn
                LuggageSpawner.AllowSpawn = false;
            }

            Instance.TimeDownText.text = Mathf.CeilToInt(time).ToString();
            time -= Time.deltaTime;
            yield return null;
        }
        // end game
        
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("end");
    }

    static IEnumerator ScoreEffect() {
        int increase = 40;
        while (increase > 0) {
            Instance.ScoreText.fontSize = ScoreStartSize + increase;
            increase--;
            yield return null;
        }
    }

    private void Awake() {
        Instance = this;
        Score = 0;
        Money = 0;
    }

    private void Start() {
        ScoreStartSize = Instance.ScoreText.fontSize;
        StartCoroutine(GameProcess());
    }
}
