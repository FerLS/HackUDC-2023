using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance { get; private set; }
    
    [HideInInspector] public List<Fish> fishList = new();

    [Header("FishSettings")] [SerializeField]
    private GameObject fishPrefab;

    [Header("Tasks")] 
   [SerializeField]  private TextMeshProUGUI taskText;

    [SerializeField] private Image taskBackground;
    

    private void Awake()
    {
        Instance = this;
    }

    public void AddTask(TMP_InputField taskTitle)
    {
        if(taskTitle.text == string.Empty) return;
        fishList.Add(CreateFish(taskTitle.text));
        taskTitle.text = string.Empty;
    }

    Fish CreateFish(string taskTitle)
    {
        Vector3 startPos = new Vector3(3.96f, 2.31f, 0.223f);

        Fish f = Instantiate(fishPrefab,startPos,Quaternion.Euler(0, 180, 56.35f)).GetComponent<Fish>();
        f.TitleTask = taskTitle;


        Transform fTransform = f.transform;
        
        fTransform.DOMove(startPos + new Vector3(-4, 1.0f), 0.5f);
        fTransform.DORotate(new Vector3(0, 180, -800), 0.5f,RotateMode.FastBeyond360).SetDelay(0.2f);
        fTransform.DOMove(startPos + new Vector3(-6 , -10), 0.5f).SetDelay(0.6f);
        
        f.PersonalizeFish();
        return f;
    }

    public void  DestroyFish(Fish fish)
    {
        this.taskText.gameObject.SetActive(true);
        taskBackground.gameObject.SetActive(true);
        
        string taskText = fish.TitleTask;
        fishList.Remove(fish);

        taskBackground.DOFade(0.1568f, 0.3f);
        this.taskText.DOFade(1, 0.5f);
        this.taskText.text = taskText;



        this.taskText.DOFade(0, .4f).SetDelay(5);
        taskBackground.DOFade(0, 0.5f).SetDelay(5).OnComplete(() =>
        {
                    
            this.taskText.gameObject.SetActive(false);
            taskBackground.gameObject.SetActive(false); 

        });
        Fisherman.Instance.SaveFish();


    }



}