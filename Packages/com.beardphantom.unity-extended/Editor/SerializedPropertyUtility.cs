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
        #region Types

        public abstract class Node
        {
            #region Properties

            public abstract object NodeObject { get; }

            public Node ParentNode { get; set; }

            public Node ChildNode { get; set; }

            public Node RootNode
            {
                get
                {
                    var node = this;
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
                    var node = this;
                    while (node.ChildNode != null)
                    {
                        node = node.ChildNode;
                    }

                    return node;
                }
            }

            #endregion

            #region Methods

            internal T GetMemberByName<T>(string name)
            {
                var obj = NodeObject;
                var field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                return (T)field.GetValue(obj);
            }

            #endregion
        }

        private class ArrayIndexNode : Node
        {
            #region Fields

            private readonly IList _list;

            private readonly int _index;

            #endregion

            #region Properties

            /// <inheritdoc />
            public override object NodeObject => _list[_index];

            #endregion

            #region Constructors

            public ArrayIndexNode(IList list, int index)
            {
                _list = list;
                _index = index;
            }

            #endregion
        }

        private class UnityObjectNode : Node
        {
            #region Properties

            /// <inheritdoc />
            public override object NodeObject { get; }

            #endregion

            #region Constructors

            public UnityObjectNode(Object obj)
            {
                NodeObject = obj;
            }

            #endregion
        }

        private class GenericObjectNode : Node
        {
            #region Properties

            /// <inheritdoc />
            public override object NodeObject { get; }

            #endregion

            #region Constructors

            public GenericObjectNode(object obj)
            {
                NodeObject = obj;
            }

            #endregion
        }

        #endregion

        #region Fields

        private static readonly Regex _arrayRegex = new Regex(@"[^\.\n\r]+\.Array\.data\[\d+\]");

        private static readonly Regex _pathPartRegex = new Regex(@"([^\.\n\r]+)\.Array\.data\[(\d+)\]|[^\.\n\r]+");

        public readonly Node Root;

        #endregion

        #region Constructors

        private SerializedPropertyTree(SerializedProperty property)
        {
            var serializedObj = property.serializedObject;
            var root = new UnityObjectNode(serializedObj.targetObject);
            Node lastNode = root;
            var originalPath = property.propertyPath;
            var pathBuilder = new StringBuilder();
            var matches = _pathPartRegex.Matches(originalPath);

            foreach (Match match in matches)
            {
                if (pathBuilder.Length > 0)
                {
                    pathBuilder.Append('.');
                }

                pathBuilder.Append(match.Value);

                var prop = serializedObj.FindProperty(pathBuilder.ToString());

                if (_arrayRegex.IsMatch(match.Value))
                {
                    var memberName = match.Groups[1].Value;
                    var list = lastNode.GetMemberByName<IList>(memberName);

                    var indexStr = match.Groups[2].Value;
                    var index = int.Parse(indexStr);
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

        #endregion

        #region Methods

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

        #endregion
    }
}