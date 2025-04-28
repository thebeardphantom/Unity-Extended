using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public class SerializedPropertyTree
    {
        private static readonly Regex _arrayRegex = new(@"[^\.\n\r]+\.Array\.data\[\d+\]");

        private static readonly Regex _pathPartRegex = new(@"([^\.\n\r]+)\.Array\.data\[(\d+)\]|[^\.\n\r]+");

        public readonly Node Root;

        private SerializedPropertyTree(SerializedProperty property)
        {
            SerializedObject serializedObj = property.serializedObject;
            var root = new UnityObjectNode(serializedObj.targetObject);
            Node lastNode = root;
            string originalPath = property.propertyPath;
            var pathBuilder = new StringBuilder();
            MatchCollection matches = _pathPartRegex.Matches(originalPath);

            foreach (Match match in matches)
            {
                if (pathBuilder.Length > 0)
                {
                    pathBuilder.Append('.');
                }

                pathBuilder.Append(match.Value);

                SerializedProperty prop = serializedObj.FindProperty(pathBuilder.ToString());

                if (_arrayRegex.IsMatch(match.Value))
                {
                    string memberName = match.Groups[1].Value;
                    var list = lastNode.GetMemberByName<IList>(memberName);

                    string indexStr = match.Groups[2].Value;
                    int index = int.Parse(indexStr);
                    lastNode = SetupNodePair(lastNode, new ArrayIndexNode(list, index));
                }
                else
                {
                    switch (prop.propertyType)
                    {
                        case SerializedPropertyType.ObjectReference:
                        {
                            lastNode = SetupNodePair(lastNode, new UnityObjectNode(prop.objectReferenceValue));
                            break;
                        }
                        case SerializedPropertyType.Generic:
                        {
                            var field = lastNode.GetMemberByName<object>(match.Value);
                            lastNode = SetupNodePair(lastNode, new GenericObjectNode(field));
                            break;
                        }
                    }
                }
            }

            Root = root;
        }

        public static Node Build(SerializedProperty property)
        {
            var tree = new SerializedPropertyTree(property);
            return tree.Root;
        }

        private static Node SetupNodePair(Node parent, Node child)
        {
            parent.ChildNode = child;
            child.ParentNode = parent;
            return child;
        }

        public abstract class Node
        {
            public abstract object NodeObject { get; }

            public Node ParentNode { get; set; }

            public Node ChildNode { get; set; }

            public Node RootNode
            {
                get
                {
                    Node node = this;
                    while (node.ParentNode != null)
                    {
                        node = node.ParentNode;
                    }

                    return node;
                }
            }

            public Node TailNode
            {
                get
                {
                    Node node = this;
                    while (node.ChildNode != null)
                    {
                        node = node.ChildNode;
                    }

                    return node;
                }
            }

            internal T GetMemberByName<T>(string name)
            {
                object obj = NodeObject;
                FieldInfo field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                return (T)field.GetValue(obj);
            }
        }

        private class ArrayIndexNode : Node
        {
            private readonly IList _list;

            private readonly int _index;

            /// <inheritdoc />
            public override object NodeObject => _list[_index];

            public ArrayIndexNode(IList list, int index)
            {
                _list = list;
                _index = index;
            }
        }

        private class UnityObjectNode : Node
        {
            /// <inheritdoc />
            public override object NodeObject { get; }

            public UnityObjectNode(Object obj)
            {
                NodeObject = obj;
            }
        }

        private class GenericObjectNode : Node
        {
            /// <inheritdoc />
            public override object NodeObject { get; }

            public GenericObjectNode(object obj)
            {
                NodeObject = obj;
            }
        }
    }
}