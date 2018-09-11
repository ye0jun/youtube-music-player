namespace Youtube_Master
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Youtube = new System.Windows.Forms.Panel();
            this.panel_leftBackground = new System.Windows.Forms.Panel();
            this.searchBar = new ZBobb.AlphaBlendTextBox();
            this.disappear_label = new System.Windows.Forms.Label();
            this.panel_right = new System.Windows.Forms.Panel();
            this.panel_rightbottom = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pb_listen = new System.Windows.Forms.PictureBox();
            this.pb_deselectAll = new System.Windows.Forms.PictureBox();
            this.pb_selectAll = new System.Windows.Forms.PictureBox();
            this.pb_myList = new System.Windows.Forms.PictureBox();
            this.line_2 = new System.Windows.Forms.PictureBox();
            this.disappear_icon = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.line_1 = new System.Windows.Forms.PictureBox();
            this.underline_my = new System.Windows.Forms.PictureBox();
            this.panel_rightbottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_listen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_deselectAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_selectAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_myList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.line_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.disappear_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.line_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.underline_my)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Youtube
            // 
            this.panel_Youtube.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Youtube.Location = new System.Drawing.Point(20, 52);
            this.panel_Youtube.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Youtube.Name = "panel_Youtube";
            this.panel_Youtube.Size = new System.Drawing.Size(280, 280);
            this.panel_Youtube.TabIndex = 0;
            // 
            // panel_leftBackground
            // 
            this.panel_leftBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_leftBackground.Location = new System.Drawing.Point(0, 0);
            this.panel_leftBackground.Margin = new System.Windows.Forms.Padding(0);
            this.panel_leftBackground.Name = "panel_leftBackground";
            this.panel_leftBackground.Size = new System.Drawing.Size(320, 554);
            this.panel_leftBackground.TabIndex = 5;
            // 
            // searchBar
            // 
            this.searchBar.BackAlpha = 10;
            this.searchBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.searchBar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchBar.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchBar.Location = new System.Drawing.Point(339, 18);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(345, 14);
            this.searchBar.TabIndex = 8;
            // 
            // disappear_label
            // 
            this.disappear_label.AutoSize = true;
            this.disappear_label.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.disappear_label.Location = new System.Drawing.Point(509, 20);
            this.disappear_label.Name = "disappear_label";
            this.disappear_label.Size = new System.Drawing.Size(29, 14);
            this.disappear_label.TabIndex = 10;
            this.disappear_label.Text = "검색";
            // 
            // panel_right
            // 
            this.panel_right.AutoScroll = true;
            this.panel_right.Location = new System.Drawing.Point(320, 60);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(480, 451);
            this.panel_right.TabIndex = 11;
            // 
            // panel_rightbottom
            // 
            this.panel_rightbottom.Controls.Add(this.pictureBox2);
            this.panel_rightbottom.Controls.Add(this.pb_listen);
            this.panel_rightbottom.Controls.Add(this.pb_deselectAll);
            this.panel_rightbottom.Controls.Add(this.pb_selectAll);
            this.panel_rightbottom.Location = new System.Drawing.Point(320, 512);
            this.panel_rightbottom.Name = "panel_rightbottom";
            this.panel_rightbottom.Size = new System.Drawing.Size(460, 42);
            this.panel_rightbottom.TabIndex = 13;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Youtube_Master.Properties.Resources.btnDelOver_Normal;
            this.pictureBox2.Location = new System.Drawing.Point(410, 9);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 24);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pb_listen
            // 
            this.pb_listen.BackgroundImage = global::Youtube_Master.Properties.Resources.btnListenOver_Normal;
            this.pb_listen.Location = new System.Drawing.Point(150, 9);
            this.pb_listen.Margin = new System.Windows.Forms.Padding(0);
            this.pb_listen.Name = "pb_listen";
            this.pb_listen.Size = new System.Drawing.Size(40, 24);
            this.pb_listen.TabIndex = 2;
            this.pb_listen.TabStop = false;
            this.pb_listen.Click += new System.EventHandler(this.pb_listen_Click);
            // 
            // pb_deselectAll
            // 
            this.pb_deselectAll.BackgroundImage = global::Youtube_Master.Properties.Resources.btnDeselectDefault_Normal;
            this.pb_deselectAll.Location = new System.Drawing.Point(81, 9);
            this.pb_deselectAll.Margin = new System.Windows.Forms.Padding(0);
            this.pb_deselectAll.Name = "pb_deselectAll";
            this.pb_deselectAll.Size = new System.Drawing.Size(59, 24);
            this.pb_deselectAll.TabIndex = 1;
            this.pb_deselectAll.TabStop = false;
            this.pb_deselectAll.Click += new System.EventHandler(this.pb_deselectAll_Click);
            // 
            // pb_selectAll
            // 
            this.pb_selectAll.BackgroundImage = global::Youtube_Master.Properties.Resources.btnAllSelectDefault_Normal;
            this.pb_selectAll.Location = new System.Drawing.Point(12, 9);
            this.pb_selectAll.Margin = new System.Windows.Forms.Padding(0);
            this.pb_selectAll.Name = "pb_selectAll";
            this.pb_selectAll.Size = new System.Drawing.Size(59, 24);
            this.pb_selectAll.TabIndex = 0;
            this.pb_selectAll.TabStop = false;
            this.pb_selectAll.Click += new System.EventHandler(this.pb_selectAll_Click);
            // 
            // pb_myList
            // 
            this.pb_myList.BackgroundImage = global::Youtube_Master.Properties.Resources.menuMymusicDefault_Normal;
            this.pb_myList.Location = new System.Drawing.Point(723, 13);
            this.pb_myList.Margin = new System.Windows.Forms.Padding(0);
            this.pb_myList.Name = "pb_myList";
            this.pb_myList.Size = new System.Drawing.Size(32, 32);
            this.pb_myList.TabIndex = 14;
            this.pb_myList.TabStop = false;
            this.pb_myList.Click += new System.EventHandler(this.pb_myList_Click);
            // 
            // line_2
            // 
            this.line_2.Location = new System.Drawing.Point(320, 511);
            this.line_2.Name = "line_2";
            this.line_2.Size = new System.Drawing.Size(460, 1);
            this.line_2.TabIndex = 12;
            this.line_2.TabStop = false;
            // 
            // disappear_icon
            // 
            this.disappear_icon.Image = global::Youtube_Master.Properties.Resources.iconSearchWhite_Normal;
            this.disappear_icon.Location = new System.Drawing.Point(489, 20);
            this.disappear_icon.Name = "disappear_icon";
            this.disappear_icon.Size = new System.Drawing.Size(14, 13);
            this.disappear_icon.TabIndex = 9;
            this.disappear_icon.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Youtube_Master.Properties.Resources.searchbar;
            this.pictureBox1.Location = new System.Drawing.Point(320, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(382, 50);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // line_1
            // 
            this.line_1.BackColor = System.Drawing.Color.White;
            this.line_1.Location = new System.Drawing.Point(320, 52);
            this.line_1.Margin = new System.Windows.Forms.Padding(0);
            this.line_1.Name = "line_1";
            this.line_1.Size = new System.Drawing.Size(461, 1);
            this.line_1.TabIndex = 6;
            this.line_1.TabStop = false;
            // 
            // underline_my
            // 
            this.underline_my.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.underline_my.Location = new System.Drawing.Point(729, 49);
            this.underline_my.Name = "underline_my";
            this.underline_my.Size = new System.Drawing.Size(20, 3);
            this.underline_my.TabIndex = 15;
            this.underline_my.TabStop = false;
            this.underline_my.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(780, 554);
            this.Controls.Add(this.underline_my);
            this.Controls.Add(this.pb_myList);
            this.Controls.Add(this.panel_rightbottom);
            this.Controls.Add(this.line_2);
            this.Controls.Add(this.panel_right);
            this.Controls.Add(this.disappear_label);
            this.Controls.Add(this.disappear_icon);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.line_1);
            this.Controls.Add(this.panel_Youtube);
            this.Controls.Add(this.panel_leftBackground);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel_rightbottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_listen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_deselectAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_selectAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_myList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.line_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.disappear_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.line_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.underline_my)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Youtube;
        private System.Windows.Forms.Panel panel_leftBackground;
        private System.Windows.Forms.PictureBox line_1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ZBobb.AlphaBlendTextBox searchBar;
        private System.Windows.Forms.PictureBox disappear_icon;
        private System.Windows.Forms.Label disappear_label;
        private System.Windows.Forms.Panel panel_right;
        private System.Windows.Forms.PictureBox line_2;
        private System.Windows.Forms.Panel panel_rightbottom;
        private System.Windows.Forms.PictureBox pb_selectAll;
        private System.Windows.Forms.PictureBox pb_deselectAll;
        private System.Windows.Forms.PictureBox pb_listen;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pb_myList;
        private System.Windows.Forms.PictureBox underline_my;
    }
}

