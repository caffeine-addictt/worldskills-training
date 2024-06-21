namespace Desktop.Forms
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.pannel = new System.Windows.Forms.Panel();
            this.homeButton = new System.Windows.Forms.Button();
            this.workshopRequestButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.workshopButton = new System.Windows.Forms.Button();
            this.surveyButton = new System.Windows.Forms.Button();
            this.saloonButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pannel
            // 
            this.pannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pannel.Location = new System.Drawing.Point(0, 59);
            this.pannel.Name = "pannel";
            this.pannel.Size = new System.Drawing.Size(1404, 491);
            this.pannel.TabIndex = 0;
            // 
            // homeButton
            // 
            this.homeButton.AutoSize = true;
            this.homeButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.homeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.homeButton.Location = new System.Drawing.Point(0, 0);
            this.homeButton.Margin = new System.Windows.Forms.Padding(0);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(92, 42);
            this.homeButton.TabIndex = 1;
            this.homeButton.Text = "Home";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // workshopRequestButton
            // 
            this.workshopRequestButton.AutoSize = true;
            this.workshopRequestButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.workshopRequestButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.workshopRequestButton.Location = new System.Drawing.Point(191, 0);
            this.workshopRequestButton.Margin = new System.Windows.Forms.Padding(0);
            this.workshopRequestButton.Name = "workshopRequestButton";
            this.workshopRequestButton.Size = new System.Drawing.Size(164, 42);
            this.workshopRequestButton.TabIndex = 3;
            this.workshopRequestButton.Text = "Workshop Requests";
            this.workshopRequestButton.UseVisualStyleBackColor = false;
            this.workshopRequestButton.Click += new System.EventHandler(this.workshopRequestsButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.AutoSize = true;
            this.logoutButton.Location = new System.Drawing.Point(1199, 6);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 30);
            this.logoutButton.TabIndex = 3;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // workshopButton
            // 
            this.workshopButton.AutoSize = true;
            this.workshopButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.workshopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.workshopButton.Location = new System.Drawing.Point(92, 0);
            this.workshopButton.Margin = new System.Windows.Forms.Padding(0);
            this.workshopButton.Name = "workshopButton";
            this.workshopButton.Size = new System.Drawing.Size(99, 42);
            this.workshopButton.TabIndex = 2;
            this.workshopButton.Text = "Workshops";
            this.workshopButton.UseVisualStyleBackColor = false;
            this.workshopButton.Click += new System.EventHandler(this.workshopButton_Click);
            // 
            // surveyButton
            // 
            this.surveyButton.AutoSize = true;
            this.surveyButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.surveyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.surveyButton.Location = new System.Drawing.Point(355, 0);
            this.surveyButton.Margin = new System.Windows.Forms.Padding(0);
            this.surveyButton.Name = "surveyButton";
            this.surveyButton.Size = new System.Drawing.Size(75, 42);
            this.surveyButton.TabIndex = 4;
            this.surveyButton.Text = "Surveys";
            this.surveyButton.UseVisualStyleBackColor = false;
            this.surveyButton.Click += new System.EventHandler(this.surveyButton_Click);
            // 
            // saloonButton
            // 
            this.saloonButton.AutoSize = true;
            this.saloonButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.saloonButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saloonButton.Location = new System.Drawing.Point(430, 0);
            this.saloonButton.Margin = new System.Windows.Forms.Padding(0);
            this.saloonButton.Name = "saloonButton";
            this.saloonButton.Size = new System.Drawing.Size(77, 42);
            this.saloonButton.TabIndex = 5;
            this.saloonButton.Text = "Saloons";
            this.saloonButton.UseVisualStyleBackColor = false;
            this.saloonButton.Click += new System.EventHandler(this.saloonButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 548);
            this.Controls.Add(this.saloonButton);
            this.Controls.Add(this.surveyButton);
            this.Controls.Add(this.workshopButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.workshopRequestButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.pannel);
            this.Name = "MainForm";
            this.Text = "MenuForm";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pannel;
    private System.Windows.Forms.Button homeButton;
    private System.Windows.Forms.Button workshopRequestButton;
    private System.Windows.Forms.Button logoutButton;
    private System.Windows.Forms.Button workshopButton;
    private System.Windows.Forms.Button surveyButton;
    private System.Windows.Forms.Button saloonButton;
  }
}