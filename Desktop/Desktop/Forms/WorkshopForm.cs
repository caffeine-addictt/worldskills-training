using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Desktop.Models;

namespace Desktop.Forms
{
  public partial class WorkshopForm : Form, Populatable
  {
    public readonly List<string> Statuses = new List<string>() { "Pending", "Approved", "Rejected" };
    public bool MarkStale = false;
    public DateTime LastFetched { get; set; }
    private List<Workshop> DataCache { get; set; }

    public WorkshopForm()
    {
      InitializeComponent();
      gridView.AutoGenerateColumns = true;
    }

    protected override void OnShown(EventArgs e)
    {
      if (MarkStale || LastFetched == null || DateTime.UtcNow - LastFetched >= TimeSpan.FromMinutes(5))
      {
        RefetchAndPopulate();
        return;
      }

      Populate(DataCache);
      base.OnShown(e);
    }

    private void Populate(List<Workshop> data)
    {
      gridView.Columns.Clear();

      List<WorkshopRow> formatted = data.Select(w => new WorkshopRow {
        Id = w.WorkshopId,
        Status = Statuses[w.Status],
        Title = w.Title,
        Category = w.Category.CategoryName,
        Saloon = w.Saloon.Name,
        Timeslot = w.Timeslot == 0 ? "10:00-12:00" : w.Timeslot == 1 ? "12:00-14:00" : "14:00-16:00",
        StartDate = w.StartDate,
        EndDate = w.EndDate,
        User = $"{{{w.User.UserId}}} {w.User.FullName}",
        RequestDate = w.RequestDate,
      }).ToList();

      gridView.DataSource = formatted;
    }

    private void RefetchAndPopulate()
    {
      LastFetched = DateTime.UtcNow;
      using (ws1Context ctx = new ws1Context())
      {
        DataCache = ctx.Workshop
          .Include(w => w.User)
          .Include(w => w.Category)
          .Include(w => w.Saloon)
          .ToList();
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

    private void WorkshopForm_VisibleChanged(object sender, EventArgs e)
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


  class WorkshopRow
  {
    public int Id {  get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Saloon { get; set; }
    public string Timeslot { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string User { get; set; }
    public DateTime RequestDate { get; set; }
  }
}
