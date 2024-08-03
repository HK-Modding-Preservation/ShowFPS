using Modding;
using UnityEngine;

namespace ShowFPS;

public class ShowFPS : Mod
{
    public ShowFPS() : base("Show FPS")
    {
        GameObject go = new GameObject("fps counter", typeof(FpsCounter));
        GameObject.DontDestroyOnLoad(go);
    }

    public override void Initialize()
    {
    }
}