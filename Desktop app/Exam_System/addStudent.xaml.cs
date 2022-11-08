using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Exam_System
{
    /// <summary>
    /// Interaction logic for addStudent.xaml
    /// </summary>
    public partial class addStudent : Window
    {
        public addStudent()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            String First, Last, Email, Pass;
            int ID, depID;
            First = studentFirstName_TxtBx.Text;
            Last = studentLastName_TxtBx.Text;
            Email = studentEmail_TxtBx.Text;
            Pass = studentPass_TxtBx.Text;
            ID = Convert.ToInt16(studentID_TxtBx.Text);
            depID = Convert.ToInt16(departmentID_TxtBx.Text);

            //Finally add to the Database:
            SqlConnection cnn;
            SqlCommand Command = new SqlCommand();

            cnn = new SqlConnection(@"Server=" + MainWindow.sqlServer_Name + ";Database=" + MainWindow.sqlDatabase_Name +
                ";Trusted_Connection=True;");
            Command.Connection = cnn;
            Command.CommandText = "Insert_St " + ID.ToString()
                                                     + ",'" + First
                                                     + "','" + Last
                                                     + "'," + depID.ToString()
                                                     + ",'" + Email
                                                     + "'," + Pass + ";";
            cnn.Open();
            Command.ExecuteNonQuery();
            cnn.Close();
            MessageBox.Show("Student added successfully.");

        }
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void studentID_TxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(studentID_TxtBx.Text != "")
            {
                int n;
                bool temp = int.TryParse(studentID_TxtBx.Text, out n);
                if (!temp)
                {
                    studentID_TxtBx.Text = "";
                    MessageBox.Show("Only Numbers are allowed.");
                }
            }
        }
        private void departmentID_TxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (departmentID_TxtBx.Text != "")
            {
                int n;
                bool temp = int.TryParse(departmentID_TxtBx.Text, out n);
                if (!temp)
                {
                    departmentID_TxtBx.Text = "";
                    MessageBox.Show("Only Numbers are allowed.");
                }
            }
        }
        private void studentPass_TxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (studentPass_TxtBx.Text != "")
            {
                int n;
                bool temp = int.TryParse(studentPass_TxtBx.Text, out n);
                if (!temp)
                {
                    studentPass_TxtBx.Text = "";
                    MessageBox.Show("Only Numbers are allowed.");
                }
            }
        }
    }
}
