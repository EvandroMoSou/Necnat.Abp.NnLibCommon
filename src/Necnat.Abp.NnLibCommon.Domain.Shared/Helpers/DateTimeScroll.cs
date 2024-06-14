using System;
using System.Collections.Generic;
using System.Linq;

namespace Necnat.Abp.NnLibCommon.Helpers
{
    public class DateTimeScroll
    {
        public DateTime InitialDateTime { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public DateTime? MaxDateTime { get; set; }
        public DateTime? MinDateTime { get; set; }
        public Dictionary<int, int>? MilestoneDict { get; set; }
        public bool HasNext { get; set; } = true;

        public DateTimeScroll(DateTime initialDateTime)
        {
            InitialDateTime = initialDateTime;
            CurrentDateTime = initialDateTime;
        }

        public void InitializePositive(DateTime? maxDateTime = null, Dictionary<int, int>? milestoneDict = null)
        {
            MaxDateTime = maxDateTime;

            if (milestoneDict == null)
                MilestoneDict = new Dictionary<int, int>
                {
                    { 1, 3 },
                    { 7, 6 },
                    { 15, 6 },
                    { 30, 9 },
                    { 60, 9 },
                    { 90, -1 }
                };
            else
                MilestoneDict = milestoneDict;
        }

        public void InitializeNegative(DateTime? minDateTime = null, Dictionary<int, int>? milestoneDict = null)
        {
            MinDateTime = minDateTime;

            if (milestoneDict == null)
                MilestoneDict = new Dictionary<int, int>
                {
                    { -1, 2 },
                    { -7, 6 },
                    { -15, 6 },
                    { -30, 9 },
                    { -60, 9 },
                    { -90, -1 }
                };
            else
                MilestoneDict = milestoneDict;
        }

        public void SetNextDatetime()
        {
            if (!HasNext)
                return;

            if (MaxDateTime != null)
            {

                if (CurrentDateTime > MaxDateTime)
                {
                    CurrentDateTime = (DateTime)MaxDateTime;
                    HasNext = false;
                    return;
                }
            }

            if (MinDateTime != null)
            {
                if (CurrentDateTime < MinDateTime)
                {
                    CurrentDateTime = (DateTime)MinDateTime;
                    HasNext = false;
                    return;
                }
            }

            if (MilestoneDict == null || MilestoneDict.Count < 1 || MilestoneDict.Where(x => x.Value != 0).Count() < 1)
            {
                HasNext = false;
                return;
            }

            var milestone = MilestoneDict.Where(x => x.Value != 0).First();

            CurrentDateTime = CurrentDateTime.AddDays(milestone.Key);

            if (MilestoneDict[milestone.Key] > 0)
                MilestoneDict[milestone.Key] = MilestoneDict[milestone.Key] - 1;

            if (MilestoneDict.Where(x => x.Value != 0).Count() < 1)
                HasNext = false;
        }
    }
}
