using UnityEngine;
using TMPro;
public class BlockInspectorController : Singleton<BlockInspectorController>
{
    public BlockHolder focusedBlockHolder;
    public TextMeshProUGUI blockTitle;
    public TextMeshProUGUI blockDescription;
    public CanvasGroup canvasGroup;

    void Start()
    {
        UnfocusBlock();
    }

    public void FocusBlock(BlockHolder blockHolder)
    {
        focusedBlockHolder = blockHolder;
        canvasGroup.alpha = 1;
        blockTitle.text = blockHolder.block.blockName;
        blockDescription.text = blockHolder.block.GetDescription();
    }

    public void UnfocusBlock(BlockHolder blockHolder = null)
    {
        if (blockHolder != null || blockHolder == focusedBlockHolder)
        {
            canvasGroup.alpha = 0;
            blockTitle.text = "";
            blockDescription.text = "";
            focusedBlockHolder = null;
        }
    }
}
