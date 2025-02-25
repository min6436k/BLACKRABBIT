using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// GameManager를 통해 접근하여 UI 스택을 관리하는 클래스
/// </summary>
public class UINavigation : MonoBehaviour
{
    private Stack<UIView> _uiStack = new Stack<UIView>();

    public UIView Open(string name)
    {
        foreach (var ui in _uiStack)
            ui.Hide();

        UIView instance = UIView.Get(name);
        instance.Show();
        
        _uiStack.Push(instance);

        return instance;
    }

    public UIView Close()
    {
        if (_uiStack.Count < 1) return null;
        
        _uiStack.Peek().Hide();
        Destroy(_uiStack.Pop().gameObject);

        if (_uiStack.Count > 0)
        {
            _uiStack.Peek().Show();
            return _uiStack.Peek();
        }
        else return null;
    }
}
