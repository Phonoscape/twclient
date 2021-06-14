using CoreTweet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static CoreTweet.OAuth;

namespace twclient
{
    class Twitter
    {
        public enum TweetType : int
        {
            Normal = 0,
            Reply,
            RetweetWith
        };

        private string[] splitWord = {"\n", " ", "　", "@", "＠", "#", "＃", "http://", "https://", "(", ")",
                                    "｛", "｝", "（", "）", "【", "】", "「", "」", "『", "』", "〈", "〉",
                                    "《", "》", ":", "：","、","。","・","．","!","！","?","？", ",", "，" };

        private string consumer_key = null;
        private string consumer_secret = null;

        private string access_token = null;
        private string access_token_secret = null;

        Tokens tokens;
        UserResponse account;

        FormTimeLine parentForm;

        List<TimeLine> timeLineList;

        TimeLine.TimeLineType timeLineIndex = TimeLine.TimeLineType.TIMELINE_HOME;
        int timeLineSubIndex = 0;

        //System.Windows.Forms.Timer updateTimer;
        System.Timers.Timer updateTimer;

        public Twitter(FormTimeLine parent)
        {
            parentForm = parent;

            //updateTimer = new System.Windows.Forms.Timer();
            //updateTimer.Tick += new EventHandler(OnUpdateTimer);

            updateTimer = new System.Timers.Timer();
            updateTimer.Elapsed += OnUpdateTimer;

        }

        ~Twitter()
        {
            updateTimer.Stop();
            updateTimer.Dispose();
        }

        private bool KeySetting()
        {
            DialogKeyIn dlgKey = new DialogKeyIn(parentForm);
            dlgKey.ShowDialog();

            consumer_key = dlgKey.GetKey();
            consumer_secret = dlgKey.GetSecret();

            return true;
        }

        private bool InitialAccess()
        {
            OAuthSession session = OAuth.Authorize(consumer_key, consumer_secret);

            parentForm.OpenUrl(session.AuthorizeUri.AbsoluteUri);

            DialogPinIn dlgPin = new DialogPinIn(parentForm);
            dlgPin.ShowDialog();
            string pin = dlgPin.GetPin();

            tokens = OAuth.GetTokens(session, pin);
            return (tokens != null);
        }

        public void Start()
        {
            Settings settings = new Settings();
            settings.Open();
            consumer_key = settings.GetValue(Settings.PARAM_CONSUMER_KEY);
            consumer_secret = settings.GetValue(Settings.PARAM_CONSUMER_SECRET);

            access_token = settings.GetValue(Settings.PARAM_TOKEN);
            access_token_secret = settings.GetValue(Settings.PARAM_TOKEN_SECRET);

            if (consumer_key.Length == 0 || consumer_secret.Length == 0)
            {
                KeySetting();
                settings.SetValue(Settings.PARAM_CONSUMER_KEY, consumer_key);
                settings.SetValue(Settings.PARAM_CONSUMER_SECRET, consumer_secret);
                settings.Flash();
            }

            if (access_token.Length != 0 || access_token_secret.Length != 0)
            {
                tokens = Tokens.Create(consumer_key, consumer_secret, access_token, access_token_secret);
            }
            else
            {
                if (InitialAccess())
                {
                    settings.SetValue(Settings.PARAM_TOKEN, tokens.AccessToken);
                    settings.SetValue(Settings.PARAM_TOKEN_SECRET, tokens.AccessTokenSecret);
                    settings.Flash();
                }
            }

            timeLineList = new List<TimeLine>();
            account = tokens.Account.VerifyCredentials();
        }

        public void GetRecent()
        {
            CoreTweet.Core.ListedResponse<Status> statuses = tokens.Statuses.HomeTimeline();
            List<Status> all = new List<Status>();

            all.AddRange(statuses);
        }

        public long GetTokenUser()
        {
            return (long)account.Id;
        }

        public List<List> GetList()
        {
            var list = tokens.Lists.List().ToList();

            return list;
        }

        public List<TimeLine> GetTimeLineList()
        {
            return timeLineList;
        }

