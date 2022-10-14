using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> GameCubes = new List<GameObject>();
    public List<GameObject> pocketTable;

    public GameObject winPanel;
    public GameObject DefeatPanel;

    public int[,] line_data = new int[3, 3]
    {
        {0, 1, 2},
        {3, 4, 5},
        {6, 7, 8}
    };

    private int[] columnIndexes = new int[3]
    {
        0,1,2
    };

    void Start()
    {
        GameCubes.AddRange(GameObject.FindGameObjectsWithTag("Object"));
    }

    private (int,int) GetPoketPosition(int poket_index)
    {
        int pos_row = -1;
        int pos_col = -1;

        for(int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if(line_data[row,col] == poket_index)
                {
                    pos_row = row;
                    pos_col = col;
                }
            }
        }

        return (pos_row, pos_col);
    }

    public int[] GetVerticalLine(int poket_index)
    {
        int[] line = new int[3];
        var poket_position_col = GetPoketPosition(poket_index).Item2;

        for(int index = 0; index < 3; index++)
        {
            line[index] = line_data[index, poket_position_col];
        }

        return line;
    }

    public void CheckIfComletedLine()
    {
        List<int[]> lines = new List<int[]>();

        //columns
        foreach(var column in columnIndexes)
        {
            lines.Add(GetVerticalLine(column));
        }

        //rows
        for(var row = 0; row < 3; row++)
        {
            List<int> data = new List<int>();
            for(int index = 0; index < 3; index++)
            {
                data.Add(line_data[row, index]);
            }

            lines.Add(data.ToArray());
        }

        var comletedLines = CheckIfAreCompleted(lines);

        StartCoroutine(CheckEnd());
    }

    private int CheckIfAreCompleted(List<int[]> data)
    {
        List<int[]> compeletedLines = new List<int[]>();

        var linesCompleted = 0;

        foreach (var line in data)
        {
            bool lineComp = true;
            foreach (var pocketIndex in line)
            {

                if (pocketTable[pocketIndex].GetComponent<TablePoket>().busy == false)
                {
                    lineComp = false;
                }
            }

            if (lineComp)
            {
                compeletedLines.Add(line);
            }
        }

        foreach ( var line in compeletedLines)
        {
            bool comleted = false;

            foreach (var pocketIndex in line)
            {
                GameObject obj = pocketTable[pocketIndex].transform.GetChild(0).gameObject;
                pocketTable[pocketIndex].GetComponent<TablePoket>().busy = false;
                Destroy(obj);
                comleted = true;
            }
            if (comleted)
            {
                linesCompleted++;
            }
        }

        return linesCompleted;
    }

    IEnumerator CheckEnd()
    {
        int sk = 9;

        for(int i = 0; i < 9; i++)
        {
            if(pocketTable[i].GetComponent<TablePoket>().busy == false)
            {
                sk--;
            }
        }

        yield return new WaitForSeconds(0.2f);
        if (sk == 0)
        {
            winPanel.SetActive(true);
        }
        else if(GameCubes.Count > 0)
        {
            DefeatPanel.SetActive(true);
        }
    }
}
