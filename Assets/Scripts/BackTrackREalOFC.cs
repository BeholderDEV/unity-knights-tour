using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackTrackREalOFC : MonoBehaviour
{
    int n = 8;
    IEnumerator Executar(Action<bool> achou)
    {
        yield return null;
        var result = false;

        

        result = true;
        if(achou != null)
        {
            achou(result);
        }
    }
    public void printSolution(int[,] sol)
    {
        for (int i = 0; i < n; i++)
        {
            string line = "";
            for (int j = 0; j < n; j++)
            {
                line += sol[i, j] + " ";
            }
            Debug.Log(line);
        }
    }
    bool isSafe(int x, int y, int[,] sol)
    {
        return (x >= 0 && x < n && y >= 0 && y < n && sol[x, y] == -1);
    }

    IEnumerator solveKTUtil(int x, int y, int movei, int[,] sol, int[] xMove, int[] yMove, Action<bool> achou)
    {
        yield return null;
        var result = false;
        int k, next_x, next_y;
        if (movei == n * n)
            achou(result);

        for (k = 0; k < 8; k++)
        {
            next_x = x + xMove[k];
            next_y = y + yMove[k];
            if (isSafe(next_x, next_y, sol))
            {
                sol[next_x, next_y] = movei;
                StartCoroutine(solveKTUtil(next_x, next_y, movei + 1, sol, xMove, yMove, achou));
                //if (achou)
                //    result = true;
                //else
                //    sol[next_x, next_y] = -1;
            }
        }

        result = false;
    }

    YieldInstruction Processo(Action<bool> achou)
    {
        int[,] sol = new int[n, n];

        for (int x = 0; x < n; x++)
            for (int y = 0; y < n; y++)
                sol[x, y] = -1;

        int[] xMove = {2, 1, -1, -2,
                      -2, -1, 1, 2};
        int[] yMove = {1, 2, 2, 1,
                      -1, -2, -2, -1};

        sol[0, 0] = 0;
        
        return StartCoroutine(solveKTUtil(0, 0, 1, sol, xMove, yMove,achou));
    }

    public IEnumerator Startar()
    {
        bool result = false;
        yield return Processo(r => result = r);

        Debug.Log(result);
    }
}
