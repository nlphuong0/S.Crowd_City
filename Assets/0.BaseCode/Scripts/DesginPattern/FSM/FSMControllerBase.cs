using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum TypeState
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    Die = 3,
}

public class FsmControllerBase : SerializedMonoBehaviour
{
    [TitleGroup("STATE")] public TypeState currentTypeState;

    [TitleGroup("STATE")] [SerializeField] private Dictionary<TypeState, StateBase> _states = new Dictionary<TypeState, StateBase>();


    public virtual void Init()
    {
        foreach (var state in _states)
        {
            state.Value.Init(this);
        }
    }

    public void ChangeState(TypeState nextState)
    {
        _states[currentTypeState].EndState();
        this.currentTypeState = nextState;
        _states[currentTypeState].StartState();
    }

    protected virtual void Update()
    {
        _states[currentTypeState].UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        _states[currentTypeState].FixedUpdateState();
    }
}