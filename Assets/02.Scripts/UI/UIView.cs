using UnityEngine;

/// <summary>
/// 프리팹화 시킨 UI에 적용하는 클래스
/// </summary>
public abstract class UIView : MonoBehaviour
{

    public static UIView Get(string name)
    {
        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>($"UI/{name}"), GameManager.Instance.canvas.transform);
        return instance.GetComponent<UIView>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
