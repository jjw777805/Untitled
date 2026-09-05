using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace MyUI
{

    public class Slider : Selectable, IPointerDownHandler, IDragHandler, IPointerUpHandler,ICancelHandler
    {
        [Header("UI 引用")]
        public RectTransform fillRect;    // Fill 的 RectTransform
        public RectTransform handleRect;  // Handle 的 RectTransform
        public RectTransform backgroundRect; // 背景（用于获取总宽度）

        [Header("参数")]
        public float value = 0.5f;
        /// <summary>
        /// 参数为新的value的值
        /// </summary>
        public UnityEvent<float> onValueChanged = new UnityEvent<float>();
        public float deltaStep=0.01f;
        public float deltaSeq=0.05f;

        // 私有变量
        private float sliderWidth;
        private bool isDragging = false;

        MyInput inputs;

        protected virtual void Initial()
        {
            inputs = GameManager.instance.GetInputs();
            UpdateVisuals();
        }
        void Start()
        {
            // 初始更新一次
            Initial();
        }

        float beginTime;
        // 在布局变化后重新计算宽度
        public virtual void Update()
        {
            // 如果宽度变化（例如窗口缩放），需更新
            float currentWidth = backgroundRect.rect.width;
            if (!Mathf.Approximately(currentWidth, sliderWidth))
            {
                sliderWidth = currentWidth;
                UpdateVisuals();
            }
            if (isSelected)
            {
                float delta = Time.realtimeSinceStartup - beginTime;
                if(delta < deltaSeq )return ;
                if (inputs.Player.Move.IsPressed())
                {   
                    Vector2 move = inputs.Player.Move.ReadValue<Vector2>();
                    if (move.x > 0.1f)SetValue(value+deltaStep);
                    else if (move.x < -0.1f)SetValue(value-deltaStep);
                    
                    beginTime = Time.realtimeSinceStartup;
                }
            }
        }

        // ---- 事件接口 ----

        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = true;
            if(!isSelected)Select();
            // 点击任意位置直接跳转
            SetValueFromPointer(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {          
            if (isDragging)
            {
                if(!isSelected)Select();
                SetValueFromPointer(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
        }

        protected bool isSelected=false;
        public override void OnSelect(BaseEventData baseEvent)
        {
            base.OnSelect(baseEvent);
            isSelected = true;
        }

        public override void OnDeselect(BaseEventData baseEvent)
        {
            base.OnDeselect(baseEvent);
            isSelected = false;
        }

        // ---- 核心方法 ----

        private void SetValueFromPointer(PointerEventData eventData)
        {
            // 将点击/拖拽位置转换为相对于 Background 的局部坐标
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                backgroundRect, eventData.position, eventData.pressEventCamera, out localPoint))
            {
                // 计算 x 在背景宽度中的比例（0~1）
                float width = backgroundRect.rect.width;
                float x = localPoint.x + width * 0.5f; // 因为 Background 锚点在左中，原点在左边界
                float clamped = Mathf.Clamp01(x / width);
                SetValue(clamped);
            }
        }

        public virtual void SetValue(float newValue)
        {
            value = Mathf.Clamp01(newValue);
            UpdateVisuals();
            onValueChanged.Invoke(value);
        }

        private void UpdateVisuals()
        {
            if (fillRect != null)
            {
                // Fill 宽度 = 总宽度 * value
                float width = backgroundRect.rect.width * value;
                fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            }

            if (handleRect != null)
            {
                float handlePosX = backgroundRect.rect.width * value;
                handleRect.anchoredPosition = new Vector2(handlePosX, handleRect.anchoredPosition.y);
            }
        }

        public float GetValue() => value;

        public void OnCancel(BaseEventData eventData)
        {
            on_Exit?.Invoke();
        }

        [SerializeField]
        protected UnityEvent on_Exit = new UnityEvent();

        public UnityEvent onExit
        {
            set
            {
                onExit = value;
            }
            get
            {
                return onExit;
            }
        }
    }
}