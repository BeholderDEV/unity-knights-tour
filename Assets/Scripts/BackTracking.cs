using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTracking : MonoBehaviour
{
    public int n = 8;
    public Vector2[] sequence;
    bool solved = false;
    bool found = false;

    // Start is called before the first frame update
    void Start()
    {
        sequence = new Vector2[n * n];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSize(int i)
    {
        n = i;
    }

    bool isSafe(int x, int y,
                       int[,] sol)
    {
        return (x >= 0 && x < n &&
                y >= 0 && y < n &&
                sol[x, y] == -1);
    }


    void printSolution(int[,] sol)
    {
        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
                Debug.Log(sol[x, y] + " ");
            Debug.Log("");
        }
    }

    bool solveKTUtil2(int x, int y, int movei,
                            int[,] sol, int[] xMove,
                            int[] yMove)
    {
        int k, next_x, next_y;
        if (movei == n * n)
            return true;

        /* Try all next moves from  
        the current coordinate x, y */
        for (k = 0; k < 8; k++)
        {
            next_x = x + xMove[k];
            next_y = y + yMove[k];
            if (isSafe(next_x, next_y, sol))
            {
                sol[next_x, next_y] = movei;
                if (solveKTUtil2(next_x, next_y, movei +
                                 1, sol, xMove, yMove))
                    return true;
                else
                    // backtracking 
                    sol[next_x, next_y] = -1;
            }
        }

        return false;
    }

    IEnumerator solveKTUtil(int x, int y, int movei,int[,] sol, int[] xMove, int[] yMove)
    {
        Debug.Log(movei);
        int k, next_x, next_y;
        if (movei == n * n)
            yield return true;

        for (k = 0; k < 8; k++)
        {
            next_x = x + xMove[k];
            next_y = y + yMove[k];
            if (isSafe(next_x, next_y, sol))
            {
                sol[next_x, next_y] = movei;
                CoroutineWithData cd = new CoroutineWithData(this, solveKTUtil(next_x, next_y, movei + 1, sol, xMove, yMove));
                yield return cd.coroutine;
                if ((bool) cd.result)
                    yield return true;
                else
                    sol[next_x, next_y] = -1;
            }
            yield return null;
        }

        yield return false;
    }

    public IEnumerator Executar(Vector2 initialPosition)
    {
        int[,] sol = new int[n, n];


        for (int x = 0; x < n; x++)
            for (int y = 0; y < n; y++)
                sol[x, y] = -1;


        int[] xMove = {2, 1, -1, -2,
                      -2, -1, 1, 2};
        int[] yMove = {1, 2, 2, 1,
                      -1, -2, -2, -1};

        sol[(int)initialPosition.x, (int)initialPosition.y] = 0;


        CoroutineWithData cd = new CoroutineWithData(this, solveKTUtil((int)initialPosition.x, (int)initialPosition.y, 1, sol,xMove, yMove));
        yield return cd.coroutine;
        if ((bool)cd.result == false)
        {
            Debug.Log("Solution does not exist");
        }
        else
            printSolution(sol);


    }

 

    IEnumerator fatorial(int num)
    {
        if (num <= 1)
            yield return 1;

        CoroutineWithData cd = new CoroutineWithData(this, fatorial(num - 1));
        //yield return cd.coroutine;
        yield return (int)cd.result * num;
    }

}
