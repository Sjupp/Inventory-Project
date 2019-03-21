using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    #region Variables

    private bool enteredButton;

    #region Targets

    [SerializeField] private List<Target> targets = new List<Target>();

    #endregion

    #region Events

    [SerializeField] public UnityEvent enter;
    [SerializeField] public UnityEvent downLeft;
    [SerializeField] public UnityEvent downRight;
    [SerializeField] public UnityEvent upLeft;
    [SerializeField] public UnityEvent upRight;
    [SerializeField] public UnityEvent exit;

    #endregion

    #endregion

    #region Target

    [System.Serializable]
    private class Target
    {

        #region Variables

        [SerializeField] private Graphic graphic;
        [SerializeField] private Color defaultColor = new Color(1f, 1f, 1f);
        [SerializeField] private Color hoverColor = new Color(1f, 1f, 1f);
        [SerializeField] private Color clickColor = new Color(1f, 1f, 1f);

        #endregion

        #region Getters

        public Graphic GetGraphic()
        {
            return graphic;
        }

        public Color GetDefaultColor()
        {
            return defaultColor;
        }

        public Color GetHoverColor()
        {
            return hoverColor;
        }

        public Color GetClickColor()
        {
            return clickColor;
        }

        #endregion

    }

    #endregion

    private void Start()
    {

        for (int i = (targets.Count - 1); i >= 0; i--)
        {

            if (targets[i].GetGraphic() == null)
            {
                targets.RemoveAt(i);
            }

        }

    }

    private void OnValidate()
    {
        
        foreach(Target target in targets)
        {
            target.GetGraphic().color = target.GetDefaultColor();
        }

    }

    #region Events

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        enteredButton = true;
        foreach (Target target in targets)
        {
            target.GetGraphic().color = target.GetHoverColor();
        }

        enter.Invoke();

    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {

            foreach (Target target in targets)
            {
                target.GetGraphic().color = target.GetClickColor();
            }

            downLeft.Invoke();

        }

        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {

            foreach (Target target in targets)
            {
                target.GetGraphic().color = target.GetClickColor();
            }

            downRight.Invoke();

        }

    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (enteredButton)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {

                foreach (Target target in targets)
                {
                    target.GetGraphic().color = target.GetHoverColor();
                }

                upLeft.Invoke();

            }

            if (pointerEventData.button == PointerEventData.InputButton.Right)
            {

                foreach (Target target in targets)
                {
                    target.GetGraphic().color = target.GetHoverColor();
                }

                upRight.Invoke();

            }

        }

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        enteredButton = false;
        foreach (Target target in targets)
        {
            target.GetGraphic().color = target.GetDefaultColor();
        }

        exit.Invoke();

    }

    #endregion

}