using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class BlockInstanciator : Singleton<BlockInstanciator>
{

    [field: SerializeField]
    public Card selectedCard { get; private set; }
    public BlockGroup blockGroup;
    public GameObject ghostGameObject;
    public float moveCooldown = 0.1f;
    float _lastMoveTime = 0f;
    Vector2Int instantiatePosition;

    void Start()
    {
        instantiatePosition = BlockContextManager.instance.GetContextPosition(transform.position);
        SetSelectedCard();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            StateManager.currentState != GameState.TURN
            || selectedCard == null
        ) return;

        Vector2Int groupContextPosition = BlockContextManager.instance.GetContextPosition(blockGroup.transform.position);
        BlockContext currentContext = BlockContextManager.instance.currentContext;

        if (Time.time < _lastMoveTime + moveCooldown)
            return;

        int disp = 0;
        if (Input.GetKeyDown(KeyCode.Q))
            disp = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            disp = 1;

        int rot = 0;
        if (Input.GetKeyDown(KeyCode.A))
            rot = -1;
        else if (Input.GetKeyDown(KeyCode.E))
            rot = 1;


        bool positionChanged = false;
        Vector2Int newTempPosition = groupContextPosition;


        if (disp != 0)
        {
            bool isValidPosition = blockGroup.IsValidPosition(
                currentContext, groupContextPosition + new Vector2Int(disp, 0)
            );
            if (isValidPosition)
            {
                newTempPosition = groupContextPosition + new Vector2Int(disp, 0);
                positionChanged = true;
            }
        }

        if (rot != 0)
        {
            blockGroup.Rotate(groupContextPosition, rot);
            blockGroup.transform.Rotate(0, 0, 90 * rot);
            newTempPosition = blockGroup.GetNearestValidPosition(currentContext, groupContextPosition);
            positionChanged = true;
        }


        if (positionChanged)
        {
            Vector2Int newPos = blockGroup.GetLowestPosition(
                BlockContextManager.instance.currentContext, newTempPosition.x
            );
            instantiatePosition = newPos;
            blockGroup.Move(newPos);
            _lastMoveTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (ResourceManager.instance.CheckCanAfford(blockGroup.cost))
            {
                Spawn();
            }
            else
                Debug.Log("Not enough resources");
        }
    }

    private void Spawn()
    {
        // blocks are registered in the Context
        blockGroup.Register();
        blockGroup.SetBlockHolderState(BlockHolderState.PLACED);

        ghostGameObject.transform.SetParent(null);
        ghostGameObject = null;

        DeckManager.instance.Discard(selectedCard);
        SetSelectedCard(DeckManager.instance.GetFirstHandCard());
        TurnManager.instance.StartNewAction();
    }

    public void SetSelectedCard(Card card = null)
    {
        selectedCard = card;

        if (ghostGameObject != null)
            Destroy(ghostGameObject);

        // remove existing object
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        if (selectedCard == null)
        {
            blockGroup = null;
            ghostGameObject = null;
        }
        else
        {
            blockGroup = new BlockGroup(selectedCard);
            // Make sure new object is not out of bounds
            Vector2Int newTempPosition = blockGroup.GetNearestValidPosition(
                BlockContextManager.instance.currentContext, instantiatePosition
            );
            Vector2Int spawnPos = blockGroup.GetLowestPosition(BlockContextManager.instance.currentContext, newTempPosition.x);
            ghostGameObject = blockGroup.InstantiateGameObject(spawnPos);
            blockGroup.SetBlockHolderState(BlockHolderState.GHOST);
        }
    }
}
