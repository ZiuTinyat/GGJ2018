using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    public static int Score { get; private set; }
    public static int Money { get; private set; }
    private static int ScoreStartSize;

    public float TotTime = 90f, ChangeTime = 60f, FinalTime = 30f;
    public float StartInterval = 8f, SecondInterval = 6f, ThirdInterval = 4f;

    [SerializeField] Text ScoreText, TimeDownText;
    [SerializeField] AudioClip ReadyGo, HurryUp, TimeOver;
    public AudioClip Grab;
    
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

    IEnumerator GameProcess() {
        TimeDownText.text = Mathf.RoundToInt(TotTime).ToString();
        LuggageSpawner.SpawnInterval = StartInterval;
        yield return new WaitForSeconds(5.4f);
        StartCoroutine(PlayAudioClip(ReadyGo));
        yield return new WaitForSeconds(2f);

        LuggageSpawner.Instance.StartSpawn();
        float time = TotTime;
        float nextHurryTime = TotTime - Random.Range(10f, 20f);
        while (time > 0) {
            TimeDownText.text = Mathf.CeilToInt(time).ToString();
            time -= Time.deltaTime;

            if (time < ChangeTime && LuggageSpawner.SpawnInterval > SecondInterval) {
                LuggageSpawner.SpawnInterval = 7f;
            }
            if (time < 30f && LuggageSpawner.SpawnInterval > 4f) {
                LuggageSpawner.SpawnInterval = 4f;
            }
            if (time < 10f && LuggageSpawner.AllowSpawn) { // stop spawn
                LuggageSpawner.AllowSpawn = false;
            }

            if (time < nextHurryTime) {
                StartCoroutine(PlayAudioClip(HurryUp));
                nextHurryTime -= Random.Range(10f, 20f);
            }

            yield return null;
        }
        // end game
        TimeDownText.text = "Time over!";
        StartCoroutine(PlayAudioClip(TimeOver));
        yield return new WaitForSeconds(3f);
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

    public IEnumerator PlayAudioClip(AudioClip clip) {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = false;
        source.Play();
        yield return new WaitForSeconds(clip.length + 0.5f);
        //Destroy(source);
    }

    private void Awake() {
        Instance = this;
        Score = 0;
        Money = 0;
    }

    private void Start() {
        ScoreStartSize = Instance.ScoreText.fontSize;
        StartCoroutine(Instance.GameProcess());
    }
}
