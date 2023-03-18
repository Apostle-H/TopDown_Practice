using System;
using System.Collections.Generic;
using LevelSystem;
using LevelSystem.Data;
using UnityEditor;
using UnityEngine;
using Utils;

namespace CustomEditors.LevelSystem
{
    [CustomEditor(typeof(LevelPart))]
    public class LevelPartEditor : Editor
    {
        private LevelPart _castedTarget;
        private Dictionary<Direction, SerializedProperty> _socketsRules;

        private float _offsetFromCenter;

        private void OnEnable()
        {
            _castedTarget = target as LevelPart;
            _socketsRules = new Dictionary<Direction, SerializedProperty>();

            SerializedProperty iterator = serializedObject.GetIterator();
            iterator.NextVisible(true);
            DirectionExtensions.ForEachDirection(direction =>
            {
                iterator.NextVisible(false);
                _socketsRules.Add(direction, iterator.Copy());
            });

            _offsetFromCenter = _castedTarget.Size * 2 / 2.1f;
        }

        private void OnSceneGUI()
        {
            DirectionExtensions.ForEachDirection(direction =>
            {
                Vector2 pos = (Vector2)_castedTarget.transform.position + (direction.GetVector2() * _offsetFromCenter);
                DrawDirectionToggle(direction, pos);
            });

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawDirectionToggle(Direction direction, Vector2 pos)
        {
            bool currentState = _socketsRules[direction].boolValue;
            Handles.color = currentState ? Color.green : Color.red;
            if (Handles.Button(pos, Quaternion.identity, 0.3f, 0.3f, Handles.SphereHandleCap))
            {
                _socketsRules[direction].boolValue = !currentState;
            }
        }
    }
}