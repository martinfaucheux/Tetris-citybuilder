using UnityEngine;
using TMPro;
public class BlockInspectorController : Singleton<BlockInspectorController>
{
    public Block focusedBlock;
    public TextMeshProUGUI blockTitle;
    public TextMeshProUGUI blockDescription;
    public CanvasGroup canvasGroup;

    void Start()
    {
        UnfocusBlock();
    }

    public void FocusBlock(Block block)
    {
        focusedBlock = block;
        canvasGroup.alpha = 1;
        blockTitle.text = block.blockName;
        blockDescription.text = block.GetDescription();
    }

    public void UnfocusBlock(Block block = null)
    {
        if (block != null || block == focusedBlock)
        {
            canvasGroup.alpha = 0;
            blockTitle.text = "";
            blockDescription.text = "";
            focusedBlock = null;
        }
    }
}
