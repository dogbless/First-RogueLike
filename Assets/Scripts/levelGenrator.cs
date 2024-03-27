using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelGenrator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor, shopColor, gunRoomColor;
    public int distanceToEnd;
    public Transform generatorPoint;
    public bool includeShop;
    public bool includeGunRoom;
    public int minDistanceToShop;
    public int maxDistanceToShop;
    public int minDistanceToGunRoom, maxDistanceToGunRoom;
    public enum Directions { right, up, down, left };
    public Directions selectedDirection;
    public float xOffSet = 18, yOffSet = 10;
    public LayerMask whatIsRoom;
    public GameObject endRoom, shopRoom, GunRoom;
    private List<GameObject> layoutRoomObject = new List<GameObject>();
    public roomPrefabs rooms;
    private List<GameObject> generatedOutline = new List<GameObject>();
    public roomCenter centerStart, centerEnd, centerShop, centerGunRoom;
    public roomCenter[] potentialCenters;
    public static levelGenrator instance;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Directions)Random.Range(1, 4);
        MoveGenerationPoint();
        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, new Vector3(generatorPoint.position.x, generatorPoint.position.y, 0f), generatorPoint.rotation);


            layoutRoomObject.Add(newRoom);

            if (i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObject.RemoveAt(layoutRoomObject.Count - 1);
                endRoom = newRoom;
            }
            selectedDirection = (Directions)Random.Range(1, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }

        }


        if (includeShop)
        {
            int shopSelector = Random.Range(minDistanceToShop, maxDistanceToShop + 1);
            shopRoom = layoutRoomObject[shopSelector];
            layoutRoomObject.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;

        }
        if (includeGunRoom)
        {
            int grSelector = Random.Range(minDistanceToGunRoom, maxDistanceToGunRoom + 1);
            GunRoom = layoutRoomObject[grSelector];
            layoutRoomObject.RemoveAt(grSelector);
            GunRoom.GetComponent<SpriteRenderer>().color = gunRoomColor;

        }
        //create room outlines
        createRoomOutline(Vector3.zero);
        foreach (GameObject room in layoutRoomObject)
        {
            createRoomOutline(room.transform.position);
        }
        createRoomOutline(endRoom.transform.position);
        if (includeShop)
        {
            createRoomOutline(shopRoom.transform.position);
        }
        if (includeGunRoom)
        {
            createRoomOutline(GunRoom.transform.position);
        }


        foreach (GameObject outline in generatedOutline)
        {
            bool generateCeneter = true;
            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<ROOMER>();
                generateCeneter = true;
            }
            if (outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<ROOMER>();
                generateCeneter = false;
            }

            if (includeShop)
            {
                if (outline.transform.position == shopRoom.transform.position)
                {
                    Instantiate(centerShop, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<ROOMER>();
                    generateCeneter = false;
                }
            }
            if (includeGunRoom)
            {
                if (outline.transform.position == GunRoom.transform.position)
                {
                    Instantiate(centerGunRoom, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<ROOMER>();
                    generateCeneter = false;
                }
            }


            if (generateCeneter)
            {
                int centereSelect = Random.RandomRange(0, potentialCenters.Length);
                Instantiate(potentialCenters[centereSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<ROOMER>();
            }

        }
    }

    // Update is called once per frame
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
#endif

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Directions.right:
                generatorPoint.position += new Vector3(xOffSet, 0f, 0f);
                break;
            case Directions.up:
                generatorPoint.position += new Vector3(0f, yOffSet, 0f);
                break;
            case Directions.down:
                generatorPoint.position += new Vector3(0f, -yOffSet, 0f);
                break;
            case Directions.left:
                generatorPoint.position += new Vector3(-xOffSet, 0f, 0f);
                break;
            default:
                break;
        }
    }
    public void createRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffSet, 0f), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffSet, 0f), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffSet, 0f, 0f), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffSet, 0f, 0f), .2f, whatIsRoom);

        int directionCount = 0;

        if (roomAbove)
        {
            directionCount++;
        }

        if (roomBelow)
        {
            directionCount++;
        }


        if (roomLeft)
        {
            directionCount++;
        }


        if (roomRight)
        {
            directionCount++;
        }




        switch (directionCount)
        {
            case 0:
                Debug.LogError("found no room exists");
                break;
            case 1:
                if (roomAbove)
                {
                    generatedOutline.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                }
                if (roomBelow)
                {
                    generatedOutline.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                }

                if (roomRight)
                {
                    generatedOutline.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }

                if (roomLeft)
                {
                    generatedOutline.Add(Instantiate(rooms.SingleLeft, roomPosition, transform.rotation));
                }


                break;
            case 2:
                if (roomAbove && roomBelow)
                {
                    generatedOutline.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight)
                {
                    generatedOutline.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                }
                if (roomAbove && roomLeft)
                {
                    generatedOutline.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft)
                {
                    generatedOutline.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                }
                if (roomBelow && roomRight)
                {
                    generatedOutline.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                }
                if (roomRight && roomLeft)
                {
                    generatedOutline.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                }

                break;
            case 3:
                if (roomRight && roomLeft && roomAbove)
                {
                    generatedOutline.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                }
                if (roomRight && roomBelow && roomLeft)
                {
                    generatedOutline.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomLeft)
                {
                    generatedOutline.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomRight)
                {
                    generatedOutline.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, transform.rotation));
                }
                break;
            case 4:
                if (roomAbove && roomBelow && roomRight && roomLeft)
                {
                    generatedOutline.Add(Instantiate(rooms.fourWay, roomPosition, transform.rotation));
                }
                break;
        }

    }
}
[System.Serializable]
public class roomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, SingleLeft,
        doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourWay;
}
