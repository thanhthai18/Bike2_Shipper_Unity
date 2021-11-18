using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_BikeMinigame2 : MonoBehaviour
{
    private BoxCollider2D col;
    public GameObject BGPrefab;
    public bool isSpawnedRight;
    public bool isSpawnedLeft;

    private void Start()
    {
        isSpawnedRight = false;
        isSpawnedLeft = false;
        col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(2 * ((Screen.width * 1.0f) / Screen.height) * GetComponent<Camera>().orthographicSize, 2 * GetComponent<Camera>().orthographicSize);
        GetComponent<Camera>().orthographicSize *=  3.0f/5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn") && !isSpawnedRight)
        {
            if (GameController_BikeMinigame2.instance.bikeObj.isRight)
            {
                isSpawnedRight = true;
                GameController_BikeMinigame2.instance.listMap.Add(Instantiate(BGPrefab, transform.GetChild(1).transform.position, Quaternion.identity));
            }
        }
        else if (collision.gameObject.CompareTag("EditorOnly") && !isSpawnedLeft)
        {
            if (!GameController_BikeMinigame2.instance.bikeObj.isRight)
            {
                isSpawnedLeft = true;
                GameController_BikeMinigame2.instance.listMap.Add(Instantiate(BGPrefab, transform.GetChild(2).transform.position, Quaternion.identity));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera") && GameController_BikeMinigame2.instance.bikeObj.isRight)
        {
            Destroy(collision.gameObject);
            isSpawnedLeft = false;
            isSpawnedRight = false;
        }
        else if(collision.gameObject.CompareTag("MainCamera") && !GameController_BikeMinigame2.instance.bikeObj.isRight)
        {
            Destroy(collision.gameObject);
            isSpawnedRight = false;
            isSpawnedLeft = false;  
        }
    }
}
