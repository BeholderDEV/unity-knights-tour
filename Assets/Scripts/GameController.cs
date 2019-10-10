using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int size = 8;
    public InputField SizeField;
    public GameObject loadPanel;
    public BoardGenerator boardGenerator;
    public BoardTopController BoardTopController;
    public BackTrackREalOFC morte;
    public CameraController camera;
    public BackTracking bt;
    public Warnsdorf wsd;
    public WarnsdorfCounter wsdc;
    public Vector2 initialPosition;
    private Vector2[] solucao;
    
    // Start is called before the first frame update
    void Start()
    {
        boardGenerator.Generate(size);
        camera.AdjustPosition(size);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(initialPosition.x + " - " + initialPosition.y);
    }

    public void PlacePiece()
    {
        initialPosition = BoardTopController.PlaceHorse(size);
    }

    public void PlacePiece00()
    {
        initialPosition = BoardTopController.PlaceHorse00(size);
    }

    public void ChangeSize()
    {
        size = int.Parse(SizeField.text);
        boardGenerator.Generate(size);
        camera.AdjustPosition(size);
        wsd.SetSize(size);
    }

    public void DoBackTrack() {
        //loadPanel.SetActive(true);
        StartCoroutine(bt.Executar(initialPosition));
        //StartCoroutine(findPath());
    }

    public void DoWarnsdorf()
    {
        wsd.Executar((int)initialPosition.x, (int)initialPosition.y);
        BoardTopController.StartMoving();
    }

    public void DoWarnsCount()
    {
        int[] testSizes = { 8, 16, 32};

        for(int a = 0; a<10; a++)
        {
            for (int i = 0; i < testSizes.Length; i++)
            {
                Debug.Log("Teste com " + testSizes[i]);
                //StartCoroutine(wsdc.Executar(0, 0, testSizes[i]));
                wsdc.Executar(Random.Range(0, testSizes[i]), Random.Range(0, testSizes[i]), testSizes[i]);
            }
        }
                
    }

    IEnumerator findPath()
    {
        //if (solution != null)
            //solucao = solution;
        loadPanel.SetActive(false);
        yield return null;
    }
}
