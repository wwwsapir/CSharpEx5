using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame
{
    class FormGameSettings : Form
    {
        private RadioButton RadioButton6x6;
        private RadioButton RadioButton8x8;
        private RadioButton RadioButton10x10;
        private Label label2;
        private Label label3;
        private CheckBox CheckBoxIsPlayer2Human;
        private TextBox TextBoxPlayer1Name;
        private TextBox TextBoxPlayer2Name;
        private Button ButtonDone;
        private Label label1;

        public FormGameSettings()
        {
            InitializeComponent();
            CheckBoxIsPlayer2Human.CheckedChanged += enablePlayer2NameChange;
            this.ButtonDone.Click += new EventHandler(ButtonDone_Click);
        }

        private void ButtonDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void enablePlayer2NameChange(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                TextBoxPlayer2Name.Enabled = true; 
            }
            else
            {
                TextBoxPlayer2Name.Enabled = false;
                TextBoxPlayer2Name.Text = "[Computer]";
            }
        }

        public string Player1Name
        {
            get { return TextBoxPlayer1Name.Text; }
        }

        public string Player2Name
        {
            get { return TextBoxPlayer2Name.Text; }
        }

        public bool IsPlayer2Human
        {
            get { return CheckBoxIsPlayer2Human.Checked; }
        }

        public byte BoardSize
        {
            get
            {
                if (RadioButton6x6.Checked)
                {
                    return 6;
                }
                else if (RadioButton8x8.Checked)
                {
                    return 8;
                }
                else
                {
                    // RadioButton10x10.Checked == true
                    return 10;
                }
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.RadioButton6x6 = new System.Windows.Forms.RadioButton();
            this.RadioButton8x8 = new System.Windows.Forms.RadioButton();
            this.RadioButton10x10 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CheckBoxIsPlayer2Human = new System.Windows.Forms.CheckBox();
            this.TextBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.TextBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.ButtonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // RadioButton6x6
            // 
            this.RadioButton6x6.AutoSize = true;
            this.RadioButton6x6.Checked = true;
            this.RadioButton6x6.Location = new System.Drawing.Point(29, 30);
            this.RadioButton6x6.Name = "RadioButton6x6";
            this.RadioButton6x6.Size = new System.Drawing.Size(48, 17);
            this.RadioButton6x6.TabIndex = 10;
            this.RadioButton6x6.TabStop = true;
            this.RadioButton6x6.Text = "6 x 6";
            this.RadioButton6x6.UseVisualStyleBackColor = true;
            // 
            // RadioButton8x8
            // 
            this.RadioButton8x8.AutoSize = true;
            this.RadioButton8x8.Location = new System.Drawing.Point(83, 30);
            this.RadioButton8x8.Name = "RadioButton8x8";
            this.RadioButton8x8.Size = new System.Drawing.Size(48, 17);
            this.RadioButton8x8.TabIndex = 20;
            this.RadioButton8x8.Text = "8 x 8";
            this.RadioButton8x8.UseVisualStyleBackColor = true;
            // 
            // RadioButton10x10
            // 
            this.RadioButton10x10.AutoSize = true;
            this.RadioButton10x10.Location = new System.Drawing.Point(137, 30);
            this.RadioButton10x10.Name = "RadioButton10x10";
            this.RadioButton10x10.Size = new System.Drawing.Size(60, 17);
            this.RadioButton10x10.TabIndex = 30;
            this.RadioButton10x10.Text = "10 x 10";
            this.RadioButton10x10.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1:";
            // 
            // CheckBoxIsPlayer2Human
            // 
            this.CheckBoxIsPlayer2Human.AutoSize = true;
            this.CheckBoxIsPlayer2Human.Location = new System.Drawing.Point(37, 102);
            this.CheckBoxIsPlayer2Human.Name = "CheckBoxIsPlayer2Human";
            this.CheckBoxIsPlayer2Human.Size = new System.Drawing.Size(67, 17);
            this.CheckBoxIsPlayer2Human.TabIndex = 50;
            this.CheckBoxIsPlayer2Human.Text = "Player 2:";
            this.CheckBoxIsPlayer2Human.UseVisualStyleBackColor = true;
            // 
            // TextBoxPlayer1Name
            // 
            this.TextBoxPlayer1Name.Location = new System.Drawing.Point(107, 73);
            this.TextBoxPlayer1Name.Name = "TextBoxPlayer1Name";
            this.TextBoxPlayer1Name.Size = new System.Drawing.Size(100, 20);
            this.TextBoxPlayer1Name.TabIndex = 40;
            // 
            // TextBoxPlayer2Name
            // 
            this.TextBoxPlayer2Name.Enabled = false;
            this.TextBoxPlayer2Name.Location = new System.Drawing.Point(107, 99);
            this.TextBoxPlayer2Name.Name = "TextBoxPlayer2Name";
            this.TextBoxPlayer2Name.Size = new System.Drawing.Size(100, 20);
            this.TextBoxPlayer2Name.TabIndex = 60;
            this.TextBoxPlayer2Name.Text = "[Computer]";
            // 
            // ButtonDone
            // 
            this.ButtonDone.Location = new System.Drawing.Point(137, 134);
            this.ButtonDone.Name = "ButtonDone";
            this.ButtonDone.Size = new System.Drawing.Size(75, 23);
            this.ButtonDone.TabIndex = 9;
            this.ButtonDone.Text = "Done";
            this.ButtonDone.UseVisualStyleBackColor = true;
            // 
            // FormGameSettings
            // 
            this.AcceptButton = this.ButtonDone;
            this.AccessibleDescription = "";
            this.ClientSize = new System.Drawing.Size(231, 172);
            this.Controls.Add(this.ButtonDone);
            this.Controls.Add(this.TextBoxPlayer2Name);
            this.Controls.Add(this.TextBoxPlayer1Name);
            this.Controls.Add(this.CheckBoxIsPlayer2Human);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RadioButton10x10);
            this.Controls.Add(this.RadioButton8x8);
            this.Controls.Add(this.RadioButton6x6);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.FormGameSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FormGameSettings_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
