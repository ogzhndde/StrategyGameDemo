using UnityEngine;

/// <summary>
/// // The class that holds all the variables on the scroll screen.
/// </summary>
public class ScrollContent : MonoBehaviour
{
    #region Public Properties
    public float ItemSpacing { get { return itemSpacing; } }
    public float HorizontalMargin { get { return horizontalMargin; } }
    public float VerticalMargin { get { return verticalMargin; } }
    public bool Horizontal { get { return horizontal; } }
    public bool Vertical { get { return vertical; } }
    public float Width { get { return width; } }
    public float Height { get { return height; } }
    public float ChildWidth { get { return childWidth; } }
    public float ChildHeight { get { return childHeight; } }
    #endregion

    #region Private Members
    private RectTransform rectTransform;
    private RectTransform[] rtChildren;
    private float width, height;
    private float childWidth, childHeight;
    [SerializeField] private float itemSpacing;
    [SerializeField] private float horizontalMargin, verticalMargin;
    [SerializeField] private bool horizontal, vertical;

    #endregion

    void Start()
    {
        // Controls the space between items according to the screen resolution.
        InvokeRepeating(nameof(CheckItemSpacing), 0, 0.5f);
    }
    public void UpdateScrollView()
    {
        CheckItemSpacing();

        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }

        width = rectTransform.rect.width - (2 * horizontalMargin);

        height = rectTransform.rect.height - (2 * verticalMargin);

        childWidth = rtChildren[0].rect.width;
        childHeight = rtChildren[0].rect.height;

        horizontal = !vertical;

        InitializeContentVertical();

        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);
    }

    private void InitializeContentVertical()
    {
        float originY = 0 - (height * 0.5f);
        float posOffset = childHeight * 0.5f;
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.y = originY + posOffset + i * (childHeight + itemSpacing);
            rtChildren[i].localPosition = childPos;
        }
    }

    private void CheckItemSpacing()
    {
        //Reference values
        float itemSpacing1920 = 25f;
        float itemSpacing2560 = -25f;
        float itemSpacing3840 = -125f;

        //Set new itemSpacing values according to reference values
        float normalizedWidth = Mathf.InverseLerp(1920f, 3840f, Screen.width);
        itemSpacing = Mathf.Lerp(itemSpacing1920, itemSpacing2560, normalizedWidth);
        if (Screen.width >= 2560)
        {
            itemSpacing = Mathf.Lerp(itemSpacing2560, itemSpacing3840, normalizedWidth);
        }
    }

}
