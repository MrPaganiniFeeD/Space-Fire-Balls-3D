using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{

    [SerializeField] private float _towerSize;
    [SerializeField] private Transform _buildPoint;
    [SerializeField] private Block _block;
    [SerializeField] private Color[] _colors;

    private List<Block> _blocks;

    public List<Block> Building()
    {
        _blocks = new List<Block>();

        Transform currentPoint = _buildPoint;

        Block firstBlock = BuildingFirstBlock(currentPoint);
        currentPoint = SetCurrentPoint(firstBlock);
        
        for (int i = 1; i < _towerSize; i++)
        {
            Block newBlock = BuildingBlock(currentPoint);
            currentPoint = SetCurrentPoint(newBlock);
        }
        return _blocks;
    }

    private static Transform SetCurrentPoint(Block firstBlock)
    {
        Transform currentPoint;
        currentPoint = firstBlock.transform;
        return currentPoint;
    }

    private Block BuildingFirstBlock(Transform currentPosition)
    {
        Block block = InstantiateBlock(currentPosition.position);
        SettingBlock(block);
        return block;
    }

    private Block BuildingBlock(Transform currentPosition)
    {
        Block block = InstantiateBlock(GetBuildPoint(currentPosition));
        SettingBlock(block);
        return block;
    }

    private void SettingBlock(Block block)
    {
        block.SetColor(_colors[Random.Range(0, _colors.Length)]);
        _blocks.Add(block);
    }

    private Vector3 GetBuildPoint(Transform currentSegment) =>
        new Vector3(_buildPoint.position.x,
            currentSegment.position.y + currentSegment.localScale.y / 2 + _block.transform.localScale.y / 2,
            _buildPoint.position.z);

    private Block InstantiateBlock(Vector3 currentPosition) => 
        Instantiate(_block, currentPosition, Quaternion.identity, _buildPoint);
}

