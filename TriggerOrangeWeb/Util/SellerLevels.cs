using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CipherPark.TriggerOrange.Web.Util
{
    public static class SellerLevels
    {
        private const long CasualMaxScore = 100;
        private const long BeginnerMaxScore = 500;
        private const long IntermediateMaxScore = 2000;
        private const long AdvancedMaxScore = 5000;
        private const long ExpertMaxScore = long.MaxValue;

        private const string CasualText = "Casual";
        private const string BeginnerText = "Beginner";
        private const string IntermediateText = "Intermediate";
        private const string AdvancedText = "Advanced";
        private const string ExpertText = "Expert";
        
        /// <summary>
        /// Gets array of seller level Tuples. Item 1: "Level Name" (string). Item 2: "Level Max Score" (long integer)
        /// </summary>
        public static Tuple<string, long>[] All
        {
            get
            {
                return new[]
                {
                    new Tuple<string, long>(CasualText, CasualMaxScore),
                    new Tuple<string, long>(BeginnerText, BeginnerMaxScore),
                    new Tuple<string, long>(IntermediateText, IntermediateMaxScore),
                    new Tuple<string, long>(AdvancedText, AdvancedMaxScore),
                    new Tuple<string, long>(ExpertText, ExpertMaxScore),
                };
            }
        }

        public static string ScoreToLevel(long score)
        {
            return All.OrderBy(x => x.Item2).First(x => score <= x.Item2).Item1;
        }
    }    
}