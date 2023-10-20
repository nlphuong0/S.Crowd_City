using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif


public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public UseProfile useProfile;
    public DataContain dataContain;
    public MusicManager musicManager;
    [HideInInspector] public SceneType currentScene;


    protected void Awake()
    {
        Instance = this;
        Init();
        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        musicManager.PlayBGMusic();
     
    }

    public void Init()
    {

        UseProfile.NumberOfAdsInPlay = 0;
        musicManager.Init();
    }

    public void LoadScene(string sceneName)
    {
        Initiate.Fade(sceneName.ToString(), Color.black, 2f);
    }

  
}
public enum SceneType
{
    StartLoading = 0,
    MainHome = 1,
    GamePlay = 2
}