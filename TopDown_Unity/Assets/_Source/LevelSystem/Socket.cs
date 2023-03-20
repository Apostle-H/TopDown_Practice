using System;
using System.Collections.Generic;
using System.Linq;
using LevelSystem.Data;
using UnityEngine;
using Utils;

namespace LevelSystem
{
    [DefaultExecutionOrder(-1)]
    public class Socket : MonoBehaviour
    {
        [SerializeField] private Socket topNeighbour;
        [SerializeField] private Socket bottomNeighbour;
        [SerializeField] private Socket leftNeighbour;
        [SerializeField] private Socket rightNeighbour;
        [field: SerializeField] public float Size { get; private set; } = 2.5f;

        private Dictionary<Direction, bool> _definedDirections = new Dictionary<Direction, bool>();

        public bool Collapsed { get; private set; }
        public LevelPart CollapsedPart { get; private set; }

        private void Awake()
        {
            DirectionExtensions.ForEachDirection(direction =>
            {
                if (this[direction] != default)
                    return;
                
                _definedDirections.Add(direction, false);
            });
        }

        public void Collapse(LevelPart[] levelParts)
        {
            IEnumerable<LevelPart> validParts = levelParts.Where(ValidateLevelPart);

            CollapsedPart = validParts.Random();
            Instantiate(CollapsedPart.gameObject, transform);
            Collapsed = true;
            
            DirectionExtensions.ForEachDirection(direction => DefineDirection(direction, CollapsedPart[direction]));
        }

        public void OpenNonClosed()
        {
            DirectionExtensions.ForEachDirection(direction =>
            {
                if (_definedDirections.ContainsKey(direction))
                    return;
                
                DefineDirection(direction, true);
            });
        }

        private bool ValidateLevelPart(LevelPart levelPart)
        {
            return _definedDirections.All(definedDirection => 
                levelPart[definedDirection.Key] == definedDirection.Value);
        }
        
        private void DefineDirection(Direction direction, bool openClose)
        {
            bool isDefined = _definedDirections.ContainsKey(direction);
            if (isDefined && _definedDirections[direction] == openClose)
                return;

            if (isDefined)
                _definedDirections.Add(direction, openClose);
            else
                _definedDirections[direction] = openClose;
            
            PropagateDefined(direction, openClose);
        }

        private void PropagateDefined(Direction direction, bool openClose) =>
            this[direction]?.DefineDirection(direction.GetAlternative(), openClose);

        public bool GetRandomNeighbour(out Socket neighbour)
        {
            neighbour = default;
            
            List<Direction> availableDirections = new List<Direction>();
            DirectionExtensions.ForEachDirection(direction =>
            {
                if (this[direction] == default)
                    return;
                
                availableDirections.Add(direction);
            });

            if (availableDirections.Count == 0)
                return false;

            neighbour = this[availableDirections.Random()];
            return true;
        }
        
        private Socket this[Direction direction] =>
            direction switch
            {
                Direction.top => topNeighbour,
                Direction.bottom => bottomNeighbour,
                Direction.left => leftNeighbour,
                Direction.right => rightNeighbour,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
    }
}