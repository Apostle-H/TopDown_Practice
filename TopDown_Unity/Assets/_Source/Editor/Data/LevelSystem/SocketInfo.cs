using System.Collections.Generic;
using LevelSystem.Data;
using UnityEditor;
using UnityEngine;

namespace Data.LevelSystem
{
    public class SocketInfo : Object
    {
        public SerializedObject serializedObject;
        public readonly Dictionary<Direction, SerializedProperty> neighboursProperties;

        public SocketInfo()
        {
            neighboursProperties = new Dictionary<Direction, SerializedProperty>();
        }
    }
}