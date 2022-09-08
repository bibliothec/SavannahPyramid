using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
カメラにアタッチする
GameManager.csが必須
*/

public class Camera : MonoBehaviour
{

    void Start()
    {
        MoveCamera();
    }

    void Update()
    {
        MoveCamera();
    }
    //カメラのy座標がGameManagerと同じ位置になるようにする
    void MoveCamera()
    {
        GameObject spawnPos = GameObject.FindWithTag("GameManager");
        transform.position = new Vector3(transform.position.x, spawnPos.transform.position.y - 2.0f, transform.position.z);
    }
}
