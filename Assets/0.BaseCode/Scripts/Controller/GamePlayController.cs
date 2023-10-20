using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    public GameScene gameScene;
    public Joystick joystick;
    
    public void Start()
    {  
        gameScene.Int();
    }
}