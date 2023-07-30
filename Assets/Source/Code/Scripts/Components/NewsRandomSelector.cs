using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;

public class NewsRandomSelector : MonoBehaviour
{
    public static NewsRandomSelector _;
    public GeneralScriptableObject general;
    public List<NewsScriptableObject> list_news_left = new List<NewsScriptableObject>();

    private void Awake()
    {       
        this.Singleton(ref _, true);
        // list_news_left = general.News.ToArray().ToList();
    }

    // int dia = 1;

    // public NewsRandomSelector() 
    // {
    //     Random rnd = new Random(6);
    //     int aux = rnd;
    //     if(dia == 1)
    //     {
    //         lst.RemoveAt(rnd);   //eliminar por posicion
    //         rnd.Next(1,6);
    //     }
    // }
}
