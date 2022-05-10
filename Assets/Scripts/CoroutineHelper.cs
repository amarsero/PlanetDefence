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
    public static IEnumerator WaitAnd(YieldInstruction delay, Action action)
    {
        yield return delay;
        action();
    }
}
