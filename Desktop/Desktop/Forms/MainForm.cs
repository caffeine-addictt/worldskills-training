using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Desktop.Forms
{
  public partial class MainForm : Form
  {
    public LoginForm LoginFormView { get; set; }
    public Dictionary<string, Form> Pages { get; set; }

    public MainForm(LoginForm lf)
    {
      LoginFormView = lf;
      Pages = new Dictionary<string, Form>() {
        { "HomeForm", new HomeForm() { TopLevel = false, FormBorderStyle = FormBorderStyle.None, AutoScroll = true } },
        { "WorkshopForm", new WorkshopForm() { TopLevel = false, FormBorderStyle= FormBorderStyle.None, AutoScroll = true } },
        { "RequestForm", new RequestsForm(this) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, AutoScroll = true } },
        { "SurveyForm", new SurveyForm() { TopLevel = false, FormBorderStyle = FormBorderStyle.None, AutoScroll = true } },
        { "SaloonForm", new SaloonForm() { TopLevel = false, FormBorderStyle = FormBorderStyle.None, AutoScroll = true } },
      };
      
      InitializeComponent();
      pannel.Controls.AddRange(Pages.Values.ToArray());
      SwitchPage("HomeForm");
    }

    public void SwitchPage(string name)
    {
      homeButton.Enabled = true;
      workshopButton.Enabled = true;
      workshopRequestButton.Enabled = true;
      surveyButton.Enabled = true;
      saloonButton.Enabled = true;

      switch (name)
      {
        case "HomeForm":
          homeButton.Enabled = false;
          break;

        case "WorkshopForm":
          workshopButton.Enabled = false;
          break;

        case "RequestForm":
          workshopRequestButton.Enabled = false;
          break;

        case "SurveyForm":
          surveyButton.Enabled = false;
          break;

        case "SaloonForm":
          saloonButton.Enabled = false;
          break;
      }

      foreach (KeyValuePair<string, Form> a in Pages)
      {
        if (a.Key == name)
          a.Value.Show();
        else
          a.Value.Hide();
      }
    }

    private void workshopRequestsButton_Click(object sender, EventArgs e)
      => SwitchPage("RequestForm");
    private void homeButton_Click(object sender, EventArgs e)
      => SwitchPage("HomeForm");
    private void workshopButton_Click(object sender, EventArgs e)
      => SwitchPage("WorkshopForm");
    private void surveyButton_Click(object sender, EventArgs e)
      => SwitchPage("SurveyForm");
    private void saloonButton_Click(object sender, EventArgs e)
      => SwitchPage("SaloonForm");

    private void logoutButton_Click(object sender, EventArgs e)
    {
      Global.CurrentUser = null;
      LoginFormView.Show();
      this.Hide();
    }
  }
}
