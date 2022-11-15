using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameExtensions
{
    public static T GetNext<T>(this List<T> list, T currentItem)
    {
        var index = list.IndexOf(currentItem);
        if (index == list.Count - 1)
            return list[0];
        else
            return list[index + 1];
    }

    public static List<T> ShuffleAndCopy<T>(this List<T> list)
    {
        return list.OrderBy(t => UnityEngine.Random.value).ToList();
    }
}
