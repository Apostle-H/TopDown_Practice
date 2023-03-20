using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevelSystem.Data;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace LevelSystem
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private List<Socket> sockets;
        [SerializeField] private LevelPartsSetSO levelPartsSetSO;
        [SerializeField] [Range(0f, 1f)] private float fillPercentage;

        private HashSet<Socket> _chosenSockets = new HashSet<Socket>();
        private List<Socket> _leftOverSockets;

        private void Awake()
        {
            RandomWalkSockets();
            CollapseRandomWalk();
        }

        private void RandomWalkSockets()
        {
            int socketsNeeded = Mathf.RoundToInt(sockets.Count * fillPercentage);

            Socket currentSocket = sockets.Random();
            _chosenSockets.Add(currentSocket);
            
            _leftOverSockets = sockets.Where(socket => true).ToList();
            _leftOverSockets.Remove(currentSocket);
            
            while (_chosenSockets.Count < socketsNeeded)
            {
                currentSocket.GetRandomNeighbour(out currentSocket);
                _chosenSockets.Add(currentSocket);

                _leftOverSockets.Remove(currentSocket);
            }
        }


        private void CollapseRandomWalk()
        {
            _leftOverSockets.ForEach(socket => socket.Collapse(levelPartsSetSO.EmptyPart));
            _chosenSockets.ForEach(socket =>
            {
                socket.OpenNonClosed();
                socket.Collapse(levelPartsSetSO.LevelParts);
            });
        }
    }
}