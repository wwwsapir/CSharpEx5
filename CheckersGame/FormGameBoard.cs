using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CheckersGame
{
    public delegate FormGameBoard.MoveInfo NotifyFatherAboutMove(Button i_SrcButton, Button i_DstButton);

    public class FormGameBoard : Form
    {
        const string k_EmptyString = "";
        private Button m_SourcePosition = null;
        public event NotifyFatherAboutMove HandleInputMove;

        private Button ButtonBa;
        private Button button3;
        private Button ButtonDa;
        private Button button5;
        private Button ButtonFa;
        private Button ButtonEb;
        private Button button8;
        private Button ButtonCb;
        private Button button10;
        private Button ButtonAb;
        private Button button12;
        private Button ButtonEd;
        private Button button14;
        private Button ButtonCd;
        private Button button16;
        private Button ButtonAd;
        private Button button18;
        private Button ButtonFc;
        private Button button20;
        private Button ButtonDc;
        private Button button22;
        private Button ButtonBc;
        private Button button24;
        private Button ButtonEf;
        private Button button26;
        private Button ButtonCf;
        private Button button28;
        private Button ButtonAf;
        private Button button30;
        private Button ButtonFe;
        private Button button32;
        private Button ButtonDe;
        private Button button34;
        private Button ButtonBe;
        private Button button36;
        private Label label1;
        private Label label2;
        private Label LabelPlayer1Score;
        private Label LabelPlayer2Score;
        private Button button1;

        public class MoveInfo
        {
            private bool m_MoveValid;
            private string m_CapturedCell;

            public bool MoveValid 
            {
                get { return m_MoveValid; }
                set { m_MoveValid = value; }
            }

            public string CapturedCell
            {
                get { return CapturedCell; }
                set { m_CapturedCell = value; }
            }
        }

        public FormGameBoard(byte i_BoardSize)
        {
            InitializeComponent();
            Button currButton;
            foreach (Control control in Controls)
            {
                currButton = control as Button;
                if (currButton != null && currButton.Enabled)
                {
                    currButton.Click += button_Clicked; // Listen to each white cell button
                }
            }
        }

        private void button_Clicked(object sender, EventArgs e)
        {
            if (m_SourcePosition == null)   // No source position determined yet
            {
                if ((sender as Button).Text != k_EmptyString)   // Set this button as source position
                {
                    m_SourcePosition = sender as Button;
                    m_SourcePosition.BackColor = Color.Aquamarine;
                    m_SourcePosition.Click -= button_Clicked;
                    m_SourcePosition.Click += button_ClickedAgain;
                }
            }
            else
            {
                // Source position already exists - check if this position is a valid destination
                MoveInfo moveInfo = OnInputMoveReceived(m_SourcePosition, sender as Button);
                if (!(moveInfo.MoveValid))
                {
                    MessageBox.Show("Invalid Move!");
                }
                else
                {
                    Button capturedButton = PositionStringToButton(moveInfo.CapturedCell);
                    if (capturedButton != null)
                    {
                        capturedButton.Text = k_EmptyString;
                    }
                    (sender as Button).Text = m_SourcePosition.Text;
                    m_SourcePosition.Text = k_EmptyString;
                    m_SourcePosition = null;
                }
            }
        }

        private Button PositionStringToButton(string i_PositionStr)
        {
            Button buttonToReturn = null;
            foreach (Control control in Controls)
            {
                if (control.Tag.ToString() == i_PositionStr)
                {
                    buttonToReturn = control as Button;
                }
            }

            return buttonToReturn;
        }

        private MoveInfo OnInputMoveReceived(Button i_srcButton, Button i_dstButton)
        {
            MoveInfo moveInfo = new MoveInfo();
            if (HandleInputMove != null)
            {
                moveInfo = HandleInputMove.Invoke(i_srcButton, i_dstButton);
            }

            return moveInfo;
        }

        private void button_ClickedAgain(object sender, EventArgs e)
        {
            m_SourcePosition.BackColor = Color.White;
            m_SourcePosition.Click -= button_ClickedAgain;
            m_SourcePosition.Click += button_Clicked;
            m_SourcePosition = null;
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.ButtonBa = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ButtonDa = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.ButtonFa = new System.Windows.Forms.Button();
            this.ButtonEb = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.ButtonCb = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.ButtonAb = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.ButtonEd = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.ButtonCd = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.ButtonAd = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.ButtonFc = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.ButtonDc = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.ButtonBc = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.ButtonEf = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.ButtonCf = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.ButtonAf = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.ButtonFe = new System.Windows.Forms.Button();
            this.button32 = new System.Windows.Forms.Button();
            this.ButtonDe = new System.Windows.Forms.Button();
            this.button34 = new System.Windows.Forms.Button();
            this.ButtonBe = new System.Windows.Forms.Button();
            this.button36 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LabelPlayer1Score = new System.Windows.Forms.Label();
            this.LabelPlayer2Score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Enabled = false;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(12, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // ButtonBa
            // 
            this.ButtonBa.BackColor = System.Drawing.Color.White;
            this.ButtonBa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonBa.Location = new System.Drawing.Point(12, 85);
            this.ButtonBa.Name = "ButtonBa";
            this.ButtonBa.Size = new System.Drawing.Size(35, 35);
            this.ButtonBa.TabIndex = 1;
            this.ButtonBa.Tag = "Ba";
            this.ButtonBa.Text = "O";
            this.ButtonBa.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gray;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(12, 119);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 35);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // ButtonDa
            // 
            this.ButtonDa.BackColor = System.Drawing.Color.White;
            this.ButtonDa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonDa.Location = new System.Drawing.Point(12, 153);
            this.ButtonDa.Name = "ButtonDa";
            this.ButtonDa.Size = new System.Drawing.Size(35, 35);
            this.ButtonDa.TabIndex = 3;
            this.ButtonDa.Tag = "Da";
            this.ButtonDa.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Gray;
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(12, 187);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(35, 35);
            this.button5.TabIndex = 4;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // ButtonFa
            // 
            this.ButtonFa.BackColor = System.Drawing.Color.White;
            this.ButtonFa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonFa.Location = new System.Drawing.Point(12, 221);
            this.ButtonFa.Name = "ButtonFa";
            this.ButtonFa.Size = new System.Drawing.Size(35, 35);
            this.ButtonFa.TabIndex = 5;
            this.ButtonFa.Tag = "Fa";
            this.ButtonFa.Text = "X";
            this.ButtonFa.UseVisualStyleBackColor = false;
            // 
            // ButtonEb
            // 
            this.ButtonEb.BackColor = System.Drawing.Color.White;
            this.ButtonEb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonEb.Location = new System.Drawing.Point(46, 187);
            this.ButtonEb.Name = "ButtonEb";
            this.ButtonEb.Size = new System.Drawing.Size(35, 35);
            this.ButtonEb.TabIndex = 11;
            this.ButtonEb.Tag = "Eb";
            this.ButtonEb.Text = "X";
            this.ButtonEb.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Gray;
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(46, 153);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(35, 35);
            this.button8.TabIndex = 10;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // ButtonCb
            // 
            this.ButtonCb.BackColor = System.Drawing.Color.White;
            this.ButtonCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonCb.Location = new System.Drawing.Point(46, 119);
            this.ButtonCb.Name = "ButtonCb";
            this.ButtonCb.Size = new System.Drawing.Size(35, 35);
            this.ButtonCb.TabIndex = 9;
            this.ButtonCb.Tag = "Cb";
            this.ButtonCb.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Gray;
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(46, 85);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(35, 35);
            this.button10.TabIndex = 8;
            this.button10.UseVisualStyleBackColor = false;
            // 
            // ButtonAb
            // 
            this.ButtonAb.BackColor = System.Drawing.Color.White;
            this.ButtonAb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonAb.Location = new System.Drawing.Point(46, 51);
            this.ButtonAb.Name = "ButtonAb";
            this.ButtonAb.Size = new System.Drawing.Size(35, 35);
            this.ButtonAb.TabIndex = 7;
            this.ButtonAb.Tag = "Ab";
            this.ButtonAb.Text = "O";
            this.ButtonAb.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Gray;
            this.button12.Enabled = false;
            this.button12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button12.Location = new System.Drawing.Point(46, 221);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(35, 35);
            this.button12.TabIndex = 6;
            this.button12.UseVisualStyleBackColor = false;
            // 
            // ButtonEd
            // 
            this.ButtonEd.BackColor = System.Drawing.Color.White;
            this.ButtonEd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonEd.Location = new System.Drawing.Point(113, 187);
            this.ButtonEd.Name = "ButtonEd";
            this.ButtonEd.Size = new System.Drawing.Size(35, 35);
            this.ButtonEd.TabIndex = 23;
            this.ButtonEd.Tag = "Ed";
            this.ButtonEd.Text = "X";
            this.ButtonEd.UseVisualStyleBackColor = false;
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.Gray;
            this.button14.Enabled = false;
            this.button14.Location = new System.Drawing.Point(113, 153);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(35, 35);
            this.button14.TabIndex = 22;
            this.button14.UseVisualStyleBackColor = false;
            // 
            // ButtonCd
            // 
            this.ButtonCd.BackColor = System.Drawing.Color.White;
            this.ButtonCd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonCd.Location = new System.Drawing.Point(113, 119);
            this.ButtonCd.Name = "ButtonCd";
            this.ButtonCd.Size = new System.Drawing.Size(35, 35);
            this.ButtonCd.TabIndex = 21;
            this.ButtonCd.Tag = "Cd";
            this.ButtonCd.UseVisualStyleBackColor = false;
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.Gray;
            this.button16.Enabled = false;
            this.button16.Location = new System.Drawing.Point(113, 85);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(35, 35);
            this.button16.TabIndex = 20;
            this.button16.UseVisualStyleBackColor = false;
            // 
            // ButtonAd
            // 
            this.ButtonAd.BackColor = System.Drawing.Color.White;
            this.ButtonAd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonAd.Location = new System.Drawing.Point(113, 51);
            this.ButtonAd.Name = "ButtonAd";
            this.ButtonAd.Size = new System.Drawing.Size(35, 35);
            this.ButtonAd.TabIndex = 19;
            this.ButtonAd.Tag = "Ad";
            this.ButtonAd.Text = "O";
            this.ButtonAd.UseVisualStyleBackColor = false;
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.Gray;
            this.button18.Enabled = false;
            this.button18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button18.Location = new System.Drawing.Point(113, 221);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(35, 35);
            this.button18.TabIndex = 18;
            this.button18.UseVisualStyleBackColor = false;
            // 
            // ButtonFc
            // 
            this.ButtonFc.BackColor = System.Drawing.Color.White;
            this.ButtonFc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonFc.Location = new System.Drawing.Point(79, 221);
            this.ButtonFc.Name = "ButtonFc";
            this.ButtonFc.Size = new System.Drawing.Size(35, 35);
            this.ButtonFc.TabIndex = 17;
            this.ButtonFc.Tag = "Fc";
            this.ButtonFc.Text = "X";
            this.ButtonFc.UseVisualStyleBackColor = false;
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.Gray;
            this.button20.Enabled = false;
            this.button20.Location = new System.Drawing.Point(79, 187);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(35, 35);
            this.button20.TabIndex = 16;
            this.button20.UseVisualStyleBackColor = false;
            // 
            // ButtonDc
            // 
            this.ButtonDc.BackColor = System.Drawing.Color.White;
            this.ButtonDc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonDc.Location = new System.Drawing.Point(79, 153);
            this.ButtonDc.Name = "ButtonDc";
            this.ButtonDc.Size = new System.Drawing.Size(35, 35);
            this.ButtonDc.TabIndex = 15;
            this.ButtonDc.Tag = "Dc";
            this.ButtonDc.UseVisualStyleBackColor = false;
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.Color.Gray;
            this.button22.Enabled = false;
            this.button22.Location = new System.Drawing.Point(79, 119);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(35, 35);
            this.button22.TabIndex = 14;
            this.button22.UseVisualStyleBackColor = false;
            // 
            // ButtonBc
            // 
            this.ButtonBc.BackColor = System.Drawing.Color.White;
            this.ButtonBc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonBc.Location = new System.Drawing.Point(79, 85);
            this.ButtonBc.Name = "ButtonBc";
            this.ButtonBc.Size = new System.Drawing.Size(35, 35);
            this.ButtonBc.TabIndex = 13;
            this.ButtonBc.Tag = "Bc";
            this.ButtonBc.Text = "O";
            this.ButtonBc.UseVisualStyleBackColor = false;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.Gray;
            this.button24.Enabled = false;
            this.button24.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button24.Location = new System.Drawing.Point(79, 51);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(35, 35);
            this.button24.TabIndex = 12;
            this.button24.UseVisualStyleBackColor = false;
            // 
            // ButtonEf
            // 
            this.ButtonEf.BackColor = System.Drawing.Color.White;
            this.ButtonEf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonEf.Location = new System.Drawing.Point(181, 187);
            this.ButtonEf.Name = "ButtonEf";
            this.ButtonEf.Size = new System.Drawing.Size(35, 35);
            this.ButtonEf.TabIndex = 35;
            this.ButtonEf.Tag = "Ef";
            this.ButtonEf.Text = "X";
            this.ButtonEf.UseVisualStyleBackColor = false;
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.Color.Gray;
            this.button26.Enabled = false;
            this.button26.Location = new System.Drawing.Point(181, 153);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(35, 35);
            this.button26.TabIndex = 34;
            this.button26.UseVisualStyleBackColor = false;
            // 
            // ButtonCf
            // 
            this.ButtonCf.BackColor = System.Drawing.Color.White;
            this.ButtonCf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonCf.Location = new System.Drawing.Point(181, 119);
            this.ButtonCf.Name = "ButtonCf";
            this.ButtonCf.Size = new System.Drawing.Size(35, 35);
            this.ButtonCf.TabIndex = 33;
            this.ButtonCf.Tag = "Cf";
            this.ButtonCf.UseVisualStyleBackColor = false;
            // 
            // button28
            // 
            this.button28.BackColor = System.Drawing.Color.Gray;
            this.button28.Enabled = false;
            this.button28.Location = new System.Drawing.Point(181, 85);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(35, 35);
            this.button28.TabIndex = 32;
            this.button28.UseVisualStyleBackColor = false;
            // 
            // ButtonAf
            // 
            this.ButtonAf.BackColor = System.Drawing.Color.White;
            this.ButtonAf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonAf.Location = new System.Drawing.Point(181, 51);
            this.ButtonAf.Name = "ButtonAf";
            this.ButtonAf.Size = new System.Drawing.Size(35, 35);
            this.ButtonAf.TabIndex = 31;
            this.ButtonAf.Tag = "Af";
            this.ButtonAf.Text = "O";
            this.ButtonAf.UseVisualStyleBackColor = false;
            // 
            // button30
            // 
            this.button30.BackColor = System.Drawing.Color.Gray;
            this.button30.Enabled = false;
            this.button30.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button30.Location = new System.Drawing.Point(181, 221);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(35, 35);
            this.button30.TabIndex = 30;
            this.button30.UseVisualStyleBackColor = false;
            // 
            // ButtonFe
            // 
            this.ButtonFe.BackColor = System.Drawing.Color.White;
            this.ButtonFe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonFe.Location = new System.Drawing.Point(147, 221);
            this.ButtonFe.Name = "ButtonFe";
            this.ButtonFe.Size = new System.Drawing.Size(35, 35);
            this.ButtonFe.TabIndex = 29;
            this.ButtonFe.Tag = "Fe";
            this.ButtonFe.Text = "X";
            this.ButtonFe.UseVisualStyleBackColor = false;
            // 
            // button32
            // 
            this.button32.BackColor = System.Drawing.Color.Gray;
            this.button32.Enabled = false;
            this.button32.Location = new System.Drawing.Point(147, 187);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(35, 35);
            this.button32.TabIndex = 28;
            this.button32.UseVisualStyleBackColor = false;
            // 
            // ButtonDe
            // 
            this.ButtonDe.BackColor = System.Drawing.Color.White;
            this.ButtonDe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonDe.Location = new System.Drawing.Point(147, 153);
            this.ButtonDe.Name = "ButtonDe";
            this.ButtonDe.Size = new System.Drawing.Size(35, 35);
            this.ButtonDe.TabIndex = 27;
            this.ButtonDe.Tag = "De";
            this.ButtonDe.UseVisualStyleBackColor = false;
            // 
            // button34
            // 
            this.button34.BackColor = System.Drawing.Color.Gray;
            this.button34.Enabled = false;
            this.button34.Location = new System.Drawing.Point(147, 119);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(35, 35);
            this.button34.TabIndex = 26;
            this.button34.UseVisualStyleBackColor = false;
            // 
            // ButtonBe
            // 
            this.ButtonBe.BackColor = System.Drawing.Color.White;
            this.ButtonBe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonBe.Location = new System.Drawing.Point(147, 85);
            this.ButtonBe.Name = "ButtonBe";
            this.ButtonBe.Size = new System.Drawing.Size(35, 35);
            this.ButtonBe.TabIndex = 25;
            this.ButtonBe.Tag = "Be";
            this.ButtonBe.Text = "O";
            this.ButtonBe.UseVisualStyleBackColor = false;
            // 
            // button36
            // 
            this.button36.BackColor = System.Drawing.Color.Gray;
            this.button36.Enabled = false;
            this.button36.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button36.Location = new System.Drawing.Point(147, 51);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(35, 35);
            this.button36.TabIndex = 24;
            this.button36.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Player 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Player 2:";
            // 
            // LabelPlayer1Score
            // 
            this.LabelPlayer1Score.AutoSize = true;
            this.LabelPlayer1Score.Location = new System.Drawing.Point(88, 20);
            this.LabelPlayer1Score.Name = "LabelPlayer1Score";
            this.LabelPlayer1Score.Size = new System.Drawing.Size(14, 13);
            this.LabelPlayer1Score.TabIndex = 38;
            this.LabelPlayer1Score.Text = "0";
            // 
            // LabelPlayer2Score
            // 
            this.LabelPlayer2Score.AutoSize = true;
            this.LabelPlayer2Score.Location = new System.Drawing.Point(178, 20);
            this.LabelPlayer2Score.Name = "LabelPlayer2Score";
            this.LabelPlayer2Score.Size = new System.Drawing.Size(14, 13);
            this.LabelPlayer2Score.TabIndex = 39;
            this.LabelPlayer2Score.Text = "0";
            // 
            // FormGameBoard
            // 
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(226, 268);
            this.Controls.Add(this.LabelPlayer2Score);
            this.Controls.Add(this.LabelPlayer1Score);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonEf);
            this.Controls.Add(this.button26);
            this.Controls.Add(this.ButtonCf);
            this.Controls.Add(this.button28);
            this.Controls.Add(this.ButtonAf);
            this.Controls.Add(this.button30);
            this.Controls.Add(this.ButtonFe);
            this.Controls.Add(this.button32);
            this.Controls.Add(this.ButtonDe);
            this.Controls.Add(this.button34);
            this.Controls.Add(this.ButtonBe);
            this.Controls.Add(this.button36);
            this.Controls.Add(this.ButtonEd);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.ButtonCd);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.ButtonAd);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.ButtonFc);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.ButtonDc);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.ButtonBc);
            this.Controls.Add(this.button24);
            this.Controls.Add(this.ButtonEb);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.ButtonCb);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.ButtonAb);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.ButtonFa);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.ButtonDa);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ButtonBa);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Name = "FormGameBoard";
            this.Load += new System.EventHandler(this.FormGameBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FormGameBoard_Load(object sender, EventArgs e)
        {

        }
    }
}
