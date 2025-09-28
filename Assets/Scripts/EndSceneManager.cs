using UnityEngine;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    public Text endMessageText; // 終了メッセージを表示するテキスト
    public Text finalScoreText; // スコアを表示するテキスト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 保存されている文字列を取得。第二引数はもし第一引数のEndMessageがなかったら表示する値
        string endMessage = PlayerPrefs.GetString("EndMessage", "結果なし");

        // 同様にスコアを取得
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        if (endMessageText != null)
        endMessageText.text = endMessage;

        if (finalScoreText != null)
        finalScoreText.text = "スコア" + finalScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
