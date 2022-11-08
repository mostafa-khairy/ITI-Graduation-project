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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exam_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string sqlServer_Name = "DESKTOP-O4872J7";
        public const string sqlDatabase_Name = "Examination System";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Shown(Object sender, EventArgs e)
        {
        }
        private void StudentGrid_MouseDown(Object sender, MouseButtonEventArgs e)
        {   
            var temp_form = new Login_Form(Login_Form.Student_Form);
            temp_form.Show();
            this.Close();
        }

        private void Instructor_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var temp_form = new Login_Form(Login_Form.Instructor_Form);
            temp_form.Show();
            this.Close();
        }
    }
}