        /*
        public TimeLine AddTimeLine(TimeLine.TimeLineType ty, string tabName,bool init = false)
        {
            for (int i = 0; i < parentForm.panelControlMainTree1.treeView1.Nodes.Count; i++)
            {
                var node = parentForm.panelControlMainTree1.treeView1.Nodes[i];

                if ((TimeLine.TimeLineType)node.Tag == ty)
                {
                    int j = 0;
                    for (j = 0; j < node.Nodes.Count; j++)
                    {
                        if (node.Nodes[j].Text == tabName)
                        {
                            parentForm.panelControlMainTree1.treeView1.SelectedNode = node.Nodes[j];
                            return SelectTimeLine(ty, tabName);
                        }
                    }

                    var childTl = new TimeLine(ty, tabName);
                    childTl.SetSubIndex(j + 1);
                    timeLineList.Add(childTl);

                    var childNode = parentForm.panelControlMainTree1.treeView1.Nodes[i].Nodes.Add(tabName);
                    childNode.Tag = ty;
                    if (!init) parentForm.panelControlMainTree1.treeView1.SelectedNode = childNode;

                    return childTl;
                }
            }

            var treenode = parentForm.panelControlMainTree1.treeView1.Nodes.Add(tabName);
            treenode.Tag = ty;

            var tl = new TimeLine(ty, tabName);
            timeLineList.Add(tl);
            return tl;
        }
        */

        public TimeLine AddTimeLine(TimeLine.TimeLineType ty, string tabName)
        {
            if (SelectTimeLine(ty, tabName) != null) return null;

            var tl = new TimeLine(ty, tabName);
            timeLineList.Add(tl);
            return tl;
        }

        public TimeLine SelectTimeLine(TimeLine.TimeLineType ty, string tabName)
        {
            foreach (var tl in timeLineList)
            {
                if (tl.GetType() == ty && tl.GetTabName() == tabName)
                {
                    return tl;
                }
            }

            return null;
        }

        public TimeLine SelectTimeLine(TimeLine.TimeLineType ty, int subIndex)
        {
            foreach (var tl in timeLineList)
            {
                if (tl.GetType() == ty && tl.GetSubIndex() == subIndex)
                {
                    return tl;
                }
            }

            return null;
        }

        public TimeLine SelectTimeLine()
        {
            TimeLine tl = null;

            foreach (var tmptl in timeLineList)
            {
                if (tmptl.GetType() == timeLineIndex)
                {
                    if (tmptl.GetCanMulti())
                    {
                        if (tmptl.GetSubIndex() == timeLineSubIndex)
                        {
                            tl = tmptl;
                            break;
                        }
                    }
                    else
                    {
                        tl = tmptl;
                        break;
                    }
                }
            }

            return tl;
        }

