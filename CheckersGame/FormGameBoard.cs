using System;
using System.Drawing;
using System.Windows.Forms;

namespace CheckersGame
{
    public class FormGameBoard : Form
    {
        private const string k_ComputerName = "Computer";
        private readonly FormGameSettings r_FormGameSettings = new FormGameSettings();
        private Checkers r_CheckersLogic = new Checkers();
        private string[] sr_cellSymbols = { string.Empty, "O", "X", "U", "K" };
        private Button m_SourcePosition;

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
        private Button buttonEh;
        private Button button35;
        private Button buttonCh;
        private Button button38;
        private Button buttonAh;
        private Button button40;
        private Button buttonFg;
        private Button button42;
        private Button buttonDg;
        private Button button44;
        private Button buttonBg;
        private Button button46;
        private Button buttonGh;
        private Button button4;
        private Button buttonHg;
        private Button button7;
        private Button buttonGf;
        private Button button11;
        private Button buttonHe;
        private Button button15;
        private Button buttonGd;
        private Button button19;
        private Button buttonHc;
        private Button button23;
        private Button buttonGb;
        private Button button27;
        private Button button29;
        private Button button31;
        private Button buttonGJ;
        private Button button6;
        private Button buttonHi;
        private Button button13;
        private Button buttonEj;
        private Button button21;
        private Button buttonCj;
        private Button button33;
        private Button buttonAj;
        private Button button39;
        private Button buttonFi;
        private Button button43;
        private Button buttonDi;
        private Button button47;
        private Button buttonBi;
        private Button button49;
        private Button button2;
        private Button button9;
        private Button button17;
        private Button button25;
        private Button buttonIh;
        private Button button41;
        private Button buttonJg;
        private Button button48;
        private Button buttonIf;
        private Button button51;
        private Button buttonJe;
        private Button button53;
        private Button buttonId;
        private Button button55;
        private Button buttonJc;
        private Button button57;
        private Button buttonIb;
        private Button button59;
        private Button buttonJa;
        private Button button61;
        private Button button1;
        private Label labelPlayer1Name;
        private Label labelPlayer2Name;
        private Label labelPlayer1Score;
        private Label labelPlayer2Score;

        public void StartGame()
        {
            // get settings and initialize game
            r_FormGameSettings.ShowDialog();
            if (r_FormGameSettings.DialogResult == DialogResult.OK)
            {
                r_CheckersLogic.InitializeGameLogic(
                    r_FormGameSettings.BoardSize,
                    r_FormGameSettings.Player1Name,
                    r_FormGameSettings.Player2Name,
                    r_FormGameSettings.IsPlayer2Human);

                switch (r_CheckersLogic.BoardSize)
                {
                    case CheckersGameBoard.eBoardSize.Small:
                        this.InitializeComponentSmall();
                        break;
                    case CheckersGameBoard.eBoardSize.Medium:
                        this.InitializeComponentMedium();
                        break;
                    case CheckersGameBoard.eBoardSize.Large:
                        this.InitializeComponentLarge();
                        break;
                    default:
                        throw new ArgumentException("Enum value not valid: eBoardSize");
                }

                this.labelPlayer1Name.Text = this.r_CheckersLogic.Player1Name;
                this.labelPlayer2Name.Text = this.r_FormGameSettings.IsPlayer2Human ? this.r_CheckersLogic.Player2Name : k_ComputerName;
                signForPossibleClickButtons();
                signForPossibleCheckersEvents();
                ShowDialog();
            }
        }

