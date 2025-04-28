using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public struct InstantiateArgs
    {
        public static InstantiateArgs Default => new();

        public Transform Parent { get; set; }

        public Vector3? Position { get; set; }

        public Quaternion? Rotation { get; set; }

        public Vector3? EulerAngles
        {
            get => Rotation?.eulerAngles ?? default;
            set => Rotation = value.HasValue ? Quaternion.Euler(value.Value) : default;
        }

        public Vector3? Scale { get; set; }

        public float? ScaleSingle
        {
            get => Scale?.magnitude ?? default;
            set => Scale = value.HasValue ? new Vector3(value.Value, value.Value, value.Value) : default;
        }

        public Space PositionSpace { get; set; }

        public Space RotationSpace { get; set; }

        public bool? Disabled { get; set; }

        public readonly GameObject Instantiate(GameObject input)
        {
            Transform inputTform = input.transform;
            bool hasParent = Parent.IsNotNull();
            Vector3 positionWorld;
            if (PositionSpace == Space.World)
            {
                positionWorld = Position ?? inputTform.position;
            }
            else
            {
                positionWorld = Position ?? inputTform.localPosition;
                if (hasParent)
                {
                    positionWorld = Parent.TransformPoint(positionWorld);
                }
            }

            Quaternion rotationWorld;
            if (RotationSpace == Space.World)
            {
                rotationWorld = Rotation ?? inputTform.rotation;
            }
            else
            {
                rotationWorld = Rotation ?? inputTform.localRotation;
                if (hasParent)
                {
                    rotationWorld = Quaternion.Inverse(Parent.rotation) * rotationWorld;
                }
            }

            bool inputWasActive = input.activeSelf;
            if (Disabled.HasValue)
            {
                input.SetActive(!Disabled.Value);
            }

            GameObject result = hasParent
                ? Object.Instantiate(input, positionWorld, rotationWorld, Parent)
                : Object.Instantiate(input, positionWorld, rotationWorld);
            if (Disabled.HasValue)
            {
                input.SetActive(inputWasActive);
            }

            return result;
        }
    }
}