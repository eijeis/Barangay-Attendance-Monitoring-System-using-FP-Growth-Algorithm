using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BAMS.UserControls
{
    public partial class FpGrowthUC : UserControl
    {
        private string connectionString =
        "Server=localhost;Database=BAMS_DB;User Id=eijeizs;Password=aspiringrapper3;TrustServerCertificate=True;";

        public FpGrowthUC()
        {
            InitializeComponent();

            dgvItems.AutoGenerateColumns = false;
            dgvPatterns.AutoGenerateColumns = false;

            dgvItems.AllowUserToResizeRows = false;
            dgvItems.AllowUserToResizeColumns = false;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.MultiSelect = false;

            dgvPatterns.AllowUserToResizeRows = false;
            dgvPatterns.AllowUserToResizeColumns = false;
            dgvPatterns.AllowUserToAddRows = false;
            dgvPatterns.AllowUserToDeleteRows = false;
            dgvPatterns.ReadOnly = true;
            dgvPatterns.RowHeadersVisible = false;
            dgvPatterns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatterns.MultiSelect = false;

            txtSearchFullName.Text = "Search Full Name";
            txtSearchFullName.ForeColor = System.Drawing.Color.Gray;

            txtSearchFullName.Enter += RemovePlaceholder;
            txtSearchFullName.Leave += SetPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (txtSearchFullName.Text == "Search Full Name")
            {
                txtSearchFullName.Text = "";
                txtSearchFullName.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchFullName.Text))
            {
                txtSearchFullName.Text = "Search Full Name";
                txtSearchFullName.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = dateTimePicker1.Value.Date;
                DateTime toDate = dateTimePicker2.Value.Date;

                string name = txtSearchFullName.Text == "Search Full Name" ? "" : txtSearchFullName.Text;

                DataTable attendance = GetAttendanceData(fromDate, toDate, name);

                if (attendance.Rows.Count == 0)
                {
                    MessageBox.Show("No attendance records found.");
                    return;
                }

                RunFPGrowth(attendance);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private DataTable GetAttendanceData(DateTime from, DateTime to, string name)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        string query = @"SELECT A.*, U.FullName
        //                         FROM Attendance A
        //                         JOIN Users U ON A.UserId = U.Id
        //                         WHERE A.Day BETWEEN @From AND @To
        //                         AND U.FullName LIKE @Name";

        //        SqlCommand cmd = new SqlCommand(query, conn);

        //        cmd.Parameters.AddWithValue("@From", from);
        //        cmd.Parameters.AddWithValue("@To", to);
        //        cmd.Parameters.AddWithValue("@Name", "%" + name + "%");

        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        //        DataTable table = new DataTable();
        //        adapter.Fill(table);

        //        return table;
        //    }
        //}
        private DataTable GetAttendanceData(DateTime from, DateTime to, string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT A.*, U.FullName
                         FROM Attendance A
                         JOIN Users U ON A.EmployeeID = U.EmployeeID
                         WHERE A.Day BETWEEN @From AND @To
                         AND U.FullName LIKE @Name";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
        }

        private List<List<string>> BuildTransactions(DataTable table)
        {
            List<List<string>> transactions = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> items = new List<string>();

                double undertime = row["Undertime"] != DBNull.Value ? Convert.ToDouble(row["Undertime"]) : 0;
                double overtime = row["Overtime"] != DBNull.Value ? Convert.ToDouble(row["Overtime"]) : 0;
                string remarks = row["Remarks"]?.ToString() ?? "";

                //if (remarks == "Present")
                    items.Add("Present");

                if (undertime > 0)
                    items.Add("Undertime");

                if (overtime > 0)
                    items.Add("Overtime");

                if (items.Count > 0)
                    transactions.Add(items);
            }

            return transactions;
        }

        //private void RunFPGrowth(DataTable table)
        //{
        //    var transactions = BuildTransactions(table);

        //    Dictionary<string, int> frequency = new Dictionary<string, int>();

        //    foreach (var t in transactions)
        //    {
        //        foreach (var item in t)
        //        {
        //            if (!frequency.ContainsKey(item))
        //                frequency[item] = 0;

        //            frequency[item]++;
        //        }
        //    }

        //    var frequentItems = frequency
        //        .Select(x => new
        //        {
        //            Item = x.Key,
        //            Support = x.Value
        //        }).ToList();

        //    dgvItems.Rows.Clear();

        //    foreach (var item in frequentItems)
        //    {
        //        dgvItems.Rows.Add(item.Item, item.Support);
        //    }

        //    var patterns = transactions
        //        .Select(t => string.Join(", ", t))
        //        .GroupBy(x => x)
        //        .Select(g => new
        //        {
        //            Pattern = g.Key,
        //            Support = g.Count()
        //        }).ToList();

        //    dgvPatterns.Rows.Clear();

        //    foreach (var pattern in patterns)
        //    {
        //        dgvPatterns.Rows.Add(pattern.Pattern, pattern.Support);
        //    }
        //}

        private void RunFPGrowth(DataTable table)
        {
            var transactions = BuildTransactions(table);

            Dictionary<string, int> frequency = new Dictionary<string, int>();

            foreach (var t in transactions)
            {
                foreach (var item in t)
                {
                    if (!frequency.ContainsKey(item))
                        frequency[item] = 0;

                    frequency[item]++;
                }
            }

            dgvItems.Rows.Clear();

            foreach (var item in frequency)
            {
                dgvItems.Rows.Add(item.Key, item.Value);
            }

            GenerateAssociationRules(transactions);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }

        private void panelItems_Paint(object sender, PaintEventArgs e) { }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void txtSearchFullName_TextChanged(object sender, EventArgs e) { }

        //Add
        private void GenerateAssociationRules(List<List<string>> transactions)
        {
            Dictionary<string, int> itemSupport = new Dictionary<string, int>();
            Dictionary<string, int> pairSupport = new Dictionary<string, int>();

            foreach (var t in transactions)
            {
                foreach (var item in t)
                {
                    if (!itemSupport.ContainsKey(item))
                        itemSupport[item] = 0;

                    itemSupport[item]++;
                }

                if (t.Count >= 2)
                {
                    for (int i = 0; i < t.Count; i++)
                    {
                        for (int j = i + 1; j < t.Count; j++)
                        {
                            string pair = t[i] + " -> " + t[j];

                            if (!pairSupport.ContainsKey(pair))
                                pairSupport[pair] = 0;

                            pairSupport[pair]++;
                        }
                    }
                }
            }

            dgvPatterns.Rows.Clear();

            foreach (var pair in pairSupport)
            {
                string[] parts = pair.Key.Split(" -> ");
                string antecedent = parts[0];

                int support = pair.Value;
                double confidence = (double)support / itemSupport[antecedent] * 100;

                dgvPatterns.Rows.Add(pair.Key, support, confidence.ToString("0.00") + "%");
            }
        }
    }
}