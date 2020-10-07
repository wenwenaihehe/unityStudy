using System.Collections;
using System.Collections.Generic;


public class StateMachine
{
    public enum stateType
    {
       STATE_START_GAME,
       STATE_STABLE,
       STATE_MACHINE,
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

                break;
            case stateType.STATE_MACHINE:
                break;
            case stateType.STATE_DISPOSE:
                break;
            case stateType.STATE_DROP:
                break;
            case stateType.STATE_DROP_OVER:
                break;
            case stateType.STATE_STABLE:
                break;
            default:
                break;
        }
    }
    public void startGame()
    {
        GameScene.getInstance();
    }
}