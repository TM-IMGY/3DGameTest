using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool gameEnded = false;
    private int score = 0;
    private float timer = 100f;
    private string endMessage = "";

    public Text timerText;
    public Text scoreText;
    public AudioSource audioSource;
    public AudioClip itemGetSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;

        // 毎フレームからの経過時間を取得し、タイムから差し引く
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameOver(false);
        }

        UpdateUI();
    }

    public void AddScore()
    {
        // スコアを増やす
        if (gameEnded) return;

        score++;

        if (audioSource != null && itemGetSound != null)
        {
            audioSource.PlayOneShot(itemGetSound);
        }

        UpdateUI();
    }

    // 引数はtrue、false問わずにゲーム終了になる
    // trueは敵にあたった場合、falseは敵にあたる以外ゲーム終了
    public void GameOver(bool hitEnemy)
    {
         if (gameEnded) return;

         gameEnded = true;

        if (!hitEnemy && score > 0)
        {
            endMessage = "CLEAR";
        }
        else
        {
            endMessage = "GAME OVER";
        }

        // エンドゲームシーンに値を渡すための保存機能
        // SetStringは文字列を保存する。endMessageの値をEndMessageに保存する。
        PlayerPrefs.SetString("EndMessage", endMessage);

        // 上記同様にスコアを保存
        PlayerPrefs.SetInt("FinalScore", score);

        SceneManager.LoadScene("EndGameScene");
    }

    private void UpdateUI()
    {
        // スコア更新
        if (scoreText != null)
        scoreText.text = "Score:" + score;

        // 残り時間を更新
        // Mathf.Ceilは小数点以下の切り上げ・切り捨てをする
        if (timerText != null)
        timerText.text = "Time:" + Mathf.Ceil(timer);
    }
}
