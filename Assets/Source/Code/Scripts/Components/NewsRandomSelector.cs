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
    public NewsRandomSelector() 
    {
        int randomIndex1 = Random.Range(0, general.list_news_left.Length);
        int randomIndex2 = Random.Range(0, general.list_news_left.Length);

        // Evitar seleccionar el mismo índice dos veces
        while (randomIndex2 == randomIndex1)
        {
            randomIndex2 = Random.Range(0, newsData.newsArray.Length);
        }

        // Obtener las noticias seleccionadas
        string selectedNews1 = newsData.newsArray[randomIndex1];
        string selectedNews2 = newsData.newsArray[randomIndex2];

        // Mostrar las noticias seleccionadas en la consola
        Debug.Log("Noticias seleccionadas: ");
        Debug.Log(selectedNews1);
        Debug.Log(selectedNews2);

            // Eliminar las noticias seleccionadas del arreglo
            newsData.newsArray[randomIndex1] = null;
            newsData.newsArray[randomIndex2] = null;

            // Si todas las noticias se han borrado, mostrar mensaje y salir
            if (AllNewsDeleted())
            {
                Debug.Log("Todas las noticias han sido borradas.");
                return;
            }
    }
}
