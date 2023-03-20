using System.Collections.Generic;
using System.Linq;
using Data.LevelSystem;
using LevelSystem;
using LevelSystem.Data;
using UnityEditor;
using UnityEngine;
using Utils;

namespace CustomEditors.LevelSystem
{
    [CustomEditor(typeof(Socket))]
    [CanEditMultipleObjects]
    public class MultipleSocketEditor : Editor
    {
        [SerializeField] private GameObject socketPrefab;

        private Socket[] _castedTargets;
        private Dictionary<Socket, SocketInfo> _socketsInfo;

        private Dictionary<Vector2, Dictionary<Direction, SocketInfo>> _creatorSockets; 
        private Transform _parent;
        private float _offset;


        private void OnEnable()
        {
            _castedTargets = targets.Cast<Socket>().ToArray();
            _socketsInfo = new Dictionary<Socket, SocketInfo>();

            _creatorSockets = new Dictionary<Vector2, Dictionary<Direction, SocketInfo>>();
            _parent = _castedTargets[0].transform.parent;
            _offset = socketPrefab.GetComponent<Socket>().Size;
            
            foreach (var socket in _castedTargets)
            {
                SocketInfo socketInfo = new SocketInfo();
            
                socketInfo.serializedObject = new SerializedObject(socket);
                SerializedProperty iterator = socketInfo.serializedObject.GetIterator();
                iterator.NextVisible(true);
                
                DirectionExtensions.ForEachDirection(direction =>
                {
                    iterator.NextVisible(false);
                    socketInfo.neighboursProperties.Add(direction, iterator.Copy());
                });
                
                _socketsInfo.Add(socket, socketInfo);
                DirectionExtensions.ForEachDirection(direction => GetCreatorPositions(direction, socket, socketInfo));
            }
        }

        private void OnSceneGUI()
        {
            Handles.color = Color.blue;
            foreach (var socket in _castedTargets)
                Handles.RectangleHandleCap(0, socket.transform.position, Quaternion.identity, socket.Size / 2, EventType.Repaint);
            
            Handles.color = Color.green;
            foreach (var creatorSocket in _creatorSockets)
                DrawNeighborCreator(creatorSocket.Key, creatorSocket.Value);
        }

        private void GetCreatorPositions(Direction direction, Socket socket, SocketInfo socketInfo)
        {
            if (socketInfo.neighboursProperties[direction].objectReferenceValue != null)
                return;
            
            Vector2 pos = (Vector2)socket.transform.position + (direction.GetVector2() * (socket.Size * 2));
            pos = new Vector2((int)pos.x, (int)pos.y);
            if (!_creatorSockets.ContainsKey(pos))
            {
                _creatorSockets.Add(pos, new Dictionary<Direction, SocketInfo>());
            }
            
            _creatorSockets[pos].Add(direction, socketInfo);
        }
        
        private void DrawNeighborCreator(Vector2 pos, Dictionary<Direction, SocketInfo> neighbours)
        {
            if (Handles.Button(pos, Quaternion.identity, _offset, _offset, Handles.RectangleHandleCap))
                InitNeighbour(pos, neighbours);
        }
        
        private void InitNeighbour(Vector2 pos, Dictionary<Direction, SocketInfo> _neighbours)
        {
            Socket newSocket = ((GameObject)PrefabUtility.InstantiatePrefab(socketPrefab, _parent)).GetComponent<Socket>();
            newSocket.transform.position = pos;
            Undo.RegisterCreatedObjectUndo(newSocket.gameObject, "Connected Socket");

            foreach (var neighbour in _neighbours)
            {
                neighbour.Value.neighboursProperties[neighbour.Key].objectReferenceValue = newSocket;
                neighbour.Value.serializedObject.ApplyModifiedProperties();
            }
            
            SerializedObject newSocketSerializedObject = new SerializedObject(newSocket);
            ConnectBack(newSocketSerializedObject, _neighbours);

            Selection.objects = Selection.objects.Append(newSocket.gameObject).ToArray();
        }
        
        private void ConnectBack(SerializedObject newSocketSerializedObject, Dictionary<Direction, SocketInfo> connectBackNeighbours)
        {
            SerializedProperty iterator = newSocketSerializedObject.GetIterator();
            iterator.NextVisible(true);
            DirectionExtensions.ForEachDirection(direction =>
            {
                iterator.NextVisible(false);
                Direction connectBackDirection = direction.GetAlternative();
                if (!connectBackNeighbours.ContainsKey(connectBackDirection))
                    return;
        
                iterator.objectReferenceValue = connectBackNeighbours[connectBackDirection].serializedObject.targetObject as Socket;
            });
            newSocketSerializedObject.ApplyModifiedProperties();
        }
    }
}