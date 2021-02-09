using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Core
    public Managers s_instance;
    public static ResourceManager _resource = new ResourceManager();
    public static SoundManager _sound = new SoundManager();
    public static UIManager _ui = new UIManager();
    public static DataManager _data = new DataManager();
    public Managers Instance { get { Init(); return s_instance; } }
    public static ResourceManager Resource { get { return _resource; } }
    public static SoundManager Sound { get { return _sound; } }
    public static UIManager UI { get { return _ui; } }
    public static DataManager Data { get { return _data; } }
    #endregion

    void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
