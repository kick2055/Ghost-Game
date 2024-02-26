using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMapGenerator
{   
    [Test]
    public void TestRandomIdsFirst()
    {
        var gameObject = new GameObject();
        var mapGenerator = gameObject.AddComponent<MapGenerator>();
        List<int> list = mapGenerator.RandomIds(9,3);
        Assert.AreEqual(3, list.Count);
    }
    [Test]
    public void TestRandomIdsSecond()
    {
        var gameObject = new GameObject();
        var mapGenerator = gameObject.AddComponent<MapGenerator>();
        List<int> list = mapGenerator.RandomIds(9, 3);
        Assert.Less(list[0],9 );
    }
    [Test]
    public void TestCheckWhatChank()
    {
        var gameObject = new GameObject();
        var mapGenerator = gameObject.AddComponent<MapGenerator>();
        MapGenerator.Point point = mapGenerator.CheckWhatChank(11.1f, 3.1f);
        Assert.AreEqual(4,point.X);
        Assert.AreEqual(1, point.Y);
    }
    [Test]
    public void TestCalculateDistance()
    {
        var gameObject = new GameObject();
        var mapGenerator = gameObject.AddComponent<MapGenerator>();
        int distance = mapGenerator.CalculateDistance(0, 0, 6, 8);
        Assert.AreEqual(100, distance);
    }
}
