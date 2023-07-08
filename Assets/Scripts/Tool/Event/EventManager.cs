using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using UnityEngine;

namespace Vocore
{
    public static class EventManager
    {
        private static int _index = 1;
        //regex only match letters and numbers and .
        private static Regex _regex = new Regex(@"^[a-zA-Z0-9\.]+$");

        private static readonly Dictionary<string, EventId> _eventIds = new Dictionary<string, EventId>();

        public static IEnumerable<EventId> EventIds
        {
            get { return _eventIds.Values; }
        }

        public static EventId Generate(string stringId)
        {
            Debug.Log("Generate EventId: " + stringId);
            if (!_regex.IsMatch(stringId))
            {
                throw new Exception("EventId must only contain letters and numbers and '.'");
            }
            if (_eventIds.ContainsKey(stringId))
            {
                throw new Exception("EventId already exists: " + stringId);
            }
            EventId eventId = new EventId(GetIndex(), stringId);
            _eventIds.Add(stringId, eventId);

            return eventId;
        }

        public static EventId GetCachedEvent(string stringId)
        {
            EventId eventId;
            if (_eventIds.TryGetValue(stringId, out eventId))
            {
                return eventId;
            }
            else
            {
                throw new Exception("EventId not found: " + stringId);
            }
        }

        private static int GetIndex()
        {
            return _index++;
        }

        private static void Reset()
        {
            _index = 1;
            _eventIds.Clear();
        }
    }
}