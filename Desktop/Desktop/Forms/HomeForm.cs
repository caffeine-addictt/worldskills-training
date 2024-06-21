using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.Forms
{
  public partial class HomeForm : Form, Populatable
  {
    public DateTime LastFetched { get; set; }

    public HomeForm()
    {
      InitializeComponent();
    }

    protected override void OnShown(EventArgs e)
    {
      headerText.Text = $"Welcome back, {Global.CurrentUser.Username}!";
      base.OnShown(e);
    }
  }
}
