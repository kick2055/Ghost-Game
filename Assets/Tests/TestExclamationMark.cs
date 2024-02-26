using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestExclamationMark
{
    [Test]
    public void TestUpdateColorExclamationFirst()
    {
        var gameObject = new GameObject();
        var exclamationMark = gameObject.AddComponent<ExclamationMark>();
        var image = gameObject.AddComponent<Image>();
        exclamationMark.image = image;
        exclamationMark.UpdateColorExclamation("red");
        Assert.AreEqual(Color.red, exclamationMark.image.color);
    }
    [Test]
    public void TestUpdateColorExclamationSecond()
    {
        var gameObject = new GameObject();
        var exclamationMark = gameObject.AddComponent<ExclamationMark>();
        var image = gameObject.AddComponent<Image>();
        exclamationMark.image = image;
        exclamationMark.UpdateColorExclamation("yellow");
        Assert.AreEqual(Color.yellow, exclamationMark.image.color);
    }
    [Test]
    public void TestUpdateColorExclamationThird()
    {
        var gameObject = new GameObject();
        var exclamationMark = gameObject.AddComponent<ExclamationMark>();
        var image = gameObject.AddComponent<Image>();
        exclamationMark.image = image;
        exclamationMark.UpdateColorExclamation("random words");
        Assert.AreEqual(Color.green, exclamationMark.image.color);
    }
}
