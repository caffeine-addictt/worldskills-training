using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace Desktop.Forms
{
  public partial class RequestsForm : Form, Populatable
  {
    private MainForm mainForm;

    public readonly List<string> Statuses = new List<string>() { "Pending", "Approved", "Rejected" };
    public bool MarkStale = false;
    public DateTime LastFetched { get; set; }
    private List<Workshop> DataCache { get; set; }

    public RequestsForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      InitializeComponent();
      gridView.AutoGenerateColumns = true;
    }

    private void Populate(List<Workshop> data)
    {
      gridView.Columns.Clear();

      DataGridViewButtonColumn approveCol = new DataGridViewButtonColumn()
      {
        Name = "Approve",
        Text = "Approve",
        UseColumnTextForButtonValue = true,
      };
      DataGridViewButtonColumn rejectCol = new DataGridViewButtonColumn()
      {
        Name = "Reject",
        Text = "Reject",
        UseColumnTextForButtonValue = true,
      };
      gridView.Columns.Add(approveCol);
      gridView.Columns.Add(rejectCol);

      List<RequestRow> formatted = data.Select(w => new RequestRow {
        Id = w.WorkshopId,
        Title = w.Title,
        Category = w.Category.CategoryName,
        Saloon = w.Saloon.Name,
        Timeslot = w.Timeslot == 0 ? "10:00-12:00" : w.Timeslot == 1 ? "12:00-14:00" : "14:00-16:00",
        StartDate = w.StartDate,
      }).ToList();

      gridView.DataSource = formatted;
    }

    private void RefetchAndPopulate()
    {
      LastFetched = DateTime.UtcNow;
      using (ws1Context ctx = new ws1Context())
      {
        DataCache = ctx.Workshop
          .Where(w => w.Status == 0)
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

    private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      using (ws1Context ctx = new ws1Context())
      {
        if (e.ColumnIndex != 0 && e.ColumnIndex != 1)
          return;

        int targettingId = DataCache[e.RowIndex].WorkshopId;
        Workshop toChange = ctx.Workshop.FirstOrDefault(w => w.WorkshopId == targettingId);
        if (toChange == null)
        {
          MessageBox.Show("Failed to locate workshop request!", "Error");
          return;
        }

        toChange.Status = e.ColumnIndex + 1;
        ctx.SaveChanges();

        MessageBox.Show("Updated workshop request", "Info");
        MarkStale = true;
        RefetchAndPopulate();
      }
    }

    private void RequestsForm_VisibleChanged(object sender, EventArgs e)
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

  class RequestRow
  {
    public int Id {  get; set; }
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
