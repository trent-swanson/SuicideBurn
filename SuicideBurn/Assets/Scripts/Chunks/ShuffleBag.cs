using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleBag : MonoBehaviour
{
    // Array of chunk prefabs
    public GameObject[] chunksArray;

    // The chunks that are currently loaded
    [HideInInspector]
    public List<GameObject> loadedChunks;

    // This list is to ensure that there won't be the same one in a row.
    [HideInInspector]
    public List<GameObject> tempChunkList;

    public GameObject groundChunk;

    public GameObject warningPanel;
    public GameObject speedHolder;

    [Tooltip("The speed that the chunks rise at.")]
    public float riseSpeed;
    [Tooltip("The speed that the chunks rise at WHILE SLOWED.")]
    public float slowSpeed;
    [Tooltip("The time it takes to go between regular speed and slow.")]
    public float slowTime;

    private bool hasGroundSpawned = false;
    private bool hasSHolderCreated = false;
    public int maxChunksBeforeGround = 0;
    public int chunksCreated = 0;

    //public GameObject chunkParent;

    //private int chunkCount;
    // Use this for initialization
    void Start()
    {
        // create first chunk
        CreateNewChunk(0);
        CreateNewChunk(10);
        populateChunkList();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(chunksCreated);
        if(chunksCreated == maxChunksBeforeGround && !hasGroundSpawned)
        {
            CreateGroundChunk();
            hasGroundSpawned = true;
        }

        //Debug.Log(loadedChunks.Count);

        if(!hasSHolderCreated)
        {
            hasSHolderCreated = true;
            CreateSpeedHolder();
        }

        if (loadedChunks.Count < 4 && !hasGroundSpawned)
        {
            if (loadedChunks.Count <= 2)
            {
                CreateNewChunk(-10);

                //chunksArray[RandomChunkNum()].transform.SetPositionAndRotation(new Vector3(0, -10, 0), Quaternion.identity);
            }

            if (loadedChunks.Count <= 3)
            {
                CreateNewChunk(-20);
                //chunksArray[RandomChunkNum()].transform.SetPositionAndRotation(new Vector3(0, -20, 0), Quaternion.identity);
            }
        }
    }

    // Create a random number between the range of the chunks array.
    public int RandomChunkNum()
    {
        // Creates an int that is randomised between 0 and the length of the throw array.
        int Selector = Random.Range(0, tempChunkList.Count);
        return Selector;
    }



    public void CreateNewChunk(float yPos)
    {
        int randChunk = RandomChunkNum();
        loadedChunks.Add(chunksArray[randChunk]);
          
        GameObject chunk = Instantiate(chunksArray[randChunk], new Vector3(0, yPos, 0), Quaternion.identity);

        chunk.AddComponent<SceneMover>();
        chunk.GetComponent<SceneMover>().gameManager = GameObject.Find("GameManager");

        chunk.AddComponent<TopDestroyer>();
        chunk.GetComponent<TopDestroyer>().shuffleBag = GameObject.Find("ShuffleBag");
        ++chunksCreated;
    }

    private void CreateGroundChunk()
    {
        warningPanel.gameObject.SetActive(true);
        GameObject newGroundChunk = Instantiate(groundChunk, new Vector3(0, -30, 0), Quaternion.identity);
        newGroundChunk.AddComponent<SceneMover>();
        newGroundChunk.GetComponent<SceneMover>().gameManager = GameObject.Find("GameManager");
        newGroundChunk.AddComponent<Ground>();
    }

    public void CreateSpeedHolder()
    {
        GameObject newSpeedHolder = Instantiate(speedHolder, new Vector3(0, 0, -200), Quaternion.identity);
        newSpeedHolder.AddComponent<SceneMover>();
        newSpeedHolder.GetComponent<SceneMover>().gameManager = GameObject.Find("GameManager");
    }

    private void populateChunkList()
    {
        for (int i = 0; i < chunksArray.Length; i++)
        {
            tempChunkList.Add(chunksArray[i]);
        }
    }

    // Used to check how fast the "Player is going"
    public float GetCurrentSpeed()
    {
        return gameObject.GetComponent<SceneMover>().currentSpeed;
    }

    //todo: check whether the player is going too fast, if he is then hits the ground, he dies.
}
