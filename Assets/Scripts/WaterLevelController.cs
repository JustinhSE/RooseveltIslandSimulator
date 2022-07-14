using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
    public GameObject waterLevel;
    public GameObject camera;
    // whether to move water up or down
    public bool moveDown;
    public bool moveUp;
    public float minLevel;
    public float maxLevel;
    public float moveUpSpeed = 0.1f;
    public float moveDownSpeed = 0.1f;

    private Color normalColor = new Color(0.75f, 0.75f, 0.85f, 0.5f);
    private Color underwaterColor = new Color(0.15f, 0.15f, 0.25f, 0.2f);

    [SerializeField] private GameObject bubblePartical;
    private bool bubbleReleased;
    private bool bubbleDeep;
    private bool bubbleDiscarded;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.0001f;
        //RenderSettings.fog = false;
        //Debug.Log("RenderSettings Changed");
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDown && minLevel < waterLevel.transform.localPosition.y)
        {
            //Debug.Log("moving water level down");
            float targetY = waterLevel.transform.localPosition.y - moveDownSpeed * Time.deltaTime;
            if (targetY < minLevel)
            {
                targetY = minLevel;
                moveDown = false;
            }
            waterLevel.transform.localPosition = new Vector3(0f, targetY, 0f);
        } 
        else if (moveUp && waterLevel.transform.localPosition.y < maxLevel)
        {
            //Debug.Log("moving water level up");
            float targetY = waterLevel.transform.localPosition.y + moveUpSpeed * Time.deltaTime;
            if (targetY > maxLevel)
            {
                targetY = maxLevel;
                moveUp = false;
            }
            waterLevel.transform.localPosition = new Vector3(0f, targetY, 0f);
        }
        CheckWaterLevel();
    }

    public void moveWaterUp()
    {
        moveDown = false;
        moveUp = true;
    }

    public void moveWaterDown()
    {
        moveDown = true;
        moveUp = false;
    }

    public void setMoveUpSpeed(float speed)
    {
        moveUpSpeed = speed;
    }
    
    public void setMoveDownSpeed(float speed)
    {
        moveDownSpeed = speed;
    }

    private void CheckWaterLevel()
    {
        if (camera.transform.position.y < waterLevel.transform.position.y)
        {
            // underwater
            RenderSettings.fogColor = underwaterColor;
            RenderSettings.fogDensity = 0.003f + (waterLevel.transform.position.y - camera.transform.position.y) / 1000f;
            if (!bubbleReleased)
            {
                bubblePartical.SetActive(true);
                bubbleReleased = true;
            }
        }
        else
        {
            // above water
            RenderSettings.fogColor = normalColor;
            RenderSettings.fogDensity = 0.0001f;
        }

        if (bubbleReleased && camera.transform.position.y < waterLevel.transform.position.y - 0.2f)
        {
            bubbleDeep = true;
        }

        if (!bubbleDiscarded && bubbleDeep && camera.transform.position.y > waterLevel.transform.position.y - 0.1f)
        {
            bubbleDiscarded = true;
            bubblePartical.SetActive(false);
        }
    }
    
    public void MoveWaterToLevel(float newWaterLevel)
    {
        if (waterLevel.transform.position.y < newWaterLevel)
        {
            maxLevel = newWaterLevel;
            moveWaterUp();
        }
        else
        {
            minLevel = newWaterLevel;
            moveWaterDown();
        }
    }
}
