using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを使用するための名前空間
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardContainer;
    public int gridSize = 4; // グリッドのサイズ (4x4)
    public TextMeshProUGUI clearText; // Clearテキストを参照するための変数

    private GridLayoutGroup gridLayout; // GridLayoutGroup の参照
    private List<string> cardValues = new List<string>();
    private List<GameObject> cards = new List<GameObject>();
    private int matchCount = 0; // 一致したペアの数を追跡
    private Card firstCard = null; // 最初に選択されたカード
    private Card secondCard = null; // 次に選択されたカード
    private bool canClick = true; // カードがクリックできるかどうかのフラグ

    void Start()
    {
        gridSize = PlayerPrefs.GetInt("GridSize", 4); // デフォルトで4x4のグリッドサイズ
        gridLayout = cardContainer.GetComponent<GridLayoutGroup>(); // GridLayoutGroup を取得
        AdjustCardSize(); // カードサイズの調整
        InitializeCardValues();
        CreateCards();
        clearText.gameObject.SetActive(false); // 初期状態でClear!テキストを非表示にする
    }

    void AdjustCardSize()
    {
        // GridLayoutGroup の設定を更新
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount; // 列の数を固定
        gridLayout.constraintCount = gridSize; // 選択された難易度に応じた列の数を設定

        // 親オブジェクトの幅と高さを取得
        RectTransform containerRect = cardContainer.GetComponent<RectTransform>();
        float containerWidth = containerRect.rect.width;
        float containerHeight = containerRect.rect.height;

        // カードのセルサイズを計算（グリッドのサイズに基づいて動的に調整）
        float cellWidth = containerWidth / gridSize - gridLayout.spacing.x;
        float cellHeight = containerHeight / gridSize - gridLayout.spacing.y;

        // カードのセルサイズを設定
        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }

    void InitializeCardValues()
    {
        // カードのペアをA〜Zの範囲で生成
        for (char c = 'A'; c < 'A' + (gridSize * gridSize) / 2; c++)
        {
            cardValues.Add(c.ToString());
            cardValues.Add(c.ToString());
        }

        // カードの値をシャッフル
        for (int i = 0; i < cardValues.Count; i++)
        {
            string temp = cardValues[i];
            int randomIndex = Random.Range(i, cardValues.Count);
            cardValues[i] = cardValues[randomIndex];
            cardValues[randomIndex] = temp;
        }
    }

    void CreateCards()
    {
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardContainer);
            Card card = cardObj.GetComponent<Card>();
            card.SetCard(cardValues[i], this); // カードの値とGameManagerの参照を渡す
            cards.Add(cardObj);
        }
    }

    public bool CanClick
    {
        get { return canClick; }
    }

    public void OnCardSelected(Card selectedCard)
    {
        if (!canClick) return; // 判定中であればクリックを無視する

        if (firstCard == null)
        {
            firstCard = selectedCard; // 最初に選択されたカードを記録
        }
        else if (secondCard == null && firstCard != selectedCard)
        {
            secondCard = selectedCard; // 次に選択されたカードを記録

            // 一致判定を行う
            canClick = false; // 判定中はクリックを無効化する
            StartCoroutine(CheckMatch());
        }
    }

    private System.Collections.IEnumerator CheckMatch()
    {
        // 少し待ってから判定を行う
        yield return new WaitForSeconds(1f);

        if (firstCard.GetCardValue() == secondCard.GetCardValue())
        {
            // 一致している場合、カードを固定する
            Debug.Log("カードが一致しました！");
            firstCard.LockCard();
            secondCard.LockCard();
            matchCount++; // 一致したペアの数を増やす

            if (matchCount == (gridSize * gridSize) / 2)
            {
                ShowClearText(); // 全てのペアが一致した場合にClear!テキストを表示
            }
        }
        else
        {
            // 一致していない場合、カードを裏返す
            Debug.Log("カードが一致していません。");
            firstCard.FlipBack();
            secondCard.FlipBack();
            firstCard.SetDefaultColor(); // 色をデフォルトに戻す
            secondCard.SetDefaultColor(); // 色をデフォルトに戻す
        }

        // 判定後、クリックを有効化する
        firstCard = null;
        secondCard = null;
        canClick = true;
    }

    private void ShowClearText()
    {
        clearText.gameObject.SetActive(true); // Clear!テキストを表示
    }
}
