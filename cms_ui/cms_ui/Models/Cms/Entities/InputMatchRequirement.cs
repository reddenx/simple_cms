using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms.Entities
{
    public enum MatchTypes
    {
        Exists,
        ExactValueMatch,
        FuzzyTypeMatch
    }

    public class InputMatchRequirement
    {
        public MatchTypes MatchType;
        public string MatchKey;
        public string MatchValue;

        public InputMatchRequirement(string key, MatchTypes type, string value = null)
        {
            this.MatchType = type;
            this.MatchKey = key;
            this.MatchValue = value;
        }

        public bool IsMatch(string key, string value = null)
        {
            if (MatchKey != key) return false;

            switch (MatchType)
            {
                case MatchTypes.ExactValueMatch:
                    return MatchValue == value;
                case MatchTypes.Exists:
                    return value != null;
                case MatchTypes.FuzzyTypeMatch:
                    return CheckForDifferentTypeMatches(value);
                default:
                    throw new NotImplementedException("this match type hasn't been built yet");
            }
        }

        private bool CheckForDifferentTypeMatches(string value)
        {
            //lower string, int, bool, guid
            if (value.ToLower() == MatchValue.ToLower()) return true;

            int intInputValue = -1;
            int intMatchValue = -1;
            if (int.TryParse(value, out intInputValue) 
                && int.TryParse(MatchValue, out intMatchValue)
                && intInputValue == intMatchValue)
            {
                return true;
            }

            bool boolInputValue = false;
            bool boolMatchValue = false;
            if (bool.TryParse(value, out boolInputValue)
                && bool.TryParse(MatchValue, out boolMatchValue)
                && intInputValue == intMatchValue)
            {
                return true;
            }

            Guid guidInputValue = Guid.Empty;
            Guid guidMatchValue = Guid.Empty;
            if (Guid.TryParse(value, out guidInputValue)
                && Guid.TryParse(MatchValue, out guidMatchValue)
                && intInputValue == intMatchValue)
            {
                return true;
            }

            return false;
        }
    }
}