using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fish: MonoBehaviour
{
    public String TitleTask;
    public SpriteRenderer spr;


    private void Update()
    {
        transform.GetChild(0).position = transform.position - new Vector3(0.1f, 0.05f);
    }

    public void PersonalizeFish()
    {
        spr.color = Random.ColorHSV();
    }

    public void Jump(Vector3 startPos)
    {
        Debug.Log("Jump");
        int dir = Random.value > 0.5f ? 1 : -1;
        int deg = dir == 1 ? 180 : 0;
        transform.rotation = Quaternion.Euler(0,deg,56.24f);
        transform.position = startPos;
        transform.DOMove(startPos + new Vector3(-1.5f * dir, 3), 0.5f);
        transform.DORotate( new Vector3(0, deg,  -50.2f), 0.5f).SetDelay(0.3f);
        transform.DOMove(startPos + new Vector3(-4 * dir,0), 0.5f).SetDelay(0.5f);

    }

}