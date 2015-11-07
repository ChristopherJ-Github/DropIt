using UnityEngine;
using System.Collections;

[System.Serializable]
public class RandomObject
{
    public string name;
    public Texture2D thumbnail;
}

[System.Serializable]
public class Character : RandomObject
{
    public GameObject prefab;
}

[System.Serializable]
public class Dropable : RandomObject
{
    public GameObject prefab;
}

[System.Serializable]
public class Planet : RandomObject
{
    public string sceneName;
}

