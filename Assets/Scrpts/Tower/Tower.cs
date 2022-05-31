using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scrpts
{
    [RequireComponent(typeof(TowerBuilder))]
    public class Tower : MonoBehaviour
    {
        public event Action<int> SizeUpdate;

        private TowerBuilder _towerBuilder;
        private List<Block> _blocks;

        private void Start()
        {
            _towerBuilder = GetComponent<TowerBuilder>();
            _blocks = _towerBuilder.Building();

            foreach (Block block in _blocks)
            {
                block.BulletHit += OnBulletHit;
            }
            SizeUpdate?.Invoke(_blocks.Count);
        }
        private void OnBulletHit(Block hitBlock)
        {
            hitBlock.BulletHit -= OnBulletHit;
            _blocks.Remove(hitBlock);

            LowerBlock();
            SizeUpdate?.Invoke(_blocks.Count);
        }
        private void LowerBlock()
        {
            foreach (Block block in _blocks) 
                block.transform.position = GetNewBuildPoint(block);
        }
        private Vector3 GetNewBuildPoint(Block currentBlock) => 
            new Vector3(currentBlock.transform.position.x, currentBlock.transform.position.y - currentBlock.transform.localScale.y , currentBlock.transform.position.z);
    }
}
