using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestStamina
{
    [Test]
    public void TestUpdateColorBarFirst()
    {
        var gameObject = new GameObject();
        var stamina = gameObject.AddComponent<Stamina>();
        var image = gameObject.AddComponent<Image>();
        stamina.image = image;

        stamina.UpdateColorBar(true);
        Assert.AreEqual(Color.red,stamina.image.color);     
    }
    [Test]
    public void TestUpdateColorBarSecond()
    {
        var gameObject = new GameObject();
        var stamina = gameObject.AddComponent<Stamina>();
        var image = gameObject.AddComponent<Image>();
        stamina.image = image;

        stamina.UpdateColorBar(false);
        Assert.AreEqual(Color.blue, stamina.image.color);
    }
    [Test]
    public void TestUpdateStaminaBar()
    { 
        var gameObject = new GameObject();
        var stamina = gameObject.AddComponent<Stamina>();
        stamina.staminaBar = gameObject.transform;

        stamina.UpdateStaminaBar(50f);
        Assert.AreEqual(new Vector3(50, 1, 1), stamina.staminaBar.localScale);
    }
}
