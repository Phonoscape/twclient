using CoreTweet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twclient
{
    public class TimeLine
    {
        // 1つのタイムラインを管理
        public enum TimeLineType : int
        {
            TIMELINE_HOME = 0,
            TIMELINE_USER,
//            TIMELINE_NOTIFICATION,
            TIMELINE_SEARCH,
            TIMELINE_LIKE,
//            TIMELINE_MESSAGE,
            TIMELINE_MENTION,
            TIMELINE_LISTS,
        };

        private List<Status> timeline;
        private List<Status> newTimeline;
        private string tabName = "";
        private int searchNo;
        private string searchStr = "";
        private string hashTagStr = "";
        private bool hashTagAddFlag = false;
        private long userId = 0;
        private long listId = 0;

        private TimeLineType timeLineType;
        private long maxId = 1;
        private long minId = long.MaxValue;
        private int getCount = 20;
        private bool enableSubIndex = false;
        private int subIndex = 0;

        private int updateTime = 0;
        private int apiLimit = 0;

        public TimeLine()
        {
            timeline = new List<Status>();
            newTimeline = new List<Status>();
            SetType(TimeLineType.TIMELINE_HOME);
            updateTime = 60;
        }

        public TimeLine(TimeLineType timeLineType, string tabName)
        {
            timeline = new List<Status>();
            newTimeline = new List<Status>();
            SetType(timeLineType);
            SetTabName(tabName);
            updateTime = 60;

            switch(timeLineType)
            {
                case TimeLineType.TIMELINE_HOME:
                    enableSubIndex = false;
                    apiLimit = 15;
                    getCount = 100;
                    break;
                case TimeLineType.TIMELINE_USER:
                    enableSubIndex = true;
                    apiLimit = 180;
                    getCount = 100;
                    break;
                case TimeLineType.TIMELINE_SEARCH:
                    enableSubIndex = true;
                    apiLimit = 180;
                    getCount = 100;
                    break;
                case TimeLineType.TIMELINE_LIKE:
                    enableSubIndex = false;
                    apiLimit = 15;
                    getCount = 100;
                    break;
                case TimeLineType.TIMELINE_MENTION:
                    enableSubIndex = false;
                    apiLimit = 15;
                    getCount = 100;
                    break;
                case TimeLineType.TIMELINE_LISTS:
                    enableSubIndex = true;
                    apiLimit = 900;
                    getCount = 100;
                    break;
                default:
                    enableSubIndex = false;
                    apiLimit = 15;
                    getCount = 100;
                    break;
            }
            updateTime = 60 / ( apiLimit / 15 );
        }

        public new TimeLineType GetType() { return timeLineType; }
        public void SetType(TimeLineType value) { timeLineType = value; }
        public void SetTabName(string value) { tabName = value; }
        public string GetTabName() { return tabName; }
        public void SetSearchNo(int value) { searchNo = value; }
        public int GetSearchNo() { return searchNo; }
        public void SetSearchStr(string value) { searchStr = value; }
        public string GetSearchStr() { return searchStr; }
        public void SetHashTag(string value) { hashTagStr = value; }
        public string GetHashTag() { return hashTagStr; }
        public void SetHashAddTag(bool value) { hashTagAddFlag = value; }
        public bool GetHashAddTag() { return hashTagAddFlag; }
        public void SetUserId(long value) { userId = value; }
        public long GetUserId() { return userId; }
        public long GetMaxId() { return maxId; }
        public void SetUpdateTime(int value) { updateTime = value; }
        public int GetUpdateTime() { return updateTime; }
        public void SetGetCount(int value) { getCount = value; }
        public int GetGetCount() { return getCount; }
        public bool GetCanMulti() { return enableSubIndex; }
        public void SetSubIndex(int value) { subIndex = value; }
        public int GetSubIndex() { return subIndex; }
        public void SetListId(long value) { listId = value; }
        public long GetListId() { return listId; }

        public List<string> saveParam()
        {
            List<string> param = new List<string>();

            param.Add(tabName);
            param.Add(searchNo.ToString());
            param.Add(searchStr);
            param.Add(hashTagStr);
            param.Add(hashTagAddFlag ? "true" : "false");
            param.Add(userId.ToString());
            param.Add(getCount.ToString());
            param.Add(subIndex.ToString());
            param.Add(updateTime.ToString());
            param.Add(apiLimit.ToString());

            return param;
        }

        public void loadParam(List<string> val)
        {
            tabName = val[0];
            searchNo = int.Parse(val[1]);
            searchStr = val[2];
            hashTagStr = val[3];
            hashTagAddFlag = (val[4] == "true" ? true : false);
            userId = long.Parse(val[5]);
            getCount = int.Parse(val[6]);
            subIndex = int.Parse(val[7]);
            updateTime = int.Parse(val[8]);
            apiLimit = int.Parse(val[9]);
        }

        public void AddTimeLine(List<Status> status)
        {
            newTimeline.Clear();
            newTimeline.AddRange(status);

            foreach (var st in status)
            {
                if (!timeline.Contains(st))
                {
                    timeline.Add(st);
                }
                else
                {
                    timeline.Remove(st);
                    timeline.Add(st);
                }
            }

            foreach (var line in status)
            {
                maxId = line.Id > maxId ? line.Id : maxId;
                minId = line.Id < minId ? line.Id : minId;
            }

            StatusComparer sc = new StatusComparer();
            timeline.Sort(sc);
        }

        public List<Status> GetNewTimeLine()
        {
            return newTimeline;
        }

        public List<Status> GetTimeLine()
        {
            return timeline;
        }

        public Status GetTweetFromId(long tweetId)
        {
            foreach (var line in timeline)
            {
                if (line.Id == tweetId) return line;
            }

            return null;
        }

        public void ClearNewTimeLine()
        {
            newTimeline.Clear();
        }

        public int GetNewTimeLineCount()
        {
            return newTimeline.Count();
        }
    }

    public class StatusComparer : IComparer<CoreTweet.Status>
    {
        public enum StatusCompare
        {
            TweetID,
            UserID,
            ScreenName,
            Tweet,
            Date
        }

        public enum Direction
        {
            Ascending,
            Descending
        }

        private StatusCompare sc = StatusCompare.TweetID;
        private Direction dir = Direction.Descending;

        public StatusCompare Sc { get => sc; set => sc = value; }
        public Direction Dir { get => dir; set => dir = value; }


        public int Compare(CoreTweet.Status x, CoreTweet.Status y)
        {
            int res = 0;

            if (dir == Direction.Ascending)
            {
                switch (sc)
                {
                    case StatusCompare.TweetID:
                        res = x.Id.CompareTo(y.Id);
                        break;
                    case StatusCompare.UserID:
                        res = x.User.Name.CompareTo(y.User.Name);
                        break;
                    case StatusCompare.ScreenName:
                        res = x.User.ScreenName.CompareTo(y.User.ScreenName);
                        break;
                    case StatusCompare.Tweet:
                        res = x.FullText.CompareTo(y.FullText);
                        break;
                    case StatusCompare.Date:
                        res = x.CreatedAt.CompareTo(y.CreatedAt);
                        break;
                    default:
                        res = x.Id.CompareTo(y.Id);
                        break;
                }
            }
            else
            {
                switch (sc)
                {
                    case StatusCompare.TweetID:
                        res = y.Id.CompareTo(x.Id);
                        break;
                    case StatusCompare.UserID:
                        res = y.User.Name.CompareTo(x.User.Name);
                        break;
                    case StatusCompare.ScreenName:
                        res = y.User.ScreenName.CompareTo(x.User.ScreenName);
                        break;
                    case StatusCompare.Tweet:
                        res = y.FullText.CompareTo(x.FullText);
                        break;
                    case StatusCompare.Date:
                        res = y.CreatedAt.CompareTo(x.CreatedAt);
                        break;
                    default:
                        res = y.Id.CompareTo(x.Id);
                        break;
                }
            }

            return res;
        }
    }
}
