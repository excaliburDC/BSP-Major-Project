using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Obstracle : MonoBehaviour,IComparable<Obstracle>
{
    public SpriteRenderer MySpriteRender{get;set;}

    private Color defaultColor;
    private Color fadedColor;

    public int CompareTo(Obstracle other)
    {
        if(MySpriteRender.sortingOrder>other.MySpriteRender.sortingOrder)
        {
            return 1;
        }else if (MySpriteRender.sortingOrder < other.MySpriteRender.sortingOrder)
        {
            return -1;
        }
        return 0;
    }
    void Start()
    {
        MySpriteRender = GetComponent<SpriteRenderer>();
        defaultColor = MySpriteRender.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.5f;
    }
    public void FadeOut()
    {
        MySpriteRender.color = fadedColor;
    }
    public void FadeIn()
    {
        MySpriteRender.color = defaultColor;
    }
}
