using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fisherman : MonoBehaviour
{
    public static Fisherman Instance { private set; get; }
    [HideInInspector] public bool fishing;
    [SerializeField] private Transform brazos;

    [SerializeField] private FishHook fishHook;
    private void Awake()
    {
        Instance = this;
    }


    public void ThrowRod()
    {
        if (fishing || FishManager.Instance.fishList.Count == 0) return;
        fishing = true;

        FishMinigame.Instance.PlayMinigame();
        
    }

  
    
    public async void CatchFish()
    {
        Fish f = FishManager.Instance.fishList[Random.Range(0, FishManager.Instance.fishList.Count - 1)];
        
        fishHook.HookFish(f.transform);
        FishManager.Instance.DestroyFish(f);
        fishing = false;
    }

    public async void CatchFailed()
    {
        fishing = false;
    }

    public void SaveFish()
    {
        
        brazos.DORotate(Vector3.forward * 19.76f, 0.3f);
        
        Transform fish =  fishHook.UnHook();
        
        brazos.DORotate( Vector3.forward * -10.84f, 0.3f).SetDelay(0.3f);

        
        fish.DOMove(new Vector3(-5.51f, 3.35f), 0.33f);
        fish.DORotate(new Vector3(0, 180, -33.07f), 0.3f);
        fish.DORotate(new Vector3(0, 180, -90),0.5f).SetDelay(0.3f);
        fish.DOMove(new Vector3(-7.53f, -0.95f), 0.3f).SetDelay(0.3f);
        fish.DOScale(0.2f, 0.3f).SetDelay(0.3f).OnComplete(() => {        Destroy(fish.gameObject);
        });



    }
}