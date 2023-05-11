using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("參考物件")]
    public Camera fpsCam;
    public Transform attackPoint;

    [Header("子彈預置物件")]
    public GameObject bullet;

    private void Update()
    {
        //判斷有沒有按下左鍵
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view           

            RaycastHit hit; //宣告一個射擊點

            Vector3 targetPoint; //宣告一個位置點變數，到時候如果有打到東西，就存到這個變數

            //如果射線有打到具備碰撞體的物件
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;        //將打到物件的位置點存進 targetPoint
            else                                //如果沒有打到物件，就以長度75的末端點取得一個點，存進 targetPoint
                targetPoint = ray.GetPoint(75); //Just a point far away from the player

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // 在測試階段將射線設定為紅色線條，來看看線條長度夠不夠？

            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
            currentBullet.transform.forward = directionWithoutSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * 20, ForceMode.Impulse);
        }
    }
}