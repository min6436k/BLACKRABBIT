using System.Collections;
using DG.Tweening;
using UnityEngine;

public class NotePaper : UIView
{
    public Transform papers;
    public Transform rightPos;
    public Transform leftPos;

    void Start()
    {
        for (int i = 0; i < papers.childCount; i++)
        {
            papers.GetChild(i).DOLocalRotate(new Vector3(0, 0, -5 + i * 2.5f), 1f);
        }

        transform.DOLocalMove(Vector3.zero, 1).SetEase(Ease.OutQuart);
    }
    [ContextMenu("set")]
    public void SetLocation()
    {
        if(papers.childCount > 4)
            StartCoroutine(SetLocationCoroutine());

    }
    

    IEnumerator SetLocationCoroutine()
    {
        papers.transform.DOMove(rightPos.position, 1f).SetEase(Ease.InOutSine);
        papers.transform.DORotate(rightPos.eulerAngles, 1f).SetEase(Ease.InOutSine);

        Transform temp = papers.GetChild(papers.childCount - 1);
        temp.SetParent(transform.parent);

        yield return null;
        
        temp.DORotate(leftPos.eulerAngles, 0.5f).SetEase(Ease.InOutSine);
        yield return temp.DOMove(leftPos.position, 0.5f).SetEase(Ease.InOutSine).WaitForCompletion();
        
        papers.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
        papers.transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
        
        temp.DORotate(papers.transform.eulerAngles, 0.5f).SetEase(Ease.InOutSine);
        temp.DOLocalMove(new Vector3(0,-321.28f,0), 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            for (int i = 0; i < papers.childCount; i++)
            {
                papers.GetChild(i).DOLocalRotate(new Vector3(0, 0, -5 + i * 2.5f), 0.5f);
            }
        });

        temp.parent = papers.transform;
        temp.SetSiblingIndex(0);
        


    }
}
