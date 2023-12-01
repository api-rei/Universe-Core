using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    [SerializeField] float distance;
    [SerializeField] Camera mainCamera;
    bool inside = false;
    float shootingTime = 0f;
    void Start()
    {
        GameDatas.SetFloatValue("fireRate", 0.2f);
        GameDatas.SetFloatValue("BulletSpeed", 0.3f);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootingTime += Time.deltaTime;
            if (shootingTime > GameDatas.GetFloatValue("fireRate"))
            {
                if (!inside)
                    Shoot();
                shootingTime = shootingTime - GameDatas.GetFloatValue("fireRate");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            shootingTime = 0f;
        }
        CursorMove();
    }
    void Shoot()
    {
        Vector2 cursorDirection = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Game.Instance.AddBullet(cursorDirection.normalized * distance, cursorDirection.normalized * GameDatas.GetFloatValue("BulletSpeed"));
    }
    void CursorMove()
    {
        Vector2 cursorDirection = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        cursorDirection.Normalize();
        if (Vector2.Distance(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero) < distance * 0.8f && !inside)
            DoInSide();
        if (Vector2.Distance(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero) > distance * 0.8f && inside)
            DoOutSide();
        float angle = Mathf.Atan2(cursorDirection.x, cursorDirection.y) / (Mathf.PI * 2) * 360f;
        cursor.transform.position = cursorDirection * distance;
        cursor.transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
    void DoInSide()
    {
        cursor.transform.DOScale(0f, 0.05f).SetEase(Ease.InQuad);
        inside = true;
    }
    void DoOutSide()
    {
        cursor.transform.DOScale(0.5f, 0.05f).SetEase(Ease.InQuad);
        inside = false;
    }
}
