using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class AnimationEventRelay : MonoBehaviour
    {
        public readonly LiteEvent<AnimationEventReceivedArgs> EventReceived = new();

        [field: SerializeField]
        public Animator Animator { get; private set; }

        private void Start()
        {
            RuntimeAnimatorController controller = Animator.runtimeAnimatorController;
            AnimationClip[] clips = controller.animationClips;
            foreach (AnimationClip clip in clips)
            {
                AnimationEvent[] events = clip.events;
                for (var j = 0; j < events.Length; j++)
                {
                    AnimationEvent evt = events[j];
                    if (evt.objectReferenceParameter is AnimationEventAsset)
                    {
                        evt.functionName = nameof(OnAnimEvent);
                        events[j] = evt;
                    }
                }

                clip.events = events;
            }
        }

        private void OnAnimEvent(AnimationEventAsset evt)
        {
            EventReceived.Invoke(new AnimationEventReceivedArgs(Animator, evt));
        }

        private void OnValidate()
        {
            TryGetComponent<Animator>(out Animator animator);
            Animator = animator;
        }

        public readonly struct AnimationEventReceivedArgs
        {
            public readonly Animator Animator;

            public readonly AnimationEventAsset Event;

            public AnimationEventReceivedArgs(Animator animator, AnimationEventAsset evt)
            {
                Animator = animator;
                Event = evt;
            }
        }
    }
}