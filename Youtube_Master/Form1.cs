using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Filters;
using Kaliko.ImageLibrary.FastFilters;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Youtube_Master
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser m_chromeBrowser = null;
        MessageFromBrowser messageFromBrowser;
        Player player;
        Label label_Title;
        MusicList musicList;
        Ye0junSeekbar seekbar;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        public Form1()
        {
            InitializeComponent();
            m_chromeBrowser = new ChromiumWebBrowser("http://13.124.243.73:3000/");
            panel_Youtube.Controls.Add(m_chromeBrowser);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
            this.Controls.SetChildIndex(this.panel_leftBackground, 1);

            

            Panel transparent = new Panel();
            transparent.BackgroundImage = Properties.Resources.transparent30;
            transparent.Width = 320;
            transparent.Height = 554;
            transparent.BackColor = Color.Transparent;
            this.panel_leftBackground.Controls.Add(transparent);
            this.Controls.SetChildIndex(this.panel_Youtube, 0);

            var image = new KalikoImage(@"http://www.jangsada.com/files/attach/images/1572/500/002/db0e26c4043c42908ad321de21aeef76.jpg");
            image.ApplyFilter(new FastGaussianBlurFilter(30.0f));
            panel_leftBackground.BackgroundImage = image.GetAsBitmap();

            var pb_logo = CreatePicturBox(transparent, Properties.Resources.logo, new Size((int)(150* 0.65f), (int)(36* 0.65f)), new Point(160 - (int)(150 * 0.65f) / 2, 15));
            var pb_close = CreatePicturBox(transparent, Properties.Resources.btnCloseDefault_Normal, new Size(22, 22), new Point(19, 15));
            var pb_hide = CreatePicturBox(transparent, Properties.Resources.btnHideDefault_Normal, new Size(22, 22), new Point(42, 15));
            var pb_play = CreatePicturBox(transparent, Properties.Resources.btnPauseDefault_Normal, new Size(44, 44), new Point(138, 430));
            var pb_prev = CreatePicturBox(transparent, Properties.Resources.btnPrevDefault_Normal, new Size(32, 32), new Point(90, 435));
            var pb_next = CreatePicturBox(transparent, Properties.Resources.btnNextDefault_Normal, new Size(32, 32), new Point(198, 435));
            var pb_shuffle = CreatePicturBox(transparent, Properties.Resources.btnShuffleDefault_Normal, new Size(32, 32), new Point(48, 435));
            var pb_repeat = CreatePicturBox(transparent, Properties.Resources.btnRepeatDefault_Normal, new Size(32, 32), new Point(239, 435));
            var pb_volume = CreatePicturBox(transparent, Properties.Resources.btnVolumeDefault_Normal, new Size(32, 32), new Point(144, 487));

            label_Title = new Label();
            label_Title.Text = "재생을 기다리고 있습니다";
            label_Title.Font = new Font("나눔바른고딕", 12);
            label_Title.ForeColor = Color.FromArgb(255, 255, 255);
            label_Title.Width = 320;
            label_Title.Location = new Point(0, 385);

            label_Title.TextAlign = ContentAlignment.MiddleCenter;
            transparent.Controls.Add(label_Title);

            Label leftTime = new Label();
            leftTime.Text = "00:00";
            leftTime.Font = new Font("나눔바른고딕", 9);
            leftTime.ForeColor = Color.FromArgb(255, 255, 255);
            leftTime.Width = 50;
            leftTime.Location = new Point(16, 355);
            transparent.Controls.Add(leftTime);

            Label rightTime = new Label();
            rightTime.Text = "00:00";
            rightTime.Font = new Font("나눔바른고딕", 9);
            rightTime.ForeColor = Color.FromArgb(255, 255, 255);
            rightTime.Width = 50;
            rightTime.Location = new Point(265, 355);
            transparent.Controls.Add(rightTime);

            seekbar = new Ye0junSeekbar(transparent, 280, 300);
            seekbar.SetPosition(new Point(16, 340));
            seekbar.SetValue(0);


            ////////////////////////////////////////////////////////////////

            pb_play.MouseClick += new MouseEventHandler((o, e) => {
                player.Toggle((b) =>
                {
                    if (b)
                        pb_play.Image = Properties.Resources.btnPauseDefault_Normal;
                    else
                        pb_play.Image = Properties.Resources.btnPlayDefault_Normal;
                });
            });

            pb_prev.MouseClick += new MouseEventHandler((o, e) => {
           
                player.preVideo(()=>
                {
                    label_Title.Text = player._title;
                }); 
            });

            pb_next.MouseClick += new MouseEventHandler((o, e) => {
                player.nextVideo();
                label_Title.Text = player._title;
            });

            pb_shuffle.MouseClick += new MouseEventHandler((o, e) => {
                player.shuffle = !player.shuffle;
                if (player.shuffle)
                {
                    pb_shuffle.Image = Properties.Resources.btnShuffleOnDefault_Normal;
                }
                else
                {
                    pb_shuffle.Image = Properties.Resources.btnShuffleDefault_Normal;
                }
            });

            pb_repeat.MouseClick += new MouseEventHandler((o, e) => {
                if (player.GetRepeatState() == 2)
                    player.SetRepeatState(0);
                else
                    player.SetRepeatState(player.GetRepeatState() + 1);

                switch (player.GetRepeatState())
                {
                    case 0:
                        pb_repeat.Image = Properties.Resources.btnRepeatDefault_Normal;
                        break;
                    case 1:
                        pb_repeat.Image = Properties.Resources.btnRepeatOnDefault_Normal;
                        break;
                    case 2:
                        pb_repeat.Image = Properties.Resources.btn1RepeatOnDefault_Normal;
                        break;
                }
            });

            Youtube youtube = new Youtube();
            searchBar.KeyDown += new KeyEventHandler((o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pb_myList.Image = Properties.Resources.menuMymusicDefault_Normal;
                    underline_my.Visible = false;
                    UTF8Encoding utf8 = new UTF8Encoding();
                    byte[] encodedBytes = utf8.GetBytes(searchBar.Text);
                    string utf8String = "";
                    foreach (byte b in encodedBytes)
                    {
                        utf8String += "%" + String.Format("{0:X}", b);
                    }
                    youtube.Search(utf8String, (arr) =>
                    {
                        musicList.SetSearchList(arr);
                    });
                }
            });

            ////////////////////////////////////////////////////////////////
            line_1.BackColor = Color.FromArgb(230, 230, 230);
            line_2.BackColor = Color.FromArgb(230, 230, 230);
            panel_rightbottom.BackColor = Color.FromArgb(243);
            underline_my.BackColor = Color.FromArgb(252, 63, 59);

            player = new Player(m_chromeBrowser,label_Title,panel_leftBackground,leftTime, seekbar);
            musicList = new MusicList(panel_right, player);


            messageFromBrowser = new MessageFromBrowser(()=>
            {
                Console.WriteLine(player.GetRepeatState() + "   ");
                if (player.GetRepeatState() != 2)
                    player.nextVideo();
                else
                    player.reVideo();
                //label_Title.Text = "ffff";
            },m_chromeBrowser,seekbar,rightTime);


            m_chromeBrowser.RegisterAsyncJsObject("toCSharp", messageFromBrowser);

            pb_close.MouseClick += new MouseEventHandler(FClose);
            pb_hide.MouseClick += new MouseEventHandler(FMinimized);

            var bMouseClick = false;
            var myX = 0;
            var myY = 0;
            transparent.MouseDown += new MouseEventHandler((o, e) =>
            {
                bMouseClick = true;
                myX = e.X;
                myY = e.Y;
            });

            transparent.MouseUp += new MouseEventHandler((o, e) =>
            {
                bMouseClick = false;
            });

            transparent.MouseMove += new MouseEventHandler((o, e) =>
            {
                if (bMouseClick)
                {
                    const short SWP_NOMOVE = 0X2;
                    const short SWP_NOSIZE = 1;
                    const short SWP_NOZORDER = 0X4;
                    const int SWP_SHOWWINDOW = 0x0040;
                    Process process = Process.GetCurrentProcess();
                    IntPtr handle = process.MainWindowHandle;

                    var finalX = MousePosition.X - myX;
                    var finalY = MousePosition.Y - myY;
                    if (handle != IntPtr.Zero)
                    {
                        SetWindowPos(handle, 0, finalX, finalY, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
                    }
                }
            });

            searchBar.GotFocus += new EventHandler((o, e) =>
            {
                disappear_icon.Visible = false;
                disappear_label.Visible = false;
            });

            searchBar.LostFocus += new EventHandler((o, e) =>
            {
                if(searchBar.Text == "")
                {
                    disappear_icon.Visible = true;
                    disappear_label.Visible = true;
                }
            });   
        }

        void FClose(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        void FMinimized(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        delegate void goNext();
        delegate void strdele(string a);
        class MessageFromBrowser
        {
            goNext go;
            ChromiumWebBrowser browser;
            Ye0junSeekbar seekbar;
            Label label;

            public MessageFromBrowser()
            {
            }

            public MessageFromBrowser(goNext go, ChromiumWebBrowser browser, Ye0junSeekbar seekbar,Label label)
            {
                this.go = go;
                this.browser = browser;
                this.seekbar = seekbar;
                this.label = label;
            }

            public void sendMessage(string msg)
            {
                int state = int.Parse(msg);
                switch (state)
                {
                    case 0:
                        this.go();
                        break;
                    case 1:
                        getMaxTime();
                        break;
                }
            }

            void SetText(string str)
            {
                if (label.InvokeRequired)
                {
                    strdele a = new strdele(SetText);
                    label.Invoke(a, str);
                }
                else
                {
                    label.Text = str;
                }
            }

            private void getMaxTime()
            {
                try
                {
                    StringBuilder sbf = new StringBuilder();
                    sbf.AppendLine("getTotalTime();");
                    var task1 = browser.EvaluateScriptAsync(sbf.ToString());
                    task1.ContinueWith(t =>
                    {
                        if (!t.IsFaulted)
                        {
                            var response = t.Result;

                            if (response.Success == true)
                            {
                                int maxTime = (int)(Math.Ceiling(double.Parse(response.Result.ToString())));
                                seekbar.SetMaxValue(maxTime);
                                TimeSpan time = TimeSpan.FromSeconds(maxTime);
                                string str = time.ToString(@"mm\:ss");
                                SetText(str);
                            }
                        }
                    });
                }
                catch (Exception e) {  }

            }
        }

        class MusicList
        {
            private JArray searchList;
            private JArray currentList;
            private Panel rootPanel;
            private Dictionary<int, int> selectedList;
            private bool bIsMyPlaylist;
            private int currentSelectMusicIndex;
            private Player player;

            public MusicList(Panel rootPanel,Player player)
            {
                this.player = player;
                this.rootPanel = rootPanel;
                selectedList = new Dictionary<int, int>();
                bIsMyPlaylist = false;
                currentSelectMusicIndex = 0;
            }

            public void SetSearchList(JArray list)
            {
                this.searchList = list;
                this.currentList = this.searchList;
                bIsMyPlaylist = false;

                rootPanel.Visible = false;
                while (rootPanel.Controls.Count > 0)
                {
                    rootPanel.Controls[0].Dispose();
                }
                selectedList.Clear();
                rootPanel.Visible = true;

                for (var i=0; i<list.Count; i++)
                {
                    AddItem((JObject)searchList[i], i);
                }
            }

            public void SetMyList(JArray list)
            {
                this.currentList = list;
                bIsMyPlaylist = true;

                rootPanel.Visible = false;
                while (rootPanel.Controls.Count > 0)
                {
                    rootPanel.Controls[0].Dispose();
                }
                selectedList.Clear();
                rootPanel.Visible = true;

                for (var i = 0; i < list.Count; i++)
                {
                    AddItem((JObject)list[i], i);
                }
            }

            public void AddItem(JObject musicData, int index)
            {
                var parentPanel = new Panel();
                parentPanel.Location = new Point(0, 0 + index * 66);
                parentPanel.Size = new Size(460, 66);
                parentPanel.BackColor = Color.FromArgb(255, 255, 255);
                rootPanel.Controls.Add(parentPanel);

                if(index != 0)
                {
                    var listLine = new PictureBox();
                    listLine.BackColor = Color.FromArgb(230, 230, 230);
                    listLine.Location = new Point(30, 0);
                    listLine.Size = new Size(400, 1);
                    parentPanel.Controls.Add(listLine);
                }


                KalikoImage image;
                try
                {
                    image = new KalikoImage(musicData["thumbnails"].ToString());
                }
                catch (Exception e)
                {
                    var url = musicData["thumbnails"].ToString();
                    url = url.Replace("maxresdefault", "hqdefault");
                    image = new KalikoImage(url);
                }
                var Thumbnail = CreatePicturBox(parentPanel, image.GetAsBitmap(), new Size(45, 45), new Point(30, 11));

                var label_name = new Label();
                label_name.Text = musicData["title"].ToString();
                label_name.Font = new Font("나눔바른고딕", 12);
                label_name.ForeColor = Color.FromArgb(0, 0, 0);
                label_name.Width = 320;
                label_name.Location = new Point(80, 24);
                parentPanel.Controls.Add(label_name);

                var data = new Label();
                data.Location = new Point(999, 24);
                data.Text = index + "";
                parentPanel.Controls.Add(data);

                foreach (Control control in parentPanel.Controls)
                {
                    control.MouseClick += new MouseEventHandler(PanelOnClickEvent);
                    control.DoubleClick += new EventHandler(PanelOnDoubleClickEvent);

                }
                parentPanel.MouseClick += new MouseEventHandler(PanelOnClickEvent);

                void PanelOnClickEvent(object sender, MouseEventArgs e)
                {
                    Control c;
                    if (((Control)sender) is Panel) { c = (Control)sender; }
                    else{ c = ((Control)sender).Parent; }

                    var dataIndex = index == 0 ? 2 : 3;
                    var clickedIndex = int.Parse(c.Controls[dataIndex].Text);
                    ItemSelect(clickedIndex);
                }

                void PanelOnDoubleClickEvent(object sender, EventArgs e)
                {
                    Control c;
                    if (((Control)sender) is Panel) { c = (Control)sender; }
                    else { c = ((Control)sender).Parent; }

                    var dataIndex = index == 0 ? 2 : 3;
                    var clickedIndex = int.Parse(c.Controls[dataIndex].Text);

                    if (!bIsMyPlaylist)
                        player.AddVideo((JObject)currentList[clickedIndex]);
                    else
                        player.PlayVideoByIndex(clickedIndex);
                }
            }
            public void ItemSelect(int itemIndex)
            {
                Control c = rootPanel.Controls[itemIndex];

                if (!bIsMyPlaylist)
                {
                    if (selectedList.ContainsKey(itemIndex))
                    {
                        selectedList.Remove(itemIndex);
                        c.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    else
                    {
                        selectedList.Add(itemIndex, itemIndex);
                        c.BackColor = Color.FromArgb(243, 244, 247);
                    }
                }
                else
                {
                    if(currentSelectMusicIndex != itemIndex)
                    {
                        c = rootPanel.Controls[currentSelectMusicIndex];
                        c.BackColor = Color.FromArgb(255, 255, 255);
                        currentSelectMusicIndex = itemIndex;
                        c = rootPanel.Controls[currentSelectMusicIndex];
                        c.BackColor = Color.FromArgb(243, 244, 247);
                    }
                }
            }
            
            public int GetCurrentSelectedMusicIndex()
            {
                return currentSelectMusicIndex;
            }

            public bool GetIsMyPlayList()
            {
                return bIsMyPlaylist;
            }

            public void SelectAll()
            {
                for(var i=0; i<currentList.Count; i++)
                {
                    if (selectedList.ContainsKey(i))
                        continue;

                    ItemSelect(i);
                }
            }

            public void DeselectAll()
            {
                Dictionary<int, int> copy = new Dictionary<int, int>(selectedList);
                foreach(KeyValuePair<int,int> item in copy)
                {
                    ItemSelect(item.Value);
                }
            }

            public JArray GetSelectedItem()
            {
                JArray returnValue = new JArray();
                foreach (var item in selectedList)
                {
                    returnValue.Add(this.currentList[item.Key]);
                }

                return returnValue;
            }
        }

        private static PictureBox CreatePicturBox(Panel parent,Bitmap resource,Size size, Point point)
        {
            var image = new Bitmap(resource, size);
            var pb = new PictureBox();
            pb.Image = image;
            pb.BackColor = Color.Transparent;
            pb.Location = point;
            pb.Width = size.Width;
            pb.Height = size.Height;
            pb.BackColor = Color.Transparent;
            parent.Controls.Add(pb);
            parent.Controls.SetChildIndex(pb, 0);

            return pb;
        }

        private PictureBox CreatePicturBox(Panel parent, Bitmap resource, Size size, Point point, int zOrder)
        {
            var image = new Bitmap(resource, size);
            var pb = new PictureBox();
            pb.Image = image;
            pb.BackColor = Color.Transparent;
            pb.Location = point;
            pb.Width = size.Width;
            pb.Height = size.Height;
            pb.BackColor = Color.Transparent;
            parent.Controls.Add(pb);
            parent.Controls.SetChildIndex(pb, zOrder);

            return pb;
        }

        private PictureBox CreatePicturBox(Panel parent, Bitmap resource, Size size, Point point, float scale)
        {
            var scaledSize = new Size((int)(size.Width * scale), (int)(size.Height * scale));
            var image = new Bitmap(resource, scaledSize);
            var pb = new PictureBox();
            pb.Image = image;
            pb.BackColor = Color.Transparent;
            pb.Width = (int)(size.Width * scale);
            pb.Height = (int)(size.Height * scale);
            pb.Location = point;
            parent.Controls.Add(pb);

            return pb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JObject test = new JObject();
            test.Add("videoId", "B7bqAsxee4I");
            test.Add("title", "Adele - Hello");
            test.Add("thumbnails", "https://i.ytimg.com/vi/YQHsXMglC9A/hqdefault.jpg");
            player.AddVideo(test);

            JObject test2 = new JObject();
            test2.Add("videoId", "i0p1bmr0EmE");
            test2.Add("title", "TWICE - \"what is love\" ");
            test2.Add("thumbnails", "https://i.ytimg.com/vi/YQHsXMglC9A/hqdefault.jpg");
            player.AddVideo(test2);

            JObject test3 = new JObject();
            test3.Add("videoId", "V2hlQkVJZhE");
            test3.Add("title", "TWICE - \"likey\" ");
            test3.Add("thumbnails", "https://i.ytimg.com/vi/YQHsXMglC9A/hqdefault.jpg");
            player.AddVideo(test3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.nextVideo();
        }

        private void pb_deselectAll_Click(object sender, EventArgs e)
        {
            musicList.DeselectAll();
        }

        private void pb_selectAll_Click(object sender, EventArgs e)
        {
            musicList.SelectAll();
        }

        private void pb_listen_Click(object sender, EventArgs e)
        {
            if (!musicList.GetIsMyPlayList())
            {
                JArray datas = musicList.GetSelectedItem();
                for(var i=0; i<datas.Count; i++)
                {
                    player.AddVideo((JObject)datas[i]);
                }
                musicList.DeselectAll();
            }
            else
            {
                int index = musicList.GetCurrentSelectedMusicIndex();
                player.PlayVideoByIndex(index);
            }
        }

        private void pb_myList_Click(object sender, EventArgs e)
        {
            pb_myList.Image = Properties.Resources.menuMymusicPressed_Normal;
            underline_my.Visible = true;
            JArray arr = new JArray();
            foreach (var item in player.getAllVideo())
            {
                arr.Add(item);
            }
            musicList.SetMyList(arr);
        }
    }
}
