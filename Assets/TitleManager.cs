using UnityEngine;
using UnityEngine.SceneManagement; // シーンのロードを行うための名前空間

public class TitleManager : MonoBehaviour
{
    public void StartEasyGame()
    {
        PlayerPrefs.SetInt("GridSize", 4); // グリッドサイズを4x4に設定
        SceneManager.LoadScene("MainScene"); // ゲームシーンをロード
    }

    public void StartHardGame()
    {
        PlayerPrefs.SetInt("GridSize", 6); // グリッドサイズを6x6に設定
        SceneManager.LoadScene("MainScene");
    }
}
