using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour {

    private Animator _animator; // 宣告一個Animaotor型態的元件

	void Start () {
        _animator = this.GetComponent<Animator>(); // 指定Turret物件中的Animator元件進來
	}

    private void PlayShootAnimation(){ // 自行定義一個 PlayShootAnimation 函式
        _animator.SetTrigger("Shoot"); // 觸發Animator狀態機中的 Trigger - Shoot
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){ // 當偵測到空白鍵按下 執行 PlayShootAnimation 函式
            PlayShootAnimation();
        }
	}
}
