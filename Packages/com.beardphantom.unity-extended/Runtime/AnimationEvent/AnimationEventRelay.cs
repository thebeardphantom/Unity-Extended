using BeardPhantom.UnityExtended;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    #region Types

    public readonly struct AnimationEventReceivedArgs
    {
        #region Fields

        public readonly AnimationEventAsset Event;

        #endregion

        #region Constructors

        public AnimationEventReceivedArgs(AnimationEventAsset evt)
        {
            Event = evt;
        }

        #endregion
    }

    #endregion

    #region Fields

    public readonly LiteEvent<AnimationEventReceivedArgs> EventReceived = new();

    #endregion

    #region Properties

    [field: SerializeField]
    private Animator Animator { get; set; }

    #endregion

    #region Methods

    private void Start()
    {
        var controller = Animator.runtimeAnimatorController;
        var clips = controller.animationClips;
        foreach (var clip in clips)
        {
            var events = clip.events;
            for (var j = 0; j < events.Length; j++)
            {
                var evt = events[j];
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
        EventReceived.Invoke(new(evt));
    }

    #endregion
}