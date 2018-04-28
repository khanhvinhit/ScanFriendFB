using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using xNet;

namespace ScanFriendFB
{
    public class FBGraph
    {
        public HttpRequest req = new HttpRequest();
        public static object _lock = new object();
        public string token = string.Empty;

        private static FBGraph _instance;

        public static FBGraph Instance
        {
            get
            {
                if (_instance == null) _instance = new FBGraph();
                return _instance;
            }
            private set { _instance = value; }
        }

        public FBGraph()
        {
            req.UserAgent = Http.ChromeUserAgent();
            req.Cookies = new CookieDictionary(false);
        }

        public string GetToken()
        {
            try
            {
                req.UserAgent = Http.ChromeUserAgent();
                string html = req.Get("https://www.facebook.com/me", null).ToString();
                token = Regex.Match(html, "access_token:\"(.*?)\"").Groups[1].ToString();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            req.UserAgent = Http.OperaMiniUserAgent();
            return token;
        }

        public void WriteLog(string log)
        {
            lock (_lock)
            {
                File.AppendAllText("debug.log", $"{log}{Environment.NewLine}{Environment.NewLine}");
            }
        }

        public HashSet<User> GetFriendUids()
        {
            HashSet<User> frList = new HashSet<User>();
            string url = "https://graph.facebook.com/v2.12/me/friends?fields=gender,name,relationship_status,birthday&limit=5000&access_token=" + token;
            try
            {
                foreach (var item in JArray.Parse(JObject.Parse(req.Get(url, null).ToString()).GetValue("data").ToString()))
                {
                    string id = JObject.Parse(item.ToString()).ContainsKey("id") ? JObject.Parse(item.ToString()).GetValue("id").ToString() : "";
                    string name = JObject.Parse(item.ToString()).ContainsKey("name") ? JObject.Parse(item.ToString()).GetValue("name").ToString() : "";
                    string gender = JObject.Parse(item.ToString()).ContainsKey("gender") ? JObject.Parse(item.ToString()).GetValue("gender").ToString() : "";
                    string relationship_status = JObject.Parse(item.ToString()).ContainsKey("relationship_status") ? JObject.Parse(item.ToString()).GetValue("relationship_status").ToString() : "";
                    DateTime age_range = JObject.Parse(item.ToString()).ContainsKey("birthday") ? DateTime.Parse(JObject.Parse(item.ToString()).GetValue("birthday").ToString()) : DateTime.Now;
                    frList.Add(new User(id, name, gender, relationship_status, age_range));
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                return frList;
            }
            return frList;
        }

        public HashSet<Post> GetPosts()
        {
            HashSet<Post> uid_fr = new HashSet<Post>();
            string url = "https://graph.facebook.com/v2.12/me/feed?fields=reactions.limit(50000),comments.limit(50000){from,message,created_time},created_time,message,story,link,with_tags&limit=5000&access_token=" + token;
            while (!string.IsNullOrEmpty(url))
            {
                try
                {
                    JObject ob = JObject.Parse(req.Get(url, null).ToString());
                    foreach (var item in JArray.Parse(ob.GetValue("data").ToString()))
                    {
                        string id = JObject.Parse(item.ToString()).ContainsKey("id") ? JObject.Parse(item.ToString()).GetValue("id").ToString() : "";
                        DateTime createdate = JObject.Parse(item.ToString()).ContainsKey("created_time") ? DateTime.Parse(JObject.Parse(item.ToString()).GetValue("created_time").ToString()) : DateTime.Now;
                        string mes = JObject.Parse(item.ToString()).ContainsKey("message") ? JObject.Parse(item.ToString()).GetValue("message").ToString() : "";
                        string story = JObject.Parse(item.ToString()).ContainsKey("story") ? JObject.Parse(item.ToString()).GetValue("story").ToString() : "";
                        string link = JObject.Parse(item.ToString()).ContainsKey("link") ? JObject.Parse(item.ToString()).GetValue("link").ToString() : "";

                        HashSet<Reaction> likes = new HashSet<Reaction>();
                        HashSet<Comment> cmts = new HashSet<Comment>();
                        HashSet<Withtag> with = new HashSet<Withtag>();

                        if (JObject.Parse(item.ToString()).ContainsKey("with_tags"))
                        {
                            string a = JObject.Parse(item.ToString()).GetValue("with_tags").ToString();
                            foreach (var l in JArray.Parse(JObject.Parse(a).GetValue("data").ToString()))
                            {
                                string uid = JObject.Parse(l.ToString()).ContainsKey("id") ? JObject.Parse(l.ToString()).GetValue("id").ToString() : "";
                                string name = JObject.Parse(l.ToString()).ContainsKey("name") ? JObject.Parse(l.ToString()).GetValue("name").ToString() : "";
                                with.Add(new Withtag(uid, name));
                            }
                        }
                        if (JObject.Parse(item.ToString()).ContainsKey("reactions"))
                        {
                            string a = JObject.Parse(item.ToString()).GetValue("reactions").ToString();
                            foreach (var l in JArray.Parse(JObject.Parse(a).GetValue("data").ToString()))
                            {
                                string uid = JObject.Parse(l.ToString()).ContainsKey("id") ? JObject.Parse(l.ToString()).GetValue("id").ToString() : "";
                                string name = JObject.Parse(l.ToString()).ContainsKey("name") ? JObject.Parse(l.ToString()).GetValue("name").ToString() : "";
                                string type = JObject.Parse(l.ToString()).ContainsKey("type") ? JObject.Parse(l.ToString()).GetValue("type").ToString() : "";
                                likes.Add(new Reaction(uid, name, type));
                            }
                        }
                        if (JObject.Parse(item.ToString()).ContainsKey("comments"))
                        {
                            string a = JObject.Parse(item.ToString()).GetValue("comments").ToString();
                            foreach (var l in JArray.Parse(JObject.Parse(a).GetValue("data").ToString()))
                            {
                                string uid = "";
                                string name = "";

                                if (JObject.Parse(l.ToString()).ContainsKey("from"))
                                {
                                    string b = JObject.Parse(l.ToString()).GetValue("from").ToString();
                                    uid = JObject.Parse(b.ToString()).ContainsKey("id") ? JObject.Parse(b.ToString()).GetValue("id").ToString() : "";
                                    name = JObject.Parse(b.ToString()).ContainsKey("name") ? JObject.Parse(b.ToString()).GetValue("name").ToString() : "";
                                }
                                string message = JObject.Parse(l.ToString()).ContainsKey("message") ? JObject.Parse(l.ToString()).GetValue("message").ToString() : "";
                                DateTime created_time = JObject.Parse(l.ToString()).ContainsKey("created_time") ? DateTime.Parse(JObject.Parse(l.ToString()).GetValue("created_time").ToString()) : DateTime.Now;
                                string mid = JObject.Parse(l.ToString()).ContainsKey("id") ? JObject.Parse(l.ToString()).GetValue("id").ToString() : "";
                                cmts.Add(new Comment(mid, message, created_time, uid, name));//, id));
                            }
                        }

                        uid_fr.Add(new Post(id, createdate, mes, story, link, with, likes, cmts));
                    }
                    url = JObject.Parse(JObject.Parse(req.Get(url, null).ToString()).GetValue("paging").ToString()).GetValue("next").ToString();
                }
                catch (NullReferenceException)
                {
                    return uid_fr;
                }
                catch (Exception ex)
                {
                    WriteLog(ex.ToString());
                }
            }

            return uid_fr;
        }

        public bool Unfriend(string uid)
        {
            try
            {
                string url = "https://mbasic.facebook.com/removefriend.php?friend_id=" + uid + "&unref=profile_gear";
                string fb_dtsg = Regex.Match(req.Get(url, null).ToString(), "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].ToString();
                url = "https://mbasic.facebook.com/a/removefriend.php";
                string param = $"fb_dtsg={fb_dtsg}&friend_id={uid}&unref=profile_gear&confirm=Confirm";
                req.Post(url, param, "application/x-www-form-urlencoded").ToString();
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                return false;
            }
        }

        public string GetPro()
        {
            string url = "https://graph.facebook.com/v2.12/me?&access_token=" + token;
            try
            {
                return JObject.Parse(req.Get(url, null).ToString()).GetValue("name").ToString();
            }
            catch (NullReferenceException)
            {
                return "";
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                return "";
            }
        }
    }
}