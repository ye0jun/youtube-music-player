using CefSharp;
using CefSharp.WinForms;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.FastFilters;
using Kaliko.ImageLibrary.Scaling;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_Master
{
    delegate void myPrevDelegate();
    delegate void delegateString(string title);
    class Player
    {
        private int _volume = 50;
        public string _title = "";
        public string _thumbnail = "";
        private PlayList list = new PlayList();
        private ChromiumWebBrowser browser;
        private Label label;
        private Panel panel;
        private Label labelTime;
        private ThreadStart ts;
        private Thread t;
        private ThreadStart ts2;
        private Thread t2;
        private int currentTime;
        private Ye0junSeekbar seekbar;
        private int repeatState = 0;

        public bool shuffle
        {
            get
            {
                return list.shuffle;
            }
            set
            {
                list.shuffle = value;
            }
        }
        public bool repeat
        {
            get
            {
                return list.repeat;
            }
            set
            {
                list.repeat = value;
            }
        }
        public Player(ChromiumWebBrowser browser, Label label,Panel panel,Label labelTime,Ye0junSeekbar seekbar)
        {
            this.label = label;
            this.browser = browser;
            this.panel = panel;
            this.labelTime = labelTime;
            this.seekbar = seekbar;
            ts = new ThreadStart(updateTimeFromServer);
            t = new Thread(ts);
            ts2 = new ThreadStart(updateTimeFromClient);
            t2 = new Thread(ts2);
            currentTime = 0;
        }

        public void updateTimeFromServer()
        {
            while (true)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("getCurrentTime();");
                    var task = browser.EvaluateScriptAsync(sb.ToString());
                    task.ContinueWith(t =>
                    {
                        if (!t.IsFaulted)
                        {
                            var response = t.Result;

                            if (response.Success == true)
                            {
                                currentTime = (int)(double.Parse(response.Result.ToString()));
                                TimeSpan time = TimeSpan.FromSeconds(currentTime);
                                string str = time.ToString(@"mm\:ss");
                                SetTextTime(str);
                                seekbar.SetValue(currentTime);
                            }
                        }
                    });

                }
                catch(Exception e)
                {
                }
                Thread.Sleep(5000);
            }
        }

        public void SetRepeatState(int state)
        {
            repeatState = state;
        }

        public int GetRepeatState()
        {
            return repeatState;
        }

        public void updateTimeFromClient()
        {
            while (true)
            {
                currentTime += 1;
                TimeSpan time = TimeSpan.FromSeconds(currentTime);
                string str = time.ToString(@"mm\:ss");
                SetTextTime(str);
                seekbar.SetValue(currentTime);
                Thread.Sleep(1000);
            }
        }

        public delegate void hello(bool a);
        public void Toggle(hello a)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("toggleVideo();");
            var task = browser.EvaluateScriptAsync(sb.ToString());
            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;

                    if (response.Success == true)
                    {
                        var state = response.Result.ToString();
                        if(state == "stop")
                        {
                            t2.Abort();
                            a(false);
                        }
                        else
                        {
                            t2 = new Thread(ts2);
                            t2.Start();
                            a(true);
                        }
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void setVolume(int volume)
        {
            this._volume = volume;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("setVolume(" + this._volume + ");");
            var task = browser.EvaluateScriptAsync(sb.ToString());
            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;

                    if (response.Success == true)
                    {
                        //MessageBox.Show(response.Result.ToString());
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
       

        void SetText(string str)
        {
            if (label.InvokeRequired)
            {
                delegateString a = new delegateString(SetText);
                label.Invoke(a, str);
            }
            else
            {
                label.Text = str;
            }
        }

        void SetTextTime(string str)
        {
            if (labelTime.InvokeRequired)
            {
                delegateString a = new delegateString(SetTextTime);
                labelTime.Invoke(a, str);
            }
            else
            {
                labelTime.Text = str;
            }
        }

        void SetBackground(string str)
        {
            if (panel.InvokeRequired)
            {
                delegateString a = new delegateString(SetBackground);
                panel.Invoke(a, str);
            }
            else
            {
                var image = new KalikoImage(str);
                image.ApplyFilter(new FastGaussianBlurFilter(30.0f));
                image = image.Scale(new FitScaling(1024, 1024));
                panel.BackgroundImage = image.GetAsBitmap();
            }
        }

        public void nextVideo()
        {
            if(list.totalIndex <= 0)
            {
                StringBuilder sbf = new StringBuilder();
                sbf.AppendLine("destroyYoutube();");
                var task1 = browser.EvaluateScriptAsync(sbf.ToString());
                task1.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;

                        if (response.Success == true)
                        {
                            //MessageBox.Show(response.Result.ToString());
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            JObject nextVideoObj = this.list.GetNextList();
            this._title = nextVideoObj["title"].ToString();
            this._thumbnail = nextVideoObj["thumbnails"].ToString();
            SetText(this._title);
            SetBackground(this._thumbnail);
            StringBuilder sb = new StringBuilder();
            var id = nextVideoObj["videoId"].ToString();
            sb.AppendLine("changeVideo('" + id + "'," + this._volume + ");");
            var task = browser.EvaluateScriptAsync(sb.ToString());
            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;

                    if (response.Success == true)
                    {
                        if (t2.IsAlive)
                            t2.Abort();
                        currentTime = 0;
                        t2 = new Thread(ts2);
                        t2.Start();
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void PlayVideoByIndex(int index)
        {
            if (list.totalIndex <= 0)
            {
                StringBuilder sbf = new StringBuilder();
                sbf.AppendLine("destroyYoutube();");
                var task1 = browser.EvaluateScriptAsync(sbf.ToString());
                task1.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;

                        if (response.Success == true)
                        {
                            //MessageBox.Show(response.Result.ToString());
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            JObject nextVideoObj = this.list.GetItemFromIndex(index);
            this._title = nextVideoObj["title"].ToString();
            this._thumbnail = nextVideoObj["thumbnails"].ToString();
            SetText(this._title);
            SetBackground(this._thumbnail);
            StringBuilder sb = new StringBuilder();
            var id = nextVideoObj["videoId"].ToString();
            sb.AppendLine("changeVideo('" + id + "'," + this._volume + ");");
            var task = browser.EvaluateScriptAsync(sb.ToString());
            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;

                    if (response.Success == true)
                    {
                        if (t2.IsAlive)
                            t2.Abort();
                        currentTime = 0;
                        t2 = new Thread(ts2);
                        t2.Start();
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void reVideo()
        {
            if (list.totalIndex <= 0)
            {
                StringBuilder sbf = new StringBuilder();
                sbf.AppendLine("destroyYoutube();");
                var task1 = browser.EvaluateScriptAsync(sbf.ToString());
                task1.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;

                        if (response.Success == true)
                        {
                            //MessageBox.Show(response.Result.ToString());
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            JObject nowVideoObj = this.list.GetNowList();
            this._title = nowVideoObj["title"].ToString();
            this._thumbnail = nowVideoObj["thumbnails"].ToString();
            string id = nowVideoObj["videoId"].ToString();
            SetBackground(this._thumbnail);
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendLine("changeVideo('" + id + "'," + this._volume + ");");
            var task2 = browser.EvaluateScriptAsync(sb2.ToString());
            currentTime = 0;
            task2.ContinueWith(tf =>
            {
                if (!tf.IsFaulted)
                {
                    var response2 = tf.Result;
                    if (response2.Success == true)
                    {
                        if (t2.IsAlive)
                            t2.Abort();
                        currentTime = 0;
                        t2 = new Thread(ts2);
                        t2.Start();
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void preVideo(myPrevDelegate callback)
        {
            if (list.totalIndex <= 0)
            {
                StringBuilder sbf = new StringBuilder();
                sbf.AppendLine("destroyYoutube();");
                var task1 = browser.EvaluateScriptAsync(sbf.ToString());
                task1.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;

                        if (response.Success == true)
                        {
                            //MessageBox.Show(response.Result.ToString());
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            

            var id = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("getCurrentTime();");
            var task = browser.EvaluateScriptAsync(sb.ToString());
            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;

                    if (response.Success == true)
                    {
                        double time = double.Parse(response.Result.ToString());
                        if (time < 2.0)
                        {
                            JObject preVideoObj = this.list.GetPreList();
                            this._title = preVideoObj["title"].ToString();
                            this._thumbnail = preVideoObj["thumbnails"].ToString();
                            id = preVideoObj["videoId"].ToString();
                            SetBackground(this._thumbnail);
                            callback();
                        }
                        else
                        {
                            JObject nowVideoObj = this.list.GetNowList();
                            this._title = nowVideoObj["title"].ToString();
                            this._thumbnail = nowVideoObj["thumbnails"].ToString();
                            id = nowVideoObj["videoId"].ToString();
                            callback();
                        }
                        StringBuilder sb2 = new StringBuilder();
                        sb2.AppendLine("changeVideo('" + id + "'," + this._volume + ");");
                        var task2 = browser.EvaluateScriptAsync(sb2.ToString());
                        task.ContinueWith(tf =>
                        {
                            if (!tf.IsFaulted)
                            {
                                var response2 = tf.Result;
                                if (response.Success == true)
                                {
                                    if (t2.IsAlive)
                                        t2.Abort();
                                    currentTime = 0;
                                    t2 = new Thread(ts2);
                                    t2.Start();
                                }
                            }
                        }, TaskScheduler.FromCurrentSynchronizationContext()); ;
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public List<JObject> getAllVideo()
        {
            return this.list.getPlayList();
        }

        public void AddVideo(JObject next)
        {
            if (!t.IsAlive)
                t.Start();

            if (this.list.totalIndex == 0)
            {
                this._title = next["title"].ToString();
                this._thumbnail = next["thumbnails"].ToString();
                this._volume = 50;

                SetText(this._title);
                SetBackground(this._thumbnail);
                StringBuilder sb = new StringBuilder();
                var id = next["videoId"].ToString();
                sb.AppendLine("createYoutube('" + id + "'," + this._volume+");");
                if (t2.IsAlive)
                    t2.Abort();
                currentTime = 0;
                t2 = new Thread(ts2);
                t2.Start();
                var task = browser.EvaluateScriptAsync(sb.ToString());
                task.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;

                        if (response.Success == true)
                        {
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            this.list.AddList(next);
        }

        public void AddVideo(params JObject[] obj)
        {
            this.list.AddList(obj);
        }

        public void RemoveVideo(params int[] _index)
        {
            list.RemoveList(_index);
        }

    }
}
