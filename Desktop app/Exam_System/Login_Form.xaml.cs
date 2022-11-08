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

namespace Exam_System
{
    /// <summary>
    /// Interaction logic for Login_Form.xaml
    /// </summary>
    public partial class Login_Form : Window
    {
        public const byte Student_Form = 1;
        public const byte Instructor_Form = 2;

        byte currentType;
        public Login_Form(byte targetType)
        {
            InitializeComponent();
            currentType = targetType;
        }
        private void LoginWindow_Shown(Object sender, EventArgs e)
        {
            switch (currentType)
            {
                case Student_Form:
                    this.Title = "Student Login";
                    formTitle_Label.Content = "Hi Student";
                    break;
                case Instructor_Form:
                    this.Title = "Instructor Login";
                    formTitle_Label.Content = "Hi Instructor";
                    break;
            }
        }
        private void emailTxtBx_Changed(Object sender, TextChangedEventArgs e)
        {
            if (emailTxtBx.Text != "") { emailHint_Label.Visibility = Visibility.Hidden; }
            else { emailHint_Label.Visibility = Visibility.Visible; }
        }
        private void passTxtBx_Changed(object sender, RoutedEventArgs e)
        {

            //When The text is empty show the hint otherwise hide it
            if ((String)passTxtBx.Password != "")
            { passHint_Label.Visibility = Visibility.Hidden; }
            else
            { passHint_Label.Visibility = Visibility.Visible; }
        }
        private void cancelBtn_MouseDown(object sender, RoutedEventArgs e)
		{
            //Close the Form
            this.Close();
        }
        private void loginBtn_MouseDown(object sender, RoutedEventArgs e)
        {
            bool Result = Check_Login(emailTxtBx.Text,passTxtBx.Password);
            if (Result)
            {
                if (currentType == Login_Form.Student_Form)
                {
                    var temp_form = new Welcome_Form(Welcome_Form.Student_Form, emailTxtBx.Text);
                    temp_form.Show();
                }
                else if(currentType == Login_Form.Instructor_Form)
                {
                    var temp_form = new Welcome_Form(Welcome_Form.Instructor_Form, emailTxtBx.Text);
                    temp_form.Show();
                }
                this.Close();
            }
            else
                MessageBox.Show("Login Data Incorrect");
        }
        bool Check_Login(String User, String Password)
        {
            string connection_String;
            SqlConnection cnn;
            SqlCommand Command = new SqlCommand();

            connection_String = @"Server=" + MainWindow.sqlServer_Name + ";Database=" + MainWindow.sqlDatabase_Name + 
                ";Trusted_Connection=True;";
            cnn = new SqlConnection(connection_String);

            Command.Connection = cnn;
            if(currentType == Student_Form)
                Command.CommandText = "STUDENT_Login '" + User + "'," + Password;
            else if(currentType == Instructor_Form)
                Command.CommandText = "Instructor_Login '" + User + "'," + Password;
            
            cnn.Open();
            Int32 result  = (Int32) Command.ExecuteScalar();
            cnn.Close();

            if (result == 0)
                return false;
            else
                return true;
        }
    }   
}
