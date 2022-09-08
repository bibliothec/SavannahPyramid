using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
空のオブジェクトにアタッチ
GameManagerタグをつける
Prefab化した動物をAnimal[]に入れておく
*/

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Animal[] Animals;
    public int score = 0;
    bool canSpawn = true;
    public bool isGameOver = false;
    bool isEnter = false;
    public static bool forceFall = false;
    public GameObject score_object = null;
    public int animalNum = 0;
    static public int scoreNum = 0;
    static public int scoreHeight = 0;
    public float totalTime = 10.0f;
    float totalTimeSpend = 10.0f;

    float SpawnInterval = 5.0f;
    float SpawnIntervalSpend = 5.0f;
    int seconds;
    //float MoveY = 0.0f;

    void Start()
    {
        //初期位置の指定
        //transform.position = new Vector2(0.0f, 3.4f);
        //動物をスポーン
        SpawnAnimal();
    }

    void Update()
    {
        if(isGameOver)
        {
            transform.position = new Vector2(0.0f, -10.0f);
        }
        if(SceneManager.GetActiveScene().name == "ModeSingle")
        {
            //一定間隔でスポーン(シングル)
            InterSpawn();
            //スコアが高さ
            //スコアが数
            AnimalNumScore();
            //スコアの表示
            ShowScore();
        }else if(SceneManager.GetActiveScene().name == "ModeVs")
        {
            //一定間隔でスポーン(VS)
            player();
        }
    }

    //対戦用のプレイヤーのテキスト表示、スポーン
    public void player()
    {
        Text player_text = GameObject.FindGameObjectWithTag("Player").GetComponent<Text>();
        Text timer_text = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();

        if(animalNum % 2 != 0)
        {
            if(!isEnter)
            {
                player_text.text = "Player1";
                totalTimeSpend -= Time.deltaTime;
                seconds = (int)totalTimeSpend;
                timer_text.text= seconds.ToString();
            }
            if(Input.GetKeyDown(KeyCode.Return) || isEnter == true)
            {
                isEnter = true;
                timer_text.text = " ";
                SpawnIntervalSpend -= Time.deltaTime;
                if(SpawnIntervalSpend <= 0)
                {
                    canSpawn = true;
                    SpawnAnimal();
                }
            }
            if(timer_text.text == "0")
            {
                //ここでゲームオブジェクトを落とす
                forceFall = true;
                isEnter = true;
                SpawnIntervalSpend -= Time.deltaTime;
                if(SpawnIntervalSpend <= 0)
                {
                    canSpawn = true;
                    SpawnAnimal();
                }
            }
        }else{
            if(!isEnter)
            {
                player_text.text = "Player2";
                totalTimeSpend -= Time.deltaTime;
                seconds = (int)totalTimeSpend;
                timer_text.text= seconds.ToString();
            }
            if(Input.GetKeyDown(KeyCode.Return) || isEnter == true)
            {
                isEnter = true;
                timer_text.text = " ";
                SpawnIntervalSpend -= Time.deltaTime;
                if(SpawnIntervalSpend <= 0)
                {
                    canSpawn = true;
                    SpawnAnimal();
                }
            }
            if(timer_text.text == "0")
            {
                //ここでゲームオブジェクトを落とす
                forceFall = true;
                isEnter = true;
                SpawnIntervalSpend -= Time.deltaTime;
                if(SpawnIntervalSpend <= 0)
                {
                    canSpawn = true;
                    SpawnAnimal();
                }
            }
        }
    }


    // シングル用エンターを押してから時間経過で次のスポーン
    void InterSpawn()
    {
        if(Input.GetKeyDown(KeyCode.Return) || isEnter == true)
        {
            isEnter = true;
            SpawnIntervalSpend -= Time.deltaTime;
            if(SpawnIntervalSpend <= 0)
            {
                canSpawn = true;
                SpawnAnimal();
            }
        }
    }

    //  動物をスポーン
    public Animal SpawnAnimal()
    {
        //VS用
        forceFall = false;
        isEnter = false;
        totalTimeSpend = totalTime;
        //これはシングルも
        SpawnIntervalSpend = SpawnInterval;
        if(canSpawn){
            Animal animal = Instantiate(GetRandomAnimal(), transform.position, Quaternion.identity);
            canSpawn = false;

            animalNum++;
            if(animal)
            {
                return animal;
            }else
            {
                return null;
            }
        }else{
            return null;
        }

    }

    // ランダムな画像を選んで返す
    Animal GetRandomAnimal()
    {
        int i = Random.Range(0, Animals.Length);
        if(Animals[i])
        {
            return Animals[i];
        }else
        {
            return null;
        }
    }
    //スコアを他のシーンに渡す
    public static int GetScore(){
            return scoreNum;
        }

//スコア表示
    public void ShowScore()
    {
        Text score_text = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        score_text.text = scoreNum.ToString();
    }

//動物の数でスコア
    public void AnimalNumScore()
    {
        scoreNum = animalNum -1;
    }

// 画像の高さでスコア
    public void ScoreCount()
    {
        GameObject obj = SearchHighestObj();
        if(obj != null)
        {
            scoreHeight =(int)(obj.transform.position.y *10 + 2.0f);
        }else{
            scoreHeight = 0;
        }
    }

    // 最も高いゲームオブジェクトを取得
    public GameObject SearchHighestObj() {
        float highestObjHeight = 0;
        GameObject highestObj = null;
        GameObject[] objs1 = GameObject.FindGameObjectsWithTag("edibleAnimal");
        GameObject[] objs2 = GameObject.FindGameObjectsWithTag("eaterAnimal");
        GameObject[] objs = objs1.Concat(objs2).ToArray();

        if (objs.Length == 0)
        {
            return null;
        }

        foreach (GameObject obj in objs)
        {
            float targetY = (int)obj.transform.position.y;
            if (highestObjHeight < targetY)
            {
                highestObj = obj;
                highestObjHeight = targetY;
            }
        }
        return highestObj;
    }



/*
    void MoveGameManager()
    {
        transform.position = new Vector2(transform.position.x, MoveY);
    }

    // コルーチン、一定時間ごと実行させる
    private IEnumerator GetHighestLoop()
    {
        GameObject highestObj =  SearchHighestObj();
        MoveY = highestObj.transform.position.y;
        // 1秒間待つ
        yield return new WaitForSeconds(1);
    }
*/
}
