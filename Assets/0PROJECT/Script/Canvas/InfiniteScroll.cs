using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class that scrolls the panel in the UI in an infinite loop.
/// </summary>

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    #region Private Members
    [SerializeField] private ScrollContent scrollContent;
    [SerializeField] private float outOfBoundsThreshold;
    private ScrollRect scrollRect;
    private Vector2 lastDragPosition;
    private bool positiveDrag;
    #endregion

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.vertical = scrollContent.Vertical;
        scrollRect.horizontal = scrollContent.Horizontal;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    // Operations while dragging
    public void OnDrag(PointerEventData eventData)
    {
        positiveDrag = eventData.position.y > lastDragPosition.y;

        lastDragPosition = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            positiveDrag = eventData.scrollDelta.y > 0;
        }
        else
        {
            positiveDrag = eventData.scrollDelta.y < 0;
        }
    }

    public void OnViewScroll()
    {
        HandleVerticalScroll();
    }

    // Repositions all child objects in a loop.
    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.y = endItem.position.y - scrollContent.ChildHeight * 1.5f + scrollContent.ItemSpacing;
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.ChildHeight * 1.5f - scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    // Check if the item reached the threshold or not
    private bool ReachedThreshold(Transform item)
    {
        float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
        float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;

        return positiveDrag ?
                item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
                item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
    }
}
