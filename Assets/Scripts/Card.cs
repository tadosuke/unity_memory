using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro���g�p���邽�߂̖��O���

public class Card : MonoBehaviour
{
    public TextMeshProUGUI cardText; // Text����TextMeshProUGUI�ɕύX
    private string cardValue;
    private bool isFlipped = false;
    private GameManager gameManager;
    private bool isLocked = false; // ��x��v�����J�[�h�����b�N���邽�߂̃t���O
    private Image cardImage; // Image�R���|�[�l���g��ǉ�

    // ��v���̐F
    public Color matchedColor = Color.green;
    // ��v���Ȃ����̐F
    public Color defaultColor = Color.white;

    void Start()
    {
        cardImage = GetComponent<Image>(); // Image�R���|�[�l���g���擾
        cardImage.color = defaultColor; // ������Ԃ̐F�ɐݒ�
    }

    public void SetCard(string value, GameManager manager)
    {
        cardValue = value;
        cardText.text = ""; // ������Ԃł̓e�L�X�g���\��
        gameManager = manager; // GameManager�̎Q�Ƃ�ێ�
    }

    public void OnCardClicked()
    {
        if (!isFlipped && !isLocked && gameManager.CanClick) // CanClick��ǉ�
        {
            FlipCard();
            gameManager.OnCardSelected(this);
        }
    }

    private void FlipCard()
    {
        isFlipped = true;
        cardText.text = cardValue; // �J�[�h�̃e�L�X�g��\��
    }

    public void FlipBack()
    {
        isFlipped = false;
        cardText.text = ""; // �J�[�h�̃e�L�X�g���\���ɖ߂�
    }

    public void LockCard()
    {
        isLocked = true; // �J�[�h�����b�N���Ă���ȏ㑀��ł��Ȃ��悤�ɂ���
        cardImage.color = matchedColor; // ��v�����J�[�h�̐F��ύX
    }

    public void SetDefaultColor()
    {
        cardImage.color = defaultColor; // �f�t�H���g�̐F�ɖ߂�
    }

    public string GetCardValue()
    {
        return cardValue;
    }
}
