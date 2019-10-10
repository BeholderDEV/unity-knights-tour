using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnsdorfCounter : MonoBehaviour
{
    int N = 8;
    static int[] cx = { 1, 1, 2, 2, -1, -1, -2, -2 };
    static int[] cy = { 2, -2, 1, -1, 2, -2, 1, -1 };
    //public List<Vector3> finalList;
    public int sx;
    public int sy;

    public double Timer = 0.0;
    public int interactions = 0;

    public void SetSize(int i)
    {
        N = i;
    }

    public void Executar(int initialX, int initialY, int size)
    {
        Timer = Time.deltaTime;
        interactions = 0;
        SetSize(size);
        sx = initialX;
        sy = initialY;
        while (!findClosedTour())
        {
            
        }       

        Timer += Time.deltaTime;
        print();
        //yield return null;
        //print("Finished with " + finalList.Count + " moves");
    }
    // function restricts the knight to remain within 
    // the 8x8 chessboard 
    bool Limits(int x, int y)
    {
        return ((x >= 0 && y >= 0) && (x < N && y < N));
    }
    // function restricts the knight to remain within 
    // the 8x8 chessboard 
    bool IsEmpty(int[] a, int x, int y)
    {
        return (Limits(x, y)) && (a[y * N + x] < 0);
    }
    /* Returns the number of empty squares adjacent 
   to (x, y) */
    int GetDegree(int[] a, int x, int y)
    {
        int count = 0;
        for (int i = 0; i < 8; ++i)
            if (IsEmpty(a, (x + cx[i % 8]), (y + cy[i % 8])))
                count++;

        return count;
    }

    // Picks next point using Warnsdorff's heuristic. 
    // Returns false if it is not possible to pick 
    // next point. 
    bool nextMove(int[] a, ref int x, ref int y)
    {
        int min_deg_idx = -1, c, min_deg = (N + 1), nx, ny;

        // Try all N adjacent of (*x, *y) starting 
        // from a random adjacent. Find the adjacent 
        // with minimum degree. 
        int start = Random.Range(0, 8);
        for (int count = 0; count < 8; ++count)
        {
            interactions++;
            int i = (start + count) % 8;
            nx = x + cx[i % 8];
            ny = y + cy[i % 8];
            if ((IsEmpty(a, nx, ny)) &&
               (c = GetDegree(a, nx, ny)) < min_deg)
            {
                min_deg_idx = i;
                min_deg = c;
            }
        }

        // IF we could not find a next cell 
        if (min_deg_idx == -1)
            return false;

        // Store coordinates of next point 
        nx = x + cx[min_deg_idx % 8];
        ny = y + cy[min_deg_idx % 8];

        // Mark next move 
        a[ny * N + nx] = a[(y) * N + (x)] + 1;


        //finalList.Add(new Vector3(nx, 0, ny));
        // Update next point 
        x = nx;
        y = ny;

        return true;
    }

    /* displays the chessboard with all the 
  legal knight's moves */
    void print()
    {
        Debug.Log("Interações para N=" + N + " foram: " + interactions);
        Debug.Log("Tempo para N=" + N + " foram: " + Timer);
    }

    /* checks its neighbouring sqaures */
    /* If the knight ends on a square that is one 
       knight's move from the beginning square, 
       then tour is closed */
    bool neighbour(int x, int y, int xx, int yy)
    {
        for (int i = 0; i < 8; ++i)
            if (((x + cx[i % 8]) == xx) && ((y + cy[i % 8]) == yy))
                return true;
            

        return false;
    }

    /* Generates the legal moves using warnsdorff's 
      heuristics. Returns false if not possible */
    bool findClosedTour()
    {
        //finalList.Clear();
        // Filling up the chessboard matrix with -1's 
        int[] a = new int[N * N];
        for (int i = 0; i < N * N; ++i)
            a[i] = -1;

        // Randome initial position 


        // Current points are same as initial points 
        int x = sx, y = sy;
        a[y * N + x] = 1; // Mark first move.
        //finalList.Add(new Vector3(sx, 0, sy));

        // Keep picking next points using 
        // Warnsdorff's heuristic 
        for (int i = 0; i < N * N - 1; ++i)
            if (!nextMove(a, ref x, ref y))
                return false;

        // Check if tour is closed (Can end 
        // at starting point) 
        if (!neighbour(x, y, sx, sy))
            return false;
        //finalList = a;
        return true;
    }
}
