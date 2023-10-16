using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RealmDisplay : MonoBehaviour
{
    public SpriteRenderer dottedlineRenderer;
    public SpriteRenderer boxRenderer;
    public TextMeshPro costText;
    [Range(0f, 1.5f)]
    public float transitionDuration = 0.5f;
    [Range(0f, 1f)]
    public float maxOppacity = 0.5f;
    public LayerMask boxLayerMask;
    private int[] _descrIds = new int[] { 0, 0 };
    private bool _isHovered = false;

    void Start()
    {
        // set box renderer to transparent
        Color color = boxRenderer.color;
        color.a = 0f;
        boxRenderer.color = color;

        Refresh();
    }

    void Update()
    {
        bool isHovered = IsHovered();
        if (isHovered)
        {
            if (!_isHovered)
            {
                CancelFading();
                LTDescr d1 = LeanTween.alpha(dottedlineRenderer.gameObject, 0f, transitionDuration).setEaseOutCubic();
                LTDescr d2 = LeanTween.alpha(boxRenderer.gameObject, maxOppacity, transitionDuration).setEaseOutCubic();
                _descrIds = new int[] { d1.id, d2.id };
            }

            if (Input.GetMouseButtonDown(0))
            {
                bool couldUnlock = RealmManager.instance.TryUnlockRealm();
                if (couldUnlock)
                    Refresh();
            }
        }
        else
        {
            if (_isHovered)
            {
                CancelFading();
                LTDescr d1 = LeanTween.alpha(dottedlineRenderer.gameObject, maxOppacity, transitionDuration).setEaseOutCubic();
                LTDescr d2 = LeanTween.alpha(boxRenderer.gameObject, 0f, transitionDuration).setEaseOutCubic();
                _descrIds = new int[] { d1.id, d2.id };

            }
        }
        _isHovered = isHovered;
    }

    private void CancelFading()
    {
        foreach (int descrId in _descrIds)
            if (descrId != 0)
                LeanTween.cancel(descrId);
    }

    private bool IsHovered()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 rayPosition = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.zero, Mathf.Infinity, boxLayerMask);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }


    public void Refresh()
    {
        RealmManager manager = RealmManager.instance;
        transform.position = new Vector3(
            0.5f, (manager.unlockedRealm + 1.5f) * manager.realmSize, transform.position.z
        );

        ResourceGroup cost = manager.realmCost;
        costText.text = $"Unlock Realm for {cost.ToStringIcon()}";

        Vector3 dottedLinePosition = new Vector3(
            0.5f, (manager.unlockedRealm + 1) * manager.realmSize + 0.5f, dottedlineRenderer.transform.position.z
        );
        LeanTween.move(dottedlineRenderer.gameObject, dottedLinePosition, transitionDuration * 2).setEaseInOutQuart();
    }
}