        private void signForPossibleClickButtons()
        {
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

        // subscribing to checker's events
        private void signForPossibleCheckersEvents()
        {
            this.r_CheckersLogic.CellChanged += m_CheckersBoard_CellChanged;
            r_CheckersLogic.GameOver += this.m_CheckersLogic_GameOverOccured;
            r_CheckersLogic.InvalidMoveGiven += m_Checkers_InvalidMoveOccured;
        }

        // When cell in the logic parts changes, this function updates the relevant cells in gui
        private void m_CheckersBoard_CellChanged(object sender, CellChangedEventArgs e)
        {
            Button buttonToBeChanged = positionStringToButton(e.CellPosition.ToAlphabetString());
            CheckersGameBoard.eCellMode newMode = e.NewCellMode;
            if (buttonToBeChanged != null)
            {
                switch (newMode)
                {
                    case CheckersGameBoard.eCellMode.Empty:
                        buttonToBeChanged.Text = sr_cellSymbols[(int)CheckersGameBoard.eCellMode.Empty];
                        break;
                    case CheckersGameBoard.eCellMode.Player1Piece:
                        buttonToBeChanged.Text = sr_cellSymbols[(int)CheckersGameBoard.eCellMode.Player1Piece];
                        break;
                    case CheckersGameBoard.eCellMode.Player2Piece:
                        buttonToBeChanged.Text = sr_cellSymbols[(int)CheckersGameBoard.eCellMode.Player2Piece];
                        break;
                    case CheckersGameBoard.eCellMode.Player1King:
                        buttonToBeChanged.Text = sr_cellSymbols[(int)CheckersGameBoard.eCellMode.Player1King];
                        break;
                    case CheckersGameBoard.eCellMode.Player2King:
                        buttonToBeChanged.Text = sr_cellSymbols[(int)CheckersGameBoard.eCellMode.Player2King];
                        break;
                    default:
                        throw new ArgumentException("Invalid Enum value : eCellMode");
                }
            }
        }

        private void button_Clicked(object sender, EventArgs e)
        {
            if (m_SourcePosition == null)
            {
                // No source position determined yet
                if ((sender as Button).Text != string.Empty)  
                {
                    // Set this button as source position
                    m_SourcePosition = sender as Button;
                    m_SourcePosition.BackColor = Color.Aquamarine;
                    m_SourcePosition.Click -= button_Clicked;
                    m_SourcePosition.Click += button_ClickedAgain;
                }
            }
            else
            {
                // Source position already exists - check if this position is a valid destination
                Move newMove = CheckersGame.Move.ParseAlphabetMove(m_SourcePosition.Tag.ToString(), (sender as Button).Tag.ToString());
                r_CheckersLogic.PlayRound(newMove); // May make the move or not if move is not valid
                m_SourcePosition.BackColor = Color.White;
                m_SourcePosition.Click -= button_ClickedAgain;
                m_SourcePosition.Click += button_Clicked;
                m_SourcePosition = null;
            }
        }

        private void m_Checkers_InvalidMoveOccured(object sender, EventArgs e)
        {
            MessageBox.Show("Invalid Move!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // Game over Occurd
        private void m_CheckersLogic_GameOverOccured(object sender, GameOverEventArgs e)
        {
            string message;
            if (e.Tie)
            {
                message = string.Format(
@"It's a Tie!
First Player : {0} scored {1} points!
Second Player : {2} scored {3} points!
Another Round?",
               e.StrongerPlayer.Name,
               e.StrongerPlayer.Score,
               e.WeakerPlayer.Name,
               e.WeakerPlayer.Score);
            }
            else
            {
                message = string.Format(
                @" {0} is the Winner !
{1} scored {2} points!
{3} scored {4} points!
Another Round?",
               e.StrongerPlayer.Name,
               e.StrongerPlayer.Name,
               e.StrongerPlayer.Score,
               e.WeakerPlayer.Name,
               e.WeakerPlayer.Score);
            }

            // updating new score
            if (e.StrongerPlayer.PlayerTag == Checkers.ePlayerTag.First)
            {
                this.labelPlayer1Score.Text = e.StrongerPlayer.Score.ToString();
                this.labelPlayer2Score.Text = e.WeakerPlayer.Score.ToString();
            }
            else
            {
                this.labelPlayer1Score.Text = e.WeakerPlayer.Score.ToString();
                this.labelPlayer2Score.Text = e.StrongerPlayer.Score.ToString();
            }

            // ask to keep playing or exit
            DialogResult result = MessageBox.Show(message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.r_CheckersLogic.ResetGame();
            }
            else
            {
                Application.Exit();
            }
        }

        // get button by position
        private Button positionStringToButton(string i_PositionStr)
        {
            Button buttonToReturn = null;
            foreach (Control control in Controls)
            {
                if (control.Tag != null && control.Tag.ToString() == i_PositionStr)
                {
                    buttonToReturn = control as Button;
                }
            }

            return buttonToReturn;
        }

        private void button_ClickedAgain(object sender, EventArgs e)
        {
            m_SourcePosition.BackColor = Color.White;
            m_SourcePosition.Click -= button_ClickedAgain;
            m_SourcePosition.Click += button_Clicked;
            m_SourcePosition = null;
        }

        private void InitializeComponentSmall()
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
            this.labelPlayer1Name = new System.Windows.Forms.Label();
            this.labelPlayer2Name = new System.Windows.Forms.Label();
            this.labelPlayer1Score = new System.Windows.Forms.Label();
            this.labelPlayer2Score = new System.Windows.Forms.Label();
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
            // labelPlayer1Name
            // 
            this.labelPlayer1Name.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer1Name.Location = new System.Drawing.Point(12, 9);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new System.Drawing.Size(102, 16);
            this.labelPlayer1Name.TabIndex = 88;
            this.labelPlayer1Name.Text = "Player 1";
            this.labelPlayer1Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlayer2Name
            // 
            this.labelPlayer2Name.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer2Name.Location = new System.Drawing.Point(110, 9);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new System.Drawing.Size(102, 16);
            this.labelPlayer2Name.TabIndex = 89;
            this.labelPlayer2Name.Text = "Player 2";
            this.labelPlayer2Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlayer1Score
            // 
            this.labelPlayer1Score.AutoSize = true;
            this.labelPlayer1Score.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer1Score.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer1Score.Location = new System.Drawing.Point(56, 30);
            this.labelPlayer1Score.Name = "labelPlayer1Score";
            this.labelPlayer1Score.Size = new System.Drawing.Size(18, 17);
            this.labelPlayer1Score.TabIndex = 90;
            this.labelPlayer1Score.Text = "0";
            // 
            // labelPlayer2Score
            // 
            this.labelPlayer2Score.AutoSize = true;
            this.labelPlayer2Score.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer2Score.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer2Score.Location = new System.Drawing.Point(156, 30);
            this.labelPlayer2Score.Name = "labelPlayer2Score";
            this.labelPlayer2Score.Size = new System.Drawing.Size(18, 17);
            this.labelPlayer2Score.TabIndex = 91;
            this.labelPlayer2Score.Text = "0";
            // 
            // FormGameBoard
            // 
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(227, 269);
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
            this.Controls.Add(this.labelPlayer2Score);
            this.Controls.Add(this.labelPlayer1Score);
            this.Controls.Add(this.labelPlayer2Name);
            this.Controls.Add(this.labelPlayer1Name);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormGameBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Checkers";
            this.Load += new System.EventHandler(this._Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeComponentMedium()
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
            this.buttonEh = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.buttonCh = new System.Windows.Forms.Button();
            this.button38 = new System.Windows.Forms.Button();
            this.buttonAh = new System.Windows.Forms.Button();
            this.button40 = new System.Windows.Forms.Button();
            this.buttonFg = new System.Windows.Forms.Button();
            this.button42 = new System.Windows.Forms.Button();
            this.buttonDg = new System.Windows.Forms.Button();
            this.button44 = new System.Windows.Forms.Button();
            this.buttonBg = new System.Windows.Forms.Button();
            this.button46 = new System.Windows.Forms.Button();
            this.buttonGh = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonHg = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.buttonGf = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.buttonHe = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.buttonGd = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.buttonHc = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.buttonGb = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.labelPlayer1Name = new System.Windows.Forms.Label();
            this.labelPlayer2Name = new System.Windows.Forms.Label();
            this.labelPlayer1Score = new System.Windows.Forms.Label();
            this.labelPlayer2Score = new System.Windows.Forms.Label();
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
            this.ButtonCb.Text = "O";
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
            this.ButtonCd.Text = "O";
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
            this.ButtonCf.Text = "O";
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
            // buttonEh
            // 
            this.buttonEh.BackColor = System.Drawing.Color.White;
            this.buttonEh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEh.Location = new System.Drawing.Point(248, 187);
            this.buttonEh.Name = "buttonEh";
            this.buttonEh.Size = new System.Drawing.Size(35, 35);
            this.buttonEh.TabIndex = 63;
            this.buttonEh.Tag = "Eh";
            this.buttonEh.UseVisualStyleBackColor = false;
            // 
            // button35
            // 
            this.button35.BackColor = System.Drawing.Color.Gray;
            this.button35.Enabled = false;
            this.button35.Location = new System.Drawing.Point(248, 153);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(35, 35);
            this.button35.TabIndex = 62;
            this.button35.UseVisualStyleBackColor = false;
            // 
            // buttonCh
            // 
            this.buttonCh.BackColor = System.Drawing.Color.White;
            this.buttonCh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCh.Location = new System.Drawing.Point(248, 119);
            this.buttonCh.Name = "buttonCh";
            this.buttonCh.Size = new System.Drawing.Size(35, 35);
            this.buttonCh.TabIndex = 61;
            this.buttonCh.Tag = "Ch";
            this.buttonCh.Text = "O";
            this.buttonCh.UseVisualStyleBackColor = false;
            // 
            // button38
            // 
            this.button38.BackColor = System.Drawing.Color.Gray;
            this.button38.Enabled = false;
            this.button38.Location = new System.Drawing.Point(248, 85);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(35, 35);
            this.button38.TabIndex = 60;
            this.button38.UseVisualStyleBackColor = false;
            // 
            // buttonAh
            // 
            this.buttonAh.BackColor = System.Drawing.Color.White;
            this.buttonAh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAh.Location = new System.Drawing.Point(248, 51);
            this.buttonAh.Name = "buttonAh";
            this.buttonAh.Size = new System.Drawing.Size(35, 35);
            this.buttonAh.TabIndex = 59;
            this.buttonAh.Tag = "Ah";
            this.buttonAh.Text = "O";
            this.buttonAh.UseVisualStyleBackColor = false;
            // 
            // button40
            // 
            this.button40.BackColor = System.Drawing.Color.Gray;
            this.button40.Enabled = false;
            this.button40.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button40.Location = new System.Drawing.Point(248, 221);
            this.button40.Name = "button40";
            this.button40.Size = new System.Drawing.Size(35, 35);
            this.button40.TabIndex = 58;
            this.button40.UseVisualStyleBackColor = false;
            // 
            // buttonFg
            // 
            this.buttonFg.BackColor = System.Drawing.Color.White;
            this.buttonFg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFg.Location = new System.Drawing.Point(214, 221);
            this.buttonFg.Name = "buttonFg";
            this.buttonFg.Size = new System.Drawing.Size(35, 35);
            this.buttonFg.TabIndex = 57;
            this.buttonFg.Tag = "Fg";
            this.buttonFg.Text = "X";
            this.buttonFg.UseVisualStyleBackColor = false;
            // 
            // button42
            // 
            this.button42.BackColor = System.Drawing.Color.Gray;
            this.button42.Enabled = false;
            this.button42.Location = new System.Drawing.Point(214, 187);
            this.button42.Name = "button42";
            this.button42.Size = new System.Drawing.Size(35, 35);
            this.button42.TabIndex = 56;
            this.button42.UseVisualStyleBackColor = false;
            // 
            // buttonDg
            // 
            this.buttonDg.BackColor = System.Drawing.Color.White;
            this.buttonDg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDg.Location = new System.Drawing.Point(214, 153);
            this.buttonDg.Name = "buttonDg";
            this.buttonDg.Size = new System.Drawing.Size(35, 35);
            this.buttonDg.TabIndex = 55;
            this.buttonDg.Tag = "Dg";
            this.buttonDg.UseVisualStyleBackColor = false;
            // 
            // button44
            // 
            this.button44.BackColor = System.Drawing.Color.Gray;
            this.button44.Enabled = false;
            this.button44.Location = new System.Drawing.Point(214, 119);
            this.button44.Name = "button44";
            this.button44.Size = new System.Drawing.Size(35, 35);
            this.button44.TabIndex = 54;
            this.button44.UseVisualStyleBackColor = false;
            // 
            // buttonBg
            // 
            this.buttonBg.BackColor = System.Drawing.Color.White;
            this.buttonBg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBg.Location = new System.Drawing.Point(214, 85);
            this.buttonBg.Name = "buttonBg";
            this.buttonBg.Size = new System.Drawing.Size(35, 35);
            this.buttonBg.TabIndex = 53;
            this.buttonBg.Tag = "Bg";
            this.buttonBg.Text = "O";
            this.buttonBg.UseVisualStyleBackColor = false;
            // 
            // button46
            // 
            this.button46.BackColor = System.Drawing.Color.Gray;
            this.button46.Enabled = false;
            this.button46.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button46.Location = new System.Drawing.Point(214, 51);
            this.button46.Name = "button46";
            this.button46.Size = new System.Drawing.Size(35, 35);
            this.button46.TabIndex = 52;
            this.button46.UseVisualStyleBackColor = false;
            // 
            // buttonGh
            // 
            this.buttonGh.BackColor = System.Drawing.Color.White;
            this.buttonGh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGh.Location = new System.Drawing.Point(248, 255);
            this.buttonGh.Name = "buttonGh";
            this.buttonGh.Size = new System.Drawing.Size(35, 35);
            this.buttonGh.TabIndex = 87;
            this.buttonGh.Tag = "Gh";
            this.buttonGh.Text = "X";
            this.buttonGh.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Gray;
            this.button4.Enabled = false;
            this.button4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button4.Location = new System.Drawing.Point(248, 289);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 35);
            this.button4.TabIndex = 86;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // buttonHg
            // 
            this.buttonHg.BackColor = System.Drawing.Color.White;
            this.buttonHg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHg.Location = new System.Drawing.Point(214, 289);
            this.buttonHg.Name = "buttonHg";
            this.buttonHg.Size = new System.Drawing.Size(35, 35);
            this.buttonHg.TabIndex = 85;
            this.buttonHg.Tag = "Hg";
            this.buttonHg.Text = "X";
            this.buttonHg.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Gray;
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(214, 255);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(35, 35);
            this.button7.TabIndex = 84;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // buttonGf
            // 
            this.buttonGf.BackColor = System.Drawing.Color.White;
            this.buttonGf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGf.Location = new System.Drawing.Point(181, 255);
            this.buttonGf.Name = "buttonGf";
            this.buttonGf.Size = new System.Drawing.Size(35, 35);
            this.buttonGf.TabIndex = 83;
            this.buttonGf.Tag = "Gf";
            this.buttonGf.Text = "X";
            this.buttonGf.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Gray;
            this.button11.Enabled = false;
            this.button11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button11.Location = new System.Drawing.Point(181, 289);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(35, 35);
            this.button11.TabIndex = 82;
            this.button11.UseVisualStyleBackColor = false;
            // 
            // buttonHe
            // 
            this.buttonHe.BackColor = System.Drawing.Color.White;
            this.buttonHe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHe.Location = new System.Drawing.Point(147, 289);
            this.buttonHe.Name = "buttonHe";
            this.buttonHe.Size = new System.Drawing.Size(35, 35);
            this.buttonHe.TabIndex = 81;
            this.buttonHe.Tag = "He";
            this.buttonHe.Text = "X";
            this.buttonHe.UseVisualStyleBackColor = false;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Gray;
            this.button15.Enabled = false;
            this.button15.Location = new System.Drawing.Point(147, 255);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(35, 35);
            this.button15.TabIndex = 80;
            this.button15.UseVisualStyleBackColor = false;
            // 
            // buttonGd
            // 
            this.buttonGd.BackColor = System.Drawing.Color.White;
            this.buttonGd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGd.Location = new System.Drawing.Point(113, 255);
            this.buttonGd.Name = "buttonGd";
            this.buttonGd.Size = new System.Drawing.Size(35, 35);
            this.buttonGd.TabIndex = 79;
            this.buttonGd.Tag = "Gd";
            this.buttonGd.Text = "X";
            this.buttonGd.UseVisualStyleBackColor = false;
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.Gray;
            this.button19.Enabled = false;
            this.button19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button19.Location = new System.Drawing.Point(113, 289);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(35, 35);
            this.button19.TabIndex = 78;
            this.button19.UseVisualStyleBackColor = false;
            // 
            // buttonHc
            // 
            this.buttonHc.BackColor = System.Drawing.Color.White;
            this.buttonHc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHc.Location = new System.Drawing.Point(79, 289);
            this.buttonHc.Name = "buttonHc";
            this.buttonHc.Size = new System.Drawing.Size(35, 35);
            this.buttonHc.TabIndex = 77;
            this.buttonHc.Tag = "Hc";
            this.buttonHc.Text = "X";
            this.buttonHc.UseVisualStyleBackColor = false;
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.Gray;
            this.button23.Enabled = false;
            this.button23.Location = new System.Drawing.Point(79, 255);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(35, 35);
            this.button23.TabIndex = 76;
            this.button23.UseVisualStyleBackColor = false;
            // 
            // buttonGb
            // 
            this.buttonGb.BackColor = System.Drawing.Color.White;
            this.buttonGb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGb.Location = new System.Drawing.Point(46, 255);
            this.buttonGb.Name = "buttonGb";
            this.buttonGb.Size = new System.Drawing.Size(35, 35);
            this.buttonGb.TabIndex = 75;
            this.buttonGb.Tag = "Gb";
            this.buttonGb.Text = "X";
            this.buttonGb.UseVisualStyleBackColor = false;
            // 
            // button27
            // 
            this.button27.BackColor = System.Drawing.Color.Gray;
            this.button27.Enabled = false;
            this.button27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button27.Location = new System.Drawing.Point(46, 289);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(35, 35);
            this.button27.TabIndex = 74;
            this.button27.UseVisualStyleBackColor = false;
            // 
            // button29
            // 
            this.button29.BackColor = System.Drawing.Color.White;
            this.button29.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button29.Location = new System.Drawing.Point(12, 289);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(35, 35);
            this.button29.TabIndex = 73;
            this.button29.Tag = "Ha";
            this.button29.Text = "X";
            this.button29.UseVisualStyleBackColor = false;
            // 
            // button31
            // 
            this.button31.BackColor = System.Drawing.Color.Gray;
            this.button31.Enabled = false;
            this.button31.Location = new System.Drawing.Point(12, 255);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(35, 35);
            this.button31.TabIndex = 72;
            this.button31.UseVisualStyleBackColor = false;
            // 
            // labelPlayer1Name
            // 
            this.labelPlayer1Name.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer1Name.Location = new System.Drawing.Point(43, 6);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new System.Drawing.Size(102, 16);
            this.labelPlayer1Name.TabIndex = 88;
            this.labelPlayer1Name.Text = "Player 1";
            this.labelPlayer1Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlayer2Name
            // 
            this.labelPlayer2Name.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer2Name.Location = new System.Drawing.Point(144, 6);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new System.Drawing.Size(102, 16);
            this.labelPlayer2Name.TabIndex = 89;
            this.labelPlayer2Name.Text = "Player 2";
            this.labelPlayer2Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlayer1Score
            // 
            this.labelPlayer1Score.AutoSize = true;
            this.labelPlayer1Score.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer1Score.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer1Score.Location = new System.Drawing.Point(87, 27);
            this.labelPlayer1Score.Name = "labelPlayer1Score";
            this.labelPlayer1Score.Size = new System.Drawing.Size(18, 17);
            this.labelPlayer1Score.TabIndex = 90;
            this.labelPlayer1Score.Text = "0";
            // 
            // labelPlayer2Score
            // 
            this.labelPlayer2Score.AutoSize = true;
            this.labelPlayer2Score.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer2Score.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer2Score.Location = new System.Drawing.Point(190, 27);
            this.labelPlayer2Score.Name = "labelPlayer2Score";
            this.labelPlayer2Score.Size = new System.Drawing.Size(18, 17);
            this.labelPlayer2Score.TabIndex = 91;
            this.labelPlayer2Score.Text = "0";
            // 
            // FormGameBoard
            // 
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(293, 337);
            this.Controls.Add(this.labelPlayer2Score);
            this.Controls.Add(this.labelPlayer1Score);
            this.Controls.Add(this.labelPlayer2Name);
            this.Controls.Add(this.labelPlayer1Name);
            this.Controls.Add(this.buttonGh);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonHg);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.buttonGf);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.buttonHe);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.buttonGd);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.buttonHc);
            this.Controls.Add(this.button23);
            this.Controls.Add(this.buttonGb);
            this.Controls.Add(this.button27);
            this.Controls.Add(this.button29);
            this.Controls.Add(this.button31);
            this.Controls.Add(this.buttonEh);
            this.Controls.Add(this.button35);
            this.Controls.Add(this.buttonCh);
            this.Controls.Add(this.button38);
            this.Controls.Add(this.buttonAh);
            this.Controls.Add(this.button40);
            this.Controls.Add(this.buttonFg);
            this.Controls.Add(this.button42);
            this.Controls.Add(this.buttonDg);
            this.Controls.Add(this.button44);
            this.Controls.Add(this.buttonBg);
            this.Controls.Add(this.button46);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Name = "FormGameBoard";
            this.Text = "Checkers";
            this.Load += new System.EventHandler(this._Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeComponentLarge()
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
            this.buttonEh = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.buttonCh = new System.Windows.Forms.Button();
            this.button38 = new System.Windows.Forms.Button();
            this.buttonAh = new System.Windows.Forms.Button();
            this.button40 = new System.Windows.Forms.Button();
            this.buttonFg = new System.Windows.Forms.Button();
            this.button42 = new System.Windows.Forms.Button();
            this.buttonDg = new System.Windows.Forms.Button();
            this.button44 = new System.Windows.Forms.Button();
            this.buttonBg = new System.Windows.Forms.Button();
            this.button46 = new System.Windows.Forms.Button();
            this.buttonGh = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonHg = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.buttonGf = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.buttonHe = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.buttonGd = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.buttonHc = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.buttonGb = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.labelPlayer1Name = new System.Windows.Forms.Label();
            this.labelPlayer2Name = new System.Windows.Forms.Label();
            this.labelPlayer1Score = new System.Windows.Forms.Label();
            this.labelPlayer2Score = new System.Windows.Forms.Label();
            this.buttonGJ = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.buttonHi = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.buttonEj = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.buttonCj = new System.Windows.Forms.Button();
            this.button33 = new System.Windows.Forms.Button();
            this.buttonAj = new System.Windows.Forms.Button();
            this.button39 = new System.Windows.Forms.Button();
            this.buttonFi = new System.Windows.Forms.Button();
            this.button43 = new System.Windows.Forms.Button();
            this.buttonDi = new System.Windows.Forms.Button();
            this.button47 = new System.Windows.Forms.Button();
            this.buttonBi = new System.Windows.Forms.Button();
            this.button49 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.buttonIh = new System.Windows.Forms.Button();
            this.button41 = new System.Windows.Forms.Button();
            this.buttonJg = new System.Windows.Forms.Button();
            this.button48 = new System.Windows.Forms.Button();
            this.buttonIf = new System.Windows.Forms.Button();
            this.button51 = new System.Windows.Forms.Button();
            this.buttonJe = new System.Windows.Forms.Button();
            this.button53 = new System.Windows.Forms.Button();
            this.buttonId = new System.Windows.Forms.Button();
            this.button55 = new System.Windows.Forms.Button();
            this.buttonJc = new System.Windows.Forms.Button();
            this.button57 = new System.Windows.Forms.Button();
            this.buttonIb = new System.Windows.Forms.Button();
            this.button59 = new System.Windows.Forms.Button();
            this.buttonJa = new System.Windows.Forms.Button();
            this.button61 = new System.Windows.Forms.Button();
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
            this.ButtonDa.Text = "O";
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
            this.ButtonCb.Text = "O";
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
            this.ButtonCd.Text = "O";
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
            this.ButtonDc.Text = "O";
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
            this.ButtonCf.Text = "O";
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
            this.ButtonDe.Text = "O";
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
            // buttonEh
            // 
            this.buttonEh.BackColor = System.Drawing.Color.White;
            this.buttonEh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEh.Location = new System.Drawing.Point(248, 187);
            this.buttonEh.Name = "buttonEh";
            this.buttonEh.Size = new System.Drawing.Size(35, 35);
            this.buttonEh.TabIndex = 63;
            this.buttonEh.Tag = "Eh";
            this.buttonEh.UseVisualStyleBackColor = false;
            // 
            // button35
            // 
            this.button35.BackColor = System.Drawing.Color.Gray;
            this.button35.Enabled = false;
            this.button35.Location = new System.Drawing.Point(248, 153);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(35, 35);
            this.button35.TabIndex = 62;
            this.button35.UseVisualStyleBackColor = false;
            // 
            // buttonCh
            // 
            this.buttonCh.BackColor = System.Drawing.Color.White;
            this.buttonCh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCh.Location = new System.Drawing.Point(248, 119);
            this.buttonCh.Name = "buttonCh";
            this.buttonCh.Size = new System.Drawing.Size(35, 35);
            this.buttonCh.TabIndex = 61;
            this.buttonCh.Tag = "Ch";
            this.buttonCh.Text = "O";
            this.buttonCh.UseVisualStyleBackColor = false;
            // 
            // button38
            // 
            this.button38.BackColor = System.Drawing.Color.Gray;
            this.button38.Enabled = false;
            this.button38.Location = new System.Drawing.Point(248, 85);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(35, 35);
            this.button38.TabIndex = 60;
            this.button38.UseVisualStyleBackColor = false;
            // 
            // buttonAh
            // 
            this.buttonAh.BackColor = System.Drawing.Color.White;
            this.buttonAh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAh.Location = new System.Drawing.Point(248, 51);
            this.buttonAh.Name = "buttonAh";
            this.buttonAh.Size = new System.Drawing.Size(35, 35);
            this.buttonAh.TabIndex = 59;
            this.buttonAh.Tag = "Ah";
            this.buttonAh.Text = "O";
            this.buttonAh.UseVisualStyleBackColor = false;
            // 
            // button40
            // 
            this.button40.BackColor = System.Drawing.Color.Gray;
            this.button40.Enabled = false;
            this.button40.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button40.Location = new System.Drawing.Point(248, 221);
            this.button40.Name = "button40";
            this.button40.Size = new System.Drawing.Size(35, 35);
            this.button40.TabIndex = 58;
            this.button40.UseVisualStyleBackColor = false;
            // 
            // buttonFg
            // 
            this.buttonFg.BackColor = System.Drawing.Color.White;
            this.buttonFg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFg.Location = new System.Drawing.Point(214, 221);
            this.buttonFg.Name = "buttonFg";
            this.buttonFg.Size = new System.Drawing.Size(35, 35);
            this.buttonFg.TabIndex = 57;
            this.buttonFg.Tag = "Fg";
            this.buttonFg.UseVisualStyleBackColor = false;
            // 
            // button42
            // 
            this.button42.BackColor = System.Drawing.Color.Gray;
            this.button42.Enabled = false;
            this.button42.Location = new System.Drawing.Point(214, 187);
            this.button42.Name = "button42";
            this.button42.Size = new System.Drawing.Size(35, 35);
            this.button42.TabIndex = 56;
            this.button42.UseVisualStyleBackColor = false;
            // 
            // buttonDg
            // 
            this.buttonDg.BackColor = System.Drawing.Color.White;
            this.buttonDg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDg.Location = new System.Drawing.Point(214, 153);
            this.buttonDg.Name = "buttonDg";
            this.buttonDg.Size = new System.Drawing.Size(35, 35);
            this.buttonDg.TabIndex = 55;
            this.buttonDg.Tag = "Dg";
            this.buttonDg.Text = "O";
            this.buttonDg.UseVisualStyleBackColor = false;
            // 
            // button44
            // 
            this.button44.BackColor = System.Drawing.Color.Gray;
            this.button44.Enabled = false;
            this.button44.Location = new System.Drawing.Point(214, 119);
            this.button44.Name = "button44";
            this.button44.Size = new System.Drawing.Size(35, 35);
            this.button44.TabIndex = 54;
            this.button44.UseVisualStyleBackColor = false;
            // 
            // buttonBg
            // 
            this.buttonBg.BackColor = System.Drawing.Color.White;
            this.buttonBg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBg.Location = new System.Drawing.Point(214, 85);
            this.buttonBg.Name = "buttonBg";
            this.buttonBg.Size = new System.Drawing.Size(35, 35);
            this.buttonBg.TabIndex = 53;
            this.buttonBg.Tag = "Bg";
            this.buttonBg.Text = "O";
            this.buttonBg.UseVisualStyleBackColor = false;
            // 
            // button46
            // 
            this.button46.BackColor = System.Drawing.Color.Gray;
            this.button46.Enabled = false;
            this.button46.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button46.Location = new System.Drawing.Point(214, 51);
            this.button46.Name = "button46";
            this.button46.Size = new System.Drawing.Size(35, 35);
            this.button46.TabIndex = 52;
            this.button46.UseVisualStyleBackColor = false;
            // 
            // buttonGh
            // 
            this.buttonGh.BackColor = System.Drawing.Color.White;
            this.buttonGh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGh.Location = new System.Drawing.Point(248, 255);
            this.buttonGh.Name = "buttonGh";
            this.buttonGh.Size = new System.Drawing.Size(35, 35);
            this.buttonGh.TabIndex = 87;
            this.buttonGh.Tag = "Gh";
            this.buttonGh.Text = "X";
            this.buttonGh.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Gray;
            this.button4.Enabled = false;
            this.button4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button4.Location = new System.Drawing.Point(248, 289);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 35);
            this.button4.TabIndex = 86;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // buttonHg
            // 
            this.buttonHg.BackColor = System.Drawing.Color.White;
            this.buttonHg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHg.Location = new System.Drawing.Point(214, 289);
            this.buttonHg.Name = "buttonHg";
            this.buttonHg.Size = new System.Drawing.Size(35, 35);
            this.buttonHg.TabIndex = 85;
            this.buttonHg.Tag = "Hg";
            this.buttonHg.Text = "X";
            this.buttonHg.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Gray;
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(214, 255);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(35, 35);
            this.button7.TabIndex = 84;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // buttonGf
            // 
            this.buttonGf.BackColor = System.Drawing.Color.White;
            this.buttonGf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGf.Location = new System.Drawing.Point(181, 255);
            this.buttonGf.Name = "buttonGf";
            this.buttonGf.Size = new System.Drawing.Size(35, 35);
            this.buttonGf.TabIndex = 83;
            this.buttonGf.Tag = "Gf";
            this.buttonGf.Text = "X";
            this.buttonGf.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Gray;
            this.button11.Enabled = false;
            this.button11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button11.Location = new System.Drawing.Point(181, 289);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(35, 35);
            this.button11.TabIndex = 82;
            this.button11.UseVisualStyleBackColor = false;
            // 
            // buttonHe
            // 
            this.buttonHe.BackColor = System.Drawing.Color.White;
            this.buttonHe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHe.Location = new System.Drawing.Point(147, 289);
            this.buttonHe.Name = "buttonHe";
            this.buttonHe.Size = new System.Drawing.Size(35, 35);
            this.buttonHe.TabIndex = 81;
            this.buttonHe.Tag = "He";
            this.buttonHe.Text = "X";
            this.buttonHe.UseVisualStyleBackColor = false;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Gray;
            this.button15.Enabled = false;
            this.button15.Location = new System.Drawing.Point(147, 255);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(35, 35);
            this.button15.TabIndex = 80;
            this.button15.UseVisualStyleBackColor = false;
            // 
            // buttonGd
            // 
            this.buttonGd.BackColor = System.Drawing.Color.White;
            this.buttonGd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGd.Location = new System.Drawing.Point(113, 255);
            this.buttonGd.Name = "buttonGd";
            this.buttonGd.Size = new System.Drawing.Size(35, 35);
            this.buttonGd.TabIndex = 79;
            this.buttonGd.Tag = "Gd";
            this.buttonGd.Text = "X";
            this.buttonGd.UseVisualStyleBackColor = false;
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.Gray;
            this.button19.Enabled = false;
            this.button19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button19.Location = new System.Drawing.Point(113, 289);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(35, 35);
            this.button19.TabIndex = 78;
            this.button19.UseVisualStyleBackColor = false;
            // 
            // buttonHc
            // 
            this.buttonHc.BackColor = System.Drawing.Color.White;
            this.buttonHc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHc.Location = new System.Drawing.Point(79, 289);
            this.buttonHc.Name = "buttonHc";
            this.buttonHc.Size = new System.Drawing.Size(35, 35);
            this.buttonHc.TabIndex = 77;
            this.buttonHc.Tag = "Hc";
            this.buttonHc.Text = "X";
            this.buttonHc.UseVisualStyleBackColor = false;
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.Gray;
            this.button23.Enabled = false;
            this.button23.Location = new System.Drawing.Point(79, 255);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(35, 35);
            this.button23.TabIndex = 76;
            this.button23.UseVisualStyleBackColor = false;
            // 
            // buttonGb
            // 
            this.buttonGb.BackColor = System.Drawing.Color.White;
            this.buttonGb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGb.Location = new System.Drawing.Point(46, 255);
            this.buttonGb.Name = "buttonGb";
            this.buttonGb.Size = new System.Drawing.Size(35, 35);
            this.buttonGb.TabIndex = 75;
            this.buttonGb.Tag = "Gb";
            this.buttonGb.Text = "X";
            this.buttonGb.UseVisualStyleBackColor = false;
            // 
            // button27
            // 
            this.button27.BackColor = System.Drawing.Color.Gray;
            this.button27.Enabled = false;
            this.button27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button27.Location = new System.Drawing.Point(46, 289);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(35, 35);
            this.button27.TabIndex = 74;
            this.button27.UseVisualStyleBackColor = false;
            // 
            // button29
            // 
            this.button29.BackColor = System.Drawing.Color.White;
            this.button29.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button29.Location = new System.Drawing.Point(12, 289);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(35, 35);
            this.button29.TabIndex = 73;
            this.button29.Tag = "Ha";
            this.button29.Text = "X";
            this.button29.UseVisualStyleBackColor = false;
            // 
            // button31
            // 
            this.button31.BackColor = System.Drawing.Color.Gray;
            this.button31.Enabled = false;
            this.button31.Location = new System.Drawing.Point(12, 255);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(35, 35);
            this.button31.TabIndex = 72;
            this.button31.UseVisualStyleBackColor = false;
            // 
            // labelPlayer1Name
            // 
            this.labelPlayer1Name.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer1Name.Location = new System.Drawing.Point(43, 7);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new System.Drawing.Size(102, 16);
            this.labelPlayer1Name.TabIndex = 88;
            this.labelPlayer1Name.Text = "Player 1";
            this.labelPlayer1Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlayer2Name
            // 
            this.labelPlayer2Name.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer2Name.Location = new System.Drawing.Point(211, 7);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new System.Drawing.Size(102, 16);
            this.labelPlayer2Name.TabIndex = 89;
            this.labelPlayer2Name.Text = "Player 2";
            this.labelPlayer2Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPlayer1Score
            // 
            this.labelPlayer1Score.AutoSize = true;
            this.labelPlayer1Score.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer1Score.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer1Score.Location = new System.Drawing.Point(87, 28);
            this.labelPlayer1Score.Name = "labelPlayer1Score";
            this.labelPlayer1Score.Size = new System.Drawing.Size(18, 17);
            this.labelPlayer1Score.TabIndex = 90;
            this.labelPlayer1Score.Text = "0";
            // 
            // labelPlayer2Score
            // 
            this.labelPlayer2Score.AutoSize = true;
            this.labelPlayer2Score.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer2Score.Font = new System.Drawing.Font("Britannic Bold", 12F, System.Drawing.FontStyle.Italic);
            this.labelPlayer2Score.Location = new System.Drawing.Point(257, 28);
            this.labelPlayer2Score.Name = "labelPlayer2Score";
            this.labelPlayer2Score.Size = new System.Drawing.Size(18, 17);
            this.labelPlayer2Score.TabIndex = 91;
            this.labelPlayer2Score.Text = "0";
            // 
            // buttonGJ
            // 
            this.buttonGJ.BackColor = System.Drawing.Color.White;
            this.buttonGJ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGJ.Location = new System.Drawing.Point(316, 255);
            this.buttonGJ.Name = "buttonGJ";
            this.buttonGJ.Size = new System.Drawing.Size(35, 35);
            this.buttonGJ.TabIndex = 107;
            this.buttonGJ.Tag = "Gj";
            this.buttonGJ.Text = "X";
            this.buttonGJ.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Gray;
            this.button6.Enabled = false;
            this.button6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button6.Location = new System.Drawing.Point(316, 289);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(35, 35);
            this.button6.TabIndex = 106;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // buttonHi
            // 
            this.buttonHi.BackColor = System.Drawing.Color.White;
            this.buttonHi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHi.Location = new System.Drawing.Point(282, 289);
            this.buttonHi.Name = "buttonHi";
            this.buttonHi.Size = new System.Drawing.Size(35, 35);
            this.buttonHi.TabIndex = 105;
            this.buttonHi.Tag = "Hi";
            this.buttonHi.Text = "X";
            this.buttonHi.UseVisualStyleBackColor = false;
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.Gray;
            this.button13.Enabled = false;
            this.button13.Location = new System.Drawing.Point(282, 255);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(35, 35);
            this.button13.TabIndex = 104;
            this.button13.UseVisualStyleBackColor = false;
            // 
            // buttonEj
            // 
            this.buttonEj.BackColor = System.Drawing.Color.White;
            this.buttonEj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEj.Location = new System.Drawing.Point(316, 187);
            this.buttonEj.Name = "buttonEj";
            this.buttonEj.Size = new System.Drawing.Size(35, 35);
            this.buttonEj.TabIndex = 103;
            this.buttonEj.Tag = "Ej";
            this.buttonEj.UseVisualStyleBackColor = false;
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.Gray;
            this.button21.Enabled = false;
            this.button21.Location = new System.Drawing.Point(316, 153);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(35, 35);
            this.button21.TabIndex = 102;
            this.button21.UseVisualStyleBackColor = false;
            // 
            // buttonCj
            // 
            this.buttonCj.BackColor = System.Drawing.Color.White;
            this.buttonCj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCj.Location = new System.Drawing.Point(316, 119);
            this.buttonCj.Name = "buttonCj";
            this.buttonCj.Size = new System.Drawing.Size(35, 35);
            this.buttonCj.TabIndex = 101;
            this.buttonCj.Tag = "Cj";
            this.buttonCj.Text = "O";
            this.buttonCj.UseVisualStyleBackColor = false;
            // 
            // button33
            // 
            this.button33.BackColor = System.Drawing.Color.Gray;
            this.button33.Enabled = false;
            this.button33.Location = new System.Drawing.Point(316, 85);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(35, 35);
            this.button33.TabIndex = 100;
            this.button33.UseVisualStyleBackColor = false;
            // 
            // buttonAj
            // 
            this.buttonAj.BackColor = System.Drawing.Color.White;
            this.buttonAj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAj.Location = new System.Drawing.Point(316, 51);
            this.buttonAj.Name = "buttonAj";
            this.buttonAj.Size = new System.Drawing.Size(35, 35);
            this.buttonAj.TabIndex = 99;
            this.buttonAj.Tag = "Aj";
            this.buttonAj.Text = "O";
            this.buttonAj.UseVisualStyleBackColor = false;
            // 
            // button39
            // 
            this.button39.BackColor = System.Drawing.Color.Gray;
            this.button39.Enabled = false;
            this.button39.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button39.Location = new System.Drawing.Point(316, 221);
            this.button39.Name = "button39";
            this.button39.Size = new System.Drawing.Size(35, 35);
            this.button39.TabIndex = 98;
            this.button39.UseVisualStyleBackColor = false;
            // 
            // buttonFi
            // 
            this.buttonFi.BackColor = System.Drawing.Color.White;
            this.buttonFi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFi.Location = new System.Drawing.Point(282, 221);
            this.buttonFi.Name = "buttonFi";
            this.buttonFi.Size = new System.Drawing.Size(35, 35);
            this.buttonFi.TabIndex = 97;
            this.buttonFi.Tag = "Fi";
            this.buttonFi.UseVisualStyleBackColor = false;
            // 
            // button43
            // 
            this.button43.BackColor = System.Drawing.Color.Gray;
            this.button43.Enabled = false;
            this.button43.Location = new System.Drawing.Point(282, 187);
            this.button43.Name = "button43";
            this.button43.Size = new System.Drawing.Size(35, 35);
            this.button43.TabIndex = 96;
            this.button43.UseVisualStyleBackColor = false;
            // 
            // buttonDi
            // 
            this.buttonDi.BackColor = System.Drawing.Color.White;
            this.buttonDi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDi.Location = new System.Drawing.Point(282, 153);
            this.buttonDi.Name = "buttonDi";
            this.buttonDi.Size = new System.Drawing.Size(35, 35);
            this.buttonDi.TabIndex = 95;
            this.buttonDi.Tag = "Di";
            this.buttonDi.Text = "O";
            this.buttonDi.UseVisualStyleBackColor = false;
            // 
            // button47
            // 
            this.button47.BackColor = System.Drawing.Color.Gray;
            this.button47.Enabled = false;
            this.button47.Location = new System.Drawing.Point(282, 119);
            this.button47.Name = "button47";
            this.button47.Size = new System.Drawing.Size(35, 35);
            this.button47.TabIndex = 94;
            this.button47.UseVisualStyleBackColor = false;
            // 
            // buttonBi
            // 
            this.buttonBi.BackColor = System.Drawing.Color.White;
            this.buttonBi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBi.Location = new System.Drawing.Point(282, 85);
            this.buttonBi.Name = "buttonBi";
            this.buttonBi.Size = new System.Drawing.Size(35, 35);
            this.buttonBi.TabIndex = 93;
            this.buttonBi.Tag = "Bi";
            this.buttonBi.Text = "O";
            this.buttonBi.UseVisualStyleBackColor = false;
            // 
            // button49
            // 
            this.button49.BackColor = System.Drawing.Color.Gray;
            this.button49.Enabled = false;
            this.button49.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button49.Location = new System.Drawing.Point(282, 51);
            this.button49.Name = "button49";
            this.button49.Size = new System.Drawing.Size(35, 35);
            this.button49.TabIndex = 92;
            this.button49.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(316, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 35);
            this.button2.TabIndex = 127;
            this.button2.Tag = "Ij";
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Gray;
            this.button9.Enabled = false;
            this.button9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button9.Location = new System.Drawing.Point(316, 357);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(35, 35);
            this.button9.TabIndex = 126;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.White;
            this.button17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button17.Location = new System.Drawing.Point(282, 357);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(35, 35);
            this.button17.TabIndex = 125;
            this.button17.Tag = "Ji";
            this.button17.Text = "X";
            this.button17.UseVisualStyleBackColor = false;
            // 
            // button25
            // 
            this.button25.BackColor = System.Drawing.Color.Gray;
            this.button25.Enabled = false;
            this.button25.Location = new System.Drawing.Point(282, 323);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(35, 35);
            this.button25.TabIndex = 124;
            this.button25.UseVisualStyleBackColor = false;
            // 
            // buttonIh
            // 
            this.buttonIh.BackColor = System.Drawing.Color.White;
            this.buttonIh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonIh.Location = new System.Drawing.Point(248, 323);
            this.buttonIh.Name = "buttonIh";
            this.buttonIh.Size = new System.Drawing.Size(35, 35);
            this.buttonIh.TabIndex = 123;
            this.buttonIh.Tag = "Ih";
            this.buttonIh.Text = "X";
            this.buttonIh.UseVisualStyleBackColor = false;
            // 
            // button41
            // 
            this.button41.BackColor = System.Drawing.Color.Gray;
            this.button41.Enabled = false;
            this.button41.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button41.Location = new System.Drawing.Point(248, 357);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(35, 35);
            this.button41.TabIndex = 122;
            this.button41.UseVisualStyleBackColor = false;
            // 
            // buttonJg
            // 
            this.buttonJg.BackColor = System.Drawing.Color.White;
            this.buttonJg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonJg.Location = new System.Drawing.Point(214, 357);
            this.buttonJg.Name = "buttonJg";
            this.buttonJg.Size = new System.Drawing.Size(35, 35);
            this.buttonJg.TabIndex = 121;
            this.buttonJg.Tag = "Jg";
            this.buttonJg.Text = "X";
            this.buttonJg.UseVisualStyleBackColor = false;
            // 
            // button48
            // 
            this.button48.BackColor = System.Drawing.Color.Gray;
            this.button48.Enabled = false;
            this.button48.Location = new System.Drawing.Point(214, 323);
            this.button48.Name = "button48";
            this.button48.Size = new System.Drawing.Size(35, 35);
            this.button48.TabIndex = 120;
            this.button48.UseVisualStyleBackColor = false;
            // 
            // buttonIf
            // 
            this.buttonIf.BackColor = System.Drawing.Color.White;
            this.buttonIf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonIf.Location = new System.Drawing.Point(181, 323);
            this.buttonIf.Name = "buttonIf";
            this.buttonIf.Size = new System.Drawing.Size(35, 35);
            this.buttonIf.TabIndex = 119;
            this.buttonIf.Tag = "If";
            this.buttonIf.Text = "X";
            this.buttonIf.UseVisualStyleBackColor = false;
            // 
            // button51
            // 
            this.button51.BackColor = System.Drawing.Color.Gray;
            this.button51.Enabled = false;
            this.button51.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button51.Location = new System.Drawing.Point(181, 357);
            this.button51.Name = "button51";
            this.button51.Size = new System.Drawing.Size(35, 35);
            this.button51.TabIndex = 118;
            this.button51.UseVisualStyleBackColor = false;
            // 
            // buttonJe
            // 
            this.buttonJe.BackColor = System.Drawing.Color.White;
            this.buttonJe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonJe.Location = new System.Drawing.Point(147, 357);
            this.buttonJe.Name = "buttonJe";
            this.buttonJe.Size = new System.Drawing.Size(35, 35);
            this.buttonJe.TabIndex = 117;
            this.buttonJe.Tag = "Je";
            this.buttonJe.Text = "X";
            this.buttonJe.UseVisualStyleBackColor = false;
            // 
            // button53
            // 
            this.button53.BackColor = System.Drawing.Color.Gray;
            this.button53.Enabled = false;
            this.button53.Location = new System.Drawing.Point(147, 323);
            this.button53.Name = "button53";
            this.button53.Size = new System.Drawing.Size(35, 35);
            this.button53.TabIndex = 116;
            this.button53.UseVisualStyleBackColor = false;
            // 
            // buttonId
            // 
            this.buttonId.BackColor = System.Drawing.Color.White;
            this.buttonId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonId.Location = new System.Drawing.Point(113, 323);
            this.buttonId.Name = "buttonId";
            this.buttonId.Size = new System.Drawing.Size(35, 35);
            this.buttonId.TabIndex = 115;
            this.buttonId.Tag = "Id";
            this.buttonId.Text = "X";
            this.buttonId.UseVisualStyleBackColor = false;
            // 
            // button55
            // 
            this.button55.BackColor = System.Drawing.Color.Gray;
            this.button55.Enabled = false;
            this.button55.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button55.Location = new System.Drawing.Point(113, 357);
            this.button55.Name = "button55";
            this.button55.Size = new System.Drawing.Size(35, 35);
            this.button55.TabIndex = 114;
            this.button55.UseVisualStyleBackColor = false;
            // 
            // buttonJc
            // 
            this.buttonJc.BackColor = System.Drawing.Color.White;
            this.buttonJc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonJc.Location = new System.Drawing.Point(79, 357);
            this.buttonJc.Name = "buttonJc";
            this.buttonJc.Size = new System.Drawing.Size(35, 35);
            this.buttonJc.TabIndex = 113;
            this.buttonJc.Tag = "Jc";
            this.buttonJc.Text = "X";
            this.buttonJc.UseVisualStyleBackColor = false;
            // 
            // button57
            // 
            this.button57.BackColor = System.Drawing.Color.Gray;
            this.button57.Enabled = false;
            this.button57.Location = new System.Drawing.Point(79, 323);
            this.button57.Name = "button57";
            this.button57.Size = new System.Drawing.Size(35, 35);
            this.button57.TabIndex = 112;
            this.button57.UseVisualStyleBackColor = false;
            // 
            // buttonIb
            // 
            this.buttonIb.BackColor = System.Drawing.Color.White;
            this.buttonIb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonIb.Location = new System.Drawing.Point(46, 323);
            this.buttonIb.Name = "buttonIb";
            this.buttonIb.Size = new System.Drawing.Size(35, 35);
            this.buttonIb.TabIndex = 111;
            this.buttonIb.Tag = "Ib";
            this.buttonIb.Text = "X";
            this.buttonIb.UseVisualStyleBackColor = false;
            // 
            // button59
            // 
            this.button59.BackColor = System.Drawing.Color.Gray;
            this.button59.Enabled = false;
            this.button59.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button59.Location = new System.Drawing.Point(46, 357);
            this.button59.Name = "button59";
            this.button59.Size = new System.Drawing.Size(35, 35);
            this.button59.TabIndex = 110;
            this.button59.UseVisualStyleBackColor = false;
            // 
            // buttonJa
            // 
            this.buttonJa.BackColor = System.Drawing.Color.White;
            this.buttonJa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonJa.Location = new System.Drawing.Point(12, 357);
            this.buttonJa.Name = "buttonJa";
            this.buttonJa.Size = new System.Drawing.Size(35, 35);
            this.buttonJa.TabIndex = 109;
            this.buttonJa.Tag = "Ja";
            this.buttonJa.Text = "X";
            this.buttonJa.UseVisualStyleBackColor = false;
            // 
            // button61
            // 
            this.button61.BackColor = System.Drawing.Color.Gray;
            this.button61.Enabled = false;
            this.button61.Location = new System.Drawing.Point(12, 323);
            this.button61.Name = "button61";
            this.button61.Size = new System.Drawing.Size(35, 35);
            this.button61.TabIndex = 108;
            this.button61.UseVisualStyleBackColor = false;
            // 
            // FormGameBoard
            // 
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(361, 404);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button25);
            this.Controls.Add(this.buttonIh);
            this.Controls.Add(this.button41);
            this.Controls.Add(this.buttonJg);
            this.Controls.Add(this.button48);
            this.Controls.Add(this.buttonIf);
            this.Controls.Add(this.button51);
            this.Controls.Add(this.buttonJe);
            this.Controls.Add(this.button53);
            this.Controls.Add(this.buttonId);
            this.Controls.Add(this.button55);
            this.Controls.Add(this.buttonJc);
            this.Controls.Add(this.button57);
            this.Controls.Add(this.buttonIb);
            this.Controls.Add(this.button59);
            this.Controls.Add(this.buttonJa);
            this.Controls.Add(this.button61);
            this.Controls.Add(this.buttonGJ);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.buttonHi);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.buttonEj);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.buttonCj);
            this.Controls.Add(this.button33);
            this.Controls.Add(this.buttonAj);
            this.Controls.Add(this.button39);
            this.Controls.Add(this.buttonFi);
            this.Controls.Add(this.button43);
            this.Controls.Add(this.buttonDi);
            this.Controls.Add(this.button47);
            this.Controls.Add(this.buttonBi);
            this.Controls.Add(this.button49);
            this.Controls.Add(this.labelPlayer2Score);
            this.Controls.Add(this.labelPlayer1Score);
            this.Controls.Add(this.labelPlayer2Name);
            this.Controls.Add(this.labelPlayer1Name);
            this.Controls.Add(this.buttonGh);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonHg);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.buttonGf);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.buttonHe);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.buttonGd);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.buttonHc);
            this.Controls.Add(this.button23);
            this.Controls.Add(this.buttonGb);
            this.Controls.Add(this.button27);
            this.Controls.Add(this.button29);
            this.Controls.Add(this.button31);
            this.Controls.Add(this.buttonEh);
            this.Controls.Add(this.button35);
            this.Controls.Add(this.buttonCh);
            this.Controls.Add(this.button38);
            this.Controls.Add(this.buttonAh);
            this.Controls.Add(this.button40);
            this.Controls.Add(this.buttonFg);
            this.Controls.Add(this.button42);
            this.Controls.Add(this.buttonDg);
            this.Controls.Add(this.button44);
            this.Controls.Add(this.buttonBg);
            this.Controls.Add(this.button46);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormGameBoard";
            this.Text = "Checkers";
            this.Load += new System.EventHandler(this._Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void _Load(object sender, EventArgs e)
        {

        }
    }
}
