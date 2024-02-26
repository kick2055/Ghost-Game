using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTimer
{
    [Test]
    public void TestUpdateTimerDisplay()
    {
        var gameObject = new GameObject();
        var timer = gameObject.AddComponent<Timer>();
        var text1 = gameObject.AddComponent<TextMeshProUGUI>();

        timer.timeUI = text1;

        timer.UpdateTimerDisplay(135f);
        Assert.AreEqual('0',timer.timeUI.text[0]);
        Assert.AreEqual('2', timer.timeUI.text[1]);
        Assert.AreEqual('1', timer.timeUI.text[3]);
        Assert.AreEqual('5', timer.timeUI.text[4]);

    }
}
