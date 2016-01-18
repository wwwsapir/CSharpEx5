using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame
{
    internal class FormGameSettings : Form
    {
        private const string k_ComputerDescription = "[Computer]";
        private const string k_ComputerName = "Computer";
        private RadioButton radioButton6x6;
        private RadioButton radioButton8x8;
        private RadioButton radioButton10x10;
        private Label label2;
        private Label label3;
        private CheckBox checkBoxIsPlayer2Human;
        private TextBox textBoxPlayer1Name;
        private TextBox textBoxPlayer2Name;
        private Button buttonDone;
        private Label label1;

        public FormGameSettings()
        {
            InitializeComponent();
            checkBoxIsPlayer2Human.CheckedChanged += enablePlayer2NameChange;
            this.buttonDone.Click += this.buttonDone_Click;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void enablePlayer2NameChange(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                textBoxPlayer2Name.Enabled = true;
                textBoxPlayer2Name.Text = string.Empty;

            }
            else
            {
                textBoxPlayer2Name.Enabled = false;
                textBoxPlayer2Name.Text = k_ComputerDescription;
            }
        }

        public string Player1Name
        {
            get { return textBoxPlayer1Name.Text.Trim(); }
        }

        public string Player2Name
        {
            get
            {
                string returnedName;

                if (IsPlayer2Human)
                {
                    returnedName = textBoxPlayer2Name.Text;
                }
                else
                {
                    returnedName = k_ComputerName;
                }

                return returnedName.Trim();
            }
        }

        public bool IsPlayer2Human
        {
            get { return checkBoxIsPlayer2Human.Checked; }
        }

        public CheckersGameBoard.eBoardSize BoardSize
        {
            get
            {
                CheckersGameBoard.eBoardSize returnedValue;
                if (radioButton6x6.Checked)
                {
                    returnedValue = CheckersGameBoard.eBoardSize.Small;
                }
                else if (radioButton8x8.Checked)
                {
                    returnedValue = CheckersGameBoard.eBoardSize.Medium;
                }
                else
                {
                    returnedValue = CheckersGameBoard.eBoardSize.Large;
                }

                return returnedValue;
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton6x6 = new System.Windows.Forms.RadioButton();
            this.radioButton8x8 = new System.Windows.Forms.RadioButton();
            this.radioButton10x10 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxIsPlayer2Human = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
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
            // 
            // radioButton6x6
            // 
            this.radioButton6x6.AutoSize = true;
            this.radioButton6x6.Checked = true;
            this.radioButton6x6.Location = new System.Drawing.Point(29, 30);
            this.radioButton6x6.Name = "radioButton6x6";
            this.radioButton6x6.Size = new System.Drawing.Size(48, 17);
            this.radioButton6x6.TabIndex = 10;
            this.radioButton6x6.TabStop = true;
            this.radioButton6x6.Text = "6 x 6";
            this.radioButton6x6.UseVisualStyleBackColor = true;
            // 
            // radioButton8x8
            // 
            this.radioButton8x8.AutoSize = true;
            this.radioButton8x8.Location = new System.Drawing.Point(83, 30);
            this.radioButton8x8.Name = "radioButton8x8";
            this.radioButton8x8.Size = new System.Drawing.Size(48, 17);
            this.radioButton8x8.TabIndex = 20;
            this.radioButton8x8.Text = "8 x 8";
            this.radioButton8x8.UseVisualStyleBackColor = true;
            // 
            // radioButton10x10
            // 
            this.radioButton10x10.AutoSize = true;
            this.radioButton10x10.Location = new System.Drawing.Point(137, 30);
            this.radioButton10x10.Name = "radioButton10x10";
            this.radioButton10x10.Size = new System.Drawing.Size(60, 17);
            this.radioButton10x10.TabIndex = 30;
            this.radioButton10x10.Text = "10 x 10";
            this.radioButton10x10.UseVisualStyleBackColor = true;
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
            this.checkBoxIsPlayer2Human.AutoSize = true;
            this.checkBoxIsPlayer2Human.Location = new System.Drawing.Point(37, 102);
            this.checkBoxIsPlayer2Human.Name = "CheckBoxIsPlayer2Human";
            this.checkBoxIsPlayer2Human.Size = new System.Drawing.Size(67, 17);
            this.checkBoxIsPlayer2Human.TabIndex = 50;
            this.checkBoxIsPlayer2Human.Text = "Player 2:";
            this.checkBoxIsPlayer2Human.UseVisualStyleBackColor = true;
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(107, 73);
            this.textBoxPlayer1Name.MaxLength = 9;
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(100, 20);
            this.textBoxPlayer1Name.TabIndex = 40;
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(107, 99);
            this.textBoxPlayer2Name.MaxLength = 9;
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(100, 20);
            this.textBoxPlayer2Name.TabIndex = 60;
            this.textBoxPlayer2Name.Text = "[Computer]";
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(137, 134);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            // 
            // FormGameSettings
            // 
            this.AcceptButton = this.buttonDone;
            this.AccessibleDescription = "";
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(231, 172);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.checkBoxIsPlayer2Human);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton10x10);
            this.Controls.Add(this.radioButton8x8);
            this.Controls.Add(this.radioButton6x6);
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
    }
}
