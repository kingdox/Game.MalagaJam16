using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Set;
using XavHelpTo.Know;


[Serializable]
public struct EnumData<TEnum, TValue>
    where TEnum : Enum
{
    public string name; // Para mostrar el contenido
    [HideInInspector] public TEnum enumme; // Tipo de valor
    public TValue value; // Valor
}

[Serializable]
public class ListEnum<TEnum,TValue>
    where TEnum : Enum
{
    public List<EnumData<TEnum,TValue>> list =  new List<EnumData<TEnum, TValue>>();
    public TValue[] Values => list.Select(d => d.value).ToArray();
    public TValue Get(TEnum @enum) => list.Find(l => l.enumme.Equals(@enum)).value;

    public void ValidateList()
    {
        int length = Supply.Lenght<TEnum>();
        EnumData<TEnum, TValue>[] datas = new EnumData<TEnum, TValue>[length];
        for (int i = 0; i < list.Count.Max(length); i++){
            object enumme = Enum.Parse(typeof(TEnum), i.ToString() ,true);
            datas[i] = new EnumData<TEnum, TValue>();
            datas[i].enumme = (TEnum)enumme;
            datas[i].value = list[i].value;
            // datas[i].name = $"{i} : {datas[i].enumme}".ToLower();
            datas[i].name = $"{i} : {datas[i].enumme}";
        }

        list.Clear();
        for (int i = 0; i < datas.Length; i++) list.Add(datas[i]);
    }

}
