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
    /// Interaction logic for addQuetion_Form.xaml
    /// </summary>
    public partial class addQuetion_Form : Window
    {
        public addQuetion_Form()
        {
            InitializeComponent();
            
            MCQ_Grid.Visibility = Visibility.Visible;
            TF_Grid.Visibility = Visibility.Collapsed;
            this.Height = 475;
        }

        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            //First Collect the data:
            int ID, Course;
            string Type, Body, Choice1, Choice2, Choice3, Choice4, ModelAnswer;
            ModelAnswer = "";

            ID = Convert.ToInt16(questionID_TxtBx.Text);
            Course = Convert.ToInt16(courseID_TxtBx.Text);
            Body = questionBody_TxtBx.Text;
            Choice1 = choice1_TxtBx.Text;
            Choice2 = choice2_TxtBx.Text;
            Choice3 = choice3_TxtBx.Text;
            Choice4 = choice4_TxtBx.Text;
            
            if (questionType_CmbBx.SelectedIndex == 0)
                Type = "MCQ";
            else
                Type = "TF";

            if(Type == "MCQ")
            {
                if (choice1_ChkBx.IsChecked == true)
                    ModelAnswer = Choice1;
                else if (choice2_ChkBx.IsChecked == true)
                    ModelAnswer = Choice2;
                else if (choice3_ChkBx.IsChecked == true)
                    ModelAnswer = Choice3;
                else if (choice4_ChkBx.IsChecked == true)
                    ModelAnswer = Choice4;
            }
            else if (Type == "TF")
            {
                Choice1 = "TRUE";
                Choice2 = "FALSE";
                Choice3 = "-";
                Choice4 = "-";

                if (true_ChkBx.IsChecked == true)
                    ModelAnswer = "TURE";
                else if (false_ChkBx.IsChecked == true)
                    ModelAnswer = "FALSE";
            }

            //Finally add to the Database:
            SqlConnection cnn;
            SqlCommand Command = new SqlCommand();
            
            cnn = new SqlConnection(@"Server=" + MainWindow.sqlServer_Name + ";Database=" + MainWindow.sqlDatabase_Name +
                ";Trusted_Connection=True;");
            Command.Connection = cnn;
            Command.CommandText = "insert_Question " + ID.ToString() 
                                                     + ",'" + Type 
                                                     + "','" + Body 
                                                     + "','" + Course 
                                                     + "','" + ModelAnswer 
                                                     + "','" + Choice1 
                                                     + "','" + Choice2 
                                                     + "','" + Choice3 
                                                     + "','" + Choice4 + "';";
            cnn.Open();
            Command.ExecuteScalar();
            cnn.Close();
            MessageBox.Show("Question added successfully.");
        }
        private void cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void choice1TxtBx_Changed(Object sender, TextChangedEventArgs e)
        {
            if (choice1_TxtBx.Text != "") { choice1Hint_Label.Visibility = Visibility.Hidden; }
            else { choice1Hint_Label.Visibility = Visibility.Visible; }
        }
        private void choice2TxtBx_Changed(Object sender, TextChangedEventArgs e)
        {
            if (choice2_TxtBx.Text != "") { choice2Hint_Label.Visibility = Visibility.Hidden; }
            else { choice2Hint_Label.Visibility = Visibility.Visible; }
        }
        private void choice3TxtBx_Changed(Object sender, TextChangedEventArgs e)
        {
            if (choice3_TxtBx.Text != "") { choice3Hint_Label.Visibility = Visibility.Hidden; }
            else { choice3Hint_Label.Visibility = Visibility.Visible; }
        }
        private void choice4TxtBx_Changed(Object sender, TextChangedEventArgs e)
        {
            if (choice4_TxtBx.Text != "") { choice4Hint_Label.Visibility = Visibility.Hidden; }
            else { choice4Hint_Label.Visibility = Visibility.Visible; }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MCQ_Grid != null)
            {
                if (questionType_CmbBx.SelectedIndex == 0)
                {
                    MCQ_Grid.Visibility = Visibility.Visible;
                    TF_Grid.Visibility = Visibility.Collapsed;
                    this.Height = 475;
                }
                else
                {
                    MCQ_Grid.Visibility = Visibility.Collapsed;
                    TF_Grid.Visibility = Visibility.Visible;
                    this.Height = 390;
                }
            }
        }
    }
}
