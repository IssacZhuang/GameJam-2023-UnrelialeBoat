using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Vocore
{
    public struct EventId : IEquatable<EventId>
    {
        private const string DebugStringFormat = "Event: id = {0}, stringId = {1}";
        private int _id;
        // used for debugging
        private string _strId;

        public int Id
        {
            get { return _id; }
        }

        public string StringId
        {
            get { return _strId; }
        }

        public EventId(int id, string strId)
        {
            _id = id;
            _strId = strId;
        }

        public override string ToString()
        {
            return string.Format(DebugStringFormat, _id, _strId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return _id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(EventId other)
        {
            return _id == other._id;
        }

        // operator ==
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(EventId lhs, EventId rhs)
        {
            return lhs.Equals(rhs);
        }

        // operator !=
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(EventId lhs, EventId rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}