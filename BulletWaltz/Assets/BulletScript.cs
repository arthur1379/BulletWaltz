using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Rigidbody2D rigidbody2D; // Ridigbody2D元件
    private SpriteRenderer spriteRenderer; // SpriteRenderer元件

    private float speed = 2; // 設定速度2 讓子彈慢一點


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
        if (rigidbody2D.velocity == Vector2.zero) // 當子彈停下來 速度為0
        {
            //確保沒有人停下來
            rigidbody2D.velocity = new Vector2(Random.Range(0, 1.0f), Random.Range(0, 1.0f)).normalized * speed; // X Y 隨機方向 給一個速度2
        }
        else
        {
            //確保碰撞後速度不變
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed; // 保持同一個方向的速度
        }
    }
}
