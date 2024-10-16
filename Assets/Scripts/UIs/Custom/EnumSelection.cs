using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{

    public class EnumSelection : Selectable, ISubmitHandler, IPointerClickHandler
    {
        /// <summary>
        /// Setting that indicates one of four directions.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// From the left to the right
            /// </summary>
            LeftToRight,

            /// <summary>
            /// From the right to the left
            /// </summary>
            RightToLeft,

            /// <summary>
            /// From the bottom to the top.
            /// </summary>
            BottomToTop,

            /// <summary>
            /// From the top to the bottom.
            /// </summary>
            TopToBottom,
        }

        [SerializeField]
        private Direction m_Direction = Direction.LeftToRight;
        public Direction direction { get { return m_Direction; } set { if (SetStruct(ref m_Direction, value)) UpdateVisuals(); } }
        enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        Axis axis { get { return (m_Direction == Direction.LeftToRight || m_Direction == Direction.RightToLeft) ? Axis.Horizontal : Axis.Vertical; } }

        public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                return false;

            currentValue = newValue;
            return true;
        }

        [Serializable]
        /// <summary>
        /// Event type used by the UI.Slider.
        /// </summary>
        public class EnumSelectionEvent : UnityEvent<int> { }

        private PointerEventData m_InputPointerEvent;

        [SerializeField] internal string saveString = "Localization";

        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;

        [Space]

        [SerializeField] protected bool loop;
        [SerializeField] protected int m_Value;
        [SerializeField] internal int maxValue;
        public int value
        {
            get
            {
                return m_Value;
            }
            set
            {
                Set(value);
            }
        }

        [Space]

        [SerializeField]
        private EnumSelectionEvent m_OnValueChanged = new EnumSelectionEvent();
        [SerializeField]
        private UnityEvent m_OnClick;

        [SerializeField]
        private UnityEvent m_OnSubmit;

        [SerializeField]
        private UnityEvent m_OnSelect;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (saveString != "")
                m_Value = PlayerPrefs.GetInt(saveString);
            Set(m_Value, false);
        }

        public virtual void SetValueWithoutNotify(int input)
        {
            Set(input, false);
        }

        protected override void Awake()
        {
            base.Awake();
            if (leftButton != null) leftButton.onClick.AddListener(OnLeftButton);
            if (rightButton != null) rightButton.onClick.AddListener(OnRightButton);
        }

        private void OnLeftButton()
        {
            Set(value - 1);
        }

        private void OnRightButton()
        {
            Set(value + 1);
        }

        private void UpdateVisuals(bool selected = true)
        {
            if (maxValue == 0)
            {
                if (leftButton != null) leftButton.gameObject.SetActive(false);
                if (rightButton != null) rightButton.gameObject.SetActive(false);
                return;
            }

            if (leftButton != null) leftButton.gameObject.SetActive(!loop ? m_Value > 0 : true);
            if (rightButton != null) rightButton.gameObject.SetActive(!loop ? m_Value < maxValue - 1 : true);

            if (EventSystem.current != null && selected)
                EventSystem.current.SetSelectedGameObject(this.gameObject);
        }

        public void SelectedGameObject(GameObject _gameObject)
        {
            if (EventSystem.current != null)
                EventSystem.current.SetSelectedGameObject(_gameObject);
        }

        public override void OnMove(AxisEventData eventData)
        {
            if (!IsActive() || !IsInteractable())
            {
                base.OnMove(eventData);
                return;
            }

            switch (eventData.moveDir)
            {
                case MoveDirection.Left:
                    if (axis == Axis.Horizontal && FindSelectableOnLeft() == null)
                    {
                        if (leftButton != null)
                            ExecuteEvents.Execute(leftButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        else
                            Set(value - 1);
                    }
                    else
                        base.OnMove(eventData);
                    break;
                case MoveDirection.Right:
                    if (axis == Axis.Horizontal && FindSelectableOnRight() == null)
                    {
                        if (rightButton != null)
                            ExecuteEvents.Execute(rightButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        else
                            Set(value + 1);
                    }
                    else
                        base.OnMove(eventData);
                    break;
                case MoveDirection.Up:
                    if (axis == Axis.Vertical && FindSelectableOnUp() == null)
                    {
                        if (rightButton != null)
                            ExecuteEvents.Execute(rightButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        else
                            Set(value + 1);
                    }
                    else
                        base.OnMove(eventData);
                    break;
                case MoveDirection.Down:
                    if (axis == Axis.Vertical && FindSelectableOnDown() == null)
                    {
                        if (rightButton != null)
                            ExecuteEvents.Execute(rightButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        else
                            Set(value - 1);
                    }
                    else
                        base.OnMove(eventData);
                    break;
            }
        }

        protected virtual void Set(int input, bool sendCallback = true)
        {
            // Clamp the input
            int newValue = ClampValue(input);

            // If the stepped value doesn't match the last one, it's time to update
            if (m_Value == newValue)
                return;

            m_Value = newValue;
            UpdateVisuals(sendCallback);
            if (sendCallback && maxValue > 0)
            {
                UISystemProfilerApi.AddMarker("EnumSelection.value", this);
                m_OnValueChanged.Invoke(newValue);
            }
        }

        int ClampValue(int input)
        {
            if (loop)
            {
                if (input < 0)
                    return maxValue - 1;

                if (input > maxValue - 1)
                    return 0;

                return input;
            }
            return Mathf.Clamp(input, 0, maxValue - 1);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_OnClick?.Invoke();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            m_OnSubmit?.Invoke();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            m_OnSelect?.Invoke();
        }
    }
}
