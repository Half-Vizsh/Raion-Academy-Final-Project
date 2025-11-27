using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static int ScoreValue = 0;
    public TextMeshProUGUI Score;
    // Start is called before the first frame update
    void Start()
    {
        Score = GetComponent<TextMeshProUGUI >();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score: " + ScoreValue;
    }
}