        public bool GetTimeLine(long tweetId = 0)
        {
            CoreTweet.Core.ListedResponse<Status> statuses;
            SearchResult result;
            long maxId;
            long userId;
            long listId;
            string searchStr;
            int ct;
            bool res = true;

            TimeLine tl = SelectTimeLine();
            //TimeLine tl = selectedTimeLine;
            if (tl == null)
            {
                parentForm.SetStatusMenu("内部エラー（Timeline取得）");
                return false;
            }

            tl.ClearNewTimeLine();

            try
            {
                switch (timeLineIndex)
                {
                    case TimeLine.TimeLineType.TIMELINE_HOME:
                        maxId = tl.GetMaxId();
                        ct = tl.GetGetCount();
                        if (tweetId == 0)
                        {
                            statuses = tokens.Statuses.HomeTimeline(since_id => maxId, count => ct, tweet_mode => TweetMode.Extended);
                        }
                        else 
                        {
                            statuses = tokens.Statuses.HomeTimeline(since_id => (tweetId -1), max_id => tweetId, tweet_mode => TweetMode.Extended);
                        }
                        tl.AddTimeLine(statuses.ToList<Status>());
                        break;
                    case TimeLine.TimeLineType.TIMELINE_USER:
                        maxId = tl.GetMaxId();
                        userId = tl.GetUserId();
                        ct = tl.GetGetCount();
                        if (tweetId == 0)
                        {
                            statuses = tokens.Statuses.UserTimeline(since_id => maxId, count => ct, user_id => userId, tweet_mode => TweetMode.Extended);
                        }
                        else
                        {
                            statuses = tokens.Statuses.UserTimeline(since_id => (tweetId - 1), max_id => tweetId, user_id => userId, tweet_mode => TweetMode.Extended);
                        }
                        tl.AddTimeLine(statuses.ToList<Status>());
                        break;
                    /*                    case TimeLine.TimeLineType.TIMELINE_NOTIFICATION:
                                            maxId = tl.GetMaxId();
                                            statuses = tokens.Statuses.HomeTimeline(since_id => maxId, tweet_mode => "extended");
                                            break;
                    */
                    case TimeLine.TimeLineType.TIMELINE_SEARCH:
                        maxId = tl.GetMaxId();
                        searchStr = tl.GetSearchStr();
                        ct = tl.GetGetCount();
                        result = tokens.Search.Tweets(q => searchStr, count => ct, since_id => maxId, tweet_mode => TweetMode.Extended);
                        tl.AddTimeLine(result.ToList());
                        break;
                    case TimeLine.TimeLineType.TIMELINE_LIKE:
                        maxId = tl.GetMaxId();
                        ct = tl.GetGetCount();
                        statuses = tokens.Favorites.List(since_id => maxId, tweet_mode => TweetMode.Extended);
                        tl.AddTimeLine(statuses.ToList<Status>());
                        break;
                    /*                    case TimeLine.TimeLineType.TIMELINE_MESSAGE:
                                            maxId = tl.GetMaxId();
                                            statuses = tokens.DirectMessages.(since_id => maxId, tweet_mode => "extended");
                                            break;
                    */
                    case TimeLine.TimeLineType.TIMELINE_MENTION:
                        maxId = tl.GetMaxId();
                        ct = tl.GetGetCount();
                        if (tweetId == 0)
                        {
                            statuses = tokens.Statuses.MentionsTimeline(count => ct, since_id => maxId, tweet_mode => TweetMode.Extended);
                        }
                        else
                        {
                            statuses = tokens.Statuses.MentionsTimeline(since_id => (tweetId - 1), max_id => tweetId, tweet_mode => TweetMode.Extended);
                        }
                        tl.AddTimeLine(statuses.ToList<Status>());
                        break;
                    case TimeLine.TimeLineType.TIMELINE_LISTS:
                        maxId = tl.GetMaxId();
                        ct = tl.GetGetCount();
                        listId = tl.GetListId();
                        if (tweetId == 0)
                        {
                            statuses = tokens.Lists.Statuses(list_id => listId, count => ct, since_id => maxId, tweet_mode => TweetMode.Extended);
                        }
                        else
                        {
                            statuses = tokens.Lists.Statuses(list_id => listId, since_id => (tweetId - 1), max_id => tweetId, tweet_mode => TweetMode.Extended);
                        }
                        tl.AddTimeLine(statuses.ToList());
                        break;
                    default:
                        maxId = tl.GetMaxId();
                        statuses = tokens.Statuses.HomeTimeline(since_id => maxId, tweet_mode => TweetMode.Extended);
                        tl.AddTimeLine(statuses.ToList<Status>());
                        break;
                }
            }
            catch (CoreTweet.TwitterException e)
            {
                res = false;

                switch (e.Status)
                {
                    case (System.Net.HttpStatusCode)System.Net.WebExceptionStatus.NameResolutionFailure:
                        parentForm.SetStatusMenu("名前解決ができません。");
                        break;
                    case (System.Net.HttpStatusCode)System.Net.WebExceptionStatus.Timeout:
                        parentForm.SetStatusMenu("応答が一定時間ありませんでした。");
                        break;
                    default:
                        parentForm.SetStatusMenu(e.Message);
                        break;
                }
                return res;
            }
            catch (System.Net.WebException e)
            {
                res = false;
                parentForm.SetStatusMenu(e.Message);
            }
            catch
            {
                res = false;
            }

            return res;
        }

        public Status GetTimeLineFromId(long tweetId)
        {
            TimeLine tl = SelectTimeLine();
            //TimeLine tl = selectedTimeLine;
            return tl?.GetTweetFromId(tweetId);
        }

        public void SetHashTagAdd(bool value)
        {
            TimeLine tl = SelectTimeLine();
            tl.SetHashAddTag(value);
        }

        public void SetHashTag(string value)
        {
            TimeLine tl = SelectTimeLine();
            tl.SetHashTag(value);
        }


        public TimeLine StartTimeLine(TimeLine.TimeLineType index, int subIndex)
        {
            timeLineIndex = index;
            timeLineSubIndex = subIndex;

            parentForm.panelTimeLine1.panelTimeLineList1.listView1.Items.Clear();

            TimeLine tl = SelectTimeLine();
            //TimeLine tl = selectedTimeLine;
            parentForm.SetListView();

            // Get TimeLine Start
            updateTimer.Interval = 1;
            updateTimer.Start();

            return tl;
        }

        private void OnUpdateTimer(object sender,System.EventArgs e)
        {
            updateTimer.Stop();

            parentForm.SetStatusMenu("更新中");

            TimeLine tl = SelectTimeLine();
            //TimeLine tl = selectedTimeLine;
            var res = GetTimeLine();
            if (res)
            {
                if (tl.GetNewTimeLineCount() > 0)
                {
                    parentForm.SetListViewNew();
                    parentForm.SetStatusMenu("待機中");
                }
                else 
                {
                    parentForm.SetStatusMenu("更新なし");
                }
            }
            else
            {
                parentForm.SetStatusMenu("待機中");
            }

            updateTimer.Interval = tl.GetUpdateTime() * 1000;
            updateTimer.Start();
        }

