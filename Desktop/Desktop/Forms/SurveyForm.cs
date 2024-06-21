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
  public partial class SurveyForm : Form
  {
    public bool MarkStale = false;
    public DateTime LastFetched { get; set; }
    private List<Survey> DataCache { get; set; }

    public SurveyForm()
    {
      InitializeComponent();
      gridView.AutoGenerateColumns = true;
    }

    private void Populate(List<Survey> data)
    {
      gridView.Columns.Clear();

      List<SurveyRow> formatted = data.Select(s => new SurveyRow {
        Id = s.SurveyId,
        Status = s.Status ? "Active" : DateTime.UtcNow > s.EndDate ? "Ended" : "Inactive",
        Title = s.Title,
        StartDate = s.StartDate,
        EndDate = s.EndDate,
      }).ToList();

      gridView.DataSource = formatted;
    }

    private void RefetchAndPopulate()
    {
      LastFetched = DateTime.UtcNow;
      using (ws1Context ctx = new ws1Context())
      {
        DataCache = ctx.Survey.ToList();
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

    private void SurveyForm_Shown(object sender, EventArgs e)
    {
      if (MarkStale || LastFetched == null || DateTime.UtcNow - LastFetched >= TimeSpan.FromMinutes(5))
      {
        RefetchAndPopulate();
        return;
      }

      Populate(DataCache);
    }
  }


  class SurveyRow
  {
    public int Id { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
