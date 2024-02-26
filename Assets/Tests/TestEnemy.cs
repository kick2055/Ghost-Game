using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestEnemy
{
    [Test]
    public void TestCheckIfCloseToMiddleOfChankFirst()
    {
        var gameObject = new GameObject();
        var enemy = gameObject.AddComponent<Enemy>();
        enemy.transform.position = new Vector3(4, 7, 0);
        bool close = enemy.CheckIfCloseToMiddleOfChank(new Vector3(3,3,0));
        Assert.AreEqual(false, close);
    }
    [Test]
    public void TestCheckIfCloseToMiddleOfChankSecond()
    {
        var gameObject = new GameObject();
        var enemy = gameObject.AddComponent<Enemy>();
        enemy.transform.position = new Vector3(3.84f, 6.4f, 0);
        bool close = enemy.CheckIfCloseToMiddleOfChank(new Vector3(1, 2, 0));
        Assert.AreEqual(true, close);
    }
}