        public String ExtendedEntiries(Status st)
        {
            String text = st.Text.ToString();
            var ent = st.Entities;

            var hash = ent.HashTags;
            var media = ent.Media;
            var urls = ent.Urls;
            var user = ent.UserMentions;

            foreach (var u in urls)
            {
                text = text.Replace(u.Url, u.ExpandedUrl);
            }

            return text;
        }

        public String MakeHtmlBody(Status tl, List<string> eventHandlerName)
        {
            string text;
            string body = "<div class=\"main\"><p>";

            if (tl.RetweetedStatus != null)
            {
                tl = tl.RetweetedStatus;
            }
            text = tl.FullText;

            int st = 0, ed = 0,ln = 0;

            string tmp, hash = "", user = "",url = "";
            List<string> img = new List<string>();
            while ( st < text.Length )
            {
                var ret = IndexOfMulti(text.Substring(st), splitWord);
                ed = ret[0];
                ln = ret[1];

                if (ed > 0)
                {
                    tmp = text.Substring(st, ed);
                }
                else 
                {
                    tmp = text.Substring(st, ln);
                }
                st += tmp.Length;

                if (tmp == "#" || tmp == "＃")
                {
                    hash += tmp;
                    continue;
                }
                else if (tmp == "@" || tmp == "＠")
                {
                    user += tmp;
                    continue;
                }
                else if (tmp == "http://" || tmp == "https://")
                {
                    url += tmp;
                    continue;
                }

                if (tmp == "\n")
                {
                    tmp = "<br>";
                }

                if (hash.Length != 0)
                {
                    hash += tmp;
                    var hashlink = hash;

                    foreach ( var h in tl.Entities.HashTags)
                    {
                        if ( h.Text == tmp )
                        {
                            hashlink = "<a href=\"" + hash + "\" alt=\"" + hash + "\">" + hash + "</a>";
                            break;
                        }
                    }
                    tmp = hashlink;
                    hash = "";
                }

                if (user.Length != 0)
                {
                    user += tmp;
                    var userlink = user;

                    foreach (var u in tl.Entities.UserMentions)
                    {
                        if (u.ScreenName == tmp)
                        {
                            userlink = "<a href=\"" + u.Id.ToString() + "\" alt=\"" + u.Id.ToString() + "\">" + user + "</a>";
                            break;
                        }
                    }

                    tmp = userlink;
                    user = "";
                }

                if (url.Length != 0)
                {
                    // URLリンク
                    foreach (var h in tl.Entities.Urls)
                    {
                        if (h.Url == url + tmp)
                        {
                            tmp = "<a href=\"" + h.ExpandedUrl + "\" alt=\"" + h.ExpandedUrl + "\">" + h.DisplayUrl + "</a>";
                            break;
                        }
                    }

                    // メディアリンク
                    if (tl.ExtendedEntities != null)
                    {
                        if (tl.ExtendedEntities.Media != null)
                        {
                            bool cont = false;

                            foreach (var i in tl.ExtendedEntities.Media)
                            {
                                if (i.Url == url + tmp)
                                {
                                    if (i.VideoInfo != null)
                                    {
                                        img.Add("<a href=\"" + i.VideoInfo.Variants[0].Url + "\"><img style=\"width: 48%;\" src=\"" + i.MediaUrlHttps + "\"></a>");
                                    }
                                    else
                                    {
                                        //img.Add("<a href=\"" + i.ExpandedUrl + "\"><img style=\"width: 48%;\" src=\"" + i.MediaUrlHttps + "\"></a>");
                                        img.Add("<a href=\"" + i.MediaUrlHttps + "\"><img style=\"width: 48%;\" src=\"" + i.MediaUrlHttps + "\"></a>");
                                    }
                                    cont = true;
                                }
                            }

                            if (cont) continue;
                        }
                    }
                        
                   url = "";
                }

                body += tmp;
                tmp = "";
            }
            body += "</p></div>\n";

            if (img.Count > 0)
            {
                body += "<div class=\"image\">";
                foreach (var i in img)
                {
                    body += i;
                }
                body += "</div>";
            }

            return body;
        }

