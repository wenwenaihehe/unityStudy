using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateMachine
{
    public enum stateType
    {
       STATE_START_GAME,
       STATE_STABLE,
       STATE_DISPOSE,
       STATE_DROP,
       STATE_DROP_OVER
    };
    private stateType m_sType;
    static StateMachine m_Instance = null;
    public static StateMachine getInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new StateMachine();
            m_Instance.init();
        }
        return m_Instance;
    }
    private void init()
    {
        m_sType = stateType.STATE_START_GAME;

    }
    public stateType GetStateType()
    {
        return this.m_sType;
    }
    public void setState(stateType nType)
    {
        m_sType = nType;
        switch (m_sType)
        {
            case stateType.STATE_START_GAME:
                startGame();
                break;
            case stateType.STATE_DISPOSE:
                dispose();
                break;
            case stateType.STATE_DROP:
                drop();
                break;
            case stateType.STATE_DROP_OVER:
                dropover();
                break;
            case stateType.STATE_STABLE:
                stable();
                break;
            default:
                break;
        }
    }
    public void startGame()
    {
        if (Match3Config.getInstance().getMatchPattern().Count > 0)
        {
            setState(stateType.STATE_DISPOSE);
        }
        else
        {
            setState(stateType.STATE_STABLE);
        }
    }
    public void dispose()
    {
       // GameScene.getInstance().CheckConnect();
        ArrayList pDisposeList = Match3Config.getInstance().getMatchPattern();

        for (int i = 0; i < pDisposeList.Count; i++)
        {
            ArrayList curPointList = (ArrayList)pDisposeList[i];
            for (int j = 0; j < curPointList.Count; j++)
            {
                Vector3 curPoint = (Vector3)curPointList[j];
                GameScene m_GameScene = GameObject.Find("Back").GetComponent<GameScene>();
                GameObject pTile = m_GameScene.getTile((int)curPoint.x, (int)curPoint.y);

                if (pTile)
                {
                    GameObject pItem = pTile.GetComponent<BaseTile>().getItem();
                    if (pItem)
                    {
                        pItem.GetComponent<fruit>().onDisposed();
                    }
                        // pTile.GetComponent<BaseTile>().AttachItem(null);
                }
            }
        }
        setState(stateType.STATE_DROP);
    }
    public void drop()
    {
        GameScene m_GameScene = GameObject.Find("Back").GetComponent<GameScene>();
        m_GameScene.startDrop();
    }
    public void dropover()
    {
        if (Match3Config.getInstance().getMatchPattern().Count > 0)
        {
            setState(stateType.STATE_DISPOSE);
            return;
        }
        setState(stateType.STATE_STABLE);
    }
    public void stable()
    {

    }
}