using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // 接触した余計なオブジェクトを削除
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject)
        {
            Destroy (other.gameObject);
        }
    }
}
