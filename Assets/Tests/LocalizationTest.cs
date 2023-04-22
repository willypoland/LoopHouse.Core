using System.Collections.Generic;
using System.Linq;
using Game.Scripts.L10n;
using NUnit.Framework;
using UnityEngine;

public class LocalizationTest
{
    [Test]
    public void LocalizationTestSimplePasses()
    {
        var kw = new[]
        {
            "box_title",
            "box_description",
            "door_title",
            "test_dialogue_0",
            "test_dialogue_1",
            "test_dialogue_2",
            "test_dialogue_3",
        };

        var ru = new[]
        {
            "Ящик",
            "Обычный ящик, создан для тестов",
            "Дверь",
            "Привет, это тестовый диалог.",
            "Как дела, например?",
            "Тестовые фразы как они есть...",
            "Конец диалога.",
        };

        var en = new[]
        {
            "Box",
            "A regular box, created for tests",
            "Door",
            "Hi, this is a test dialog.",
            "How are you, for example?",
            "Test phrases as they are...",
            "End of dialog.",
        };

        var gc = ScriptableObject.CreateInstance<GameConfig>();
        var service = new LocalizationService(gc);
        service.ChangeLanguage("ru");

        for (int i = 0; i < kw.Length; i++)
            Assert.AreEqual(ru[i], service.Get(kw[i]));
        
        service.ChangeLanguage("en");
        for (int i = 0; i < kw.Length; i++)
            Assert.AreEqual(en[i], service.Get(kw[i]));
    }

    [Test]
    public void TestParameterizedText()
    {
        var gc = ScriptableObject.CreateInstance<GameConfig>();
        var service = new LocalizationService(gc);
        
        var pos = new Vector3(245.861f, 421.5f, 0.0051f);
        var key = "test_param";
        var expect = "Моё имя: Ящик, а позиция: (x:245.9 y:421.5 z:0.005)";
        service.ChangeLanguage("ru");
        var name = service.Get("box_title");
        var text = service.Get(key, name, pos.x, pos.y, pos.z);
        Assert.AreEqual(expect, text);

        expect = "My name: Box, and position: (x:245.9 y:421.5 z:0.005)";
        service.ChangeLanguage("en");
        name = service.Get("box_title");
        text = service.Get(key, name, pos.x, pos.y, pos.z);
        Assert.AreEqual(expect, text);
    }

    [Test]
    public void NotExistText()
    {
        var gc = ScriptableObject.CreateInstance<GameConfig>();
        var service = new LocalizationService(gc);
        service.ChangeLanguage("ru");
        var text = service.Get("__not_exitst_example");
        Assert.AreEqual("KEY_NOT_EXIST:__not_exitst_example", text);
    }
}
