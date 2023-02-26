using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishMinigame : MonoBehaviour
{
    public static FishMinigame Instance { get; private set; }

    [SerializeField] private Vector2 limits;
    [SerializeField] private Transform arrow;
    [SerializeField] private Transform block;
    [SerializeField] private SpriteRenderer[] sprs;

    private Sequence minigameSeq;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Fisherman.Instance.fishing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                minigameSeq.Kill();
                arrow.DOPunchScale(1.01f * Vector3.one, 0.3f, 2, 0.2f);

                TryCatch();
            }
        }
    }

    public void PlayMinigame()
    {
        gameObject.SetActive(true);
        foreach (SpriteRenderer s in sprs)
        {
            s.DOFade(1, 0.2f);
        }
        transform.DOPunchScale(0.46f* Vector3.one, 0.3f, 2, 0.2f);
        arrow.localPosition = new Vector3(arrow.localPosition.x, Random.Range(limits.x, limits.y));
        minigameSeq = DOTween.Sequence();
        block.transform.localPosition = new Vector3(0, -4, 0);
        minigameSeq.Append(block.DOLocalMoveY(4, 1).SetEase(Ease.Linear));
        minigameSeq.Append(block.DOLocalMoveY(-4, 1).SetEase(Ease.Linear));

        minigameSeq.SetLoops(-1);
    }

    void TryCatch()
    {
        foreach (SpriteRenderer s in sprs)
        {
            s.DOFade(0, 0.2f);
        }
        gameObject.SetActive(false);
        Rope.Instance.isHooked = false;

        if (arrow.position.y < (block.position.y + 0.55f) && arrow.position.y > (block.position.y - 0.55f))
        {
            Fisherman.Instance.CatchFish();
        }
        else
        {
            Fisherman.Instance.CatchFailed();
        }
    }


 

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(limits.y * Vector3.up, limits.y * Vector3.up - Vector3.right);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(limits.x * -Vector3.up, limits.x * -Vector3.up - Vector3.right);
    }
}