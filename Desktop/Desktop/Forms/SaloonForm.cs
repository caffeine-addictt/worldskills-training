using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.Models;

namespace Desktop.Forms
{
  public partial class SaloonForm : Form, Populatable
  {
    public bool MarkStale = false;
    public DateTime LastFetched { get; set; }
    private List<Saloon> DataCache { get; set; }

    public SaloonForm()
    {
      InitializeComponent();
      gridView.AutoGenerateColumns = true;
    }

    private void Populate(List<Saloon> data)
    {
      gridView.Columns.Clear();

      List<SaloonRow> formatted = data.Select(s => new SaloonRow {
        Id = s.SaloonId,
        Name = s.Name,
      }).ToList();

      gridView.DataSource = formatted;
    }

    private void RefetchAndPopulate()
    {
      LastFetched = DateTime.UtcNow;
      using (ws1Context ctx = new ws1Context())
      {
        DataCache = ctx.Saloon.ToList();
      }
      Populate(DataCache);
    }

    private void reloadButton_Click(object sender, EventArgs e)
    {
      TimeSpan elapsed = DateTime.UtcNow - LastFetched;
      TimeSpan left =  TimeSpan.FromMinutes(1) - elapsed;

      if (MarkStale || (LastFetched != null && left.Seconds < 0))
      {
        MarkStale = false;
        RefetchAndPopulate();
      }
      else if (LastFetched != null)
      {
        MessageBox.Show($"Refetching too quickly! Try again in {left.Seconds}s", "Too fast!");
      }
      else
      {
        MessageBox.Show("Still fetching... Please wait!", "Be patient!");
      }
    }

    private void SaloonForm_VisibleChanged(object sender, EventArgs e)
    {
      if (!Visible) return;
      if (MarkStale || LastFetched == null || DateTime.UtcNow - LastFetched >= TimeSpan.FromMinutes(5))
      {
        RefetchAndPopulate();
        return;
      }

      Populate(DataCache);
    }
  }


  class SaloonRow
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
