using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OnClick()
    {
        //SceneManager.LoadScene("Day 2");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //If you're feeling fancy
    }
}
