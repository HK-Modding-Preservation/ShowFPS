using System.Globalization;
using Modding;
using UnityEngine;

namespace ShowFPS
{
    public class ShowFPS : Mod
    {
        public ShowFPS() : base("Show FPS")
        {
            GameObject go = new GameObject("fps counter", typeof(FpsCounter));
            GameObject.DontDestroyOnLoad(go);

            ModHooks.ApplicationQuitHook += () =>
            {
                //var counter = go.GetComponent<FpsCounter>();
                //foreach (var fps in counter.GetFpsNumbers())
                //{
                //    Log("[FPS] - " + fps.ToString("F1", CultureInfo.InvariantCulture));
                //}
            };
        }

        public override void Initialize()
        {
            Vector2 test2Orig = new Vector2(1f, 1f);
            Vector3 test3Orig = new Vector3(1f, 1f, 1f);
            Vector4 test4Orig = new Vector4(1f, 1f, 1f, 1f);

            Vector2 test2From2 = test2Orig;
            Vector2 test2From3 = test3Orig;
            Vector2 test2From4 = test4Orig;
            Vector3 test3From2 = test2Orig;
            Vector3 test3From3 = test3Orig;
            Vector3 test3From4 = test4Orig;
            Vector4 test4From2 = test2Orig;
            Vector4 test4From3 = test3Orig;
            Vector4 test4From4 = test4Orig;
        }
    }
}