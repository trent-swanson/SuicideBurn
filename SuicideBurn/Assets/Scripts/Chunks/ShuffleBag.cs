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

    //public GameObject chunkParent;

    //private int chunkCount;
    // Use this for initialization
    void Start()
    {
        // create first chunk
        CreateNewChunk(0);
        CreateNewChunk(10);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(loadedChunks.Count);


        if (loadedChunks.Count < 4)
        {
            if (loadedChunks.Count <= 2)
            {
                CreateNewChunk(-10);
            }

            if (loadedChunks.Count <= 3)
            {
                CreateNewChunk(-20);
            }
        }
    }

    // Create a random number between the range of the chunks array.
    public int RandomChunkNum()
    {
        // Creates an int that is randomised between 0 and the length of the throw array.
        int Selector = Random.Range(0, chunksArray.Length);
        return Selector;
    }

    public void CreateNewChunk(float yPos)
    { 
        int randChunk = RandomChunkNum();
        loadedChunks.Add(chunksArray[randChunk]);
        GameObject chunk = Instantiate(chunksArray[randChunk], new Vector3(0, yPos, 0), Quaternion.identity);
        chunk.AddComponent<SceneMover>();


        //chunk.transform.parent = chunkParent.transform;
        chunk.AddComponent<TopDestroyer>();
        chunk.GetComponent<TopDestroyer>().shuffleBag = GameObject.Find("ShuffleBag");
    }



}
