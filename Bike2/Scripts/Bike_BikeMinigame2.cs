using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike_BikeMinigame2 : MonoBehaviour
{
    public float speed;
    public int indexLane;
    public bool isRight;
    public bool isMove;
    public GameObject startParticelPrefab;


    private void Awake()
    {
        speed = 5;
        indexLane = 2;
        isMove = false;
        isRight = true;
        transform.position = new Vector2(0, transform.position.y);
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void GetTop()
    {
        transform.DOMoveY(transform.position.y + 1.11f, 0.1f);
        indexLane++;

    }
    public void GetBot()
    {
        transform.DOMoveY(transform.position.y - 1.11f, 0.1f);
        indexLane--;

    }
    public void GetRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        isRight = true;
    }
    public void GetLeft()
    {
        transform.eulerAngles = new Vector3(0, -180.0f, 0);
        isRight = false;
    }

    void CheckStageCompeleted(Collider2D col)
    {
        Debug.Log(col.name);
        Debug.Log(GameController_BikeMinigame2.instance.canvas.transform.GetChild(0).GetChild(GameController_BikeMinigame2.instance.currentStage).GetChild(3).ToString());
        if (col.name + "Icon(Clone) (UnityEngine.RectTransform)" == GameController_BikeMinigame2.instance.canvas.transform.GetChild(0).GetChild(GameController_BikeMinigame2.instance.currentStage).GetChild(3).ToString())
        {
            isMove = false;
            GameController_BikeMinigame2.instance.isCameraFollow = false;
            GameController_BikeMinigame2.instance.isPause = true;
            GetComponent<BoxCollider2D>().enabled = false;

            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                var tmpStarFX = Instantiate(startParticelPrefab, transform.GetChild(0).transform);
                tmpStarFX.GetComponent<ParticleSystem>().Emit(5);
                Destroy(tmpStarFX, 2);
                GameController_BikeMinigame2.instance.GetCompletedIcon(GameController_BikeMinigame2.instance.currentStage);
                Invoke(nameof(DelayResume), 2);
            });              
        }
    }

    void DelayResume()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetEase(Ease.Linear);
        GetComponent<BoxCollider2D>().enabled = true;
        isMove = true;
        GameController_BikeMinigame2.instance.isCameraFollow = true;
        GameController_BikeMinigame2.instance.isPause = false;
        GameController_BikeMinigame2.instance.currentStage++;
        if (GameController_BikeMinigame2.instance.currentStage < 5)
        {
            GameController_BikeMinigame2.instance.GetShowIconStage(GameController_BikeMinigame2.instance.currentStage);
        }
        else
        {
            GameController_BikeMinigame2.instance.Win();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController_BikeMinigame2.instance.currentStage < 5)
        {
            if (collision.gameObject.CompareTag("Box")) //house0
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("Tree"))//house1
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("People"))//house2
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("Balloon"))//house3
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("ColorHive"))//house4
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("BoundScreen"))//house5
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("TrashRecycleTruck"))//house6
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("GroundBoxRecycleTruck"))//house7
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("HeadBoxRecycleTruck"))//house8
            {
                CheckStageCompeleted(collision);
            }
            if (collision.gameObject.CompareTag("PointWindow"))//house9
            {
                CheckStageCompeleted(collision);
            }
        }
    }
}
