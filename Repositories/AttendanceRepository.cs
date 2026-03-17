using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BAMS.Repositories
{
    public class AttendanceRepository
    {
        private string connectionString =
            @"Data Source=localhost;
              Initial Catalog=BAMS_DB;
              Integrated Security=True";

        public DataTable GetAttendanceLogs(DateTime from, DateTime to, string name, string role)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    a.AttendanceId,
                    a.EmployeeID,
                    u.FullName,
                    u.Position,
                    a.Day,
                    a.AM_In,
                    a.AM_Out,
                    a.PM_In,
                    a.PM_Out,
                    a.Undertime,
                    a.Overtime,
                    a.TotalHours
                FROM Attendance a
                INNER JOIN Users u ON a.EmployeeID = u.EmployeeID
                WHERE a.Day BETWEEN @From AND @To
                AND (@Name = '' OR u.FullName LIKE '%' + @Name + '%')
                AND (@Role = 'All' OR u.Position = @Role)
                ORDER BY a.Day DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@From", from.Date);
                cmd.Parameters.AddWithValue("@To", to.Date);
                cmd.Parameters.AddWithValue("@Name", name ?? "");
                cmd.Parameters.AddWithValue("@Role", role ?? "All");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
        }

        public DataTable GetMonthlyAttendance(int month, int year)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT
                    u.FullName,
                    u.Position,
                    COUNT(a.AttendanceId) AS DaysPresent,
                    SUM(a.TotalHours) AS TotalHours,
                    SUM(a.Overtime) AS TotalOvertime,
                    SUM(a.Undertime) AS TotalUndertime
                FROM Attendance a
                INNER JOIN Users u ON a.EmployeeID = u.EmployeeID
                WHERE MONTH(a.Day) = @Month AND YEAR(a.Day) = @Year
                GROUP BY u.FullName, u.Position
                ORDER BY u.FullName";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
        }


        public void UpdateComputedHours(int attendanceId, double undertime, double overtime, double totalHours)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE Attendance
                SET Undertime = @UT,
                    Overtime = @OT,
                    TotalHours = @TH
                WHERE AttendanceId = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@UT", undertime);
                cmd.Parameters.AddWithValue("@OT", overtime);
                cmd.Parameters.AddWithValue("@TH", totalHours);
                cmd.Parameters.AddWithValue("@Id", attendanceId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertAttendance(int employeeId, DateTime day,
            TimeSpan? amIn, TimeSpan? amOut,
            TimeSpan? pmIn, TimeSpan? pmOut)
        {
            string query = @"INSERT INTO Attendance
            (EmployeeID, Day, AM_In, AM_Out, PM_In, PM_Out)
            VALUES
            (@empid,@day,@amin,@amout,@pmin,@pmout)";

            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@empid", employeeId);
            cmd.Parameters.AddWithValue("@day", day);
            cmd.Parameters.AddWithValue("@amin", (object?)amIn ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@amout", (object?)amOut ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pmin", (object?)pmIn ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pmout", (object?)pmOut ?? DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public int GetTotalUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetTotalOfficials()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Position='Official'";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetTotalStaff()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Position='Staff'";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetTodayAttendance()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Attendance WHERE Day = CAST(GETDATE() AS DATE)";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                return (int)cmd.ExecuteScalar();
            }
        }
    }
}