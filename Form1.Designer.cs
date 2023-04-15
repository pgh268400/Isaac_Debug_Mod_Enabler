namespace IsaacDebugConsoleEnabler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_restart_game = new System.Windows.Forms.Button();
            this.btn_run_game = new System.Windows.Forms.Button();
            this.label_isaac_path = new System.Windows.Forms.Label();
            this.textbox_game_path = new System.Windows.Forms.TextBox();
            this.btn_change_game_dir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkbox_console = new System.Windows.Forms.CheckBox();
            this.checkbox_mod = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_restart_game
            // 
            this.btn_restart_game.Location = new System.Drawing.Point(21, 143);
            this.btn_restart_game.Name = "btn_restart_game";
            this.btn_restart_game.Size = new System.Drawing.Size(313, 40);
            this.btn_restart_game.TabIndex = 2;
            this.btn_restart_game.Text = "게임 재시작";
            this.btn_restart_game.UseVisualStyleBackColor = true;
            this.btn_restart_game.Click += new System.EventHandler(this.restart_game);
            // 
            // btn_run_game
            // 
            this.btn_run_game.Location = new System.Drawing.Point(21, 97);
            this.btn_run_game.Name = "btn_run_game";
            this.btn_run_game.Size = new System.Drawing.Size(313, 40);
            this.btn_run_game.TabIndex = 3;
            this.btn_run_game.Text = "게임 구동";
            this.btn_run_game.UseVisualStyleBackColor = true;
            this.btn_run_game.Click += new System.EventHandler(this.run_game);
            // 
            // label_isaac_path
            // 
            this.label_isaac_path.AutoSize = true;
            this.label_isaac_path.Location = new System.Drawing.Point(25, 205);
            this.label_isaac_path.Name = "label_isaac_path";
            this.label_isaac_path.Size = new System.Drawing.Size(71, 15);
            this.label_isaac_path.TabIndex = 4;
            this.label_isaac_path.Text = "아이작 경로";
            // 
            // textbox_game_path
            // 
            this.textbox_game_path.Location = new System.Drawing.Point(102, 202);
            this.textbox_game_path.Name = "textbox_game_path";
            this.textbox_game_path.ReadOnly = true;
            this.textbox_game_path.Size = new System.Drawing.Size(191, 23);
            this.textbox_game_path.TabIndex = 5;
            // 
            // btn_change_game_dir
            // 
            this.btn_change_game_dir.Location = new System.Drawing.Point(299, 201);
            this.btn_change_game_dir.Name = "btn_change_game_dir";
            this.btn_change_game_dir.Size = new System.Drawing.Size(35, 25);
            this.btn_change_game_dir.TabIndex = 6;
            this.btn_change_game_dir.Text = "...";
            this.btn_change_game_dir.UseVisualStyleBackColor = true;
            this.btn_change_game_dir.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "By File (pgh268400@naver.com)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkbox_console);
            this.groupBox1.Controls.Add(this.checkbox_mod);
            this.groupBox1.Location = new System.Drawing.Point(21, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 72);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options [체크시 활성화]";
            // 
            // checkbox_console
            // 
            this.checkbox_console.AutoSize = true;
            this.checkbox_console.Location = new System.Drawing.Point(16, 46);
            this.checkbox_console.Name = "checkbox_console";
            this.checkbox_console.Size = new System.Drawing.Size(50, 19);
            this.checkbox_console.TabIndex = 1;
            this.checkbox_console.Text = "콘솔";
            this.checkbox_console.UseVisualStyleBackColor = true;
            this.checkbox_console.Click += new System.EventHandler(this.checkbox_console_Click);
            // 
            // checkbox_mod
            // 
            this.checkbox_mod.AutoSize = true;
            this.checkbox_mod.Location = new System.Drawing.Point(16, 22);
            this.checkbox_mod.Name = "checkbox_mod";
            this.checkbox_mod.Size = new System.Drawing.Size(50, 19);
            this.checkbox_mod.TabIndex = 0;
            this.checkbox_mod.Text = "모드";
            this.checkbox_mod.UseVisualStyleBackColor = true;
            this.checkbox_mod.Click += new System.EventHandler(this.checkbox_mod_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "경로 자동 감지";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.detect_game_path);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 274);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_change_game_dir);
            this.Controls.Add(this.textbox_game_path);
            this.Controls.Add(this.label_isaac_path);
            this.Controls.Add(this.btn_run_game);
            this.Controls.Add(this.btn_restart_game);
            this.Name = "Form1";
            this.Text = "Isaac Debug Console Enabler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btn_restart_game;
        private Button btn_run_game;
        private Label label_isaac_path;
        private TextBox textbox_game_path;
        private Button btn_change_game_dir;
        private Label label2;
        private GroupBox groupBox1;
        private CheckBox checkbox_console;
        private CheckBox checkbox_mod;
        private Button button1;
    }
}