using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaAgitated
{
    public class SeaAgitatedForm : Form
    {
        public SeaAgitatedForm()
        {
            this.Width = 1920;
            this.Height = 1080;
            Text = "Море волнуется";
            BackgroundImage = Backgrounds.menuBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            StartPosition = FormStartPosition.CenterScreen;
            Icon = Systems.icon;

            Load += (sender, eventArgs) => OnSizeChanged(EventArgs.Empty);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint, true);

            UpdateStyles();

            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show("Действительно закрыть?", ":С", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };

            TextForGame.CreateArray();
            StartMenuButtons();
            StartMenuControl();
        }

        public void StartMenuView()
        {
            BackgroundImage = Backgrounds.menuBackground;
            start.Show();
            quit.Show();
            CreateLabel(phrase: TextForGame.textForLabel[0], x: 20, y: 30, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 35, width: 1500, height: 400, millisecond: 100, contentAlignment: ContentAlignment.TopLeft);
            Counts.accessLevel = 1;
        }

        public void StartMenuButtons()
        {
            start = new PictureBox();
            start.BackColor = Color.Transparent;
            start.BackgroundImage = Systems.startGame;
            start.BackgroundImageLayout = ImageLayout.Stretch;

            quit = new PictureBox();
            quit.BackColor = Color.Transparent;
            quit.BackgroundImage = Systems.quit;
            quit.BackgroundImageLayout = ImageLayout.Stretch;

            start.Location = new Point(SizeAndLOcation.locationXMenuButtons, SizeAndLOcation.locationYMenuButtons);
            start.Size = new Size(SizeAndLOcation.widthMenuButtons, SizeAndLOcation.heightMenuButtons);
            quit.Location = new Point(SizeAndLOcation.locationXMenuButtons, 
                SizeAndLOcation.locationYMenuButtons + SizeAndLOcation.heightMenuButtons + SizeAndLOcation.distanceMenuButtons);
            quit.Size = new Size(SizeAndLOcation.widthMenuButtons, SizeAndLOcation.heightMenuButtons);

            CreateLabel(phrase: TextForGame.textForLabel[0], x: 20, y: 30, colorFront: Color.FromArgb(198, 185, 210), 
                fontSize: 35, width: 1500, height: 400, millisecond: 100, contentAlignment: ContentAlignment.TopLeft);
        }

        public void StartMenuControl()
        {
            start.MouseEnter += new EventHandler(this.StartEnter);
            start.MouseLeave += new EventHandler(this.StartLeave);
            start.Click += new EventHandler(this.StartClick);
            Controls.Add(start);

            quit.MouseEnter += new EventHandler(this.QuitEnter);
            quit.MouseLeave += new EventHandler(this.QuitLeave);
            quit.Click += new EventHandler(this.QuitClick);
            Controls.Add(quit);
        }

        public void HideMenuButton()
        {
            start.Hide();
            quit.Hide();
            outputLabel.Hide();
        }

        public void ShowMenuButton()
        {
            start.Show();
            quit.Show();
            outputLabel.Show();
        }

        private void StartEnter(object sender, EventArgs eventArgs) => start.BackgroundImage = Systems.startGameColor;
        private void StartLeave(object sender, EventArgs eventArgs) => start.BackgroundImage = Systems.startGame;
        private void StartClick(object sender, EventArgs eventArgs)
        {
            HideMenuButton();
            LevelsView();
            OpenMap();
            CreateSkipButton();
        }

        private void QuitEnter(object sender, EventArgs eventArgs) => quit.BackgroundImage = Systems.quitColor;
        private void QuitLeave(object sender, EventArgs eventArgs) => quit.BackgroundImage = Systems.quit;
        private void QuitClick(object sender, EventArgs eventArgs) => Close();

        public void OpenMap()
        {
            BackgroundImage = Backgrounds.Map;
            ShowLevels();
            AccessLevel();
        }

        public void LevelsView()
        {
            level1 = new PictureBox();
            level1.BackColor = Color.Transparent;
            level1.BackgroundImage = Systems.level1;
            level1.BackgroundImageLayout = ImageLayout.Stretch;

            level2 = new PictureBox();
            level2.BackColor = Color.Transparent;
            level2.BackgroundImage = Systems.level2;
            level2.BackgroundImageLayout = ImageLayout.Stretch;

            level3 = new PictureBox();
            level3.BackColor = Color.Transparent;
            level3.BackgroundImage = Systems.level3;
            level3.BackgroundImageLayout = ImageLayout.Stretch;

            level1.Size = new Size(SizeAndLOcation.widthLevels, SizeAndLOcation.heightLevels);
            level1.Location = new Point(350, 350);
            level2.Size = new Size(SizeAndLOcation.widthLevels, SizeAndLOcation.heightLevels);
            level2.Location = new Point(800, 180);
            level3.Size = new Size(SizeAndLOcation.widthLevels, SizeAndLOcation.heightLevels);
            level3.Location = new Point(1000, 470);

            LevelsControl();
        }

        public void LevelsControl()
        {
            Controls.Add(level1);
            Controls.Add(level2);
            Controls.Add(level3);
        }

        public void AccessLevel()
        {
            if (Counts.accessLevel == 1)
            {
                level1.MouseEnter += new EventHandler(this.Level1Enter);
                level1.MouseLeave += new EventHandler(this.Level1Leave);
                level1.Click += new EventHandler(this.Level1Click);
            }

            if (Counts.accessLevel == 2)
            {
                level2.MouseEnter += new EventHandler(this.Level2Enter);
                level2.MouseLeave += new EventHandler(this.Level2Leave);
                level2.Click += new EventHandler(this.Level2Click);
            }

            if (Counts.accessLevel == 3)
            {
                level3.MouseEnter += new EventHandler(this.Level3Enter);
                level3.MouseLeave += new EventHandler(this.Level3Leave);
                level3.Click += new EventHandler(this.Level3Click);
            }
        }

        private void Level1Enter(object sender, EventArgs eventArgs) => level1.BackgroundImage = Systems.level1Color;
        private void Level1Leave(object sender, EventArgs eventArgs) => level1.BackgroundImage = Systems.level1;
        private void Level1Click(object sender, EventArgs eventArgs) => OpenLevel1View();

        private void Level2Enter(object sender, EventArgs eventArgs) => level2.BackgroundImage = Systems.level2Color;
        private void Level2Leave(object sender, EventArgs eventArgs) => level2.BackgroundImage = Systems.level2;
        private void Level2Click(object sender, EventArgs eventArgs) => OpenLevel2View();

        private void Level3Enter(object sender, EventArgs eventArgs) =>level3.BackgroundImage = Systems.level3Color;
        private void Level3Leave(object sender, EventArgs eventArgs) =>level3.BackgroundImage = Systems.level3;
        private void Level3Click(object sender, EventArgs eventArgs) => OpenLevel3View();

        public void CreateLabel(String phrase, int x, int y, Color colorFront, int fontSize, int width, int height, int millisecond, ContentAlignment contentAlignment)
        {
            outputLabel = new Label();
            Controls.Add(outputLabel);
            outputLabel.Font = new Font("Christmas Reign PERSONAL", fontSize, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            outputLabel.Location = new Point(x, y);
            outputLabel.Size = new Size(width, height);
            outputLabel.BackColor = Color.Transparent;
            outputLabel.ForeColor = colorFront;
            outputLabel.TextAlign = contentAlignment;
            StartOutputText(phrase, millisecond);
        }

        public void HideLevels()
        {
            level1.Hide();
            level2.Hide();
            level3.Hide();
        }

        public void ShowLevels()
        {
            level1.Show();
            level2.Show();
            level3.Show();
        }

        public void StartOutputText(String phrase, int millisecond)
        {
            outputPhrase = phrase.ToCharArray();
            Counts.countLetter = 0;
            var timer = new Timer();
            timer.Interval = millisecond;
            timer.Tick += new EventHandler(this.OutputText);
            timer.Start();
            if (Counts.countLetter == outputPhrase.Length)
                timer.Stop();

        }

        private void OutputText(object sender, EventArgs eventArgs)
        {
            if (Counts.countLetter < outputPhrase.Length)
                outputLabel.Text += outputPhrase[Counts.countLetter];
            Counts.countLetter++;
        }

        public void CreateNextButton()
        {
            next = new PictureBox();
            next.BackColor = Color.Transparent;
            next.BackgroundImage = Systems.next;
            next.BackgroundImageLayout = ImageLayout.Stretch;
            next.Size = new Size(SizeAndLOcation.widthNovell, SizeAndLOcation.heightNovell);
            next.Location = new Point(SizeAndLOcation.locationXnovell1, SizeAndLOcation.locationYnovell);
            NextButtonControl();
        }

        public void NextButtonControl()
        {
            Controls.Add(next);
            next.MouseEnter += new EventHandler(this.NextMouseEnter);
            next.MouseLeave += new EventHandler(this.NextMouseLeave);
            next.Click += new EventHandler(this.NextClick);
        }

        private void NextMouseEnter(object sender, EventArgs eventArgs) => next.BackgroundImage = Systems.nextColor;
        private void NextMouseLeave(object sender, EventArgs eventArgs) => next.BackgroundImage = Systems.next;
        private void NextClick(object sender, EventArgs eventArgs)
        {
            Counts.countNovell++;
            novellLabel.Text = "";

            if (Counts.countNovell <= 4)
                FollArrayNovell();
            if (Counts.countNovell == 5)
                EndNovell1View();
            if ((Counts.countNovell > 5) & (Counts.countNovell <= 66))
                FollArrayNovell();
            if (Counts.countNovell == 67)
                EndNovell2View();
            if ((Counts.countNovell > 67) & (Counts.countNovell <= 115))
                FollArrayNovell();
            if (Counts.countNovell == 116)
                EndNovell3View();
        }

        public void CreateSkipButton()
        {
            skip = new PictureBox();
            skip.BackColor = Color.Transparent;
            skip.BackgroundImage = Systems.skip;
            skip.BackgroundImageLayout = ImageLayout.Stretch;
            skip.Visible = false;
            skip.Size = new Size(SizeAndLOcation.widthNovell, SizeAndLOcation.heightNovell);
            SkipButtonControl();
        }

        public void SkipButtonControl()
        {
            Controls.Add(skip);
            skip.MouseEnter += new EventHandler(this.SkipMouseEnter);
            skip.MouseLeave += new EventHandler(this.SkipMouseLeave);
        }

        private void SkipMouseEnter(object sender, EventArgs eventArgs) => skip.BackgroundImage = Systems.skipColor;
        private void SkipMouseLeave(object sender, EventArgs eventArgs) => skip.BackgroundImage = Systems.skip;

        private void SkipClickStartNovell1(object sender, EventArgs eventArgs) => StartNovell1View();
        private void SkipClickEndNovell1(object sender, EventArgs eventArgs) => EndNovell1View();
        private void CloseLevel1(object sender, EventArgs eventArgs)
        {
            outputLabel.Hide();
            skip.Hide();
            Counts.accessLevel = 2;
            OpenMap();
        }

        private void SkipClickStartNovell2(object sender, EventArgs eventArgs) => StartNovell2View();
        private void SkipClickEndNovell2(object sender, EventArgs eventArgs) => EndNovell2View();
        private void CloseLevel2(object sender, EventArgs eventArgs)
        {
            outputLabel.Hide();
            skip.Hide();
            Counts.accessLevel = 3;
            OpenMap();
        }

        private void SkipClickStartNovell3(object sender, EventArgs eventArgs) => StartNovell3View();
        private void SkipClickEndNovell3(object sender, EventArgs eventArgs) => EndNovell3View();
        private void CloseLevel3(object sender, EventArgs eventArgs)
        {
            outputLabel.Hide();
            Epilogue1View();
        }

        private void CloseEpilogue1(object sender, EventArgs eventArgs) => Epilogue2View();
        private void CloseEpilogue2(object sender, EventArgs eventArgs)
        {
            mermaid.Hide();
            outputLabel.Hide();
            skip.Hide();
            StartMenuView();
        }

        public void OpenLevel1View()
        {
            HideLevels();
            skip.Show();
            skip.Location = new Point(SizeAndLOcation.locationXnovell1, SizeAndLOcation.locationYnovell);
            BackgroundImage = Backgrounds.black;
            outputLabel.SendToBack();
            CreateLabel(phrase: TextForGame.textForLabel[1], x: 0, y: 60, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 40, width: 1500, height: 700, millisecond: 100, contentAlignment: ContentAlignment.MiddleCenter);
            OpenLevel1Model();
        }

        public void OpenLevel1Model()
        {
            Counts.countNovell = 0;
            ClearClick();
            skip.Click += new EventHandler(this.SkipClickStartNovell1);
        }

        public void OpenLevel2View()
        {
            HideLevels();
            BackgroundImage = Backgrounds.black;
            skip.Location = new Point(SizeAndLOcation.locationXnovell1, SizeAndLOcation.locationYnovell);
            outputLabel.SendToBack();
            CreateLabel(phrase: TextForGame.textForLabel[3], x: 0, y: 60, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 40, width: 1500, height: 700, millisecond: 100, contentAlignment: ContentAlignment.MiddleCenter);
            skip.Show();
            OpenLevel2Model();
        }

        public void OpenLevel2Model()
        {
            Counts.countNovell = 5;
            ClearClick();
            skip.Click += new EventHandler(this.SkipClickStartNovell2);
        }

        public void OpenLevel3View()
        {
            HideLevels();
            BackgroundImage = Backgrounds.black;
            outputLabel.SendToBack();
            CreateLabel(phrase: TextForGame.textForLabel[5], x: 0, y: 60, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 40, width: 1500, height: 700, millisecond: 100, contentAlignment: ContentAlignment.MiddleCenter);
            skip.Show();
            skip.Location = new Point(SizeAndLOcation.locationXnovell1, SizeAndLOcation.locationYnovell);
            OpenLevel3Model();
        }

        public void OpenLevel3Model()
        {
            Counts.countNovell = 67;
            ClearClick();
            skip.Click += new EventHandler(this.SkipClickStartNovell3);
        }

        private void StartNovell1View()
        {
            outputLabel.Hide();
            BackgroundImage = Backgrounds.underwaterBackgroundOne;
            CreateLabelForNovell();
            ShowMermaid();
            StartNovell1Model();
        }

        private void StartNovell2View()
        {
            outputLabel.Hide();
            ShowSailor();
            sailor.Hide();
            BackgroundImage = Backgrounds.port;
            CreateLabelForNovell();
            StartNovell2Model();
        }

        private void StartNovell3View()
        {
            outputLabel.Hide();
            BackgroundImage = Backgrounds.shipFog;
            CreateLabelForNovell();
            sailor.SendToBack();
            sailor.Show();
            StartNovell3Model();
        }

        private void StartNovell1Model()
        {
            CreateNextButton();
            skip.Location = new Point(SizeAndLOcation.locationXnovell2, SizeAndLOcation.locationYnovell);
            FollArrayNovell();
            ClearClick();
            skip.Click += new EventHandler(this.SkipClickEndNovell1);
        }

        public void StartNovell2Model()
        {
            next.Show();
            skip.Location = new Point(SizeAndLOcation.locationXnovell2, SizeAndLOcation.locationYnovell);
            FollArrayNovell();
            ClearClick();
            skip.Click += new EventHandler(this.SkipClickEndNovell2);
        }

        public void StartNovell3Model()
        {
            next.Show();
            skip.Location = new Point(SizeAndLOcation.locationXnovell2, SizeAndLOcation.locationYnovell);
            FollArrayNovell();
            ClearClick();
            skip.Click += new EventHandler(this.SkipClickEndNovell3);
        }

        public void EndNovell1View()
        {
            EndNovell1Model();
            BackgroundImage = Backgrounds.black;
            novellLabel.Hide();
            mermaid.Hide();
            next.Hide();
            CreateLabel(phrase: TextForGame.textForLabel[2], x: 0, y: 60, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 40, width: 1500, height: 700, millisecond: 100, contentAlignment: ContentAlignment.MiddleCenter);
        }

        public void EndNovell2View()
        {
            EndNovell2Model();
            BackgroundImage = Backgrounds.black;
            novellLabel.Hide();
            if (Counts.countNovell > 7)
                sailor.Hide();
            if((Counts.countNovell > 6) & (Counts.countNovell < 67))
            {
                mermaid.Hide();
                man.Hide();
            };
            next.Hide();
            CreateLabel(phrase: TextForGame.textForLabel[4], x: 0, y: 60, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 40, width: 1500, height: 700, millisecond: 100, contentAlignment: ContentAlignment.MiddleCenter);
        }

        public void EndNovell3View()
        {
            EndNovell3Model();
            BackgroundImage = Backgrounds.black;
            novellLabel.Hide();
            sailor.Hide();
            mermaid.Hide();
            next.Hide();
            CreateLabel(phrase: TextForGame.textForLabel[6], x: 0, y: 60, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 40, width: 1500, height: 700, millisecond: 100, contentAlignment: ContentAlignment.MiddleCenter);
        }

        public void EndNovell1Model()
        {
            skip.Location = new Point(SizeAndLOcation.locationXnovell1, SizeAndLOcation.locationYnovell);
            ClearClick();
            skip.Click += new EventHandler(this.CloseLevel1);
        }

        public void EndNovell2Model()
        {
            skip.Location = new Point(SizeAndLOcation.locationXnovell1, SizeAndLOcation.locationYnovell);
            ClearClick();
            skip.Click += new EventHandler(this.CloseLevel2);
        }

        public void EndNovell3Model()
        {
            skip.Location = new Point(SizeAndLOcation.locationXnovell1, SizeAndLOcation.locationYnovell);
            ClearClick();
            skip.Click += new EventHandler(this.CloseLevel3);
        }

        public void Epilogue1View()
        {
            BackgroundImage = Backgrounds.shipDark;
            sailor.Show();
            sailor.Location = new Point(230, 150);
            sailor.BackgroundImage = Heroes.sailorCalm;
            skip.Location = new Point(1300, SizeAndLOcation.locationYnovell);
            Epilogue1Model();
            CreateLabel(phrase: TextForGame.textForLabel[7], x: 630, y: 0, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 20, width: 900, height: 1000, millisecond: 100, contentAlignment: ContentAlignment.TopRight);
            outputLabel.SendToBack();
        }

        public void Epilogue1Model()
        {
            ClearClick();
            skip.Click += new EventHandler(this.CloseEpilogue1);
        }

        public void Epilogue2View()
        {
            Epilogue2Model();
            sailor.Hide();
            outputLabel.Hide();
            BackgroundImage = Backgrounds.castle;
            mermaid.Show();
            mermaid.Location = new Point(950, 150);
            mermaid.Size = new Size(600, 700);
            skip.Location = new Point(0, SizeAndLOcation.locationYnovell);
            CreateLabel(phrase: TextForGame.textForLabel[8], x: 0, y: 0, colorFront: Color.FromArgb(198, 185, 210),
                fontSize: 20, width: 900, height: 1000, millisecond: 100, contentAlignment: ContentAlignment.TopLeft);
            outputLabel.SendToBack();
        }

        public void Epilogue2Model()
        {
            ClearClick();
            skip.Click += new EventHandler(this.CloseEpilogue2);
        }

        public void CreateLabelForNovell()
        {
            novellLabel = new Label();
            novellLabel.BackgroundImage = Systems.textBackground;
            novellLabel.BackgroundImageLayout = ImageLayout.Stretch;
            novellLabel.Font = new Font("Christmas Reign PERSONAL", 20, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            novellLabel.ForeColor = Color.Black;
            novellLabel.Size = new Size(1400, 170);
            novellLabel.Location = new Point(68, 610);
            novellLabel.Padding = new Padding(50, 18, 30, 0);
            Controls.Add(novellLabel);
        }

        public void ShowMermaid()
        {
            mermaid = new PictureBox();
            mermaid.BackgroundImage = Heroes.mermaidCalm;
            mermaid.BackColor = Color.Transparent;
            mermaid.BackgroundImageLayout = ImageLayout.Stretch;
            mermaid.Location = new Point(950, 225);
            mermaid.Size = new Size(450, 550);
            Controls.Add(mermaid);
        }

        public void ShowSailor()
        {
            sailor = new PictureBox();
            sailor.BackgroundImage = Heroes.sailorAngry;
            sailor.BackColor = Color.Transparent;
            sailor.BackgroundImageLayout = ImageLayout.Stretch;
            sailor.Location = new Point(150, 50);
            sailor.Size = new Size(350, 700);
            Controls.Add(sailor);
        }

        public void ShowMan()
        {
            man = new PictureBox();
            man.BackgroundImage = Heroes.man;
            man.BackColor = Color.Transparent;
            man.BackgroundImageLayout = ImageLayout.Stretch;
            man.Location = new Point(550, 150);
            man.Size = new Size(400, 550);
            Controls.Add(man);
        }

        public void StartOutputTextNovell(String phrase)
        {
            outputPhraseNovell = phrase.ToCharArray();
            Counts.countLetterNovell = 0;
            var timer = new Timer();
            timer.Interval = 75;
            timer.Tick += new EventHandler(this.OutputTextNovell);
            timer.Start();
            if (Counts.countLetterNovell == outputPhraseNovell.Length)
                timer.Stop();
        }

        private void OutputTextNovell(object sender, EventArgs eventArgs)
        {
            if (Counts.countLetterNovell < outputPhraseNovell.Length)
                novellLabel.Text += outputPhraseNovell[Counts.countLetterNovell];
            Counts.countLetterNovell++;
        }

        public void FollArrayNovell()
        {
            StartOutputTextNovell(TextForGame.textForNovell[Counts.countNovell]);
            NovellView();
        }

        public void NovellView()
        {
            if (Counts.countNovell == 2)
                BackgroundImage = Backgrounds.underwaterBackgroundTwo;
            if (Counts.countNovell == 7) 
                ShowMan();
            if (Counts.countNovell == 8) 
                ShowSailor();
            if (Counts.countNovell == 11)
                sailor.BackgroundImage = Heroes.sailorHappy;
            if (Counts.countNovell == 12)
            {
                mermaid.SendToBack();
                mermaid.Show();
                man.Hide();
            };
            if (Counts.countNovell == 25)
            {
                mermaid.BackgroundImage = Heroes.mermaidSurprise;
                sailor.BackgroundImage = Heroes.sailorCalm;
            };
            if (Counts.countNovell == 39)
                mermaid.BackgroundImage = Heroes.mermaidCalm;
            if (Counts.countNovell == 58)
                sailor.Hide();
            if (Counts.countNovell == 61)
            {
                BackgroundImage = Backgrounds.ship;
                mermaid.Hide();
                sailor.Show();
            };
            if (Counts.countNovell == 71)
            {
                mermaid.SendToBack();
                mermaid.Show();
            };
            if (Counts.countNovell == 86)
                mermaid.BackgroundImage = Heroes.mermaidSurprise;
            if (Counts.countNovell == 88)
            {
                BackgroundImage = Backgrounds.rocks;
                mermaid.BackgroundImage = Heroes.mermaidCalm;
            }
            if (Counts.countNovell == 93)
                sailor.BackgroundImage = Heroes.sailorSad;

        }

        public void ClearClick()
        {
            skip.Click -= CloseLevel1;
            skip.Click -= CloseLevel2;
            skip.Click -= CloseLevel3;
            skip.Click -= SkipClickStartNovell1;
            skip.Click -= SkipClickStartNovell2;
            skip.Click -= SkipClickStartNovell3;
            skip.Click -= SkipClickEndNovell1;
            skip.Click -= SkipClickEndNovell2;
            skip.Click -= SkipClickEndNovell3;
            skip.Click -= CloseEpilogue1;
            skip.Click -= CloseEpilogue2;
        }

        public PictureBox start;
        public PictureBox quit;
        public PictureBox level1;
        public PictureBox level2;
        public PictureBox level3;
        public PictureBox next;
        public PictureBox skip;
        public PictureBox mermaid;
        public PictureBox sailor;
        public PictureBox man;

        public Label outputLabel;
        public Label novellLabel;

        public Char[] outputPhrase;
        public Char[] outputPhraseNovell;
    }
}