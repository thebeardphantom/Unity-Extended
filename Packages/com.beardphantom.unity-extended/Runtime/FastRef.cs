using System.Diagnostics;
using UnityEngine.Assertions;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// A method of storing a reference to a UnityEngine.Object that you KNOW FOR CERTAIN WILL NOT BE DESTROYED.
    /// Used to avoid null checks.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FastRef<T> : IReadOnlyFastRef<T> where T : class
    {
        #region Fields

        private T _value;

        private bool _hasValue;

        #endregion

        #region Properties

        public T Value
        {
            get
            {
                AssertInSync();
                return _value;
            }
            set
            {
                _value = value;
                _hasValue = _value.IsNotNull();
            }
        }

        public bool HasValue
        {
            get
            {
                AssertInSync();
                return _hasValue;
            }
        }

        #endregion

        #region Methods

        public void ClearValue()
        {
            _value = default;
            _hasValue = default;
        }

        [Conditional("UNITY_ASSERTIONS")]
        private void AssertInSync()
        {
            var hasValue = _value.IsNotNull();
            Assert.AreEqual(_hasValue, hasValue, "Value was destroyed and this FastRef is out of sync.");
        }

        #endregion
    }
}