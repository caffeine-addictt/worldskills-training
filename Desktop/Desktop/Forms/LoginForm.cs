using System;
using System.Linq;
using System.Windows.Forms;
using Desktop.Models;
using Desktop.Utils;

namespace Desktop.Forms
{
  public partial class LoginForm : Form
  {
    public MainForm MainForm;

    public LoginForm()
    {
      InitializeComponent();
      this.MainForm = new MainForm(this);
    }

    private void loginbutton_Click(object sender, EventArgs e)
    {
      errorLabel.Visible = false;

      //using (ws1Context ctx = new ws1Context())
      //{
      //  Tuple<string, byte[]> hashed = Auth.HashPassword("Admin");
      //  User usr = new User()
      //  {
      //    UserId = ctx.User.Count(),
      //    FullName = "Admin",
      //    Username = "Admin",
      //    Password = hashed.Item1,
      //    Salt = Convert.ToBase64String(hashed.Item2),
      //    Tel = 9,
      //  };
      //  ctx.User.Add(usr);
      //  ctx.SaveChanges();
      //}
      //return;

      if (String.IsNullOrWhiteSpace(usernameTextInput.Text))
      {
        errorLabel.Text = "Username is required!";
        errorLabel.Visible = true;
        return;
      }

      if (String.IsNullOrEmpty(passwordTextInput.Text))
      {
        errorLabel.Text = "Password is required!";
        errorLabel.Visible = true;
        return;
      }

      using (ws1Context ctx = new ws1Context())
      {
        User user = ctx.User.FirstOrDefault(u =>  u.Username == usernameTextInput.Text);
        if (user == null)
        {
          errorLabel.Text = "Invalid username or password!";
          errorLabel.Visible = true;
          return;
        }

        // Check password
        if (!Auth.ComparePassword(user, passwordTextInput.Text))
        {
          errorLabel.Text = "Invalid username or password";
          errorLabel.Visible = true;
          return;
        }

        // Success
        Global.CurrentUser = user;
        errorLabel.Text = "";
        usernameTextInput.Text = "";
        passwordTextInput.Text = "";

        Hide();
        MainForm.Show();
      }
    }
  }
}
