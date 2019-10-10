using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTopController : MonoBehaviour
{
    public GameObject horse;
    public GameObject nextMove;
    GameObject horseobj;
    public GameController gc;
    private bool move = false;
    private Vector2 initialPosition;
    private List<Vector3> movementList;
    int listposition = 0;

    Vector3[] possibleMoves =
        {
            new Vector3(1,0,2),
            new Vector3(1,0,-2),
            new Vector3(2,0,1),
            new Vector3(2,0,-1),
            new Vector3(-1,0,-2),
            new Vector3(-1,0,-2),
            new Vector3(-2,0,1),
            new Vector3(-2,0,-1)
        };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {          
            horseobj.transform.position = movementList[listposition];
            GameObject overobj = Instantiate(nextMove, movementList[listposition], Quaternion.identity);
            overobj.transform.parent = transform;

            listposition++;
            Debug.Log("moved");
            if (listposition == movementList.Count)
            {
                move = false;
                listposition = 0;
            }
        }
    }

    public void StartMoving()
    {        
        initialPosition = gc.initialPosition;
        movementList = gc.gameObject.GetComponent<Warnsdorf>().finalList;
        move = true;
    }

    public Vector2 PlaceHorse(int size)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        int initialX = Random.Range(0, size);
        int initialY = Random.Range(0, size);

        //int initialX = 0;
        //int initialY = 0;

        Vector3 initialpos = new Vector3(initialX, 0, initialY);
        horseobj = Instantiate(horse, initialpos, Quaternion.identity);
        horseobj.transform.parent = transform;

        foreach (Vector3 move in possibleMoves)
        {
            Vector3 nextPosition = initialpos + move + new Vector3(0,0.01f,0);
            if(nextPosition.x < size && nextPosition.x>=0 && nextPosition.z<size && nextPosition.z >= 0)
            {
                //GameObject overobj = Instantiate(nextMove, nextPosition, Quaternion.identity);
                //overobj.transform.parent = transform;
            }
        }
        return new Vector2(initialX, initialY);
    }

    public Vector2 PlaceHorse00(int size)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int initialX = 0;
        int initialY = 0;

        Vector3 initialpos = new Vector3(initialX, 0, initialY);
        horseobj = Instantiate(horse, initialpos, Quaternion.identity);
        horseobj.transform.parent = transform;

        foreach (Vector3 move in possibleMoves)
        {
            Vector3 nextPosition = initialpos + move + new Vector3(0, 0.01f, 0);
            if (nextPosition.x < size && nextPosition.x >= 0 && nextPosition.z < size && nextPosition.z >= 0)
            {
                //GameObject overobj = Instantiate(nextMove, nextPosition, Quaternion.identity);
                //overobj.transform.parent = transform;
            }
        }
        return new Vector2(initialX, initialY);
    }
}
