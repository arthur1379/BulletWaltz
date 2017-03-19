using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Rigidbody2D rigidbody2D; // Ridigbody2D元件
    private SpriteRenderer spriteRenderer; // SpriteRenderer元件
    private float speed = 5; // 設定速度5

    public void InitAndShoot(Vector2 direction) // 外部函式
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>(); // 指定BulletCandidate的Rigidbody2D
        spriteRenderer = this.GetComponent<SpriteRenderer>(); // 指定BulletCandidate的SpriteRenderer
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); // 變更顏色為白色
        rigidbody2D.velocity = speed * direction;  // 往direction這個方向前進
    }
    // Update is called once per frame
    void Update()
    {

    }
}
