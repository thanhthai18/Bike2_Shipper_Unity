using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_BikeMinigame2 : MonoBehaviour
{
    public static GameController_BikeMinigame2 instance;

    public Camera mainCamera;
    public Bike_BikeMinigame2 bikeObj;
    public Vector2 startMousePos;
    public Vector2 endMousePos;
    public bool isCameraFollow;
    private float f2;
    public float speedCamera;
    public bool isWin, isLose;
    public List<GameObject> listMap = new List<GameObject>();
    public List<GameObject> listHouseIcon = new List<GameObject>();
    public Canvas canvas;
    public int currentStage, preStage;
    public bool isBegin, isTut, isPause;
    public GameObject giftObj;
    public GameObject tutorial;
    public Button btnTutorial;
    public bool isMouseOverButton;
    public List<Transform> listPosEnemy = new List<Transform>();
    public List<Enemy_BikeMinigame2> listEnemyPrefab = new List<Enemy_BikeMinigame2>();
    public bool isHadEnemy;



    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        speedCamera = 5;
        isLose = false;
        isWin = false;
        isBegin = false;
        isPause = false;
        isTut = false;
        isHadEnemy = false;
        giftObj.transform.position = new Vector2(-0.3f, 3.42f);
    }

    private void Start()
    {
        SetSizeCamera();
        isCameraFollow = true;
        tutorial.SetActive(false);
        startMousePos = Vector2.zero;
        endMousePos = Vector2.zero;
        currentStage = 0;
        preStage = currentStage - 1;
        Intro();
        btnTutorial.onClick.AddListener(OnClickButtonTutorial);
        //SpawnIcon();
        //GetHideAllIconStage();
        //GetShowIconStage(currentStage);     
    }

    void SetSizeCamera()
    {
        float f1;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;

        mainCamera.orthographicSize *= f1 / f2;
    }

    void Intro()
    {
        giftObj.transform.DOMoveY(0.44f, 3).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            mainCamera.DOOrthoSize(mainCamera.orthographicSize * 5.0f / 3, 2).OnComplete(() =>
            {
                giftObj.transform.parent = bikeObj.transform;
                isBegin = true;
                bikeObj.isMove = true;
                SpawnIcon();
                GetHideAllIconStage();
                GetShowIconStage(currentStage);
                Invoke(nameof(ShowTutorial), 2);
            });

        });
    }

    void OnClickButtonTutorial()
    {
        if (!canvas.transform.GetChild(1).gameObject.activeSelf && !canvas.transform.GetChild(2).gameObject.activeSelf)
        {
            isTut = true;
            ShowTutorial();
        }
        else if (canvas.transform.GetChild(1).gameObject.activeSelf || canvas.transform.GetChild(2).gameObject.activeSelf)
        {
            canvas.transform.GetChild(1).gameObject.SetActive(false);
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            Time.timeScale = 1;
            isTut = false;
            if (!bikeObj.isMove)
            {
                bikeObj.isMove = true;
            }
        }
    }

    void ShowTutorial()
    {
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        Time.timeScale = 0;
        isTut = true;
        bikeObj.isMove = false;
        StartCoroutine(CountingSpawnEnemy());
    }

    void SpawnIcon()
    {
        List<int> listCheckSame = new List<int>();
        int ran;
        for (int v = 0; v < listHouseIcon.Count; v++)
        {
            listCheckSame.Add(v);
        }
        for (int i = 0; i < canvas.transform.GetChild(0).childCount; i++)
        {
            ran = Random.Range(0, listCheckSame.Count);
            var tmpIcon = Instantiate(listHouseIcon[listCheckSame[ran]]);
            listCheckSame.RemoveAt(ran);
            tmpIcon.transform.parent = canvas.transform.GetChild(0).GetChild(i);
            tmpIcon.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            tmpIcon.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            tmpIcon.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    void SpawnEnemy(int indexPos, int indexEnemy)
    {
        if (indexPos < 5 && indexPos > 0)
        {
            var tmpEnemy = Instantiate(listEnemyPrefab[indexEnemy], listPosEnemy[indexPos].position, Quaternion.identity);
            tmpEnemy.transform.eulerAngles = Vector3.zero;
            tmpEnemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (indexPos > 3 && indexPos < 8)
        {
            var tmpEnemy = Instantiate(listEnemyPrefab[indexEnemy], listPosEnemy[indexPos].position, Quaternion.identity);
            tmpEnemy.transform.eulerAngles = new Vector3(0, 180, 0);
            tmpEnemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    IEnumerator CountingSpawnEnemy()
    {
        while (isBegin)
        {
            if (!isHadEnemy)
            {
                isHadEnemy = true;
                if (!bikeObj.isRight)
                {
                    SpawnEnemy(Random.Range(0, listPosEnemy.Count / 2), Random.Range(0, listEnemyPrefab.Count));
                }
                else
                {
                    SpawnEnemy(Random.Range(listPosEnemy.Count / 2, listPosEnemy.Count), Random.Range(0, listEnemyPrefab.Count));
                }
                yield return new WaitForSeconds(2);
            }
            yield return new WaitForSeconds(1);
        }
    }

    void GetHideAllIconStage()
    {
        for (int i = 0; i < canvas.transform.GetChild(0).childCount; i++)
        {
            canvas.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(true);
            canvas.transform.GetChild(0).GetChild(i).GetChild(2).gameObject.SetActive(false);
            if (canvas.transform.GetChild(0).GetChild(i).GetChild(3).gameObject != null)
            {
                canvas.transform.GetChild(0).GetChild(i).GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    public void GetShowIconStage(int stageIndex)
    {
        canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetChild(stageIndex) .GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(2).gameObject.SetActive(false);
        if (canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(3).gameObject != null)
        {
            canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(3).gameObject.SetActive(true);
        }
    }

    public void GetCompletedIcon(int stageIndex)
    {
        canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(2).gameObject.SetActive(true);
        if (canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(3).gameObject != null)
        {
            canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(3).gameObject.SetActive(true);
            canvas.transform.GetChild(0).GetChild(stageIndex).GetChild(3).SetAsFirstSibling();
        }
    }

    public void Win()
    {
        isWin = true;
        isCameraFollow = false;
        bikeObj.transform.DOMoveX(bikeObj.transform.position.x + 20, 3).OnComplete(() => { Destroy(bikeObj.gameObject); });
        StopAllCoroutines();
    }

    public void Lose()
    {
        isLose = true;
        StopAllCoroutines();
        Debug.Log("Thua");
        bikeObj.GetComponent<BoxCollider2D>().enabled = false;
    }




    private void FixedUpdate()
    {
        if (isCameraFollow && isBegin)
        {
            mainCamera.transform.position = new Vector3(bikeObj.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLose && !isWin && isBegin && !isPause)
        {
            if (!isTut)
            {
                startMousePos = (Vector2)(mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position);
            }
            else
            {
                startMousePos = Vector2.zero;
                endMousePos = Vector2.zero;
            }
            if (canvas.transform.GetChild(2).gameObject.activeSelf && !isMouseOverButton)
            {
                canvas.transform.GetChild(2).gameObject.SetActive(false);
                Time.timeScale = 1;
                isTut = false;
                bikeObj.isMove = true;
            }
            if (canvas.transform.GetChild(1).gameObject.activeSelf && !isMouseOverButton)
            {
                canvas.transform.GetChild(1).gameObject.SetActive(false);
                canvas.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(0) && !isLose && !isWin && isBegin && !isPause)
        {
            if (!isTut)
            {
                endMousePos = (Vector2)(mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position);
            }
            else
            {
                startMousePos = Vector2.zero;
                endMousePos = Vector2.zero;
            }
            if (startMousePos.x == endMousePos.x)
            {
                return;
            }
            else if (startMousePos.x + 0.1f < endMousePos.x && Mathf.Abs(startMousePos.y - endMousePos.y) < 2)
            {
                if (!bikeObj.isRight)
                {
                    Debug.Log("Phai");
                    bikeObj.GetRight();
                }
            }
            else if (startMousePos.x > endMousePos.x + 0.1f && Mathf.Abs(startMousePos.y - endMousePos.y) < 2)
            {
                if (bikeObj.isRight)
                {
                    Debug.Log("Trai");
                    bikeObj.GetLeft();
                }
            }
            else if (startMousePos.y + 0.1f < endMousePos.y && Mathf.Abs(startMousePos.x - endMousePos.x) < 2)
            {
                if (bikeObj.indexLane < 3)
                {
                    Debug.Log("Len");
                    bikeObj.GetTop();
                }
            }
            else if (startMousePos.y > endMousePos.y + 0.1f && Mathf.Abs(startMousePos.x - endMousePos.x) < 2)
            {
                if (bikeObj.indexLane > 0)
                {
                    Debug.Log("Xuong");
                    bikeObj.GetBot();
                }
            }

            //startMousePos = Vector2.zero;
            //endMousePos = Vector2.zero;
        }
    }
}