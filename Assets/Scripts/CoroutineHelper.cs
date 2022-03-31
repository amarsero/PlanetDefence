using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineHelper
{
    public static IEnumerator WaitSecondsAnd(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}
