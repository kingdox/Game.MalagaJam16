using System;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter, peopleWriter;
    public GeneralScriptableObject general;
    public event Action OnLastTextIsShown;

    public void Init()
    {
        textWriter.ResetText();
        peopleWriter.ResetText();
    }

    public void SetText(PeopleTextScriptableObject peopleName)
    {
        textWriter.SetText(peopleName.Text);
        peopleWriter.SetText(general.Peoples.Get(peopleName.people).Name);
    }


    public void SetText(TextScriptableObject text)
    {
        textWriter.ResetText();
        peopleWriter.ResetText();
        textWriter.SetText(text.Text);
    }
}