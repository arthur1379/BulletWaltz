using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // 呼叫Dotween函式庫

public class TurretManager : MonoBehaviour {

    private Animator _animator; // 宣告一個Animaotor型態的元件

    const int DirectionCount = 8; // 宣告常數DirectionCount
    public Ease RotateEaseFunction; // 宣告Dotween函式庫中的Ease 名稱為RotateEaseFunction
    public float rotateDuration; // 宣告浮點數

    public Camera GameCamera; // 宣告Camera型態 GameCamera
    public float CameraShakeDuration; // 宣告浮點數 CameraShakeDuration
    public float CameraShakeStrenth; // 宣告浮點數 CameraShakeStrenth

    public GameObject bulletCandidate; // 宣告遊戲物件GameObject  bulletCandidate
    private float bulletOffset = 0.6f; // 宣告浮點數 bulletOffset

    public ScoreManager scoreManager;
    public GameLoopManager gameLoopManager;

    void Start () {
        _animator = this.GetComponent<Animator>(); // 指定Turret物件中的Animator元件進來
	}

    public void PlayShootAnimation(){ // 自行定義一個 PlayShootAnimation 函式
        _animator.SetTrigger("Shoot"); // 觸發Animator狀態機中的 Trigger - Shoot
        GameCamera.transform.DOShakePosition(CameraShakeDuration, CameraShakeStrenth); // 讓相機有震動的功能

        scoreManager.AddScore(1);

        GameObject bulletobj = GameObject.Instantiate(bulletCandidate); // 動態生成 bulletCandidate (就是BulletCandidate預置物)
        BulletScript bulletScript = bulletobj.GetComponent<BulletScript>(); // 從動態生成bulletCandidate物件上 擷取物件上的BulletScript
        bulletScript.transform.position = this.transform.position + bulletOffset * this.gameObject.transform.right; // 子彈的位置 = 子彈目前位置 + 0.6f * Turret物件的X軸 (1,0,0)
        bulletScript.transform.rotation = this.transform.rotation; // 子彈的旋轉值 = 大砲物件的旋轉值
        Vector3 shootDirection3D = this.gameObject.transform.right; // Vector3 shootDirection3D 為 Turret物件的X軸 
        Vector2 shootDirection2D = new Vector2(shootDirection3D.x, shootDirection3D.y); // 將 shootDirection3D X 和 Y 指定給 Vector2 shootDirection2D
        bulletScript.InitAndShoot(shootDirection2D); // 執行 bulletScript.cs 程式中的 InitAndShoot 程式 給定方向 shootDirection2D 讓程式帶入計算

        gameLoopManager.bullets.Add(bulletScript);
    }

    public void PlayRotateAnimation() // 控制Turret的旋轉
    {
        float targetDegree = 360.0f / DirectionCount * Random.Range(0, DirectionCount); // targetDegree 的數字使用隨機的數字來產生
        this.transform.DORotate(new Vector3(0, 0, targetDegree), rotateDuration); // 讓Turret的旋轉產生圓滑的效果
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){ // 當偵測到空白鍵按下 執行 PlayShootAnimation 函式
            PlayShootAnimation();
        }

        if (Input.GetKeyDown(KeyCode.R)) // 每當偵測到R鍵按下 執行 PlayRotateAnimation 函式
        {
            PlayRotateAnimation();
        }
    }
}
