using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayer
{
    [Test]
    public void TestChangeSpeed()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<Player>();
        player.ChangeSpeed(0.1f);
        Assert.LessOrEqual(-0.1f, player.weightSlow);
    }
    [Test]
    public void TestCheckWhatChankFirst()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<Player>();
        Vector3 testVector = player.CheckWhatChank(1f, 5f);
        Vector3 finalVector = new Vector3(0, 1, 0);
        Assert.AreEqual(finalVector, testVector);
    }
    [Test]
    public void TestCheckWhatChankSecond()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<Player>();
        Vector3 testVector = player.CheckWhatChank(-3f, -7f);
        Vector3 finalVector = new Vector3(-2, -3, 0);
        Assert.AreEqual(finalVector, testVector);
    }





}
