using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Init(FsmControllerBase controller);
    public void StartState();
    public void UpdateState();
    public void FixedUpdateState();
    public void EndState();
}
