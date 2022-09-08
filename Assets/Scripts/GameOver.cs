using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
画面外の下にオブジェクトを配置し、動物が落ちてそこに触れればゲームオーバーになるようにする
このオブジェクトは横長になるようにする
BoxCollider2Dをオブジェクトにアタッチ
*/

public class GameOver : MonoBehaviour
{

    // 動物が触れたらゲームオーバー
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject)
        {
            if(SceneManager.GetActiveScene().name == "ModeSingle")
            {
                SceneManager.LoadScene("GameOverSingle");
            }
            if(SceneManager.GetActiveScene().name == "ModeVs")
            {
                SceneManager.LoadScene("GameOverVs");
            }
        }
    }

}
