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

namespace Exam_System
{
    /// <summary>
    /// Interaction logic for Exam_Result.xaml
    /// </summary>
    public partial class Exam_Result : Window
    {

        public Exam_Result(int Grade,int Total)
        {
            InitializeComponent();
            grade_Label.Content = Grade.ToString();
            total_Label.Content = Total.ToString();
            if(Grade > 50)
            {
                result_Image.Source = new BitmapImage(new Uri("Graphics/Pass.png", UriKind.Relative));
                result_Label.Content = "Congratulations, You passed this exam.";
            }
            else
            {
                result_Image.Source = new BitmapImage(new Uri("Graphics/Fail.png", UriKind.Relative));
                result_Label.Content = "Sorry, You faileded this exam.";
            }
        }
    }
}
