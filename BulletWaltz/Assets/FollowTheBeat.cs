using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheBeat : MonoBehaviour {

    
    private const float beatPeriod = 1.485f; //節奏時間距離
    
    private float rotateCounter = 0.2f; //拍點提前0.2s旋轉
    
    private float shootCounter = -0.5f - beatPeriod * 3; //前面三次拍點不做事，延遲0.5秒發射

    private TurretManager turret; // TurretManager.cs 的程式
    // Use this for initialization
    void Start()
    {
        turret = this.GetComponent<TurretManager>(); // 指定TurretManager物件中的TurretManager.cs程式進來
    }

    // Update is called once per frame
    void Update()
    {
        rotateCounter += Time.deltaTime; // 時間累加
        shootCounter += Time.deltaTime; // 時間累加
        if (rotateCounter > beatPeriod) // rotateCounter (0.2f) 0.2s 一直累加到 beatPeriod (1.485f) 1.485s
        {
            turret.PlayRotateAnimation(); // 執行旋轉
            rotateCounter -= beatPeriod; // 再扣回 0.2f (代表再度以0.2秒做累加)
        }
        if (shootCounter > beatPeriod) // shootCounter (-0.5f - beatPeriod * 3) -4.995 一直累加到 beatPeriod(1.485f) 1.485s (第一次)
        {
            turret.PlayShootAnimation(); // 執行旋轉
            shootCounter -= beatPeriod; //  扣1.485s (代表再度以0秒做累加，當下一個節拍點到就會再度執行if判斷式)
        }

    }
}
