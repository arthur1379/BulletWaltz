using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Rigidbody2D rigidbody2D; // Ridigbody2D元件
    private SpriteRenderer spriteRenderer; // SpriteRenderer元件

    private float speed = 2; // 設定速度2 讓子彈慢一點

    const float flashDuration = 0.1f;
    float flashCounter = 0;

    public void InitAndShoot(Vector2 direction) // 外部函式
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>(); // 指定BulletCandidate的Rigidbody2D
        spriteRenderer = this.GetComponent<SpriteRenderer>(); // 指定BulletCandidate的SpriteRenderer
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); // 變更顏色為白色
        rigidbody2D.velocity = speed * direction;  // 往direction這個方向前進

        flashCounter = flashDuration;  // 設定 flashCounter = 0.1f
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

        float rotationZ = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg; // 計算子彈旋轉的角度 算兩個角度的中間 * (PI/2)
        Debug.Log(rotationZ);
        this.transform.eulerAngles = new Vector3(0, 0, rotationZ); // 設定子彈旋轉的角度

        if (flashCounter > 0) // 當 flashCounter 大於 0 時
        {
            flashCounter -= Time.deltaTime; // 從0.1開始倒數
            spriteRenderer.color = Color.white; // 子彈變為白色
        }
        else
        {
            spriteRenderer.color = Color.green; // 子彈變為綠色
        }
    }

    void OnCollisionEnter2D(Collision2D coll) // 當子彈物件碰撞到其他物件時
    {
        flashCounter = flashDuration; // 設定 flashCounter = 0.1f
    }
}