        private List<int> IndexOfMulti(string str, string[] splitChar)
        {
            int min = str.Length;
            int ln = 0;

            foreach (var st in splitChar)
            {
                var pos = str.IndexOf(st,StringComparison.InvariantCulture);
                if (pos == -1) continue;
                if ( min > pos )
                {
                    min = pos;
                    ln = st.Length;
                }
            }

            List<int> ret = new List<int>();
            ret.Add(min);
            ret.Add(ln);

            return ret;
        }

        // Send

        public void Send(String tweetText)
        {
            if (tweetText.Length > 0)
            {
                tokens.Statuses.UpdateAsync(tweetText);
            }
        }

        public void Reply(String tweetText,long id)
        {
            if (tweetText.Length > 0)
            {
                tokens.Statuses.UpdateAsync(status => tweetText, in_reply_to_status_id => id);
            }
        }

        public void RetweetWith(String tweetText)
        {
            if (tweetText.Length > 0)
            {
                tokens.Statuses.UpdateAsync(tweetText);
            }
        }

        public bool ChangeSubIndex(TimeLine.TimeLineType ty, int src,int dst)
        {
            if (src == 0) return false;

            foreach(var tl in timeLineList)
            {
                if ( tl.GetType() == ty && tl.GetSubIndex() == src )
                {
                    tl.SetSubIndex(dst);
                    return true;
                }
            }

            return false;
        }

        public bool DelSubIndex(TimeLine.TimeLineType ty, int no)
        {
            if (no == 0) return false;

            foreach (var tl in timeLineList)
            {
                if (tl.GetType() == ty && tl.GetSubIndex() == no)
                {
                    timeLineList.Remove(tl);
                    return true;
                }
            }

            return false;
        }

        public long SearchUserId(string name)
        {
            if (name[0] == '@') name = name.Substring(1);

            var id = tokens.Users.Lookup(screen_name => name);

            return (long)id[0].Id;
        }

        public bool CheckRetweet(long tweetId)
        {
            try
            {
                var st = tokens.Statuses.Show(id => tweetId);

                return (bool)st.IsRetweeted;
            }
            catch(CoreTweet.TwitterException e)
            {
                parentForm.SetStatusMenu(e.Message);
            }
            catch(System.Net.WebException e)
            {
                parentForm.SetStatusMenu(e.Message);
            }

            return false;
        }

        public void Retweet(long tweetId)
        {
            tokens.Statuses.RetweetAsync(id => tweetId);
        }

        public void UnRetweet(long tweetId)
        {
            tokens.Statuses.UnretweetAsync(id => tweetId);
        }

        public bool CheckLike(long tweetId)
        {
            try
            {
                var st = tokens.Favorites.List(since_id => (tweetId - 1), max_id => tweetId);

                if (st.Count > 0)
                {
                    return true;
                }
            }
            catch(CoreTweet.TwitterException e)
            {
                parentForm.SetStatusMenu(e.Message);
                return false;
            }
            catch(System.Net.WebException e)
            {
                parentForm.SetStatusMenu(e.Message);
                return false;
            }

            return false;
        }

        public bool CheckSelfTweet(long tweetId)
        {
            try
            {
                var userId = GetTokenUser();
                var st = tokens.Statuses.UserTimeline(user_id => userId, since_id => (tweetId - 1), max_id => tweetId );

                if (st.Count > 0)
                {
                    return true;
                }
            }
            catch (CoreTweet.TwitterException e)
            {
                parentForm.SetStatusMenu(e.Message);
                return false;
            }
            catch (System.Net.WebException e)
            {
                parentForm.SetStatusMenu(e.Message);
                return false;
            }

            return false;
        }

        public void Like(long tweetId)
        {
            tokens.Favorites.CreateAsync(id => tweetId);
        }

        public void UnLike(long tweetId)
        {
            tokens.Favorites.DestroyAsync(id => tweetId);
        }

        public void DeleteTweet(long tweetId)
        {
            tokens.Statuses.Destroy(id => tweetId);
            var tl = SelectTimeLine();
            var st = tl.GetTweetFromId(tweetId);
            tl.GetTimeLine().Remove(st);
        }

        public CoreTweet.Status GetTimeLineFromAPI(long? quotedStatusId)
        {
            CoreTweet.Core.ListedResponse<CoreTweet.Status> status;

            try
            {
                status = tokens.Statuses.Lookup(id => quotedStatusId, tweet_mode => TweetMode.Extended);
            }
            catch (CoreTweet.TwitterException e)
            {
                parentForm.SetStatusMenu(e.Message);
                return null;
            }
            catch (System.Net.WebException e)
            {
                parentForm.SetStatusMenu(e.Message);
                return null;
            }

            if (status.Count == 0) return null;

            return status[0];
        }
    }
}
