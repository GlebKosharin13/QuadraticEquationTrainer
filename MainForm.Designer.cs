namespace QuadraticEquationTrainer
{
    partial class MainForm
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
            lblEquation = new Label();
            txtX1 = new TextBox();
            txtX2 = new TextBox();
            lblX1Label = new Label();
            lblX2Label = new Label();
            btnCheck = new Button();
            btnShowSolution = new Button();
            btnNew = new Button();
            txtSolution = new TextBox();
            lblStats = new Label();
            btnNoRoots = new Button();
            SuspendLayout();
            // 
            // lblEquation
            // 
            lblEquation.Anchor = AnchorStyles.Top;
            lblEquation.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblEquation.Location = new Point(254, 49);
            lblEquation.Name = "lblEquation";
            lblEquation.Size = new Size(500, 32);
            lblEquation.TabIndex = 0;
            lblEquation.Text = "Нажмите \"Новое уравнение\" для начала";
            lblEquation.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtX1
            // 
            txtX1.Anchor = AnchorStyles.Top;
            txtX1.Font = new Font("Arial", 12F);
            txtX1.Location = new Point(430, 117);
            txtX1.Name = "txtX1";
            txtX1.Size = new Size(50, 26);
            txtX1.TabIndex = 1;
            txtX1.TextAlign = HorizontalAlignment.Center;
            // 
            // txtX2
            // 
            txtX2.Anchor = AnchorStyles.Top;
            txtX2.Font = new Font("Arial", 12F);
            txtX2.Location = new Point(559, 117);
            txtX2.Name = "txtX2";
            txtX2.Size = new Size(50, 26);
            txtX2.TabIndex = 3;
            txtX2.TextAlign = HorizontalAlignment.Center;
            // 
            // lblX1Label
            // 
            lblX1Label.Anchor = AnchorStyles.Top;
            lblX1Label.AutoSize = true;
            lblX1Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblX1Label.ImageAlign = ContentAlignment.BottomLeft;
            lblX1Label.Location = new Point(384, 120);
            lblX1Label.Name = "lblX1Label";
            lblX1Label.Size = new Size(43, 21);
            lblX1Label.TabIndex = 0;
            lblX1Label.Text = "x1 =";
            lblX1Label.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblX2Label
            // 
            lblX2Label.Anchor = AnchorStyles.Top;
            lblX2Label.AutoSize = true;
            lblX2Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblX2Label.ImageAlign = ContentAlignment.BottomLeft;
            lblX2Label.Location = new Point(513, 120);
            lblX2Label.Name = "lblX2Label";
            lblX2Label.Size = new Size(43, 21);
            lblX2Label.TabIndex = 2;
            lblX2Label.Text = "x2 =";
            lblX2Label.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCheck
            // 
            btnCheck.Anchor = AnchorStyles.Top;
            btnCheck.BackColor = Color.FromArgb(128, 255, 128);
            btnCheck.Enabled = false;
            btnCheck.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCheck.Location = new Point(274, 188);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(206, 51);
            btnCheck.TabIndex = 4;
            btnCheck.Text = "Проверить";
            btnCheck.UseVisualStyleBackColor = false;
            btnCheck.Click += btnCheck_Click;
            // 
            // btnShowSolution
            // 
            btnShowSolution.Anchor = AnchorStyles.Top;
            btnShowSolution.BackColor = Color.FromArgb(255, 128, 128);
            btnShowSolution.Enabled = false;
            btnShowSolution.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnShowSolution.Location = new Point(513, 188);
            btnShowSolution.Name = "btnShowSolution";
            btnShowSolution.Size = new Size(206, 51);
            btnShowSolution.TabIndex = 5;
            btnShowSolution.Text = "Показать решение";
            btnShowSolution.UseVisualStyleBackColor = false;
            btnShowSolution.Click += btnShowSolution_Click;
            // 
            // btnNew
            // 
            btnNew.Anchor = AnchorStyles.Top;
            btnNew.BackColor = Color.FromArgb(128, 255, 255);
            btnNew.Cursor = Cursors.Hand;
            btnNew.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnNew.Location = new Point(274, 264);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(445, 51);
            btnNew.TabIndex = 6;
            btnNew.Text = "Новое уравнение";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // txtSolution
            // 
            txtSolution.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSolution.BackColor = SystemColors.Info;
            txtSolution.Font = new Font("Segoe UI", 12F);
            txtSolution.Location = new Point(67, 424);
            txtSolution.Multiline = true;
            txtSolution.Name = "txtSolution";
            txtSolution.ReadOnly = true;
            txtSolution.ScrollBars = ScrollBars.Vertical;
            txtSolution.Size = new Size(874, 358);
            txtSolution.TabIndex = 7;
            // 
            // lblStats
            // 
            lblStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStats.BackColor = Color.LightGray;
            lblStats.BorderStyle = BorderStyle.FixedSingle;
            lblStats.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblStats.Location = new Point(67, 369);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(360, 23);
            lblStats.TabIndex = 8;
            lblStats.Text = "Всего уравнений: 0 (верно: 0, неверно: 0)";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnNoRoots
            // 
            btnNoRoots.Anchor = AnchorStyles.Top;
            btnNoRoots.Enabled = false;
            btnNoRoots.Font = new Font("Segoe UI", 14.25F);
            btnNoRoots.Location = new Point(645, 112);
            btnNoRoots.Name = "btnNoRoots";
            btnNoRoots.Size = new Size(140, 35);
            btnNoRoots.TabIndex = 9;
            btnNoRoots.Text = "Корней нет";
            btnNoRoots.UseVisualStyleBackColor = true;
            btnNoRoots.Visible = false;
            btnNoRoots.Click += btnNoRoots_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 819);
            Controls.Add(btnNoRoots);
            Controls.Add(lblStats);
            Controls.Add(txtSolution);
            Controls.Add(btnNew);
            Controls.Add(btnShowSolution);
            Controls.Add(btnCheck);
            Controls.Add(lblX2Label);
            Controls.Add(lblX1Label);
            Controls.Add(txtX2);
            Controls.Add(txtX1);
            Controls.Add(lblEquation);
            Name = "MainForm";
            Text = "Тренажёр квадратных уравнений";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEquation;
        private TextBox txtX1;
        private TextBox txtX2;
        private Label lblX1Label;
        private Label lblX2Label;
        private Button btnCheck;
        private Button btnShowSolution;
        private Button btnNew;
        private TextBox txtSolution;
        private Label lblStats;
        private Button btnNoRoots;
    }
}
