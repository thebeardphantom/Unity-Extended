using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(DampenedValue<>), true)]
    public class DampenedValuePropertyDrawer : PropertyDrawer
    {
        #region Fields

        private SerializedPropertyTree.Node _tree;

        private UnityEditor.Editor _editor;

        #endregion

        #region Methods

        private static bool ShouldDrawBonusSection(SerializedProperty property)
        {
            return property.isExpanded && Application.isPlaying;
        }

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            if (!ShouldDrawBonusSection(property))
            {
                return;
            }

            if (_tree == null)
            {
                _tree = SerializedPropertyTree.Build(property);
            }

            var dampenedValue = (IDampenedValue)_tree.TailNode.NodeObject;
            using (new EditorGUI.DisabledScope(true))
            {
                using (new EditorGUI.IndentLevelScope(1))
                {
                    var defaultHeight = EditorGUI.GetPropertyHeight(property, label, true);
                    var nextRect = position;
                    nextRect.yMin += defaultHeight;
                    nextRect.height = EditorGUIUtility.singleLineHeight;
                    switch (dampenedValue)
                    {
                        case DampenedFloat dampenedFloat:
                        {
                            EditorGUI.FloatField(nextRect, nameof(dampenedFloat.Value), dampenedFloat.Value);
                            nextRect.y += nextRect.height;
                            EditorGUI.FloatField(nextRect, nameof(dampenedFloat.TargetValue), dampenedFloat.TargetValue);
                            nextRect.y += nextRect.height;
                            EditorGUI.FloatField(nextRect, nameof(dampenedFloat.Velocity), dampenedFloat.Velocity);
                            break;
                        }
                        case DampenedDouble dampenedDouble:
                        {
                            EditorGUI.DoubleField(nextRect, nameof(dampenedDouble.Value), dampenedDouble.Value);
                            nextRect.y += nextRect.height;
                            EditorGUI.DoubleField(nextRect, nameof(dampenedDouble.TargetValue), dampenedDouble.TargetValue);
                            nextRect.y += nextRect.height;
                            EditorGUI.DoubleField(nextRect, nameof(dampenedDouble.Velocity), dampenedDouble.Velocity);
                            break;
                        }
                        case DampenedVector2 dampenedVector2:
                        {
                            EditorGUI.Vector2Field(nextRect, nameof(dampenedVector2.Value), dampenedVector2.Value);
                            nextRect.y += nextRect.height;
                            EditorGUI.Vector2Field(nextRect, nameof(dampenedVector2.TargetValue), dampenedVector2.TargetValue);
                            nextRect.y += nextRect.height;
                            EditorGUI.Vector2Field(nextRect, nameof(dampenedVector2.Velocity), dampenedVector2.Velocity);
                            break;
                        }
                        case DampenedVector3 dampenedVector3:
                        {
                            EditorGUI.Vector3Field(nextRect, nameof(dampenedVector3.Value), dampenedVector3.Value);
                            nextRect.y += nextRect.height;
                            EditorGUI.Vector3Field(nextRect, nameof(dampenedVector3.TargetValue), dampenedVector3.TargetValue);
                            nextRect.y += nextRect.height;
                            EditorGUI.Vector3Field(nextRect, nameof(dampenedVector3.Velocity), dampenedVector3.Velocity);
                            break;
                        }
                    }
                }
            }

            if (_editor == null)
            {
                foreach (var editor in ActiveEditorTracker.sharedTracker.activeEditors)
                {
                    if (editor.serializedObject == property.serializedObject)
                    {
                        _editor = editor;
                        break;
                    }
                }
            }

            _editor.Repaint();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property, label, true);
            if (ShouldDrawBonusSection(property))
            {
                height += EditorGUIUtility.singleLineHeight * 3;
            }

            return height;
        }

        #endregion
    }
}