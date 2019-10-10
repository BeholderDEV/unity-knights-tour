using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public int size = 8;
    public GameObject whitecube;
    public GameObject blackcube;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void Generate(int size){
        this.size = size;
        Generate();
    }

    public void Generate()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        bool white = false;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject cube;
                if (white)
                {
                    cube = whitecube;
                }
                else
                {
                    cube = blackcube;
                }
                white = !white;
                GameObject board = Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity);
                board.transform.parent = transform;
            }
            white = !white;
        }
    }
}
