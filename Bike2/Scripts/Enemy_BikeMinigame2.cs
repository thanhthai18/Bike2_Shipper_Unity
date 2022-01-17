using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BikeMinigame2 : MonoBehaviour
{
    public int speed = 4;
    public bool isInCamera = false;
    public bool isEnemyMove = true;



    void DelayDestroy()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);

        }
    }

    void Update()
    {
        if (isEnemyMove)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            isInCamera = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController_BikeMinigame2.instance.isBegin = false;     
            isEnemyMove = false;
            GameController_BikeMinigame2.instance.Lose();
            GameController_BikeMinigame2.instance.bikeObj.isMove = false;
            GameController_BikeMinigame2.instance.isCameraFollow = false;
            if (GameController_BikeMinigame2.instance.bikeObj.isRight)
            {
                collision.transform.DOMoveX(collision.transform.position.x - 3, 0.5f);
            }
            else if (!GameController_BikeMinigame2.instance.bikeObj.isRight)
            {
                collision.transform.DOMoveX(collision.transform.position.x + 3, 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            if (isInCamera)
            {                
                Destroy(gameObject);
                GameController_BikeMinigame2.instance.isHadEnemy = false;
            }
        }
    }
}
