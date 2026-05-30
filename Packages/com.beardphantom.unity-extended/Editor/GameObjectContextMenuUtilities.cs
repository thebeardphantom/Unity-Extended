using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class GameObjectContextMenuUtilities
    {
        private const string SortByNameMenuItemPath = "GameObject/Sort/By Name";
        private const string SortByXMenuItemPath = "GameObject/Sort/By X";
        private const string SortByYMenuItemPath = "GameObject/Sort/By Y";
        private const string SortByZMenuItemPath = "GameObject/Sort/By Z";
        private const string SortByXZMenuItemPath = "GameObject/Sort/By XZ";

        [MenuItem(SortByNameMenuItemPath)]
        private static void SortByName()
        {
            SortObjects(
                "Sort by Name",
                (t1, t2) => EditorUtility.NaturalCompare(t1.name, t2.name));
        }

        [MenuItem(SortByXMenuItemPath)]
        private static void SortByX()
        {
            SortObjects(
                "Sort by X",
                (t1, t2) => t1.position.x.CompareTo(t2.position.x));
        }

        [MenuItem(SortByYMenuItemPath)]
        private static void SortByY()
        {
            SortObjects(
                "Sort by Y",
                (t1, t2) => t1.position.y.CompareTo(t2.position.y));
        }

        [MenuItem(SortByZMenuItemPath)]
        private static void SortByZ()
        {
            SortObjects(
                "Sort by Z",
                (t1, t2) => t1.position.z.CompareTo(t2.position.z));
        }

        [MenuItem(SortByXZMenuItemPath)]
        private static void SortByXZ()
        {
            SortObjects(
                "Sort by XZ",
                (t1, t2) =>
                {
                    Vector3 position1 = t1.position;
                    Vector3 position2 = t2.position;
                    int cmp = position1.z.CompareTo(position2.z);
                    if (cmp != 0)
                    {
                        return cmp;
                    }

                    return position1.x.CompareTo(position2.x);
                });
        }

        [MenuItem(SortByNameMenuItemPath, true)]
        [MenuItem(SortByXMenuItemPath, true)]
        [MenuItem(SortByYMenuItemPath, true)]
        [MenuItem(SortByZMenuItemPath, true)]
        [MenuItem(SortByXZMenuItemPath, true)]
        private static bool SortValidate()
        {
            return Selection.transforms.Length > 0;
        }

        private static void SortObjects(string operationDisplayName, Comparison<Transform> comparison)
        {
            Transform[] allSelected = Selection.transforms;
            IEnumerable<IGrouping<Transform, Transform>> selectedByParent = allSelected
                .GroupBy(t => t.parent);

            Undo.SetCurrentGroupName(operationDisplayName);
            int undoGroup = Undo.GetCurrentGroup();
            foreach (IGrouping<Transform, Transform> group in selectedByParent)
            {
                List<Transform> list = group.ToList();
                int firstIndex = list.Min(t => t.GetSiblingIndex());
                list.Sort(comparison);
                for (var i = 0; i < list.Count; i++)
                {
                    Transform tform = list[i];
                    Undo.SetSiblingIndex(tform, firstIndex + i, operationDisplayName);
                }
            }

            Undo.CollapseUndoOperations(undoGroup);
        }
    }
}