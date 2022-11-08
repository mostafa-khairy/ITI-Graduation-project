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
using System.Data;
using System.Collections;

namespace Exam_System
{
    /// <summary>
    /// Interaction logic for Welcome_Form.xaml
    /// </summary>
    public partial class Welcome_Form : Window
    {

        public const byte Student_Form = 1;
        public const byte Instructor_Form = 2;
        private byte currentProfile = 0;

        public struct User
        {
            public int ID;
            public string First_Name;
            public string Last_Name;
            public string Email;
            public string Address;
            public int Department_ID;
            public string Gender;
        }

        const int studentID_ColumnIndex = 0;
        const int studentFirstName_ColumnIndex = 1;
        const int studentLastName_ColumnIndex = 2;
        const int studentEmail_ColumnIndex = 3;
        const int studentAddress_ColumnIndex = 4;
        const int studentDepID_ColumnIndex = 7;
        const int studentGender_ColumnIndex = 8;
        const int instructorAddress_ColumnIndex = 5;
        const int instructorDepID_ColumnIndex = 6;

        const int courseName_ColumnIndex = 3;
        const int courseGrade_ColumnIndex = 4;

        private ArrayList Courses = new ArrayList();
        private ArrayList CoursesResults = new ArrayList();

        private User targetUser;
        public Welcome_Form(byte form_Profile,string user_email)
        {
            InitializeComponent();
            targetUser.Email = user_email;
            currentProfile = form_Profile;
        }
      
        private void Window_Activated(object sender, EventArgs e)
        {
            targetUser = getUser_Data(targetUser.Email);
            writeUserData(targetUser);

            if (currentProfile == Student_Form)
            {
                getCourses_Data(targetUser.ID);
                Courses_Grid.Visibility = Visibility.Visible;
                instructorTools_Grid.Visibility = Visibility.Collapsed;
                gender_Label.Content = "Gender:";
                student_Label.Content = "Student ID:";
            }
            else if (currentProfile == Instructor_Form)
            {
                Courses_Grid.Visibility = Visibility.Collapsed;
                instructorTools_Grid.Visibility = Visibility.Visible;
                gender_Label.Content = "Age:";
                student_Label.Content = "Instructor ID:";
            }
        }

