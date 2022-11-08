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

using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace Exam_System
{
    /// <summary>
    /// Interaction logic for Exam_Form.xaml
    /// </summary>
    public partial class Exam_Form : Window
    {
        private const int questionID_ExamTableColumnIndex = 0;
        private const int examID_ExamTableColumnIndex = 1;
        private const int questionBody_ExamTableColumnIndex = 2;
        private const int questionType_ExamTableColumnIndex = 3;
        private const int questionGrade_ExamTableColumnIndex = 4;
        private const int questionChoice_ExamTableColumnIndex = 6;
        
        private struct Question
        {
            public string Body;
            public int ID;
            public int Exam_ID;
            public string Type;
            public string Choice_1;
            public string Choice_2;
            public string Choice_3;
            public string Choice_4;
            

            //For MCQ Questions
            public Question (int id,int exam_ID,string body)
            {                
                Body = body;
                ID = id;
                Exam_ID = exam_ID;
                Type = "TF";
                Choice_1 = "TF";
                Choice_2 = "TF";
                Choice_3 = "TF";
                Choice_4 = "TF";
            }
            public Question (int id,int exam_ID,string body,string choice_1,string choice_2,string choice_3,string choice_4)
            {
                Body = body;
                ID = id;
                Exam_ID = exam_ID;
                Type = "MCQ";
                Choice_1 = choice_1;
                Choice_2 = choice_2;
                Choice_3 = choice_3;
                Choice_4 = choice_4;
            }
        }

        private List<Question> All_Questions = new List<Question>();
        private int currentQuestionIndex = 0;
        private string[] StudentAnswers;
        private string courseName;
        Welcome_Form.User targetStudent;

        public Exam_Form(string CourseName,Welcome_Form.User targetUser)
        {
            InitializeComponent();
            targetStudent = targetUser;
            courseName = CourseName;

            SqlConnection cnn;
            SqlCommand Command = new SqlCommand();
            DataTable dataTable = new DataTable();

            cnn = new SqlConnection(@"Server=" + MainWindow.sqlServer_Name + ";Database=" + MainWindow.sqlDatabase_Name +
                ";Trusted_Connection=True;");

            Command.Connection = cnn;

            Command.CommandText = "Exam_for_Course '" + CourseName + "';";
        
            cnn.Open();
            SqlDataAdapter tempData = new SqlDataAdapter(Command);
            tempData.Fill(dataTable);
            cnn.Close();
            tempData.Dispose();

            for (int Counter = 0; Counter < dataTable.Rows.Count;Counter++ )
            {
                if(dataTable.Rows[Counter].ItemArray[questionType_ExamTableColumnIndex].ToString() == "TF")
                {
                    All_Questions.Add(new Question(
                            (int)dataTable.Rows[Counter].ItemArray[questionID_ExamTableColumnIndex],            //Question ID
                            (int)dataTable.Rows[Counter].ItemArray[examID_ExamTableColumnIndex],                //Exam ID
                            (string)dataTable.Rows[Counter].ItemArray[questionBody_ExamTableColumnIndex]));     //Question Body
                    
                    Counter++;//The False Choice.
                }
                else if (dataTable.Rows[Counter].ItemArray[questionType_ExamTableColumnIndex].ToString() == "MCQ")
                {
                    All_Questions.Add(new Question(
                            (int)dataTable.Rows[Counter].ItemArray[questionID_ExamTableColumnIndex],            //Question ID
                            (int)dataTable.Rows[Counter].ItemArray[examID_ExamTableColumnIndex],                //Exam ID
                            (string)dataTable.Rows[Counter].ItemArray[questionBody_ExamTableColumnIndex],       //Question Body
                            (string)dataTable.Rows[Counter++].ItemArray[questionChoice_ExamTableColumnIndex],   //Choice #1
                            (string)dataTable.Rows[Counter++].ItemArray[questionChoice_ExamTableColumnIndex],   //Choice #2
                            (string)dataTable.Rows[Counter++].ItemArray[questionChoice_ExamTableColumnIndex],   //Choice #3
                            (string)dataTable.Rows[Counter].ItemArray[questionChoice_ExamTableColumnIndex]));   //Choice #4
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            StudentAnswers = new string[All_Questions.Count];
            //Reset All The Answers...
            for (int Counter = 0; Counter < All_Questions.Count;Counter++)
            { StudentAnswers[Counter] = ""; }
            Show_Question(0);
            exam_ProgressBar.Maximum = All_Questions.Count;
        }
        void Show_Question(int QuestionIndex)
        {
            //Set The Titles:
            questionCount_Label.Content = "Question " + (QuestionIndex + 1).ToString() + " of " + (All_Questions.Count).ToString();
            if(QuestionIndex == 0)
            {
                //Means The first Question then we disable the back button
                back_Btn.IsEnabled = false;
            }
            else if(QuestionIndex == All_Questions.Count - 1)
            {
                //Means the Last Question so we disable the next Button
                next_Btn.IsEnabled = false;
            }
            else
            {
                back_Btn.IsEnabled = true;
                next_Btn.IsEnabled = true;
            }

            //Set the Progress Bar...
            exam_ProgressBar.Value = QuestionIndex + 1;
            
            //Check the Type:
            if(All_Questions[QuestionIndex].Type == "MCQ")
            {
                //Show The MCQ Label and Hide the other...
                MCQ_Panel.Visibility = Visibility.Visible;
                TF_Panel.Visibility = Visibility.Collapsed;

                choice1_rBtn.Content = All_Questions[QuestionIndex].Choice_1;
                choice2_rBtn.Content = All_Questions[QuestionIndex].Choice_2;
                choice3_rBtn.Content = All_Questions[QuestionIndex].Choice_3;
                choice4_rBtn.Content = All_Questions[QuestionIndex].Choice_4;

                choice1_rBtn.IsChecked = false;
                choice2_rBtn.IsChecked = false;
                choice3_rBtn.IsChecked = false;
                choice4_rBtn.IsChecked = false;
            }
            else
            {
                //Show The MCQ Label and Hide the other...
                MCQ_Panel.Visibility = Visibility.Collapsed;
                TF_Panel.Visibility = Visibility.Visible;

                true_rBtn.IsChecked = false;
                false_rBtn.IsChecked = false;
            }

            //Finally Set the Question Body:
            questionBody_Label.Content = All_Questions[QuestionIndex].Body;
        }
        void Store_QuestionAnswer(int QuestionIndex)
        {
            if(All_Questions[QuestionIndex].Type == "MCQ")
            {
                if (choice1_rBtn.IsChecked == true)
                { StudentAnswers[QuestionIndex] = choice1_rBtn.Content.ToString(); }
                else if (choice2_rBtn.IsChecked == true)
                { StudentAnswers[QuestionIndex] = choice2_rBtn.Content.ToString(); }
                else if (choice3_rBtn.IsChecked == true)
                { StudentAnswers[QuestionIndex] = choice3_rBtn.Content.ToString(); }
                else if (choice4_rBtn.IsChecked == true)
                { StudentAnswers[QuestionIndex] = choice4_rBtn.Content.ToString(); }
            }
            else
            {
                if (true_rBtn.IsChecked == true)
                { StudentAnswers[QuestionIndex] = "TRUE"; }
                else if (false_rBtn.IsChecked == true)
                { StudentAnswers[QuestionIndex] = "FALSE"; }
            }
        }
        private void finishExam_Btn_Click(object sender, RoutedEventArgs e)
        {
            Store_QuestionAnswer(currentQuestionIndex);

            SqlConnection cnn;
            SqlCommand Command = new SqlCommand();
            DataTable dataTable = new DataTable();

            cnn = new SqlConnection(@"Server=" + MainWindow.sqlServer_Name + ";Database=" + MainWindow.sqlDatabase_Name +
                ";Trusted_Connection=True;");
            Command.Connection = cnn;
            cnn.Open();

            for(int Counter=0;Counter<All_Questions.Count;Counter++)
            {
                Command.CommandText = "Exam_St_answer " + targetStudent.ID + " , " 
                                                        + All_Questions[Counter].Exam_ID + " , " 
                                                        + All_Questions[Counter].ID + " , '" 
                                                        + StudentAnswers[Counter] + "';";
                Command.ExecuteNonQuery();
            }
            
            //Now Correct the Exam via database code:
            Command.CommandText = "Exam_corr " + targetStudent.ID + "," + All_Questions[0].Exam_ID + ";";
            Command.ExecuteNonQuery();
            
            //Finally get the results:
            Command.CommandText = "Grade_by_Stid_Crs " + targetStudent.ID + ",'" + courseName + "';";
            SqlDataAdapter tempData = new SqlDataAdapter(Command);
            tempData.Fill(dataTable);
            cnn.Close();
            tempData.Dispose();
            var temp_form = new Exam_Result((int)dataTable.Rows[0].ItemArray[0],All_Questions.Count);
            temp_form.Show();
            cnn.Close();            
            this.Close();


            /*
            int Total_Grade = 0,Wrong_Answers = 0;
            //Correct The Exam
            for (int Counter = 0; Counter < All_Questions.Count; Counter++)
            { 
                if(StudentAnswers[Counter] == All_Questions[Counter].ModelAnswer)
                {
                    //Means That the Answer is correct...
                    Total_Grade += All_Questions[Counter].Grade;
                }
                else
                { Wrong_Answers++; }
            }*/
        }
        private void cancelExam_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void back_Btn_Click(object sender, RoutedEventArgs e)
        {
            Store_QuestionAnswer(currentQuestionIndex);
            currentQuestionIndex -= 1;
            Show_Question(currentQuestionIndex);
        }
        private void next_Btn_Click(object sender, RoutedEventArgs e)
        {
            Store_QuestionAnswer(currentQuestionIndex);
            currentQuestionIndex += 1;
            Show_Question(currentQuestionIndex);
        }
    }
}
