using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sea : MonoBehaviour
{

    [SerializeField] private ParticleSystem splash;
    [SerializeField] private Vector2 seaLimits;
    [SerializeField] private float jumpDelay = 3;
     private float _jumpDelay ;

    private void Start()
    {
        _jumpDelay = jumpDelay;

        Transform child0 = transform.GetChild(0);
        float startPos = child0.localPosition.x;
        Sequence seq0 = DOTween.Sequence();
        
        seq0.Append(child0.DOLocalMoveX(startPos + 0.5f, 4));
        Transform child1 = transform.GetChild(1);
         startPos = child0.localPosition.x;
        child0.DOLocalMoveX(startPos + 0.5f, 4);
        
        Transform child2 = transform.GetChild(2);
         startPos = child0.localPosition.x;
        child0.DOLocalMoveX(startPos + 0.5f, 4);
    }

    private void Update()
    {
        _jumpDelay -= Time.deltaTime;
        if (_jumpDelay < 0 && !Fisherman.Instance.fishing)
        {
            _jumpDelay = jumpDelay;
            MakeJumpFish();
        }
    }
    
    void MakeJumpFish()
    {
        if(FishManager.Instance.fishList.Count == 0) return;
        Vector3 pos = new Vector3(Random.Range(seaLimits.x, seaLimits.y), -6f);
        FishManager.Instance.fishList[Random.Range(0, FishManager.Instance.fishList.Count - 1)].Jump(pos);

    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fish"))
        {
            _jumpDelay = 1;
            Instantiate(splash, col.transform.position, Quaternion.identity);
        }
      
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(seaLimits.x,transform.position.y + 1),new Vector3(seaLimits.x,transform.position.y - 1));
        Gizmos.DrawLine(new Vector3(seaLimits.y,transform.position.y + 1),new Vector3(seaLimits.y,transform.position.y - 1));
    }
}
