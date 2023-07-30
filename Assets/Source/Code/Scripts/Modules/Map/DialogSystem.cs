using System;
using Kingdox.UniFlux;
using UnityEngine;

public class DialogSystem : MonoFlux
{
    [SerializeField] private TextWriter textWriter, peopleWriter;
    public GeneralScriptableObject general;
    public TextScriptableObject text_news;
    public Action OnEnableEnter;
    protected override void OnFlux(in bool condition)
    {
        condition.Subscribe(ref textWriter.OnTextEnd, EnableEnter);
    }

    public void Init()
    {
        textWriter.ResetText();
        peopleWriter.ResetText();
    }

    public void SetText(PeopleTextScriptableObject peopleName)
    {
        
        textWriter.textTextMeshProUGUI.color = general.Peoples.Get(peopleName.people).peopleColor;
        peopleWriter.textTextMeshProUGUI.color = general.Peoples.Get(peopleName.people).peopleColor;

        textWriter.canPlaySound = false;

        textWriter.SetText(peopleName.Text);
        peopleWriter.SetText(general.Peoples.Get(peopleName.people).Name);
        
        peopleWriter.StartWrite();
        textWriter.StartWrite();
    }

    private void EnableEnter()
    {
        OnEnableEnter?.Invoke();
    }


    public void SetText(TextScriptableObject text)
    {
        textWriter.textTextMeshProUGUI.color = Color.white;
        peopleWriter.textTextMeshProUGUI.color = Color.white;
        
        textWriter.canPlaySound = true;
        ResetTexts();
        
        textWriter.SetText(text.Text);
        textWriter.StartWrite();

        peopleWriter.SetText(text_news.Text);
        peopleWriter.StartWrite();
    }

    public void ResetTexts()
    {
        textWriter.ResetText();
        peopleWriter.ResetText();
    }
}