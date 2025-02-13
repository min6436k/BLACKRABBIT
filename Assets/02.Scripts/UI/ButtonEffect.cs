using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Serialization;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private int particleCount = 7;
    [SerializeField] private float interval = 0.03f;
    public Color textHighlightColor;
    private ParticleSystem _particle;
    private TextMeshProUGUI _tmp;

    void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        _tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    #region 텍스트 색 변경

    public void OnPointerDown(PointerEventData eventData)
    {
        _tmp.color = textHighlightColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _tmp.color = Color.white;
    }

    #endregion

    #region 파티클 재생
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(SpawnParticles());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        _particle.Play();
    }
    
    #endregion

    IEnumerator SpawnParticles()
    {
        while (_particle.isPlaying)
        {
            yield return null;
        }

        for (int i = 0; i < particleCount; i++)
        {
            _particle.Emit(1);
            yield return new WaitForSeconds(interval);
        }

        _particle.Pause();
    }
}