using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomEditor(typeof(InspectorSection))]
    public class InspectorSectionEditor : UnityEditor.Editor
    {
        private InspectorSection _section;
        private VisualElement _editorsRoot;

        public override VisualElement CreateInspectorGUI()
        {
            _section = (InspectorSection)target;
            var root = new VisualElement
            {
                name = "root",
            };
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            root.RegisterCallback<DragEnterEvent>(evt =>
            {
                root.style.backgroundColor = new Color(0.3f, 0.6f, 0.3f, 0.2f);
            });

            root.RegisterCallback<DragLeaveEvent>(evt =>
            {
                root.style.backgroundColor = Color.clear;
            });

            root.RegisterCallback<DragPerformEvent>(evt =>
            {
                root.style.backgroundColor = Color.clear;

                var needsEditorsRebuild = false;
                foreach (Component component in DragAndDrop.objectReferences.OfType<Component>())
                {
                    needsEditorsRebuild |= _section.TryAdd(component);
                }

                if (needsEditorsRebuild)
                {
                    RebuildEditors();
                }

                DragAndDrop.AcceptDrag();
                evt.StopPropagation();
            });

            root.RegisterCallback<DragUpdatedEvent>(evt =>
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Move;
                evt.StopPropagation();
            });
            _editorsRoot = new VisualElement
            {
                name = "editors_root",
                style = { flexGrow = 1f, },
            };
            root.Add(_editorsRoot);
            RebuildEditors();
            return root;
        }

        private void RebuildEditors()
        {
            _editorsRoot.Clear();
            foreach (Component component in _section.ComponentsReadOnly)
            {
                var foldout = new Foldout
                {
                    text = ObjectNames.GetInspectorTitle(component),
                    toggleOnLabelClick = true,
                    value = InternalEditorUtility.GetIsInspectorExpanded(component),
                };
                foldout
                    .WithChild(
                        new InspectorElement(component)
                        {
                            name = $"{component}_editor-root",
                        });
                foldout.RegisterValueChangedCallback(evt =>
                {
                    InternalEditorUtility.SetIsInspectorExpanded(component, evt.newValue);
                });
                foldout.Q(className: Foldout.inputUssClassName)
                    .WithChild(
                        new Button
                        {
                            text = "X",
                            style =
                            {
                                backgroundColor = new Color(0.7f, 0.13f, 0.13f),
                                paddingLeft = 4f,
                                color = Color.white,
                            },
                        }.WithClickedCallback(() =>
                        {
                            if (!_section.TryRemove(component))
                            {
                                return;
                            }

                            RebuildEditors();
                            InternalEditorUtility.SetIsInspectorExpanded(_section, false);
                            InternalEditorUtility.SetIsInspectorExpanded(_section, true);
                        }));
                _editorsRoot.Add(foldout);
            }
        }
    }
}