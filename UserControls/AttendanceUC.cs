using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using PdfColor = QuestPDF.Helpers.Colors;

namespace BAMS.UserControls
{
    public partial class AttendanceUC : UserControl
    {
        private readonly string connectionString =
        "Server=localhost;Database=BAMS_DB;User Id=eijeizs;Password=aspiringrapper3;TrustServerCertificate=True;";

        public AttendanceUC()
        {
            InitializeComponent();

            dgvAttendance.AutoGenerateColumns = true;
            dgvAttendance.Columns.Clear();

            dtFrom.Value = DateTime.Today.AddMonths(-1);
            dtTo.Value = DateTime.Today;

            InitializeEmployeeCombo();
            StyleGrid();
            LoadAttendance();
        }

        private void InitializeEmployeeCombo()
        {
            cmbEmployee.Items.Clear();

            cmbEmployee.Items.AddRange(new object[]
            {
                "All",
                "Admin",
                "Official",
                "Staff"
            });

            cmbEmployee.SelectedIndex = 0;
        }

        private void StyleGrid()
        {
            dgvAttendance.BorderStyle = BorderStyle.None;
            dgvAttendance.EnableHeadersVisualStyles = false;

            dgvAttendance.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.RoyalBlue;
            dgvAttendance.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

            dgvAttendance.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvAttendance.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);

            dgvAttendance.RowTemplate.Height = 35;

            dgvAttendance.AlternatingRowsDefaultCellStyle.BackColor =
            System.Drawing.Color.FromArgb(240, 240, 240);

            dgvAttendance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttendance.MultiSelect = false;

            dgvAttendance.AutoGenerateColumns = true;
        }

        private void txtSearchName_TextChanged(object? sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void dtFrom_ValueChanged(object? sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void dtTo_ValueChanged(object? sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void cmbEmployee_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void LoadAttendance()
        {
            try
            {
                using (var conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    conn.Open();

                    string name = txtSearchName.Text?.Trim() ?? "";
                    string role = cmbEmployee.SelectedItem?.ToString() ?? "All";

                    string query = @"
                    SELECT
                        U.EmployeeID,
                        U.FullName,
                        U.Position,
                        YEAR(A.Day) AS Year,
                        MONTH(A.Day) AS Month,
                        COUNT(DISTINCT CAST(A.Day AS DATE)) AS DaysPresent,
                        MIN(A.Day) AS FirstLog,
                        MAX(A.Day) AS LastLog
                    FROM Attendance A
                    INNER JOIN Users U ON A.EmployeeID = U.EmployeeID
                    WHERE 
                        A.Day BETWEEN @From AND @To
                        AND (@Name='' OR U.FullName LIKE '%' + @Name + '%')
                        AND (@Role='All' OR U.Position=@Role)
                    GROUP BY 
                        U.EmployeeID,
                        U.FullName,
                        U.Position,
                        YEAR(A.Day),
                        MONTH(A.Day)
                    ORDER BY 
                        Year DESC,
                        Month DESC";

                    var cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@From", dtFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@To", dtTo.Value.Date);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Role", role);

                    var adapter = new Microsoft.Data.SqlClient.SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvAttendance.Columns.Clear();
                    dgvAttendance.DataSource = dt;

                    dgvAttendance.AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance: " + ex.Message);
            }
        }

        private void btnExportPdf_Click(object? sender, EventArgs e)
        {
            if (dgvAttendance.Rows.Count == 0)
            {
                MessageBox.Show("No attendance data to export.");
                return;
            }

            using SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "Attendance_Report.pdf"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                string filePath = saveFileDialog.FileName;

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(20);

                        page.Header().Column(column =>
                        {
                            column.Item().Text("Monthly Attendance Report")
                                .FontSize(20)
                                .SemiBold()
                                .AlignCenter();

                            column.Item().Text(
                                $"From: {dtFrom.Value:MM/dd/yyyy}  To: {dtTo.Value:MM/dd/yyyy}")
                                .FontSize(12)
                                .AlignCenter();
                        });

                        page.Content().PaddingVertical(10).Table(table =>
                        {
                            int columnCount = dgvAttendance.Columns.Count;

                            table.ColumnsDefinition(columns =>
                            {
                                for (int i = 0; i < columnCount; i++)
                                    columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                foreach (DataGridViewColumn column in dgvAttendance.Columns)
                                {
                                    header.Cell()
                                        .Border(1)
                                        .Background(PdfColor.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text(column.HeaderText)
                                        .SemiBold();
                                }
                            });

                            foreach (DataGridViewRow row in dgvAttendance.Rows)
                            {
                                if (row.IsNewRow) continue;

                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    table.Cell()
                                        .Border(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text(cell.Value?.ToString() ?? "");
                                }
                            }
                        });
                    });
                })
                .GeneratePdf(filePath);

                MessageBox.Show("PDF exported successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message);
            }
        }

        private void AttendanceUC_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void dgvAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}