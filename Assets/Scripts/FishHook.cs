using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHook : MonoBehaviour
{
    [SerializeField] private LineRenderer rope;
    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 pos1 = rope.GetPosition(rope.positionCount - 1);
        Vector2 pos2 = rope.GetPosition(rope.positionCount - 2);
        transform.position = rope.GetPosition(rope.positionCount - 1) ;
        transform.rotation = Quaternion.LookRotation(Vector3.forward,pos2 - pos1);
    }

    public void HookFish(Transform fish)
    {
        fish.SetParent(transform);
        fish.localPosition = Vector3.zero + new Vector3(0.67f,-6.28f,0);
        fish.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
    }

    public Transform UnHook()
    {
        Transform fish=  transform.GetChild(0);
        fish.SetParent(null);
        return fish;
    }
    
}
