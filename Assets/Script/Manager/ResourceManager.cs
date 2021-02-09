﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    public GameObject Instantiate(string path)
    {
        GameObject insObj = GameObject.Instantiate(Resources.Load<GameObject>($"Prefabs/{path}"));

        return insObj;
    }
}
