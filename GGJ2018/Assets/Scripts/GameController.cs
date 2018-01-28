using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    public static int Score { get; private set; }
    public static int Money { get; private set; }
    private static int ScoreStartSize;

    [SerializeField] Text ScoreText;
    
    public static void AddScore(int add) {
        Score += add;
        Instance.ScoreText.text = Score.ToString();
        Instance.StopCoroutine(ScoreEffect());
        Instance.StartCoroutine(ScoreEffect());
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
    }

    private void Start() {
        ScoreStartSize = Instance.ScoreText.fontSize;
    }
}
