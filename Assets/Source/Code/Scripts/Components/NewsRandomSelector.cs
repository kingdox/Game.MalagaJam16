using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using XavHelpTo;
using XavHelpTo.Get;

public class NewsRandomSelector : MonoBehaviour
{
    public static NewsRandomSelector _;
    public GeneralScriptableObject general;
    // public List<NewsScriptableObject> listNewsLeft = new List<NewsScriptableObject>();
    private List<NewsScriptableObject> _copyListNewsLeft;
    private readonly int newsInEachRound = 2;

    private void Awake()
    {
        this.Singleton(ref _, true);
        // list_news_left = general.News.ToArray().ToList();
        _copyListNewsLeft = new List<NewsScriptableObject>(general.News);
    }

    public List<NewsScriptableObject> GetTwoRandomNews()
    {
        var selectedNews = new List<NewsScriptableObject>();
        var selected = 0;
        do
        {
            var soundEnum = Get.Range(_copyListNewsLeft.ToArray());
            _copyListNewsLeft.Remove(soundEnum);
            selectedNews.Add(soundEnum);
            selected++;
        } while (selected < newsInEachRound);

        return selectedNews;
    }
}