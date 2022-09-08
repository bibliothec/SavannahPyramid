using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
動物のオブジェクトにこのスクリプト、Rigidbody2D、PolyginCollider2Dをアタッチする
Rigidbody2Dのgravityはゼロにする
Rigidbody2DにPhysicsMaterial2Dをつける
動物にはedibleAnimalかeaterAnimalタグをつける
動物をPreFab化し、GameManagerに渡す
←→で左右に動く、↑↓で回転、Enterで下に落ちる
*/

public class Animal : MonoBehaviour
{

    private bool canMove = true;
    private bool canFall = false;
    private bool canSE = true;

    public static bool isCollision;
    AudioSource se;


    void Start()
    {
        var material = GetComponent<Rigidbody2D>().sharedMaterial;
        material.friction = 1.0f;
        material.bounciness = 0.0f;
        se = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        Rotate();
        Enter();
    }

    // 左右の矢印キーで移動回転
    public void Move()
    {
        //左に移動
        if (Input.GetKey(KeyCode.LeftArrow) && canMove == true && transform.position.x > -2.0f) {
            transform.Translate(-0.1f, 0.0f, 0.0f, Space.World);

        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow) && canMove == true && transform.position.x < 2.0f) {
            transform.Translate (0.1f, 0.0f, 0.0f, Space.World);

        }
    }

    // 上下の矢印キーで回転
    public void Rotate()
    {
        //右に回転
        if (Input.GetKeyDown(KeyCode.UpArrow) && canMove == true)
        {
            gameObject.transform.Rotate (new Vector3(0, 0, 45));
        }
        //左に回転
        if (Input.GetKeyDown(KeyCode.DownArrow) && canMove == true)
        {
            gameObject.transform.Rotate  (new Vector3(0, 0, -45));
        }
    }

    // エンターで落ちる関数
    public void Enter()
    {

        if(Input.GetKeyDown(KeyCode.Return) || GameManager.forceFall == true)
        {
            canMove = false;
            canFall = true;
            if(canSE == true)
            {
                GetComponent<AudioSource>().PlayOneShot(se.clip);
            }
            canSE = false;
        }
            if(canFall)
            {
                Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2 (0.0f,-4.0f));
                //rb.mass = 1.0f;
            }
    }

    // もし自分のタグがedibleAnimalでeaterAnimalをもつタグとぶつかったら消える
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject)
        {
            isCollision = true;
        }
        if((this.gameObject.tag == "snake" || this.gameObject.tag == "usagi") && other.gameObject.tag == "bird")
        {
            Destroy(this.gameObject);
        }
        if(this.gameObject.tag == "buzoku" && other.gameObject.tag == "sasori")
        {
            Destroy(this.gameObject);
        }
        if((this.gameObject.tag == "nu" || this.gameObject.tag == "inpara" || this.gameObject.tag == "honeybadger") && other.gameObject.tag == "lion")
        {
            Destroy(this.gameObject);
        }
        if(this.gameObject.tag == "usagi" && other.gameObject.tag == "snake")
        {
            Destroy(this.gameObject);
        }
        if((this.gameObject.tag == "snake" || this.gameObject.tag == "sasori")&& other.gameObject.tag == "honeybadger")
        {
            Destroy(this.gameObject);
        }
        if((this.gameObject.tag == "zou" || this.gameObject.tag == "sai" || this.gameObject.tag == "dachou") && other.gameObject.tag == "buzoku")
        {
            Destroy(this.gameObject);
        }
        if((this.gameObject.tag == "sima" || this.gameObject.tag == "buzoku") && other.gameObject.tag == "wani")
        {
            Destroy(this.gameObject);
        }
        if((this.gameObject.tag == "usagi" || this.gameObject.tag == "inpara") && other.gameObject.tag == "cheetah")
        {
            Destroy(this.gameObject);
        }
        if((this.gameObject.tag == "snake" || this.gameObject.tag == "bird") && other.gameObject.tag == "jackal")
        {
            Destroy(this.gameObject);
        }
        if(this.gameObject.tag == "buzoku" && other.gameObject.tag == "kaba")
        {
            Destroy(this.gameObject);
        }
    }

}
