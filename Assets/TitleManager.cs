using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���̃��[�h���s�����߂̖��O���

public class TitleManager : MonoBehaviour
{
    public void StartEasyGame()
    {
        PlayerPrefs.SetInt("GridSize", 4); // �O���b�h�T�C�Y��4x4�ɐݒ�
        SceneManager.LoadScene("MainScene"); // �Q�[���V�[�������[�h
    }

    public void StartHardGame()
    {
        PlayerPrefs.SetInt("GridSize", 6); // �O���b�h�T�C�Y��6x6�ɐݒ�
        SceneManager.LoadScene("MainScene");
    }
}
