using System;
using UnityEngine;
using System.Collections;
public class Match3Config
{
    private int nWidth;
    private int nHeight;
    private GameScene m_GameScene;
    public Match3Config()
    {
        m_GameScene = GameObject.Find("Back").GetComponent<GameScene>();
        nWidth = m_GameScene.getBoardWidth();
        nHeight = m_GameScene.getBoardHeight();
    }
    static Match3Config m_pInstance = null;
    static public Match3Config getInstance()
    {
        if (m_pInstance == null)
        {
            m_pInstance = new Match3Config();
        }
        return m_pInstance;
    }
    ~Match3Config()
    {
        m_pInstance = null;
    }
    public ArrayList getMatchPattern()
    {
        GameObject[,] m_pTile = m_GameScene.getAllTile();
        ArrayList targetList = new ArrayList();
        int[,] mark = new int[nWidth, nHeight];
        for (int nx = 0; nx < nWidth; nx++)
        {
            for (int ny = 0; ny < nHeight; ny++)
            {
                mark[nx, ny] = 0;
            }
        }
        for (int x = 0; x < nWidth; x++)
        {
            for (int y = 0; y < nHeight; y++)
            {
                if (mark[x, y] == 1)
                {
                    continue;
                }
                if (m_pTile[x, y].GetComponent<BaseTile>().getItem() != null)
                {
                    GameObject pItem = m_pTile[x, y].GetComponent<BaseTile>().getItem();
                    ArrayList ArrayPos = GetConnectPos(x, y, pItem.GetComponent<fruit>().getColor());
                    if (ArrayPos.Count >= 3)
                    {
                        ArrayList parttern = getPartternStart(ArrayPos, 3);
                        if (parttern.Count >= 3)
                        {
                            targetList.Add(parttern);
                            for (int z = 0; z < parttern.Count; z++)
                            {
                                Vector3 curPos = (Vector3)parttern[z];
                                mark[(int)curPos.x, (int)curPos.y] = 1;
                            }
                        }
                    }
                }
            }

        }
        return targetList;
    }
    ArrayList GetConnectPos(int x, int y, int nColor)
    {
        ArrayList targetList = new ArrayList();
        int[,] mark = new int[nWidth, nHeight];
        for (int nx = 0; nx < nWidth; nx++)
        {
            for (int ny = 0; ny < nHeight; ny++)
            {
                mark[nx, ny] = 0;
            }
        }
        getConnect(nColor, x, y, targetList, mark);
        return targetList;
    }
    void getConnect(int color, int x, int y, ArrayList vector3s, int[,] mark)
    {
        if (x < 0 || x >= nWidth || y < 0 || y >= nWidth)
        {
            return;
        }
        if (mark[x, y] == 1)
        {
            return;
        }
        GameObject[,] m_pTile = m_GameScene.getAllTile();
        Vector3 curPos = new Vector3(x, y);
        Vector3[] pPos = { new Vector3(0, 1), new Vector3(1, 0), new Vector3(-1, 0), new Vector3(0, -1) };
        mark[x, y] = 1;
        if (m_pTile[x, y].GetComponent<BaseTile>().getItem() != null)
        {
            GameObject pItem = m_pTile[x, y].GetComponent<BaseTile>().getItem();
            if (pItem.GetComponent<fruit>().getColor() == color)
            {
                Vector3 pos = new Vector3(x, y, 0);
                vector3s.Add(pos);
                for (int i = 0; i < 4; i++)
                {
                    Vector3 targetPos = curPos + pPos[i];
                    getConnect(color, (int)targetPos.x, (int)targetPos.y, vector3s, mark);
                }
            }
        }
    }
    ArrayList getPartternStart(ArrayList points, int Line)
    {
        ArrayList _targetList = new ArrayList();
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 curPoint = (Vector3)points[i];
            ArrayList Ts = checkLinePattern(points, Line, curPoint);
            if (Ts.Count >= Line)
            {
                return Ts;
            }
        }
        return _targetList;
    }
    ArrayList checkLinePattern(ArrayList points, int Line, Vector3 curPoint)
    {
        Vector3[] pPos = { new Vector3(0, 1), new Vector3(1, 0), new Vector3(-1, 0), new Vector3(0, -1) };
        ArrayList _targetList = new ArrayList();
        _targetList.Add(curPoint);
        for (int i = 0; i < 4; i++)
        {
            bool haveTarget = true;
            for (int j = 1; j < Line; j++)
            {
                Vector3 Tf = curPoint + pPos[i] * j;
                if (!points.Contains(Tf))
                {
                    haveTarget = false;
                    break;
                }
            }
            if (haveTarget)
            {
                for (int j = 1; j < Line; j++)
                {
                    Vector3 Tf = curPoint + pPos[i] * j;

                    _targetList.Add(Tf);
                }
                break;
            }
        }

        return _targetList;
    }
}
