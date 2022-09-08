using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleResult : MonoBehaviour
{
    // Start is called before the first frame update
    int score;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        score = GameManager.GetScore();
        Text result_text = GameObject.FindGameObjectWithTag("Result").GetComponent<Text>();
        result_text.text = "Score: " + score.ToString();
    }
}
