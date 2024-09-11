using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用するための名前空間

public class Card : MonoBehaviour
{
    public TextMeshProUGUI cardText; // TextからTextMeshProUGUIに変更
    private string cardValue;
    private bool isFlipped = false;
    private GameManager gameManager;
    private bool isLocked = false; // 一度一致したカードをロックするためのフラグ
    private Image cardImage; // Imageコンポーネントを追加

    // 一致時の色
    public Color matchedColor = Color.green;
    // 一致しない時の色
    public Color defaultColor = Color.white;

    void Start()
    {
        cardImage = GetComponent<Image>(); // Imageコンポーネントを取得
        cardImage.color = defaultColor; // 初期状態の色に設定
    }

    public void SetCard(string value, GameManager manager)
    {
        cardValue = value;
        cardText.text = ""; // 初期状態ではテキストを非表示
        gameManager = manager; // GameManagerの参照を保持
    }

    public void OnCardClicked()
    {
        if (!isFlipped && !isLocked && gameManager.CanClick) // CanClickを追加
        {
            FlipCard();
            gameManager.OnCardSelected(this);
        }
    }

    private void FlipCard()
    {
        isFlipped = true;
        cardText.text = cardValue; // カードのテキストを表示
    }

    public void FlipBack()
    {
        isFlipped = false;
        cardText.text = ""; // カードのテキストを非表示に戻す
    }

    public void LockCard()
    {
        isLocked = true; // カードをロックしてこれ以上操作できないようにする
        cardImage.color = matchedColor; // 一致したカードの色を変更
    }

    public void SetDefaultColor()
    {
        cardImage.color = defaultColor; // デフォルトの色に戻す
    }

    public string GetCardValue()
    {
        return cardValue;
    }
}