        User getUser_Data(string email)
        {
            User result = new User();
            SqlConnection cnn;
            SqlCommand Command = new SqlCommand();
            DataTable dataTable = new DataTable();

            cnn = new SqlConnection(@"Server=" + MainWindow.sqlServer_Name + ";Database=" + MainWindow.sqlDatabase_Name +
                ";Trusted_Connection=True;");

            Command.Connection = cnn;
            if(currentProfile == Student_Form)
                Command.CommandText = "All_St_Data '" + email + "';";
            else
                Command.CommandText = "All_Ins_Data '" + email + "';";

            cnn.Open();
            SqlDataAdapter tempData = new SqlDataAdapter(Command);
            tempData.Fill(dataTable);
            cnn.Close();
            tempData.Dispose();

            result.ID = Convert.ToInt16(dataTable.Rows[0].ItemArray[studentID_ColumnIndex]);
            result.First_Name = dataTable.Rows[0].ItemArray[studentFirstName_ColumnIndex].ToString();
            result.Last_Name = dataTable.Rows[0].ItemArray[studentLastName_ColumnIndex].ToString();
            result.Email = email;
            result.Gender = dataTable.Rows[0].ItemArray[studentGender_ColumnIndex].ToString();
            if (currentProfile == Student_Form)
            {
                result.Address = dataTable.Rows[0].ItemArray[studentAddress_ColumnIndex].ToString();
                result.Department_ID = Convert.ToInt16(dataTable.Rows[0].ItemArray[studentDepID_ColumnIndex]);
            }
            else if(currentProfile == Instructor_Form)
            {
                result.Address = dataTable.Rows[0].ItemArray[instructorAddress_ColumnIndex].ToString();
                result.Department_ID = Convert.ToInt16(dataTable.Rows[0].ItemArray[instructorDepID_ColumnIndex]);
            
            }
            
            return result;
        }
        void writeUserData(User targetUser)
        {
            studentGreeting_Label.Content = "Hi," + targetUser.First_Name;
            studentName_Label.Content = targetUser.First_Name + " " + targetUser.Last_Name;
            studentID_Label.Content = targetUser.ID.ToString();
            studentEmail_Label.Content = targetUser.Email;
            studentGender_Label.Content = targetUser.Gender;
            studentDepID_Label.Content = targetUser.Department_ID;
            studentAddress_Label.Content = targetUser.Address;
        }
        void getCourses_Data(int ID)
        {            
            SqlConnection cnn;
            SqlCommand Command = new SqlCommand();
            DataTable dataTable = new DataTable();

            cnn = new SqlConnection(@"Server=" + MainWindow.sqlServer_Name + ";Database=" + MainWindow.sqlDatabase_Name +
                ";Trusted_Connection=True;");

            Command.Connection = cnn;
            Command.CommandText = "All_St_Grade " + ID.ToString() + ";";

            cnn.Open();
            SqlDataAdapter tempData = new SqlDataAdapter(Command);
            tempData.Fill(dataTable);
            cnn.Close();
            tempData.Dispose();

            //Now write the data:
            coursesCount_Label.Content = dataTable.Rows.Count.ToString();
            
            //First Reset the data:
            Courses.Clear(); CoursesResults.Clear();

            //Get The Courses and the Results
            if (dataTable.Rows[0].ItemArray[0].ToString() == "this Student does not exist")
            { MessageBox.Show("The Student Course Data doesn't exist."); }
            else
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Courses.Add(row.ItemArray[courseName_ColumnIndex]);
                    CoursesResults.Add(row.ItemArray[courseGrade_ColumnIndex]);
                }

                //Now Add the Courses to the comboBox:
                Courses_ComboBx.Items.Clear();
                foreach (string temp in Courses) { Courses_ComboBx.Items.Add(temp); }
            }

        }

        private void Courses_ComboBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Courses_ComboBx.Items.Count == Courses.Count)
            {
                coursesGrade_Label.Content = CoursesResults[Courses_ComboBx.SelectedIndex].ToString();
                if (CoursesResults[Courses_ComboBx.SelectedIndex] != DBNull.Value)
                {
                    startExam_Btn.IsEnabled = false;
                    Int32 Result = Convert.ToInt32(CoursesResults[Courses_ComboBx.SelectedIndex]);
                    if (Result > 50)
                    {
                        coursesResult_Label.Content = "Congrats, you passed this course.";
                        coursesResult_Image.Source = new BitmapImage(new Uri("Graphics/Pass.png", UriKind.Relative));
                    }
                    else
                    {
                        coursesResult_Label.Content = "Sorry, you failed this course.";
                        coursesResult_Image.Source = new BitmapImage(new Uri("Graphics/Fail.png", UriKind.Relative));
                    }
                }
                else
                {
                    startExam_Btn.IsEnabled = true;
                    coursesResult_Label.Content = "You still haven't taken this course yet.";
                    coursesResult_Image.Source = new BitmapImage(new Uri("Graphics/notTaken.png", UriKind.Relative));
                }
            }
        }

        private void startExam_Btn_Click(object sender, RoutedEventArgs e)
        {
            //First Get the Exam ID:
            var temp_form = new Exam_Form(Courses_ComboBx.SelectedItem.ToString(),targetUser);
            temp_form.Show();
        }

        private void addStudent_Btn_Click(object sender, RoutedEventArgs e)
        {
            var temp_form = new addStudent();
            temp_form.Show();
        }
        private void addQuestion_Btn_Click(object sender, RoutedEventArgs e)
        {
            var temp_form = new addQuetion_Form();
            temp_form.Show();
        }

        private void viewReports_Btn_Click(object sender, RoutedEventArgs e)
        {
            var temp_form = new reports_Form();
            temp_form.Show();
        }
    }
}
