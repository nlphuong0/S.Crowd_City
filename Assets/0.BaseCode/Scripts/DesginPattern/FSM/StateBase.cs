using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour, IState
{
    protected FsmControllerBase controller;

    public virtual void Init(FsmControllerBase fsmController)
    {
        controller = fsmController;
    }
    public virtual void StartState()
    {
    }

    public virtual void UpdateState()
    {
    }

    public virtual void FixedUpdateState()
    {
    }

    public virtual void EndState()
    {
    }
}